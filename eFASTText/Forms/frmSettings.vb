Imports System.Data.SqlClient

Public Class frmSettings
    Private Query As New CPerformQuery

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If CheckValues() = True Then
            MsgBox("Fill up the form completely.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Try
            Dim EncryptedPassword As New CPasswordEncrypt
            Dim Encrypt As String = EncryptedPassword.Encrypt(txtPassword.Text)

            Dim oWrite As System.IO.StreamWriter

            oWrite = New System.IO.StreamWriter(Application.StartupPath & "\Settings.ini")
            oWrite.WriteLine("[Databases]")
            oWrite.WriteLine("Main_server=" & Trim(txtServer.Text))
            oWrite.WriteLine("Main_database=" & cboDatabase.Text)
            oWrite.WriteLine("Main_userid=" & Trim(txtUserID.Text))
            oWrite.WriteLine("Main_password=" & Encrypt)
            oWrite.WriteLine("")
            oWrite.WriteLine("eFastText_server=" & Trim(txtServer.Text))
            oWrite.WriteLine("eFastText_database=" & cboeFastText.Text)
            oWrite.WriteLine("eFastText_userid=" & Trim(txtUserID.Text))
            oWrite.WriteLine("eFastText_password=" & Encrypt)
            oWrite.WriteLine("")
            oWrite.WriteLine("Consolidator_server=" & Trim(txtServer.Text))
            oWrite.WriteLine("Consolidator_database=" & cboConsolidator.Text)
            oWrite.WriteLine("Consolidator_userid=" & Trim(txtUserID.Text))
            oWrite.WriteLine("Consolidator_password=" & Encrypt)
            oWrite.WriteLine("")
            oWrite.WriteLine("[Settings]")
            oWrite.WriteLine("Application_Update_Location=" & Trim(txtApplication.Text))
            oWrite.WriteLine("EXE_Updater_Update_Location=" & Trim(txtEXEUpdater.Text))
            oWrite.WriteLine("Auto_Update_On_Startup=" & IIf(CheckBox1.Checked = True, "True", False))
            oWrite.WriteLine("EXE_Name=" & Trim(txtEXEName.Text))

            oWrite.Close()

            MsgBox("Saving settings has been successful.", MsgBoxStyle.Information)
            frmLogin.lblDB.Text = "Connected to " & cboDatabase.Text
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        
    End Sub

    Private Sub cboDatabase_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDatabase.DropDown
        cboDatabase.Items.Clear()

        Me.Cursor = Cursors.WaitCursor

        Try
            oConn = New SqlConnection
            oConn.ConnectionString = "server=" & Trim(txtServer.Text) & ";user id=" & Trim(txtUserID.Text) & ";password=" & Trim(txtPassword.Text) & ";database=Northwind"
            oConn.Open()

            dt = Query.PerformSelectQuery("EXEC sp_databases", oConn)
            If dt.Rows.Count > 0 Then
                Dim c As Int16, db As String
                Do While c < dt.Rows.Count
                    db = UCase(dt.Rows(c).Item(0).ToString)
                    If db = UCase("Northwind") Or db = UCase("SBO-COMMON") Or db = UCase("master") Or db = UCase("model") Or db = UCase("msdb") Or db = UCase("pubs") Or db = UCase("tempdb") Then

                    Else
                        cboDatabase.Items.Add(dt.Rows(c).Item(0))
                    End If
                    c += 1
                Loop
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MsgBox("No databases found in the server.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub frmDatabaseSettings_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmLogin.Show()
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True
        If txtServer.TextLength <= 0 Then
            txtServer.Focus()
            Exit Function
        End If
        If txtUserID.TextLength <= 0 Then
            txtUserID.Focus()
            Exit Function
        End If

        If cboDatabase.SelectedIndex = -1 Then
            cboDatabase.Focus()
            Exit Function
        End If
        If cboeFastText.SelectedIndex = -1 Then
            cboeFastText.Focus()
            Exit Function
        End If
        If cboConsolidator.SelectedIndex = -1 Then
            cboConsolidator.Focus()
            Exit Function
        End If
        If txtApplication.TextLength <= 0 Then
            txtApplication.Focus()
            Exit Function
        End If
        If txtEXEUpdater.TextLength <= 0 Then
            txtEXEUpdater.Focus()
            Exit Function
        End If
        If txtEXEName.TextLength <= 0 Then
            txtEXEName.Focus()
            Exit Function
        End If

        CheckValues = False
        Return CheckValues
    End Function

    Private Sub cboeFastText_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboeFastText.DropDown
        cboeFastText.Items.Clear()

        Me.Cursor = Cursors.WaitCursor

        Try
            oConn = New SqlConnection
            oConn.ConnectionString = "server=" & Trim(txtServer.Text) & ";user id=" & Trim(txtUserID.Text) & ";password=" & Trim(txtPassword.Text) & ";database=Northwind"
            oConn.Open()

            dt = Query.PerformSelectQuery("EXEC sp_databases", oConn)
            If dt.Rows.Count > 0 Then
                Dim c As Int16, db As String
                Do While c < dt.Rows.Count
                    db = UCase(dt.Rows(c).Item(0).ToString)
                    If db = UCase("Northwind") Or db = UCase("SBO-COMMON") Or db = UCase("master") Or db = UCase("model") Or db = UCase("msdb") Or db = UCase("pubs") Or db = UCase("tempdb") Then

                    Else
                        cboeFastText.Items.Add(dt.Rows(c).Item(0))
                    End If
                    c += 1
                Loop
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MsgBox("No databases found in the server.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub cboConsolidator_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboConsolidator.DropDown
        cboConsolidator.Items.Clear()

        Me.Cursor = Cursors.WaitCursor

        Try
            oConn = New SqlConnection
            oConn.ConnectionString = "server=" & Trim(txtServer.Text) & ";user id=" & Trim(txtUserID.Text) & ";password=" & Trim(txtPassword.Text) & ";database=Northwind"
            oConn.Open()

            dt = Query.PerformSelectQuery("EXEC sp_databases", oConn)
            If dt.Rows.Count > 0 Then
                Dim c As Int16, db As String
                Do While c < dt.Rows.Count
                    db = UCase(dt.Rows(c).Item(0).ToString)
                    If db = UCase("Northwind") Or db = UCase("SBO-COMMON") Or db = UCase("master") Or db = UCase("model") Or db = UCase("msdb") Or db = UCase("pubs") Or db = UCase("tempdb") Then

                    Else
                        cboConsolidator.Items.Add(dt.Rows(c).Item(0))
                    End If
                    c += 1
                Loop
                Me.Cursor = Cursors.Default
            Else
                Me.Cursor = Cursors.Default
                MsgBox("No databases found in the server.", MsgBoxStyle.Critical)
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


End Class