Public Class frmSMSInbox

    Private Sub frmSMSInbox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmdRefresh_Click(sender, e)
    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        On Error GoTo xError
        cmdRefresh.Enabled = False

        Me.Cursor = Cursors.WaitCursor
        Dim Query As New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT inDate[In Date],inMobile[Mobile],inMsgArrived[Message] FROM tmp_tblMsgArrived ORDER BY inID DESC", oConn2)
        DataGridView1.DataSource = dt
        LblRecordsFound.Text = dt.Rows.Count & " record(s) found."

        Me.Cursor = Cursors.Default
        cmdRefresh.Enabled = True

        Exit Sub
xError:
        Me.Cursor = Cursors.Default

        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmSMSInbox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 851
        Me.Height = 464
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim frm As New frmViewSMSInbox
            frm.LblNumber.Text = DataGridView1.Item(1, e.RowIndex).Value
            frm.LblDateTime.Text = DataGridView1.Item(0, e.RowIndex).Value
            frm.txtInboxMessage.Text = DataGridView1.Item(2, e.RowIndex).Value
            frm.ShowDialog(Me)
        End If
        
    End Sub
End Class