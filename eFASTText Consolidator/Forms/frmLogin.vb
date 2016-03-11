Imports System.Text
Imports System.IO

Public Class frmLogin
    Private SQL As StringBuilder
    Private Query As ClassPerformQuery

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        On Error GoTo xError
        'Me.Cursor = Cursors.WaitCursor

        If txtUserName.Text.Length <= 0 Then
            MsgBox("Invalid username and/or password.", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            UserName = txtUserName.Text
            frmMain.Show()
            Me.Close()
        End If

        Me.Cursor = Cursors.Default

        'Dim EncryptedPassword As New CPasswordEncrypt

        'Dim Query As New ClassPerformQuery

        'If ErrorFound = False Then
        'SQL = New StringBuilder
        'SQL.Append("Select UserName,Psswrd,Active,UserLevel FROM SystemUsers Where UserName='" & Trim$(txtUserName.Text) & "' COLLATE SQL_Latin1_General_CP1_CI_AS")
        'Query = New ClassPerformQuery
        'dt = Query.PerformSelectQuery(SQL.ToString, oConn3)

        'If StrComp(txtPassword.Text, EncryptedPassword.Encrypt(dt.Rows(0).Item("Psswrd")), CompareMethod.Binary) <> 0 Then
        'MsgBox("Invalid Username or password.", MsgBoxStyle.Critical)
        'Exit Sub
        'End If

        'If dt.Rows(0).Item("Active").ToString = False Then

        'MsgBox("Account is inactive. Please contact your system administrator.", MsgBoxStyle.Exclamation)
        'Exit Sub
        'End If

        'UserLevel = dt.Rows(0).Item("UserLevel")    'Admin=1; user=0
        'UserName = dt.Rows(0).Item("UserName")
        'frmMain.Show()
        'Me.Close()
        'ElseIf txtUserName.Text = "Admin" And txtPassword.Text = "fdcletmein" Then

        'UserLevel = 1
        'UserName = txtUserName.Text
        'frmMain.Show()
        'Me.Close()
        'Else
        'MsgBox("Username or password is invalid.", MsgBoxStyle.Critical)
        'End If
        'End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        End
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim WriteLog As New CLogFile

        Call Main()
        If ErrorFound = True Then
            
        Else
            If ErrorFound = True Then
                
            End If
        End If


        Me.Text = "eFastTxt - Consolidator v." & Application.ProductVersion

        Me.Cursor = Cursors.Default
        Exit Sub
xError:
        WriteLog = New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & ": " & Err.Description)
        Me.Cursor = Cursors.Default
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmLogin_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 425
        Me.Height = 228
    End Sub

    Private Sub LinkSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkSettings.LinkClicked
        
    End Sub
End Class
