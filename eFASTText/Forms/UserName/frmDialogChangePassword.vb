Option Explicit On
Public Class frmDialogChangePassword
    Public pswd As String
    Public Success As Boolean = False

    Private Sub frmDialogChangePassword2_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 388
        Me.Height = 290
    End Sub

    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Dim EncryptPasswordText As New CPasswordEncrypt, Str As String
        Str = EncryptPasswordText.Encrypt(Text1.Text)
        
        If txtOldPassword.Visible = False Then  'Own username password being changed
            If StrComp(txtNewPassword.Text, txtConfirm.Text) <> 0 Then
                MsgBox("Confirmation and new password doesn't match.", vbCritical)
            Else
                MsgBox("Password check has been successful.", vbInformation)
                EncryptPasswordText = New CPasswordEncrypt
                pswd = EncryptPasswordText.Encrypt(txtNewPassword.Text)
                Success = True

                Me.Close()
            End If
            Exit Sub
        End If

        If StrComp(Str, txtOldPassword.Text) <> 0 Then
            MsgBox("Old password is invalid.", vbCritical)
        Else
            If StrComp(txtNewPassword.Text, txtConfirm.Text) <> 0 Then
                MsgBox("Confirmation and new password doesn't match.", vbCritical)
            Else
                MsgBox("Password check has been successful.", vbInformation)
                EncryptPasswordText = New CPasswordEncrypt
                pswd = EncryptPasswordText.Encrypt(txtNewPassword.Text)
                Success = True

                Me.Close()
            End If
        End If
    End Sub

End Class