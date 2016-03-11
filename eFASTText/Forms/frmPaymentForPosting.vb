Imports System.Text
Public Class frmPaymentForPosting
    Dim Query As CPerformQuery
    Dim Onloading As Boolean = True
    Private SQL As New StringBuilder

    Private Sub frmPaymentForPosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRefresh.Enabled = False

        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        ListView1.Items.Clear()
        dt.Rows.Clear()

        txtGrandTotal.Text = "0.00"

        Dim ListItemVar As ListViewItem
        Query = New CPerformQuery

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        Dim c As Int16 = 0

        Do While c < dt.Rows.Count

            ListItemVar = ListView1.Items.Add(c + 1)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("Narration"))
            ListItemVar.SubItems.Add(dt.Rows(c).Item("ORNo").ToString)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("oRegName").ToString)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("Bank").ToString)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("DueDate").ToString)
            ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("Payment").ToString, "Standard"))
            ListItemVar.SubItems.Add(dt.Rows(c).Item("DateRemitted"))
            ListItemVar.SubItems.Add(IIf(IsDBNull(dt.Rows(c).Item("OnlinePymnt")), "False", "True"))

            txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(c).Item("Payment"))

            c += 1
        Loop

        txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")

        If dt.Rows.Count <= 0 Then
            MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
        Else
            ToolStripMenuItem5.Enabled = True
        End If

        cmdRefresh.Enabled = True
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        cmdRefresh.Enabled = True
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub


    Private Function GetSQL() As String
        On Error GoTo xError
        SQL = New StringBuilder

        'SQL.Append("Declare @DateForCashFrom as DateTime,@DateForCashTo as DateTime,@DateForCheckFrom as DateTime,@DateForCheckTo as DateTime,@DateWeekName as nvarchar(15), @Salesperson as nvarchar(20) ")
        SQL.Append("SELECT TableA.*,c.Narration FROM (")
        SQL.Append("SELECT CommandUsed,oRegName,''[Bank],''[DueDate],dbo.fn_GetCashPayment(CommandUsed)[Payment],DateRemitted,OnlinePymnt,dbo.fn_GetOR(CommandUsed)[ORNo] FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCH %') AND Remitted=1 AND CommandUsed Not Like '%AS%' ")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        SQL.Append("AND oRepliedMSG Like '%successful%' ")
        SQL.Append("AND dbo.fn_GetCashPayment(CommandUsed)>0 ")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT CommandUsed,oRegName,dbo.fn_GetBankName(CommandUsed)[Bank],convert(nvarchar(30),dbo.fn_GetCheckDueDate(CommandUsed),101)[DueDate],dbo.fn_GetCheckPayment(CommandUsed)[Payment],DateRemitted,OnlinePymnt,dbo.fn_GetOR(CommandUsed) FROM tblsmsOUT b INNER JOIN tblClients a ON a.cClientID=b.oClientID ")
        SQL.Append("INNER JOIN [" & oConn.Database & "].dbo.ArPostDatedCh c ON dbo.fn_GetOR(CommandUsed)=substring(c.Cheque,CHARINDEX('-',c.Cheque,0)+1,len(c.Cheque)) ")
        SQL.Append("WHERE a.cApprover=7 AND b.oVoid=0 AND (b.CommandUsed Like 'PAYCK %') AND Remitted=1  AND CommandUsed Not Like '%AS%'")
        SQL.Append("AND DateRemitted IS NOT NULL ")
        SQL.Append(") as TableA INNER JOIN [" & oConn.Database & "].dbo.ArPostDatedCh c ON dbo.fn_GetOR(CommandUsed)=substring(c.Cheque,CHARINDEX('-',c.Cheque,0)+1,len(c.Cheque)) ")
        SQL.Append("ORDER BY ORNo ")

        Return SQL.ToString

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crPaymentForPosting

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        With Report
            .SetDataSource(dt)

            Report.SetParameterValue(0, UserName)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "Payment for Posting"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    
    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        frmPaymentForPosting_Load(sender, e)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        On Error GoTo xError

        SaveFileDialog1.Filter = "Text File(*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName <> "" Then
            Dim ExportToText As New CExportToText

            Dim c = 0, RowText As String = ""
            r = 0
            Dim LVColumnCnt As Int16 = ListView1.Columns.Count, LVRCnt As Integer = ListView1.Items.Count

            Do While c < LVColumnCnt
                RowText &= ListView1.Columns(c).Text & IIf(c = ListView1.Columns.Count - 1, "", vbTab)
                c += 1
            Loop

            ExportToText.CreateFile(SaveFileDialog1.FileName)
            ExportToText.WriteText(RowText)

            c = 0
            RowText = ""

            Do While r < LVRCnt
                Do While c < LVColumnCnt
                    RowText &= ListView1.Items(r).SubItems(c).Text & IIf(c = LVColumnCnt - 1, "", vbTab)
                    c += 1
                Loop

                ExportToText.WriteText(RowText)

                c = 0
                r += 1
                RowText = ""
            Loop

            ExportToText.CloseFile()
            SaveFileDialog1.FileName = ""

            MessageBox.Show("Export has been successful!", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

End Class