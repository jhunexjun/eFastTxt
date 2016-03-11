
Public Class frmAuthentication
    Private Query As ClassPerformQuery
    Friend CorrectPassword As Boolean = False

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Query = New ClassPerformQuery
        Dim EncryptedPW As New CPasswordEncrypt

        'dt = Query.PerformSelectQuery("SELECT UserLevel,Active FROM SystemUsers WHERE UserName='" & txtUserName.Text & "' AND Psswrd='" & EncryptedPW.Encrypt(txtPassword.Text) & "' COLLATE SQL_Latin1_General_CP1_CS_AS AND UserLevel=1", oConn3)
        'If dt.Rows.Count > 0 Then
        'CorrectPassword = True
        'Me.Close()
        'Else
        'MsgBox("Authentication is not valid or is not a Superuser.", MsgBoxStyle.Critical)
        'txtUserName.Focus()
        'CorrectPassword = False
        'End If

    End Sub

    Private Sub frmAuthentication_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmAuthentication_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 336
        Me.Height = 196
    End Sub
End Class