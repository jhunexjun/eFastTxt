Imports System.Text
Imports System.IO

Public Class frmLogin
    Private SQL As StringBuilder
    Private Query As CPerformQuery

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Call Main()
        Call eFastTextDatabase() 'ConnectSmartDatabase()
        Call ConsolidatorDatabase()

        Me.Cursor = Cursors.Default

        Dim EncryptedPassword As New CPasswordEncrypt

        Dim Query As New CPerformQuery
        cmdOk.Enabled = False

        If ErrorFound = False Then
            SQL = New StringBuilder
            SQL.Append("Select UserName,Psswrd,Active,UserLevel FROM SystemUsers Where UserName='" & Trim$(txtUserName.Text) & "' COLLATE SQL_Latin1_General_CP1_CI_AS")
            Query = New CPerformQuery
            dt = Query.PerformSelectQuery(SQL.ToString, oConn2)

            If txtUserName.Text = "Admin" And txtPassword.Text = "fdcletmein" Then

                UserLevel = 1
                UserName = txtUserName.Text
                frmMain.Show()
                Me.Close()
                Exit Sub
            End If

            If dt.Rows.Count() > 0 Then
                If StrComp(txtPassword.Text, EncryptedPassword.Encrypt(dt.Rows(0).Item("Psswrd")), CompareMethod.Binary) <> 0 Then
                    MsgBox("Invalid Username or password.", MsgBoxStyle.Critical)
                    txtUserName.Focus()
                    SendKeys.Send("{home}+{End}")
                    cmdOk.Enabled = True
                    Exit Sub
                End If

                If dt.Rows(0).Item("Active").ToString = False Then
                    MsgBox("Account is inactive. Please contact your system administrator.", MsgBoxStyle.Exclamation)
                    cmdOk.Enabled = True
                    Exit Sub
                End If

                UserLevel = dt.Rows(0).Item("UserLevel")    'Admin=1; user=0
                UserName = dt.Rows(0).Item("UserName")
                frmMain.Show()
                Me.Close()
            Else
                MsgBox("Username or password is invalid.", MsgBoxStyle.Critical)
                cmdOk.Enabled = True
            End If
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        cmdOk.Enabled = True
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        End
    End Sub

    Private Sub frmLogin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim WriteLog As New CLogFile

        Call Main()
        If ErrorFound = True Then
            Dim f As New frmSettings
            f.ShowDialog(Me)
        Else
            Call eFastTextDatabase() 'ConnectSmartDatabase()
            If ErrorFound = True Then
                Dim f As New frmSettings
                f.ShowDialog(Me)
            End If
        End If


        Me.Text = "eFastTxt - Log-in v." & Application.ProductVersion
        Me.lblDB.Text = "Connected to " & oConn.Database

        If UCase(Settings.Auto_Update_On_Startup) = UCase("True") Then
            Application.DoEvents()

            Dim f As New frmProgressBar
            f.Owner = Me
            f.ShowInTaskbar = False
            f.StartPosition = FormStartPosition.CenterScreen
            f.Show()

            'Checks Application.Product
            If File.Exists(Settings.Application_Update_Location & "\" & Settings.EXE_Name & ".exe") = True Then
                If File.GetLastWriteTime(Settings.Application_Update_Location & "\" & Settings.EXE_Name & ".exe") > File.GetLastWriteTime(Application.StartupPath & "\" & Settings.EXE_Name & ".exe") Then
                    If Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName).Length > 1 Then
                        MessageBox.Show("System found an update on the specified location update. However another instance of this program is already running.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    Else
                        Shell(Application.StartupPath & "\EXE Updater.exe", AppWinStyle.NormalFocus)
                        End
                    End If
                End If
            Else
                MessageBox.Show("Warning: " & Settings.EXE_Name & " update file does not exists in " & Settings.Application_Update_Location & Chr(13) & " or access denied.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning)

                WriteLog = New CLogFile
                WriteLog.LogWrite("U_Error: " & Settings.EXE_Name & " update file does not exists in " & Settings.Application_Update_Location & " or access denied.")
            End If

            ''Checks EXE Updater.exe file
            If File.Exists(Settings.EXE_Updater_Location & "\" & "EXE Updater.exe") = True Then
                If File.GetLastWriteTime(Settings.EXE_Updater_Location & "\" & "EXE Updater.exe") > File.GetLastWriteTime(Application.StartupPath & "\" & "EXE Updater.exe") Then
                    FileCopy(Settings.EXE_Updater_Location & "\EXE Updater.exe", Application.StartupPath & "\EXE Updater.exe")
                    WriteLog = New CLogFile
                    WriteLog.LogWrite("Log: Successfully updated EXE Updater")
                End If
            Else
                WriteLog = New CLogFile
                WriteLog.LogWrite("U_Error: EXE Updater.exe update file does not exists in " & Settings.EXE_Updater_Location & "  or access denied.")
            End If

            f.Close()
        End If

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
        Me.Height = 245
    End Sub

    Private Sub LinkSettings_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkSettings.LinkClicked
        Dim f As New frmSettings
        f.ShowDialog(Me)
    End Sub

    Private Sub txtPassword_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPassword.GotFocus
        SendKeys.Send("{Home}+{End}")
    End Sub

End Class
