Imports System.Text
Public Class frmDCREntry
    Dim Query As New CPerformQuery, dt As DataTable
    Dim SQL As StringBuilder
    Dim r, c As Int16

    Private Sub frmSalesmanDCREntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        
        dt = Query.PerformSelectQuery("SELECT top 1 Branch,Division FROM OADM", oConn2)
        txtBranch.Text = dt.Rows(0).Item("Branch")
        cboDivision.SelectedItem = dt.Rows(0).Item("Division")

        InitialSettings()
        CreateDatagridColumns(cboPayment.SelectedItem)

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)

    End Sub
    Private Sub InitialSettings()
        'cboDivision.SelectedIndex = 0
        DataGridView1.AllowUserToAddRows = True
        DataGridView1.AllowUserToDeleteRows = True
        cmdAdd.Enabled = False
        'cmdPrint.Enabled = False
        TSPrintPreview.Enabled = False
        cmdFind.Text = "&Add"

        cboPayment.Items.Add("CASH")
        cboPayment.Items.Add("CHECK")
        cboPayment.SelectedIndex = 0
        DTFrom.Enabled = False
        DTTo.Enabled = False
    End Sub

    Private Sub CreateDatagridColumns(ByVal Item As String)
        With DataGridView1
            With .Columns
                .Clear()
                .Add("colPaymentType", "Payment Type")          '0  'to be removed
                .Add("colOR", "O.R. No.")                       '1
                .Add("colSINo", "S.I. No.")                     '2
                .Add("colPaymentAmt", "Payment Amount")         '3 !
                '.Item(1).Width = 210

                If Item.ToUpper = "CASH" Then
                    .Remove("colPaymentType")
                ElseIf Item.ToUpper = "CHECK" Then
                    .Remove("colPaymentType")
                    .Add("colBank", "Bank")                     '4
                    .Add("colCheckDate", "Check Date")          '5
                ElseIf Item.ToUpper = "ALL" Then
                    .RemoveAt(3)
                    .Add("colPaymentAmt", "Cash Amount")        '3 !
                    .Add("colBank", "Bank")                     '4
                    .Add("colCheckDate", "Check Date")          '5
                    .Add("colCheckpayment", "Check Amount")
                End If

                .Add("colCNCM", "CN/CM No.")                    '4/6
                .Add("colCNCMAmt", "CN/CM Amount")

                .Item("colPaymentAmt").DefaultCellStyle.NullValue = "0.00"
                .Item("colPaymentAmt").DefaultCellStyle.Format = "#,##0.00"
                .Item("colPaymentAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Item("colCNCM").DefaultCellStyle.NullValue = "-"
                .Item("colCNCMAmt").DefaultCellStyle.NullValue = "0.00"
                .Item("colCNCMAmt").DefaultCellStyle.Format = "#,##0.00"
                .Item("colCNCMAmt").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                .Add("oID", "Record #")
            End With
            .Columns("oID").Visible = False

        End With

    End Sub

    Private Sub cboPayment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPayment.SelectedIndexChanged
        CreateDatagridColumns(cboPayment.SelectedItem)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Add" Then
            cboDivision.Focus()
            txtSalesman.Clear()
            'txtBranch.Clear()
            cboPayment.Items.Clear()
            cboPayment.Items.Add("ALL")
            cboPayment.SelectedIndex = 0

            DTFrom.Value = Today
            DTTo.Value = Today
            DataGridView1.Rows.Clear()
            cmdFind.Text = "&Find"
            cmdAdd.Enabled = True
            DTFrom.Enabled = True
            DTTo.Enabled = True
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.ReadOnly = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        cboPayment.Items.Clear()

        InitialSettings()
        DataGridView1.AllowUserToAddRows = True
        DataGridView1.ReadOnly = False
        DataGridView1.DataSource = Nothing
        txtSalesman.Clear()
    End Sub

    Private Sub cmdFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        If cmdFind.Text = "&Find" Then
            TSPrintPreview.Enabled = False
            DataGridView1.Rows.Clear()

            SQL = New StringBuilder()

            SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime ")
            SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
            SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
            SQL.Append("SET @DateFrom=CONVERT(nvarchar,@DateFrom,101) ")
            SQL.Append("SET @DateTo=CONVERT(nvarchar,@DateTo,101) ")
            SQL.Append("SELECT dbo.fn_GetOR(CommandUsed)[ORNumber],")
            SQL.Append("dbo.fn_GetPaymentType(CommandUsed)[PaymentType],")
            SQL.Append("b.oRegName[oRegName],")
            SQL.Append("dbo.fn_GetSINumber(CommandUsed)[SINumber],oSIAmount,dbo.fn_GetCashPayment(CommandUsed)[CashPayment],dbo.fn_GetCNCMAmount(CommandUsed)[CNDNAmount],dbo.fn_GetCNCMNumber(b.CommandUsed)[CN/CM No.],dbo.fn_GetCheckDueDate(CommandUsed)[DueDate],dbo.fn_GetBankName(CommandUsed)[BankName],dbo.fn_GetCheckPayment(CommandUsed)[CheckPayment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID WHERE a.cApprover=7 AND a.Salesperson='" & txtSalesman.Text & "' AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK%' or b.CommandUsed Like 'PAYCH%') AND CONVERT(nvarchar(30),oDateTimeIn,101)>=@DateFrom AND CONVERT(nvarchar(30),oDateTimeIn,101)<=@DateTo AND oRepliedMSG Like '%successful%' ")
            SQL.Append("ORDER BY dbo.fn_GetOR(CommandUsed)")

            Query = New CPerformQuery
            dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
            r = 0 : c = 0

            Do While r < dt.Rows.Count
                With DataGridView1
                    .Rows.Add()
                    .Item(0, r).Value = dt.Rows(r).Item("PaymentType")
                    .Item(1, r).Value = dt.Rows(r).Item("ORNumber")
                    .Item(2, r).Value = dt.Rows(r).Item("SINumber")
                    .Item("colPaymentAmt", r).Value = Format(dt.Rows(r).Item("CashPayment"), "Standard")
                    .Item("colBank", r).Value = dt.Rows(r).Item("BankName")
                    .Item("colCheckDate", r).Value = dt.Rows(r).Item("DueDate")
                    .Item("colCheckpayment", r).Value = Format(dt.Rows(r).Item("CheckPayment"), "Standard")
                    .Item("colCNCM", r).Value = dt.Rows(r).Item("CN/CM No.")
                    .Item("colCNCMAmt", r).Value = Format(dt.Rows(r).Item("CNDNAmount"), "Standard")
                End With
                c = 0
                r += 1
            Loop

            If DataGridView1.RowCount > 0 Then
                TSPrintPreview.Enabled = True
            Else
                TSPrintPreview.Enabled = False
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            End If

        ElseIf cmdFind.Text = "&Add" Then
            If CheckValues() = False Then
                If MsgBox("Are you sure you want to add this document?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If

                Dim RowCountTotal As Int16 = DataGridView1.RowCount - 1, RowCounter As Int16 = 0

                r = 0 : c = 0

                Dim vOR, vCustomer, vSI, vPaymentAmt As String
                Dim vBank, vCheckDate As String
                Dim vCNCMNo, vCNCMAmount As String
                vBank = "" : vCheckDate = "" : vCNCMNo = "" : vCNCMAmount = ""

                SQL = New StringBuilder
                SQL.Append("Declare @intErrorCode int,@reply as nvarchar(480) ")
                SQL.Append("Declare @Num as char(20) ")
                SQL.Append("BEGIN TRAN ")
                SQL.Append("SET @Num=(SELECT top 1 cMobile FROM tblClients WHERE Salesperson='" & txtSalesman.Text & "' AND cApprover=7) ")
                Do While r < DataGridView1.Rows.Count - 1
                    With DataGridView1
                        vOR = .Item(0, r).Value
                        vCustomer = "x"
                        vSI = .Item(1, r).Value
                        vPaymentAmt = .Item(2, r).Value
                        If cboPayment.SelectedItem = "CASH" Then
                            vCNCMNo = .Item(3, r).Value
                            vCNCMAmount = .Item(4, r).Value
                        ElseIf cboPayment.SelectedItem = "CHECK" Then
                            vBank = .Item(3, r).Value
                            vCheckDate = .Item(4, r).Value
                            vCNCMNo = .Item(5, r).Value
                            vCNCMAmount = .Item(6, r).Value
                        End If
                    End With

                    Dim cmdSB As New StringBuilder()
                    If cboPayment.SelectedItem = "CASH" Then
                        cmdSB.Append("PAYCH ")
                    Else
                        cmdSB.Append("PAYCK ")
                    End If

                    cmdSB.Append(vCustomer)
                    cmdSB.Append(" ")
                    cmdSB.Append(vOR)
                    cmdSB.Append(" ")
                    cmdSB.Append(vSI)
                    cmdSB.Append(" ")
                    cmdSB.Append(vPaymentAmt)
                    cmdSB.Append(" ")
                    If cboPayment.SelectedItem = "CASH" Then
                        cmdSB.Append(vCNCMNo)
                        cmdSB.Append(" ")
                        cmdSB.Append(vCNCMAmount)
                    ElseIf cboPayment.SelectedItem = "CHECK" Then
                        cmdSB.Append(vBank)
                        cmdSB.Append(" ")
                        cmdSB.Append(vCheckDate)
                        cmdSB.Append(" ")
                        cmdSB.Append(vCNCMNo)
                        cmdSB.Append(" ")
                        cmdSB.Append(vCNCMAmount)
                    End If

                    SQL.Append("EXEC sp_ValidateSMSIn1 @Num,'" & cmdSB.ToString & "' ")
                    SQL.Append(" SET @intErrorCode=@@ERROR ")
                    SQL.Append(" IF (@intErrorCode<>0) GOTO PROBLEM ")
                    r += 1
                Loop

                SQL.Append("COMMIT TRAN ")
                SQL.Append("PROBLEM: ")
                SQL.Append("IF (@intErrorCode<>0) ROLLBACK TRAN ")

                If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                    MsgBox("Adding has been successful.", MsgBoxStyle.Information)
                    txtBranch.Clear()
                    txtSalesman.Clear()
                    DataGridView1.Rows.Clear()
                    'Else
                    'MsgBox("Adding was not successful. Please try again.", MsgBoxStyle.Critical)
                End If
            End If
        End If

        
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''Upgraded
        'Do While r < DataGridView1.Rows.Count - 1
        'With DataGridView1
        'vOR = .Item(0, r).Value
        'vCustomer = "x"
        'vSI = .Item(1, r).Value
        'vPaymentAmt = .Item(2, r).Value
        'If cboPayment.SelectedItem = "CASH" Then
        'vCNCMNo = .Item(3, r).Value
        'vCNCMAmount = .Item(4, r).Value
        'ElseIf cboPayment.SelectedItem = "CHECK" Then
        'vBank = .Item(3, r).Value
        'vCheckDate = .Item(4, r).Value
        'vCNCMNo = .Item(5, r).Value
        'vCNCMAmount = .Item(6, r).Value
        'End If
        'End With

        'Dim cmdSB As New StringBuilder()
        'If cboPayment.SelectedItem = "CASH" Then
        'cmdSB.Append("PAYCH ")
        'Else
        'cmdSB.Append("PAYCK ")
        'End If

        'cmdSB.Append(vCustomer)
        'cmdSB.Append(" ")
        'cmdSB.Append(vOR)
        'cmdSB.Append(" ")
        'cmdSB.Append(vSI)
        'cmdSB.Append(" ")
        'cmdSB.Append(vPaymentAmt)
        'cmdSB.Append(" ")
        'If cboPayment.SelectedItem = "CASH" Then
        'cmdSB.Append(vCNCMNo)
        'cmdSB.Append(" ")
        'cmdSB.Append(vCNCMAmount)
        'ElseIf cboPayment.SelectedItem = "CHECK" Then
        'cmdSB.Append(vBank)
        'cmdSB.Append(" ")
        'cmdSB.Append(vCheckDate)
        'cmdSB.Append(" ")
        'cmdSB.Append(vCNCMNo)
        'cmdSB.Append(" ")
        'cmdSB.Append(vCNCMAmount)
        'End If

        'Query = New CPerformQuery
        'SQL = New StringBuilder()
        'SQL.Append("Declare @Num as char(20) ")
        'SQL.Append("SET @Num=(SELECT top 1 cMobile FROM tblClients WHERE Salesperson='" & txtSalesman.Text & "' AND cApprover=7) ")
        'SQL.Append("EXEC sp_ValidateSMSIn1 @Num,'" & cmdSB.ToString & "'")

        'If Query.PerformUpdateQuery(SQL.ToString, oConn2) <= 0 Then
        'MsgBox("Adding was unsuccessful. Please try again.", MsgBoxStyle.Critical)
        'GoTo CountAdded
        'Else
        'RowCounter += 1
        'DataGridView1.Rows.RemoveAt(r)
        'End If

        'r += 1
        'Loop
        'CountAdded:
        'MsgBox(RowCounter & " out of " & RowCountTotal & " has been added successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        'If RowCounter = RowCountTotal Then
        'txtBranch.Clear()
        'txtSalesman.Clear()
        'End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True
        If cboDivision.SelectedIndex = -1 Then
            MessageBox.Show("Please select Division Type.", "DCR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If
        If txtSalesman.Text = "" Then
            MessageBox.Show("Please enter salesman code.", "DCR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If
        If cboPayment.SelectedIndex = -1 Then
            MessageBox.Show("Please select Payment Type.", "DCR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If
        If txtBranch.Text = "" Then
            MessageBox.Show("Please enter Branch code.", "DCR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End If
        If DataGridView1.RowCount - 1 <= 0 Then
            MsgBox("No data to save.", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        r = 0 : c = 0
        Dim v As String

        Do While r < DataGridView1.Rows.Count - 1
            Do While c < DataGridView1.Columns.Count - 1
                v = Trim(DataGridView1.Item(c, r).Value)

                If cboPayment.SelectedItem = "CASH" Then
                    If c = 3 AndAlso v.Length <= 0 Then
                        v = "-"
                        DataGridView1.Item(c, r).Value = v
                    ElseIf (c = 2 Or c = 4) AndAlso v.Length <= 0 Then
                        v = "0.00"
                        DataGridView1.Item(c, r).Value = v
                    End If
                End If

                If cboPayment.SelectedItem = "CHECK" Then
                    If c = 5 AndAlso v.Length <= 0 Then
                        v = "-"
                        DataGridView1.Item(c, r).Value = v
                    ElseIf (c = 2 Or c = 6) AndAlso v.Length <= 0 Then
                        v = "0.00"
                        DataGridView1.Item(c, r).Value = v
                    End If
                End If

                If v = "" Then
                    MsgBox("Invalid data in item(Row=" & r + 1 & ",Column=" & c + 1 & ")", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    DataGridView1.CurrentCell.Selected = False
                    DataGridView1.Item(c, r).Selected = True
                    Exit Function
                End If

                If c = 2 AndAlso Not IsNumeric(v) Then
                    MsgBox("Please enter numeric values only.", MsgBoxStyle.Exclamation)
                    DataGridView1.CurrentCell.Selected = False
                    DataGridView1.Item(c, r).Selected = True
                    Exit Function
                End If

                If c = 2 AndAlso CDbl(v) < CDbl(0) Then
                    MsgBox("Please enter payment amount not less than zero.", MsgBoxStyle.Exclamation)
                    DataGridView1.CurrentCell.Selected = False
                    DataGridView1.Item(c, r).Selected = True
                    Exit Function
                End If

                If cboPayment.SelectedItem = "CASH" Then
                    If c = 4 AndAlso Not IsNumeric(v) = True Then
                        MsgBox("Please enter numeric values only.", MsgBoxStyle.Exclamation)
                        DataGridView1.CurrentCell.Selected = False
                        DataGridView1.Item(c, r).Selected = True
                        Exit Function
                    End If
                ElseIf cboPayment.SelectedItem = "CHECK" Then
                    If c = 6 AndAlso Not IsNumeric(v) = True Then
                        MsgBox("Please enter numeric values only.", MsgBoxStyle.Exclamation)
                        DataGridView1.CurrentCell.Selected = False
                        DataGridView1.Item(c, r).Selected = True
                        Exit Function
                    End If
                End If

                c += 1
            Loop
            c = 0
            r += 1
        Loop

        CheckValues = False
        Return CheckValues
    End Function

    Private Sub frmSalesmanDCREntry_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Width <= 546 Then
            Me.Width = 546
        End If
        If Me.Height <= 426 Then
            Me.Height = 426
        End If
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub DataGridView1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        With DataGridView1
            If cboPayment.SelectedValue = "CASH" Then
                .Item(2, e.RowIndex).Value = "0.00"
                .Item(3, e.RowIndex).Value = "-"
                .Item(4, e.RowIndex).Value = "0.00"
            ElseIf cboDivision.SelectedValue = "CHECK" Then
                .Item(2, e.RowIndex).Value = "0.00"
                .Item(5, e.RowIndex).Value = "-"
                .Item(6, e.RowIndex).Value = "0.00"
            End If
        End With
    End Sub

    Private Sub TSPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSPrintPreview.Click
        On Error GoTo xError

        Me.Cursor = Cursors.WaitCursor
        Dim Report As New crDCR_All

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, cboDivision.SelectedItem)
            .SetParameterValue(1, txtBranch.Text)
            .SetParameterValue(3, txtSalesman.Text)
            .SetParameterValue(2, cboPayment.SelectedItem)
            .SetParameterValue(4, DTFrom.Value)
            .SetParameterValue(5, DTTo.Value)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With


        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .MdiParent = frmMain
            .Show()
            .Text = "Daily Collection Report (DCR)"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub
End Class