Imports System.Text
Imports System.Data.SqlClient

Public Class frmTaggingOfRemittances
    Private Query As CPerformQuery
    Dim SQL As StringBuilder, c ', r As Int16, 
    Dim rCount As Int16

    Dim colRemittedValue As Boolean = False
    Private SQLcmd As SqlCommand
    Private HT As Hashtable

    Private Sub frmforDeposit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HT = New Hashtable

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT top 1 Branch,Division FROM OADM", oConn2)
        txtBranch.Text = dt.Rows(0).Item("Branch")
        cboDivision.SelectedItem = dt.Rows(0).Item("Division")

        DateTimePicker1.Value = Now().Date
        DateTimePicker2.Value = Now().Date
        DateTimePicker1.MaxDate = Now().Date
        DateTimePicker2.MaxDate = Now().Date

        txtCNCM.Text = "0.00"
        txtCash.Text = "0.00"

        Query = New CPerformQuery
        If Query.PerformSelectQuery("SELECT top 1 WithOnlnePy FROM OADM", oConn2).Rows(0).Item(0).ToString = "Y" Then
            colOnLinePymnt.Visible = True
        End If

    End Sub

    Private Function GetSQL() As String
        SQL = New StringBuilder
        SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime ")
        SQL.Append("SET @DateFrom='" & DateTimePicker1.Value & "' ")
        SQL.Append("SET @DateTo='" & DateTimePicker2.Value & "' ")
        SQL.Append("SELECT oID, (CASE WHEN Remitted IS NULL THEN 0 ELSE Remitted END)[Remitted],OnLinePymnt,dbo.fn_GetOR(CommandUsed)[ORNumber] ")
        SQL.Append(",b.oRegName[oRegName] ")
        SQL.Append(",dbo.fn_GetSINumber(CommandUsed)[SINumber] ")
        SQL.Append(",oSIAmount ")
        SQL.Append(",dbo.fn_GetCNCMNumber(b.CommandUsed)[CN/CM No.] ")
        SQL.Append(",dbo.fn_GetCNCMAmount(CommandUsed)[CNDNAmount] ")
        SQL.Append(",dbo.fn_GetCashPayment(CommandUsed)[CashPayment] ")
        SQL.Append(",dbo.fn_GetCheckDueDate(CommandUsed)[DueDate] ")
        SQL.Append(",dbo.fn_GetBankName(CommandUsed)[BankName] ")
        SQL.Append(",dbo.fn_GetCheckPayment(CommandUsed)[CheckPayment] ")
        SQL.Append(",oSIAmount-(dbo.fn_GetCashPayment(CommandUsed)+dbo.fn_GetCheckPayment(CommandUsed)+dbo.fn_GetCNCMAmount(CommandUsed))[Over_Under] ")
        SQL.Append("FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND a.Salesperson='" & txtSalesmanCode.Text & "' AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK%' or b.CommandUsed Like 'PAYCH%') AND CONVERT(nvarchar(30),oDateTimeIn,101)>=CONVERT(nvarchar(30),@DateFrom,101) AND CONVERT(nvarchar(30),oDateTimeIn,101)<=CONVERT(nvarchar(30),@DateTo,101) AND oRepliedMSG Like '%successful%' /*AND oSIAmount>0*/ ORDER BY dbo.fn_GetOR(CommandUsed) ")

        Return SQL.ToString
    End Function

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError
        cmdFind.Enabled = False

        If cmdFind.Text = "&Find" Then
            HT = New Hashtable
            Dim Index1stSpace = 0, Index2ndSpace = 0, Index3rdSpace = 0, Index4thSpace = 0, Index5thSpace = 0, Index6thSpace = 0, Index7thSpace = 0, Index8thSpace = 0 ', str As String

            txtSI.Text = "0.00"
            txtCNCM.Text = "0.00"
            txtCash.Text = "0.00"
            txtCheck.Text = "0.00"
            txtGrandTotal.Text = "0.00"

            Me.Cursor = Cursors.WaitCursor
            DataGridView1.Rows.Clear()

            If CheckValues() = False Then

                Query = New CPerformQuery
                dt = Query.PerformSelectQuery(GetSQL, oConn2)
                If dt.Rows.Count > 0 Then
                    c = 0 : r = 0   'c=column,r=rows

                    With DataGridView1
                        Do While r < dt.Rows.Count
                            .Rows.Add()
                            Do While c < .ColumnCount
                                If c = 2 Then
                                    .Item(c, r).Value = IIf(dt.Rows(r).Item(c).ToString = "Y", "True", "False")
                                Else
                                    .Item(c, r).Value = dt.Rows(r).Item(c)
                                End If

                                If c = 1 AndAlso .Item(1, r).Value = 1 Then
                                    .Item(1, r).ReadOnly = True
                                End If

                                c += 1
                            Loop

                            txtSI.Text = Format(CDbl(txtSI.Text) + IIf(IsDBNull(.Item(6, r).Value) = True, 0, .Item(6, r).Value), "Standard")
                            txtCNCM.Text = Format(CDbl(txtCNCM.Text) + .Item(8, r).Value, "Standard")
                            txtCash.Text = Format(CDbl(txtCash.Text) + .Item(9, r).Value, "Standard")
                            txtCheck.Text = Format(CDbl(txtCheck.Text) + .Item(12, r).Value, "Standard")
                            txtGrandTotal.Text = Format((CDbl(txtCash.Text) + CDbl(txtCheck.Text)), "standard")

                            If .Item(1, r).Value = 1 Then
                                HT.Add(dt.Rows(r).Item(0), "r")
                            End If

                            r += 1
                            c = 0
                        Loop
                    End With

                    Me.Cursor = Cursors.Default
                Else
                    Me.Cursor = Cursors.Default
                    MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
                End If
            End If
        Else
            If MessageBox.Show("Are you sure you want to update the document now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                r = 0 : rCount = DataGridView1.RowCount
                Dim WithDataToSave As Boolean = False
                Do While r < rCount
                    If DataGridView1.Item(1, r).Value = True And HT.ContainsKey(DataGridView1.Item(0, r).Value) = False Then
                        WithDataToSave = True
                        Exit Do
                    End If
                    r += 1
                Loop

                If WithDataToSave = False Then
                    MsgBox("No data to update.", MsgBoxStyle.Critical)
                    cmdFind.Enabled = True
                    cmdFind.Focus()
                    Exit Sub
                End If

                r = 0 : rCount = DataGridView1.RowCount

                SQL = New StringBuilder
                SQL.Append("DECLARE @intErrorCode int ")
                SQL.Append("BEGIN TRAN ")
                Do While r < rCount
                    If DataGridView1.Item(1, r).Value = True And HT.ContainsKey(DataGridView1.Item(0, r).Value) = False Then
                        IIf(IsDBNull(DataGridView1.Item(2, r).Value), True, False)
                        If DataGridView1.Item(2, r).Value = False Then
                            SQL.Append(" Update tblsmsOut SET Remitted=1,DateRemitted='" & Now & "' WHERE oID=" & DataGridView1.Item(0, r).Value)
                        Else
                            SQL.Append(" Update tblsmsOut SET Remitted=1,DateRemitted='" & Now & "',OnLinePymnt='Y' WHERE oID=" & DataGridView1.Item(0, r).Value)
                        End If
                        SQL.Append(" SET @intErrorCode=@@ERROR ")
                        SQL.Append("IF (@intErrorCode<>0) GOTO PROBLEM ")
                    End If

                    r += 1
                Loop
                SQL.Append("COMMIT TRAN ")
                SQL.Append("PROBLEM: ")
                SQL.Append("    IF (@intErrorCode<>0) ROLLBACK TRAN ")

                SQLcmd = New SqlCommand(SQL.ToString, oConn2)
                If SQLcmd.ExecuteNonQuery > 0 Then
                    MessageBox.Show("Updating has been successful.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    cmdFind.Text = "&Find"
                Else
                    MessageBox.Show("Updating was not successful.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End If

        cmdFind.Enabled = True
        cmdFind.Focus()

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        cmdFind.Enabled = True
    End Sub

    Private Function CheckValues() As Boolean
        On Error GoTo xError

        CheckValues = True
        If cboDivision.SelectedIndex = -1 Then
            Me.Cursor = Cursors.Default
            MsgBox("Please choose Nestle division.", MsgBoxStyle.Exclamation)
            cboDivision.Focus()
            Exit Function
        End If

        If txtSalesmanCode.Text = "" Then
            Me.Cursor = Cursors.Default
            MsgBox("Please enter salesman code.", MsgBoxStyle.Exclamation)
            txtSalesmanCode.Focus()
            Exit Function
        End If

        If txtBranch.Text = "" Then
            Me.Cursor = Cursors.Default
            MsgBox("Please enter branch.", MsgBoxStyle.Exclamation)
            txtBranch.Focus()
            Exit Function
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''
        If txtSalesmanCode.Text.IndexOf("*") >= 0 Then
            SQL = New StringBuilder
            SQL.Append("SELECT Salesperson,cClientID[Client ID],LTRIM(RTRIM(cFamilyName))[Family Name],LTRIM(RTRIM(cFirstName))[First Name] FROM tblClients WHERE Salesperson IS NOT NULL OR Salesperson LIKE '" & txtSalesmanCode.Text.Trim.Replace("*", "%") & "'")
            Dim f As New frmDialog
            f.DatabaseConnection = 3
            f.strToPerform = SQL.ToString
            f.ShowDialog()

            If f.Sel(0).ToString.Length <> 0 Then
                txtSalesmanCode.Text = f.Sel(0).ToString
            End If
            Me.Cursor = Cursors.Default
            Exit Function
        End If

        CheckValues = False

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If DataGridView1.RowCount > 0 Then
            DataGridView1.Rows.Clear()
            txtSalesmanCode.Text = ""

            txtSI.Text = "0.00"
            txtCNCM.Text = "0.00"
            txtCash.Text = "0.00"
            txtCheck.Text = "0.00"
            txtGrandTotal.Text = "0.00"

            cboDivision.SelectedItem = "Grocery"
            cmdFind.Text = "&Find"
            txtSalesmanCode.Focus()
            cmdFind.Enabled = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub VoidToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoidToolStripMenuItem.Click
        cmdFind.Text = "&Update"
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 1 AndAlso DataGridView1.Item(1, e.RowIndex).Value = 0 Then
                cmdFind.Text = "&Update"
            End If
        End If
        
    End Sub

    Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
        If e.ColumnIndex = 1 Then
            r = 0
            rCount = DataGridView1.RowCount

            Do While r < rCount
                If DataGridView1.Item(1, r).ReadOnly = False Then
                    DataGridView1.Item(1, r).Value = IIf(colRemittedValue = False, True, False)
                    cmdFind.Text = "&Update"
                End If
                r += 1
            Loop
            colRemittedValue = IIf(colRemittedValue = False, True, False)
        End If

    End Sub

End Class