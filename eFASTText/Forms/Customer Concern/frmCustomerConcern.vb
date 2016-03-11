Option Explicit On
Imports System.Data.SqlClient
Imports System.Text

Public Class frmCustomerConcern
    Private dt As DataTable
    Private Query As CPerformQuery
    Dim ListItemVar As System.Windows.Forms.ListViewItem
    Private SQL As StringBuilder

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Function SQLstring() As String
        SQL = New StringBuilder

        If Checkbox1.Checked = True Then
            Sql.Append("Declare @Date1 as DateTime,@Date2 as DateTime ")
            Sql.Append("SET @Date1='" & DTFrom.Value & "' ")
            Sql.Append("SET @Date2='" & DTTo.Value & "' ")
            SQL.Append("Select cID,cMobile,cName,SUBSTRING(cMessage,5,len(cMessage))[cMessage],cDateTimeIn,cRead,cReply,(CASE WHEN cStatus IS NULL THEN 'O' ELSE cStatus END)[cStatus] FROM tblConcern Where convert(varchar(20),cDateTimeIn,101)>=convert(varchar(20),@Date1,101) AND convert(varchar(20),cDateTimeIn,101)<=convert(varchar(20),@Date2,101) Order by cID Desc ")
        Else
            SQL.Append("Select cID,cMobile,cName,SUBSTRING(cMessage,5,len(cMessage))[cMessage],cDateTimeIn,cRead,cReply,(CASE WHEN cStatus IS NULL THEN 'O' ELSE cStatus END)[cStatus] FROM tblConcern Order by cID Desc")
        End If

        Return Sql.ToString
    End Function

    Private Sub frmCustomerConcern_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Dim str As String
        Call eFastTextDatabase()
        ToolStripMenuItem5.Enabled = False

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQLstring, oConn2)
        If dt.Rows.Count() > 0 Then
            Dim c As Int32
            Do While c < dt.Rows.Count
                ListItemVar = ListView1.Items.Add(c + 1)

                With ListItemVar.SubItems
                    .Add(dt.Rows(c).Item("cID"))
                    .Add(IIf(dt.Rows(c).Item("cStatus") = "O", "Open", "Close"))
                    .Add(dt.Rows(c).Item("cDateTimeIn"))
                    .Add(dt.Rows(c).Item("cMobile"))
                    .Add(dt.Rows(c).Item("cName"))
                    .Add(dt.Rows(c).Item("cMessage"))
                    .Add(IIf(IsDBNull(dt.Rows(c).Item("cReply")) = True, "", dt.Rows(c).Item("cReply")))
                End With

                c += 1
            Loop
            'Lblrecordscount.Text = Format(ListView1.Items.Count, "#,##0") & " record(s) found."
            cmdOpen.Enabled = True
            ToolStripMenuItem5.Enabled = True
        Else
            'Lblrecordscount.Text = "0 record(s) found."
            cmdOpen.Enabled = False
        End If


        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        cmdRefresh.Enabled = False
        'Lblrecordscount.Text = ""
        Application.DoEvents()
        ListView1.Items.Clear()
        Call frmCustomerConcern_Load(sender, e)

        cmdRefresh.Enabled = True
    End Sub

    Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
        On Error GoTo xError
        ListView1.Focus()
        Dim frm As New frmViewMessage
        With frm
            .LblName.Text = ListView1.FocusedItem.SubItems(5).Text
            .LblNumber.Text = ListView1.FocusedItem.SubItems(4).Text
            .LblDateTime.Text = ListView1.FocusedItem.SubItems(3).Text
            .txtInboxMessage.Text = ListView1.FocusedItem.SubItems(6).Text
            .txtMsgIndex.Text = ListView1.FocusedItem.SubItems(1).Text  'ListView1.FocusedItem.Text
            .cmdReply.Focus()
            .ShowDialog()

        End With

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOpen.Click
        Call ListView1_MouseDoubleClick(sender, e)
    End Sub

    Private Sub Checkbox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Checkbox1.CheckedChanged
        If Checkbox1.Checked = True Then
            DTFrom.Visible = True
            DTTo.Visible = True

            DTFrom.Value = Date.Today
            DTTo.Value = Date.Today
        Else
            DTFrom.Visible = False
            DTTo.Visible = False
        End If
        ListView1.Items.Clear()
    End Sub

    Private Sub cmdCancel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub OpenMessageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenMessageToolStripMenuItem.Click
        On Error GoTo xError
        ListView1.Focus()
        Dim frm As New frmViewMessage
        With frm
            .LblName.Text = ListView1.FocusedItem.SubItems(5).Text
            .LblNumber.Text = ListView1.FocusedItem.SubItems(4).Text
            .LblDateTime.Text = ListView1.FocusedItem.SubItems(3).Text
            .txtInboxMessage.Text = ListView1.FocusedItem.SubItems(6).Text
            .txtMsgIndex.Text = ListView1.FocusedItem.SubItems(1).Text  'ListView1.FocusedItem.Text
            .cmdReply.Focus()
            .ShowDialog()

        End With

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ViewReplyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewReplyToolStripMenuItem.Click
        Dim f As New frmViewReply
        f.txtInboxMessage.Text = ListView1.FocusedItem.SubItems(7).Text
        f.ShowDialog(Me)
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Me.Cursor = Cursors.WaitCursor
        Dim Report As New crCustomerConcern

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQLstring, oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, DTFrom.Value)
            .SetParameterValue(1, DTTo.Value)
            .SetParameterValue(2, Checkbox1.CheckState)

            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
        End With

        With frmCrystalViewer
            .CrystalReportViewer1.ReportSource = Report
            .MdiParent = frmMain
            .Show()
            .WindowState = FormWindowState.Normal
            .Text = "Messages"
        End With

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ContextMenuStrip1_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStrip1.Opened
        StatusToolStripMenuItem.Enabled = True
        If ListView1.FocusedItem.SubItems(2).Text.ToString = "Open" Then
            StatusToolStripMenuItem.Text = "Close"
        ElseIf ListView1.FocusedItem.SubItems(2).Text.ToString = "Close" Then
            StatusToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub StatusToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StatusToolStripMenuItem.Click
        If MsgBox("Are you sure you want to close this item?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            SQL = New StringBuilder
            SQL.Append("UPDATE tblConcern SET cStatus='C' WHERE cID=" & ListView1.FocusedItem.SubItems(1).Text)
            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                MsgBox("Closing item has been successful.", MsgBoxStyle.Information)
                ListView1.FocusedItem.SubItems(2).Text = "Close"
            End If
        End If
    End Sub
End Class