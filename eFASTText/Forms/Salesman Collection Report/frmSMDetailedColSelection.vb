Public Class frmSMDetailedColSelection

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChckFilter.CheckedChanged
        If ChckFilter.Checked = True Then
            lblSalesperson.Visible = True
            txtSalesperson.Visible = True
            ChckDue.Visible = True
        Else
            lblSalesperson.Visible = False
            txtSalesperson.Visible = False
            ChckDue.Visible = False
        End If
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click
        Dim frm As New frmSMDetailedColSummary

        frm.owned = True
        frm.vFrom = DTFrom.Value
        frm.vTo = DTTo.Value
        frm.vFilter = ChckFilter.Checked
        frm.vSalesperson = txtSalesperson.Text
        frm.vCheckDueOnly = ChckDue.Checked

        frm.MdiParent = frmMain
        frm.Show()
        frm.cmdFind_Click(sender, e)

    End Sub

    Private Sub frmDetailedSMColSelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.Value = Today
        DTTo.Value = Today
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class