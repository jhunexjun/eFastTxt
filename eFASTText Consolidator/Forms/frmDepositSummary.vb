Imports System.Text
Imports System.Data.SqlClient

Public Class frmDepositSummary
    Dim Query As ClassPerformQuery
    Private SQL As New StringBuilder, ItemCount As Int16
    Dim r As Int16

    Private Sub frmDetailedCollectionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTDate.Value = Today
    End Sub

    Private Sub frmDetailedCollectionSummary_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 824
        Me.Height = 593
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        cmdFind.Enabled = False
        ListView1.Items.Clear()
        txtGrandTotal.Text = "0.00"
        cmdPrint.Enabled = False

        Dim ListItemVar As System.Windows.Forms.ListViewItem

        x = 0 : r = 0

        Do While x < SiteCount

            Query = New ClassPerformQuery
            dt = Query.PerformSelectQuery(GetSQL(x), oConn(x))
            System.Windows.Forms.Application.DoEvents()

            Do While r < dt.Rows.Count
                ListItemVar = ListView1.Items.Add(dt.Rows(r).Item("BranchName").ToString)
                ListItemVar.SubItems.Add(dt.Rows(r).Item("Type").ToString)
                ListItemVar.SubItems.Add(dt.Rows(r).Item("oRegName").ToString)
                ListItemVar.SubItems.Add(dt.Rows(r).Item("Bank").ToString)
                ListItemVar.SubItems.Add(dt.Rows(r).Item("DueDate").ToString)
                ListItemVar.SubItems.Add(Format(dt.Rows(r).Item("Payment").ToString, "Standard"))

                txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(r).Item("Payment"))

                r += 1
            Loop
            r = 0
            x += 1
        Loop

        txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")
        ItemCount = ListView1.Items.Count

        Label2.Text = ItemCount & " record(s) found."
        If ItemCount <= 0 Then
            MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
        Else
            cmdPrint.Enabled = True
        End If

        cmdFind.Enabled = True

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)

    End Sub

    Private Function GetSQL(ByVal x) As String
        On Error GoTo xError

        SQL = New StringBuilder
        SQL.Append("Declare @DateForCash as DateTime,@DateForCheck as DateTime,@DateWeekName as nvarchar(15), @Salesperson as nvarchar(20) ")
        SQL.Append("SET @DateForCash='" & DTDate.Value & "' ")
        SQL.Append("SET @DateForCheck=@DateForCash ")
        SQL.Append("SET @Salesperson='" & txtSalesperson.Text & "' ")
        SQL.Append("SET @DateForCash=CONVERT(nvarchar(30),@DateForCash,101) ")
        SQL.Append("SET @DateForCheck=CONVERT(nvarchar(30),@DateForCheck,101) ")
        SQL.Append("SELECT '" & conn(x).BranchName & "'[BranchName],'Cash'[Type],oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND b.Remitted=1 ")
        If CheckBox2.Checked = True Then
            SQL.Append("AND a.Salesperson=@Salesperson ")
        End If
        SQL.Append("AND dbo.fn_DepSummaryCash(oDateTimeIn)=@DateForCash AND oRepliedMSG Like '%successful%' ")
        SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
        SQL.Append("UNION ")
        SQL.Append("SELECT '" & conn(x).BranchName & "'[BranchName],'Check'[Type],oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND b.Remitted=1 ")
        If CheckBox2.Checked = True Then
            SQL.Append(" AND a.Salesperson=@Salesperson ")
        End If
        SQL.Append("AND oRepliedMSG Like '%successful%' ")

        If CheckBox1.Checked = True Then
            SQL.Append("AND dbo.fn_DepSumCheckDue(oID,@DateForCheck)=@DateForCheck ")
        End If

        Return SQL.ToString

        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crDepositSummary

        Dim csvFile As String = My.Application.Info.DirectoryPath & "\DepSum.csv"
        Dim outFile As IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(csvFile, False)
        outFile.AutoFlush = True

        r = 0
        outFile.WriteLine("Branch,Type,oRegName,Bank,DueDate,Payment")
        Do While r < ItemCount
            With ListView1.Items(r)
                outFile.WriteLine(.Text & "," & .SubItems(1).Text & "," & .SubItems(2).Text & "," & .SubItems(3).Text & "," & .SubItems(4).Text & "," & CDbl(.SubItems(5).Text))
            End With

            r += 1
        Loop
        csvFile = Nothing
        outFile.Close()

        System.Windows.Forms.Application.DoEvents()

        Dim cn As New ADODB.Connection
        Dim rs As ADODB.Recordset

        cn.ConnectionString = "Driver={Microsoft Text Driver (*.txt; *.csv)};DefaultDir=" & Dir
        cn.Open()

        SQL = New StringBuilder
        SQL.Append("Select * FROM DepSum.csv")
        rs = cn.Execute(SQL.ToString)
        'MsgBox(UserName)

        With Report
            .SetDataSource(rs)
            .SetParameterValue(0, CheckBox1.Checked)
            .SetParameterValue(1, DTDate.Value)
            .SetParameterValue(2, UserName)
            .SetParameterValue(3, txtSalesperson.Text)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "Deposit Summary Report"
        End With

        dt = Nothing
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtSalesperson_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalesperson.TextChanged
        ListView1.Items.Clear()
        Label2.Text = "0 record(s) found."
        txtGrandTotal.Text = "0.00"
    End Sub

    Private Sub DTTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DTDate.ValueChanged
        ListView1.Items.Clear()
        Label2.Text = "0 record(s) found."
        txtGrandTotal.Text = "0.00"
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            GroupBox1.Visible = True
        Else
            GroupBox1.Visible = False
        End If
    End Sub
End Class