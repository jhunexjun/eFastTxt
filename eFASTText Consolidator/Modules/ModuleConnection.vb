Imports System.Data.SqlClient
Imports System.Text

Module ModuleConnection
    Friend Settings As Update_Settings

    Sub Main()
        Dim Cn1 As ADODB.Connection
        Dim Rs1 As ADODB.Recordset
        Dim iSQLStr As String

        Try
            Cn1 = New ADODB.Connection
            Cn1.ConnectionString = "Driver={Microsoft Text Driver (*.txt; *.csv)};" & "DefaultDir=" & Dir
            Cn1.Open()

            System.Windows.Forms.Application.DoEvents()

            iSQLStr = "Select * FROM " & File
            Rs1 = Cn1.Execute(iSQLStr)

            If Rs1.RecordCount > 20 Then
                MsgBox("Consolidator is intended only for 20 sites.", MsgBoxStyle.Exclamation)
            Else
                While Not Rs1.EOF
                    conn(x) = New Connection
                    conn(x).server = Rs1("IPAddress").Value.ToString.Remove(0, 1)
                    conn(x).server = conn(x).server.Remove(Len(conn(x).server) - 1, 1)

                    conn(x).database = Rs1("Database").Value
                    conn(x).username = Rs1("UserID").Value
                    conn(x).password = Rs1("Psswrd").Value
                    conn(x).BranchName = Rs1("BranchName").Value

                    oConn(x) = New SqlConnection
                    oConn(x).ConnectionString = "server=" & conn(x).server & ";database=" & conn(x).database & ";user id=" & conn(x).username & ";password=" & conn(x).password
                    oConn(x).Open()

                    System.Windows.Forms.Application.DoEvents()
                    SiteCount = SiteCount + 1

                    Rs1.MoveNext()
                    x += 1
                End While

            End If

        Catch ex As Exception
            Dim WriteLog As New CLogFile
            WriteLog.LogWrite("Main(): " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Critical)

        End Try

        
        

        





        'ErrorFound = False
        'Catch ex As Exception

        'Dim WriteLog As New CLogFile
        'WriteLog.LogWrite("Error: Main()" & vbTab & Err.Number & Err.Description)

        'ErrorFound = True
        'MsgBox(ex.Message, MsgBoxStyle.Critical)


        'End Try

    End Sub

End Module
