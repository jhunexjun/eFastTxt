Imports System.Text

Public Class frmPDCforPosting
    Dim Query As CPerformQuery
    Private SQL As StringBuilder

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor
        ListView1.Items.Clear()

        Dim ListItemVar As ListViewItem
        Query = New CPerformQuery

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        Dim c As Int16 = 0
        txtGrandTotal.Text = "0.00"

        Do While c < dt.Rows.Count

            ListItemVar = ListView1.Items.Add(c + 1)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("CheckDate"))
            ListItemVar.SubItems.Add(dt.Rows(c).Item("ORNo").ToString)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("CustomerName").ToString)
            ListItemVar.SubItems.Add(Format(dt.Rows(c).Item("CheckAmount"), "standard"))

            txtGrandTotal.Text = CDbl(txtGrandTotal.Text) + CDbl(dt.Rows(c).Item("CheckAmount"))

            c += 1
        Loop

        txtGrandTotal.Text = Format(txtGrandTotal.Text, "Standard")

        'lblRCount.Text = ListView1.Items.Count & " record(s) found."
        If dt.Rows.Count <= 0 Then
            MsgBox("No matching record(s) found. Cheque(s) may have been posted already.", MsgBoxStyle.Critical)
        Else
            ToolStripMenuItem5.Enabled = True
        End If
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.ToString & vbTab & Err.Number & vbTab & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)

    End Sub

    Private Function GetSQL() As String
        On Error GoTo xError

        SQL = New StringBuilder
        SQL.Append("Declare @From as DateTime, @To as DateTime ")
        SQL.Append("SET @From = '" & DTFrom.Value & "' ")
        SQL.Append("SET @To = '" & DTTo.Value & "' ")
        SQL.Append("SET @From = CONVERT(nvarchar,@From,101) ")
        SQL.Append("SET @To = CONVERT(nvarchar,@To,101) ")
        SQL.Append("SELECT /*(CASE WHEN dbo.fn_GetOR(CommandUsed) IS NULL THEN 'Syspro' ELSE 'eFastText' END )[Origin],*/ CONVERT(nvarchar,dbo.fn_GetCheckDueDate(CommandUsed),101)[CheckDate],dbo.fn_GetOR(CommandUsed)[ORNo],oRegName collate Database_default as CustomerName,dbo.fn_GetCheckPayment(CommandUsed)[CheckAmount] ")
        SQL.Append("FROM [" & oConn2.Database & "].[dbo].[tblsmsOut] a INNER JOIN [" & oConn.Database & "].[dbo].[ArPostDatedCh] b ON [" & oConn2.Database & "].[dbo].fn_GetOR(CommandUsed)=substring(b.Cheque,CHARINDEX('-',b.Cheque,0)+1,len(b.Cheque)) ")
        SQL.Append("WHERE CommandUsed Like 'PAYCK %' AND oRepliedMSG Like '%successful%' AND Remitted=1 AND oVoid=0 ")
        SQL.Append("AND CONVERT(nvarchar,dbo.fn_GetCheckDueDate(CommandUsed),101)>=@From AND CONVERT(nvarchar,dbo.fn_GetCheckDueDate(CommandUsed),101)<=@To ")
        SQL.Append("ORDER BY oRegName ")

        Return SQL.ToString

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        cmdFind.Enabled = True
    End Function

    Private Sub frmPDCforPosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.Value = Today
        DTTo.Value = Today
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Dim Report As New crPDCforPosting

        dt = Query.PerformSelectQuery(GetSQL(), oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, DTFrom.Value)
            .SetParameterValue(1, DTTo.Value)
            .SetParameterValue(2, StrConv(UserName, VbStrConv.ProperCase))

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait

        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .Show()
            .MdiParent = frmMain
            .Text = "PDC for Posting"
        End With

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub
End Class