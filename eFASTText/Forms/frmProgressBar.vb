Public Class frmProgressBar

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Value = ProgressBar1.Value + 20
        Application.DoEvents()
        Label2.Text = ProgressBar1.Value
        If ProgressBar1.Value >= 200 Then
            ProgressBar1.Value = 0
            Application.DoEvents()
        End If

    End Sub

    Private Sub frmProgressBar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.DoEvents()
        Label1.Text = "Searching..." & Settings.Application_Update_Location
    End Sub
End Class