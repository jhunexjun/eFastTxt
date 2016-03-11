Imports System.Text
Imports System.Windows.Forms

Imports System.Data.SqlClient
Imports System.Data

Public Class frmSOASummary
    Private Query As CPerformQuery
    Private ListItemVar As ListViewItem
    Friend StrToPerform As String
    Dim c As Int16 = 0, Filter As Int16

    Friend vCustomerCode, vCustomerName As String, vFiltered As Boolean, vFrom As DateTime, vTo As DateTime

    Private Sub cmCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmCancel.Click
        Me.Close()
    End Sub

    Private Sub frmSOASummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        vFrom = Today
        vTo = Today

        Filter = vFiltered

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(StrToPerform, oConn2)

        c = 0
        Do While c < dt.Rows.Count
            ListItemVar = ListView1.Items.Add(c + 1)
            ListItemVar.SubItems.Add(dt.Rows(c).Item("oDateTimeIn"))
            ListItemVar.SubItems.Add(dt.Rows(c).Item("Name"))
            ListItemVar.SubItems.Add(dt.Rows(c).Item("oRepliedMSG").ToString)
            c += 1
        Loop

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim frm As New frmViewSOA
        frm.lblIndex.Text = ListView1.FocusedItem.Text
        frm.LblDateTime.Text = ListView1.FocusedItem.SubItems(1).Text
        frm.lblName.Text = ListView1.FocusedItem.SubItems(2).Text
        frm.txtMessage.Text = ListView1.FocusedItem.SubItems(3).Text
        frm.ShowDialog()
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        OpenToolStripMenuItem_Click(sender, e)
    End Sub

End Class