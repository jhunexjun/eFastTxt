Public Class frmDCR
    Private Query As CPerformQuery
    Dim SQL As String, c ', r As Int16

    Private Sub frmDCR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT top 1 Branch,Division FROM OADM", oConn2)
        txtBranch.Text = dt.Rows(0).Item("Branch")
        cboDivision.SelectedItem = dt.Rows(0).Item("Division")

        DateTimePicker1.Value = Now().Date
        DateTimePicker2.Value = Now().Date
        DateTimePicker1.MaxDate = Now().Date
        DateTimePicker2.MaxDate = Now().Date


        cboType.SelectedItem = "All"
        'cboDivision.SelectedIndex = 0
        Call CreateColumns(cboType.SelectedItem)
    End Sub

    Private Sub CreateColumns(ByVal sType As String)
        txtCNCM.Text = "0.00"
        txtCash.Text = "0.00"
        'cmdPrint.Enabled = False
        TSPrintPreview.Enabled = False
        With DataGridView1
            .Columns.Clear()
            .ReadOnly = True
            With .Columns

                .Add("colORNo", "OR No.")
                .Add("colCustomerName", "Customer Name") '1
                .Add("colSINo", "S.I. No.")              '2
                .Add("colSIAmt", "S.I. Amt")             '3
                .Add("ColCNNo", "CN/CM No.")             '4
                .Add("ColCNAmt", "CN/CM Amt")            '5
                .Add("colPaymentAmt", "Cash Amt")        '6

                .Add("colChkDate", "Check Date")         '7
                .Add("colBank", "Bank")                  '8
                .Add("colCheckPaymentAmt", "Check Amt")  '9

                .Add("colOverUnder", "Over/Under Collection")    '10

                .Item(1).Width = 210
                .Item(3).DefaultCellStyle.Format = "#,##0.00"
                .Item(3).DefaultCellStyle.NullValue = "0.00"
                .Item(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Item(5).DefaultCellStyle.Format = "#,##0.00"
                .Item(5).DefaultCellStyle.NullValue = "0.00"
                .Item(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Item(6).DefaultCellStyle.Format = "#,##0.00"
                .Item(6).DefaultCellStyle.NullValue = "0.00"
                .Item(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Item(9).DefaultCellStyle.Format = "#,##0.00"
                .Item(9).DefaultCellStyle.NullValue = "0.00"
                .Item(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Item(10).DefaultCellStyle.Format = "#,##0.00"
                .Item(10).DefaultCellStyle.NullValue = "0.00"
                .Item(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Item(10).Width = 140
                .Add("oID", "Record #")
            End With
            .Columns("oID").Visible = False
        End With



    End Sub

    Private Function GetSQL(ByRef str As String) As String
        Dim s As String = ""
        If str = "Cash" Then
            s = "Declare @DateFrom as DateTime,@DateTo as DateTime " & _
                "SET @DateFrom='" & DateTimePicker1.Value & "' " & _
                "SET @DateTo='" & DateTimePicker2.Value & "' " & _
                "SELECT dbo.fn_GetOR(CommandUsed)[ORNumber],b.oRegName[oRegName],dbo.fn_GetSINumber(CommandUsed)[SINumber],oSIAmount,dbo.fn_GetCashPayment(CommandUsed)[Payment],dbo.fn_GetCNCMAmount(CommandUsed)[CNDNAmount],CAST(oSIAmount as money)-(dbo.fn_GetCashPayment(CommandUsed)+dbo.fn_GetCNCMAmount(b.CommandUsed))[Variance],dbo.fn_GetCNCMNumber(b.CommandUsed)[CNDNNumber],oID FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID WHERE a.cApprover=7 AND a.Salesperson='" & txtSalesmanCode.Text & "' AND b.oVoid=0 AND b.CommandUsed Like 'PAYCH%' AND CONVERT(nvarchar(30),oDateTimeIn,101)>=CONVERT(nvarchar(30),@DateFrom,101) AND CONVERT(nvarchar(30),oDateTimeIn,101)<=CONVERT(nvarchar(30),@DateTo,101) AND oRepliedMSG Like '%successful%' ORDER BY dbo.fn_GetOR(CommandUsed)"
        ElseIf str = "Check" Then
            s = "Declare @DateFrom as DateTime,@DateTo as DateTime " & _
                "SET @DateFrom='" & DateTimePicker1.Value & "' " & _
                "SET @DateTo='" & DateTimePicker2.Value & "' " & _
                "SELECT dbo.fn_GetOR(CommandUsed)[ORNumber],b.oRegName[oRegName],dbo.fn_GetSINumber(CommandUsed)[SINumber],oSIAmount,dbo.fn_GetCheckPayment(CommandUsed)[Payment],dbo.fn_GetCNCMAmount(CommandUsed)[CNDNAmount],(CAST(oSIAmount as money)-(dbo.fn_GetCheckPayment(CommandUsed)+dbo.fn_GetCNCMAmount(CommandUsed)))[Variance],dbo.fn_GetBankName(b.CommandUsed)[BankName],dbo.fn_GetCheckDueDate(b.CommandUsed)[DueDate],dbo.fn_GetCNCMNumber(b.CommandUsed)[CN/DN No.],oID FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID WHERE a.cApprover=7 AND a.Salesperson='" & txtSalesmanCode.Text & "' AND b.oVoid=0 AND b.CommandUsed Like 'PAYCK%' AND CONVERT(nvarchar(30),oDateTimeIn,101)>=CONVERT(nvarchar(30),@DateFrom,101) AND CONVERT(nvarchar(30),oDateTimeIn,101)<=CONVERT(nvarchar(30),@DateTo,101) AND oRepliedMSG Like '%successful%' ORDER BY dbo.fn_GetOR(CommandUsed)"
        ElseIf str = "All" Then

            s = "Declare @DateFrom as DateTime,@DateTo as DateTime " & _
                "SET @DateFrom='" & DateTimePicker1.Value & "' " & _
                "SET @DateTo='" & DateTimePicker2.Value & "' " & _
                "SELECT dbo.fn_GetOR(CommandUsed)[ORNumber] " & _
                ",b.oRegName[oRegName] " & _
                ",dbo.fn_GetSINumber(CommandUsed)[SINumber] " & _
                ",oSIAmount " & _
                ",dbo.fn_GetCNCMNumber(b.CommandUsed)[CN/CM No.] " & _
                ",dbo.fn_GetCNCMAmount(CommandUsed)[CNDNAmount] " & _
                ",dbo.fn_GetCashPayment(CommandUsed)[CashPayment] " & _
                ",dbo.fn_GetCheckDueDate(CommandUsed)[DueDate] " & _
                ",dbo.fn_GetBankName(CommandUsed)[BankName] " & _
                ",dbo.fn_GetCheckPayment(CommandUsed)[CheckPayment] " & _
                ",(CASE WHEN dbo.fn_GetSINumber(CommandUsed)='X' THEN 0 ELSE oSIAmount-(dbo.fn_GetCashPayment(CommandUsed)+dbo.fn_GetCheckPayment(CommandUsed)+dbo.fn_GetCNCMAmount(CommandUsed)) END )[Over_Under] " & _
                ",oID " & _
                "FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID " & _
                "WHERE a.cApprover=7 AND a.Salesperson='" & txtSalesmanCode.Text & "' AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK%' or b.CommandUsed Like 'PAYCH%') AND CONVERT(nvarchar(30),oDateTimeIn,101)>=CONVERT(nvarchar(30),@DateFrom,101) AND CONVERT(nvarchar(30),oDateTimeIn,101)<=CONVERT(nvarchar(30),@DateTo,101) AND oRepliedMSG Like '%successful%' AND dbo.fn_GetOR(CommandUsed) Not Like 'AS%' ORDER BY dbo.fn_GetOR(CommandUsed) "

            'Update
            '",oSIAmount-(dbo.fn_GetCashPayment(CommandUsed)+dbo.fn_GetCheckPayment(CommandUsed)+dbo.fn_GetCNCMAmount(CommandUsed))[Over_Under] " & _
        End If

        Return s

    End Function

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        Dim Index1stSpace = 0, Index2ndSpace = 0, Index3rdSpace = 0, Index4thSpace = 0, Index5thSpace = 0, Index6thSpace = 0, Index7thSpace = 0, Index8thSpace = 0, str As String

        txtSI.Text = "0.00"
        txtCNCM.Text = "0.00"
        txtCash.Text = "0.00"
        txtCheck.Text = "0.00"
        txtGrandTotal.Text = "0.00"

        Me.Cursor = Cursors.WaitCursor
        DataGridView1.Rows.Clear()

        If CheckValues() = False Then
            SQL = GetSQL(cboType.SelectedItem)


            Query = New CPerformQuery
            dt = Query.PerformSelectQuery(SQL, oConn2)
            If dt.Rows.Count > 0 Then
                c = 0 : r = 0   'c=column,r=rows

                If cboType.SelectedItem = "Cash" Then
                    With DataGridView1
                        Do While r < dt.Rows.Count
                            .Rows.Add()
                            Do While c < .ColumnCount
                                If c = 8 Then
                                    .Item(8, r).Value = dt.Rows(r).Item("oID")
                                Else
                                    .Item(c, r).Value = dt.Rows(r).Item(c)
                                End If

                                c += 1
                            Loop

                            txtCNCM.Text = Format(CDbl(txtCNCM.Text) + .Item(5, r).Value, "Standard")
                            txtCash.Text = Format(CDbl(txtCash.Text) + .Item(4, r).Value, "Standard")

                            r += 1
                            c = 0
                        Loop
                    End With

                ElseIf cboType.SelectedItem = "Check" Then
                    With DataGridView1

                        Do While r < dt.Rows.Count
                            .Rows.Add()
                            Do While c < .ColumnCount
                                If c = 10 Then
                                    .Item(10, r).Value = dt.Rows(r).Item("oID")
                                Else
                                    .Item(c, r).Value = dt.Rows(r).Item(c)
                                End If

                                c += 1
                            Loop
                            txtCNCM.Text = Format(CDbl(txtCNCM.Text) + .Item(5, r).Value, "Standard")
                            txtCash.Text = Format(CDbl(txtCash.Text) + .Item(4, r).Value, "Standard")

                            r += 1
                            c = 0
                        Loop
                    End With
                ElseIf cboType.SelectedItem = "All" Then
                    With DataGridView1
                        Do While r < dt.Rows.Count
                            .Rows.Add()
                            Do While c < .ColumnCount
                                .Item(c, r).Value = dt.Rows(r).Item(c)
                                c += 1
                            Loop

                            txtSI.Text = Format(CDbl(txtSI.Text) + IIf(IsDBNull(.Item(3, r).Value) = True, 0, .Item(3, r).Value), "Standard")
                            txtCNCM.Text = Format(CDbl(txtCNCM.Text) + .Item(5, r).Value, "Standard")
                            txtCash.Text = Format(CDbl(txtCash.Text) + .Item(6, r).Value, "Standard")
                            txtCheck.Text = Format(CDbl(txtCheck.Text) + .Item(9, r).Value, "Standard")
                            txtGrandTotal.Text = Format((CDbl(txtCash.Text) + CDbl(txtCheck.Text)), "standard")

                            r += 1
                            c = 0
                        Loop
                    End With


                End If

                TSPrintPreview.Enabled = True
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            End If
        End If

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True

        If txtSalesmanCode.Text.Trim = "" Then
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

        If cboDivision.SelectedIndex = -1 Then
            Me.Cursor = Cursors.Default
            MsgBox("Please choose Nestle division.", MsgBoxStyle.Exclamation)
            cboDivision.Focus()
            Exit Function
        End If
        If cboType.SelectedIndex = -1 Then
            Me.Cursor = Cursors.Default
            MsgBox("Please choose payment type.", MsgBoxStyle.Exclamation)
            cboType.Focus()
            Exit Function
        End If

        If txtSalesmanCode.Text.IndexOf("*") >= 0 Then
            SQL = "SELECT Salesperson,cClientID[Client ID],LTRIM(RTRIM(cFamilyName))[Family Name],LTRIM(RTRIM(cFirstName))[First Name] FROM tblClients WHERE Salesperson IS NOT NULL OR Salesperson LIKE '" & txtSalesmanCode.Text.Trim.Replace("*", "%") & "'"
            Dim f As New frmDialog
            f.DatabaseConnection = 3
            f.strToPerform = SQL
            f.ShowDialog()

            If f.Sel(0).ToString.Length <> 0 Then
                txtSalesmanCode.Text = f.Sel(0).ToString
            End If
            Me.Cursor = Cursors.Default
            Exit Function
        End If

        CheckValues = False
    End Function

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        CreateColumns(cboType.SelectedItem)
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If DataGridView1.RowCount > 0 Then
            DataGridView1.Rows.Clear()
            txtSalesmanCode.Text = ""

            txtSI.Text = "0.00"
            txtCNCM.Text = "0.00"
            txtCash.Text = "0.00"
            txtCheck.Text = "0.00"
            txtGrandTotal.Text = "0.00"

            cboType.SelectedItem = "All"
            cboDivision.SelectedItem = "Grocery"
            TSPrintPreview.Enabled = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub txtSalesmanCode_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSalesmanCode.Leave
        txtSalesmanCode.Text = txtSalesmanCode.Text.ToUpper()
    End Sub

    Private Sub VoidToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoidToolStripMenuItem.Click
        On Error GoTo xError
        Dim WriteLog As New CLogFile

        If DataGridView1.CurrentRow.ToString = "" Then
            MsgBox("Please choose record to void.", MsgBoxStyle.Exclamation)
        Else
            If MsgBox("Are you sure you want to void '" & DataGridView1.CurrentRow.Cells.Item(0).Value & "'; '" & DataGridView1.CurrentRow.Cells.Item(1).Value & "'; '" & Format(DataGridView1.CurrentRow.Cells.Item(3).Value, "Standard") & "'.", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                Dim f As New frmAuthentication, x As Int64
                If cboType.SelectedItem = "Cash" Then
                    x = DataGridView1.CurrentRow.Cells.Item(8).Value
                ElseIf cboType.SelectedItem = "Check" Then
                    x = DataGridView1.CurrentRow.Cells.Item(10).Value
                ElseIf cboType.SelectedItem = "All" Then
                    x = DataGridView1.CurrentRow.Cells.Item(11).Value
                End If

                If UserLevel = False Then
                    f.Owner = Me
                    f.ShowDialog()
                Else
                    f.CorrectPassword = True
                End If

                If f.CorrectPassword = True Then
                    Query = New CPerformQuery
                    SQL = "UPDATE tblsmsOUT SET oVoid=1 WHERE oID=" & x
                    If Query.PerformUpdateQuery(SQL, oConn2) > 0 Then
                        If Len(DataGridView1.CurrentRow.Cells.Item(8).Value) > 0 Then
                            SQL = "SELECT dbo.fn_GetCustCode(oRegName)[CustCode],dbo.fn_GetOR(CommandUsed)[ORNumber],dbo.fn_GetCheckPayment(CommandUsed)[CheckAmount] FROM tblsmsOUT WHERE oID=" & x
                            Query = New CPerformQuery
                            dt = Query.PerformSelectQuery(SQL, oConn2)

                            SQL = "DELETE ArPostDatedCh WHERE Customer='" & dt.Rows(0).Item("CustCode").ToString & "' AND Cheque LIKE '%" & dt.Rows(0).Item("ORNumber").ToString & "%' AND Amount=" & dt.Rows(0).Item("CheckAmount")
                            Query = New CPerformQuery
                            If Query.PerformUpdateQuery(SQL, oConn) > 0 Then
                                MsgBox("Void transaction and deleting from check maintenance has been successful.", MsgBoxStyle.Information)
                                WriteLog = New CLogFile
                                WriteLog.LogWrite("Log: Void transaction and deleting from check maintenance has been successful. " & sender.ToString)

                                DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                            Else
                                Query = New CPerformQuery
                                If Query.PerformUpdateQuery("UPDATE tblsmsOUT SET oVoid=0 WHERE oID=" & x, oConn2) > 0 Then
                                    MsgBox("Deleting record in ArPostDatedCh was not successful. Void transaction has been reversed. ", MsgBoxStyle.Critical)
                                    WriteLog = New CLogFile
                                    WriteLog.LogWrite("Log: Deleting record in ArPostDatedCh was not successful. Void transaction has been reversed. " & sender.ToString)
                                Else
                                    MsgBox("Deleting record and void transaction reversal was unsuccessful. ", MsgBoxStyle.Critical)
                                    WriteLog = New CLogFile
                                    WriteLog.LogWrite("Log: Deleting record and void transaction reversal was unsuccessful. " & sender.ToString)
                                End If
                            End If
                        Else
                            MsgBox("Void transaction was successful.", MsgBoxStyle.Information)
                            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                        End If
                    Else
                        MsgBox("Void transaction was unsuccessful. Please try again.", MsgBoxStyle.Critical)
                    End If

                End If
            End If
        End If

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub ChagToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChagToolStripMenuItem.Click
        On Error GoTo xError
        Dim WriteLog As New CLogFile

        If MessageBox.Show("Are you sure you want to change the date with S.I. No." & DataGridView1.CurrentRow.Cells(2).Value, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            Dim f As New frmAuthentication, x As Int64
            x = DataGridView1.CurrentRow.Cells.Item(11).Value

            If UserLevel = False Then
                f.Owner = Me
                f.ShowDialog()
            Else
                f.CorrectPassword = True
            End If

            'f.Owner = Me
            'f.ShowDialog()

            If f.CorrectPassword = True Then
                Dim frm As New Dialog1
                frm.x = DataGridView1.CurrentRow.Cells(11).Value
                frm.ShowDialog(Me)

                If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                    SQL = "UPDATE tblsmsOUT SET oDateTimeIn='" & frm.DTTo.Value & "' WHERE oID=" & x
                    If Query.PerformUpdateQuery(SQL, oConn2) > 0 Then
                        MessageBox.Show("Updating has been successful.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        If (frm.ToDate < DateTimePicker1.Value) Or (frm.ToDate > DateTimePicker2.Value) Then
                            DataGridView1.Rows.Remove(DataGridView1.CurrentRow)
                        End If
                    End If
                End If
            Else
            End If
        End If

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub


    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSPrintPreview.Click

        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor
        SQL = GetSQL(cboType.SelectedItem)
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL, oConn2)


        Dim Report As New crDCR_All

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, cboDivision.SelectedItem)
            .SetParameterValue(1, txtBranch.Text.ToUpper)
            .SetParameterValue(3, txtSalesmanCode.Text.ToUpper)
            .SetParameterValue(2, cboType.SelectedItem)
            .SetParameterValue(4, DateTimePicker1.Value)
            .SetParameterValue(5, DateTimePicker2.Value)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
        End With


        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "Daily Collection Report (DCR)"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtBranch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBranch.TextChanged

    End Sub
End Class