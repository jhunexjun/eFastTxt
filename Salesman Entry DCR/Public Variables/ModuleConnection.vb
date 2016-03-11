Imports System.Data.SqlClient

Module ModuleConnection
    Dim conn As Connection
    'Dim conn2 As Connection
    Dim conn3 As Connection

    Private oRead As System.IO.StreamReader
    Dim s As String
    Dim Path As String = Application.StartupPath & "\Settings.ini"

    Sub Main()

        conn = New Connection
        
        Try
            oRead = System.IO.File.OpenText(Path)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            End
        End Try

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

        End While

        Dim EncryptPassword As New CPasswordEncrypt
        Dim Encrypt As String = EncryptPassword.Encrypt(conn.password)

        Try
            oConn = New SqlConnection
            oConn.ConnectionString = "server=" & conn.server & ";database=" & conn.database & ";user id=" & conn.username & ";password=" & Encrypt  '& conn.password
            oConn.Open()
            oRead.Close()

            ErrorFound = False
        Catch ex As Exception

            Dim WriteLog As New CLogFile
            WriteLog.LogWrite("Error: Main()" & vbTab & Err.Number & Err.Description)

            MsgBox(ex.Message, MsgBoxStyle.Critical)
            ErrorFound = True
        End Try

    End Sub


    Sub ConnectSmartDatabase()
        conn3 = New Connection

        Try
            oRead = System.IO.File.OpenText(Path)
            While oRead.Peek <> -1
                s = Trim$(oRead.ReadLine)

                If InStr(Trim(UCase(s)), Trim(UCase("eFastText_server="))) = 1 Then
                    conn3.server = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("eFastText_database="))) = 1 Then
                    conn3.database = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("eFastText_userid="))) = 1 Then
                    conn3.username = Mid(s, InStr(s, "=") + 1)
                End If
                If InStr(Trim(UCase(s)), Trim(UCase("eFastText_password="))) = 1 Then
                    conn3.password = Mid(s, InStr(s, "=") + 1)
                End If
            End While

            Dim EncryptPassword As New CPasswordEncrypt
            Dim Encrypt As String = EncryptPassword.Encrypt(conn.password)

            oConn3 = New SqlConnection
            oConn3.ConnectionString = "server=" & conn3.server & ";database=" & conn3.database & ";user id=" & conn3.username & ";password=" & Encrypt  '& conn3.password
            oConn3.Open()
            oRead.Close()
            ErrorFound = False
        Catch ex As Exception
            Dim WriteLog As New CLogFile
            WriteLog.LogWrite("Error: ConnectSmartDatabase()" & vbTab & Err.Number & Err.Description)

            ErrorFound = True
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Module
