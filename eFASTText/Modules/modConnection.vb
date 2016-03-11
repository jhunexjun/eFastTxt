Imports System.Data.SqlClient

Module ModConnection
    Dim conn As Connection
    Dim conn2 As Connection
    Dim conn3 As Connection

    Private oRead As System.IO.StreamReader
    Dim s As String
    Friend Settings As Update_Settings

    Dim Path As String = Application.StartupPath & "\Settings.ini"

    Sub Main()

        conn = New Connection

        Try
            oRead = System.IO.File.OpenText(Path)
            While oRead.Peek <> -1
                s = Trim$(oRead.ReadLine)

                If InStr(Trim(UCase(s)), Trim(UCase("main_server="))) = 1 Then
                    conn.server = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("main_database="))) = 1 Then
                    conn.database = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("main_userid="))) = 1 Then
                    conn.username = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("main_password="))) = 1 Then
                    conn.password = Mid(s, InStr(s, "=") + 1)
                End If

                '''''' [Settings]
                If InStr(Trim(UCase(s)), Trim(UCase("Application_Update_Location="))) = 1 Then
                    Settings.Application_Update_Location = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("EXE_Updater_Update_Location="))) = 1 Then
                    Settings.EXE_Updater_Location = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("Auto_Update_On_Startup="))) = 1 Then
                    Settings.Auto_Update_On_Startup = Mid(s, InStr(s, "=") + 1)
                End If

                If InStr(Trim(UCase(s)), Trim(UCase("EXE_Name="))) = 1 Then
                    Settings.EXE_Name = Mid(s, InStr(s, "=") + 1)
                End If

            End While

            Dim EncryptPassword As New CPasswordEncrypt
            Dim Encrypt As String = EncryptPassword.Encrypt(conn.password)

            oConn = New SqlConnection
            oConn.ConnectionString = "server=" & conn.server & ";database=" & conn.database & ";user id=" & conn.username & ";password=" & Encrypt  'conn.password
            oConn.Open()

            oRead.Close()

            ErrorFound = False
        Catch ex As Exception
            oRead.Close()

            Dim WriteLog As New CLogFile
            WriteLog.LogWrite("Error: Main()" & vbTab & Err.Number & Err.Description)

            ErrorFound = True
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        End Try

    End Sub

    Sub eFastTextDatabase() 'ConnectSmartDatabase()
        conn2 = New Connection

        oRead = System.IO.File.OpenText(Path)
        While oRead.Peek <> -1
            s = Trim$(oRead.ReadLine)

            If InStr(Trim(UCase(s)), Trim(UCase("eFastText_server="))) = 1 Then
                conn2.server = Mid(s, InStr(s, "=") + 1)
            End If
            If InStr(Trim(UCase(s)), Trim(UCase("eFastText_database="))) = 1 Then
                conn2.database = Mid(s, InStr(s, "=") + 1)
            End If
            If InStr(Trim(UCase(s)), Trim(UCase("eFastText_userid="))) = 1 Then
                conn2.username = Mid(s, InStr(s, "=") + 1)
            End If
            If InStr(Trim(UCase(s)), Trim(UCase("eFastText_password="))) = 1 Then
                conn2.password = Mid(s, InStr(s, "=") + 1)
            End If
        End While

        Try
            Dim EncryptPassword As New CPasswordEncrypt
            Dim Encrypt As String = EncryptPassword.Encrypt(conn2.password)

            oConn2 = New SqlConnection
            oConn2.ConnectionString = "server=" & conn2.server & ";database=" & conn2.database & ";user id=" & conn2.username & ";password=" & Encrypt  'conn2.password
            oConn2.Open()

            oRead.Close()
            ErrorFound = False
        Catch ex As Exception
            oRead.Close()

            Dim WriteLog As New CLogFile
            WriteLog.LogWrite("Error: eFastTextDatabase()" & vbTab & Err.Number & Err.Description)

            ErrorFound = True
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Friend Sub ConsolidatorDatabase()
        conn3 = New Connection

        oRead = System.IO.File.OpenText(Path)
        While oRead.Peek <> -1
            s = Trim$(oRead.ReadLine)

            If InStr(Trim(UCase(s)), Trim(UCase("Consolidator_server="))) = 1 Then
                conn3.server = Mid(s, InStr(s, "=") + 1)
            End If
            If InStr(Trim(UCase(s)), Trim(UCase("Consolidator_database="))) = 1 Then
                conn3.database = Mid(s, InStr(s, "=") + 1)
            End If
            If InStr(Trim(UCase(s)), Trim(UCase("Consolidator_userid="))) = 1 Then
                conn3.username = Mid(s, InStr(s, "=") + 1)
            End If
            If InStr(Trim(UCase(s)), Trim(UCase("Consolidator_password="))) = 1 Then
                conn3.password = Mid(s, InStr(s, "=") + 1)
            End If
        End While

        Try
            Dim EncryptPassword As New CPasswordEncrypt
            Dim Encrypt As String = EncryptPassword.Encrypt(conn3.password)

            oConn3 = New SqlConnection
            oConn3.ConnectionString = "server=" & conn3.server & ";database=" & conn3.database & ";user id=" & conn3.username & ";password=" & Encrypt
            oConn3.Open()

            oRead.Close()
            ErrorFound = False
        Catch ex As Exception
            oRead.Close()

            Dim WriteLog As New CLogFile
            WriteLog.LogWrite("Error: ConsolidatorDatabase()" & vbTab & Err.Number & Err.Description)

            ErrorFound = True
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Module
