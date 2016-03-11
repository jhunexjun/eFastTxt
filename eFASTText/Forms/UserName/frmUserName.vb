Public Class frmUserName
    Private Query As New CPerformQuery

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        Dim EncryptedPassword As CPasswordEncrypt
        Dim EncryptedPasswordStr As String


        If cmdFind.Text = "&Find" Then

            Dim xs As Control, AsteriskChecker As Boolean = False, Str As String
            'AsteriskChecker = False

            Str = "SELECT a.UserName[User Name],a.FName[First],a.MidName[Middle],a.LName[Last],(CASE WHEN a.Active=1 THEN 'True' ELSE 'False' END)[Active],(CASE WHEN a.UserLevel=1 THEN 'Administrator' ELSE 'User' END)[User Level] From SystemUsers a Where"

            For Each xs In Me.Controls
                If TypeOf xs Is TextBox Then
                    If InStr(1, xs.Text, "*") > 0 Then
                        If xs.Name = txtUserName.Name Then
                            Str = Str & " a.UserName Like '" & Replace(txtUserName.Text, "*", "%") & "' And "
                        ElseIf xs.Name = txtFName.Name Then
                            Str = Str & " a.FName Like '" & Replace(txtFName.Text, "*", "%") & "' And "
                        ElseIf xs.Name = txtMidName.Name Then
                            Str = Str & " a.MidName Like '" & Replace(txtMidName.Text, "*", "%") & "' And "
                        ElseIf xs.Name = txtLName.Name Then
                            Str = Str & " a.LName Like '" & Replace(txtLName.Text, "*", "%") & "' And "
                        End If
                        AsteriskChecker = True
                    End If
                End If
            Next xs
            If AsteriskChecker = False Then
                Str = Str & " a.UserName='" & Trim(txtUserName.Text) & "'"
            Else
                Str = Mid$(Str, 1, Len(Str) - 5)
            End If

            dt = New DataTable

            dt = Query.PerformSelectQuery(Str, oConn2)
            If dt.Rows.Count() > 1 Then
                Dim f As New frmDialog
                f.strToPerform = Str
                f.DatabaseConnection = 3

                f.ShowDialog()

                If f.Sel(0).ToString = "" Then
                    Exit Sub
                Else
                    txtUserName.Text = f.Sel(0).ToString
                    cmdFind_Click(sender, e)
                End If
            ElseIf dt.Rows.Count() = 1 Then
                txtUserName.Text = dt.Rows(0).Item("User Name")
                CheckBox1.Checked = dt.Rows(0).Item("Active")
                ComboBox1.Text = dt.Rows(0).Item("User Level")
                txtFName.Text = IIf(IsDBNull(dt.Rows(0).Item("First")) = True, "", dt.Rows(0).Item("First"))
                txtLName.Text = IIf(IsDBNull(dt.Rows(0).Item("Last")) = True, "", dt.Rows(0).Item("Last"))
                txtMidName.Text = IIf(IsDBNull(dt.Rows(0).Item("Middle")) = True, "", dt.Rows(0).Item("Middle"))

                txtUserName.ReadOnly = True
                txtUserName.BackColor = Color.LightGray

                cmdPassword.Enabled = True
                Query = New CPerformQuery

                dt = Query.PerformSelectQuery("SELECT a.Psswrd From SystemUsers a Where a.UserName='" & Trim(txtUserName.Text) & "'", oConn2)

                txtPassword.Text = dt.Rows(0).Item("Psswrd")

                If UCase$(UserName) <> UCase$(txtUserName.Text) Then
                    If UserLevel = True Then
                        cmdPassword.Enabled = True
                    Else
                        cmdPassword.Enabled = False
                    End If
                End If

                If UserLevel = True Then
                    ComboBox1.Enabled = True
                Else
                    ComboBox1.Enabled = False
                End If

                cmdFind.Text = "&Ok"
            Else
                MsgBox("No matching record(s) found.", MsgBoxStyle.Exclamation)
                txtUserName.Focus()
            End If

        ElseIf cmdFind.Text = "&Add" Then
            If MsgBox("Are you sure you want to add this user?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Exit Sub
            End If

            If CheckValues() = True Then
                Exit Sub
            End If

            dt = New DataTable
            Query = New CPerformQuery
            dt = Query.PerformSelectQuery("SELECT UserName From SystemUsers Where UserName='" & Trim$(txtUserName.Text) & "'", oConn2)
            If dt.Rows.Count() > 0 Then
                MsgBox("Username already exist.", MsgBoxStyle.Exclamation)
            Else
                EncryptedPassword = New CPasswordEncrypt
                EncryptedPasswordStr = EncryptedPassword.Encrypt(txtPassword.Text)

                Query = New CPerformQuery
                Query.PerformUpdateQuery("Insert Into SystemUsers(UserName,Psswrd,FName,MidName,LName,UserLevel,Active,DateCreated) " & _
                        "Values('" & Trim$(txtUserName.Text) & "','" & Trim(EncryptedPasswordStr) & "','" & Trim(Replace(txtFName.Text, "'", "''")) & "','" & Trim(Replace(txtMidName.Text, "'", "''")) & "','" & Trim(Replace(txtLName.Text, "'", "''")) & "'," & IIf(ComboBox1.SelectedIndex = 0, 1, 0) & "," & IIf(CheckBox1.Checked = True, 1, 0) & ",GetDate())", oConn2)
                MsgBox("Adding has been successful.", vbInformation)
                cmdAdd.Enabled = True
                Call ClearValues()
                cmdFind.Text = "&Find"
            End If

        ElseIf cmdFind.Text = "&Ok" Then
            Me.Close()
        ElseIf cmdFind.Text = "&Update" Then
            If CheckValues() = True Then
                Exit Sub
            End If

            Query = New CPerformQuery
            Query.PerformUpdateQuery("Update SystemUsers Set Psswrd='" & Trim(txtPassword.Text) & "',UpdateDate=GetDate(),FName='" & Trim(Replace(txtFName.Text, "'", "''")) & "',MidName='" & Trim(Replace(txtMidName.Text, "'", "''")) & "',LName='" & Trim(Replace(txtLName.Text, "'", "''")) & "',UserLevel=" & IIf(ComboBox1.SelectedIndex = 0, 1, 0) & ",Active='" & IIf(CheckBox1.Checked = True, 1, 0) & "' Where UserName='" & Trim(txtUserName.Text) & "'", oConn2)

            MsgBox("Updating has been successful.", vbInformation)

            If UCase$(UserName) <> UCase$(txtUserName.Text) Then
                If UserLevel = False Then
                    cmdPassword.Enabled = False
                    cmdAdd.Enabled = False
                Else
                    cmdPassword.Enabled = True
                    cmdAdd.Enabled = True
                End If
            End If

            cmdFind.Text = "&Ok"
            cmdPassword.Enabled = True
            txtUserName.ReadOnly = False
            If UserName.ToUpper = txtUserName.Text.ToUpper Then
                If ComboBox1.SelectedIndex = 1 Then
                    UserLevel = False
                End If
            End If

        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub ClearValues()

        txtUserName.Clear()
        txtPassword.Clear()
        cmdPassword.Enabled = False
        CheckBox1.Checked = 0
        ComboBox1.SelectedIndex = -1
        txtFName.Clear()
        txtMidName.Clear()
        txtLName.Clear()
        txtUserName.ReadOnly = False
        txtPassword.ReadOnly = True

        txtUserName.ReadOnly = False
        txtUserName.BackColor = Color.White

        txtUserName.Focus()

        If UserLevel = False Then
            cmdAdd.Enabled = False
        Else
            cmdAdd.Enabled = True
        End If

    End Sub

    Function CheckValues() As Boolean
        On Error GoTo xError
        Dim Str As String
        CheckValues = False
        Str = "must at least 4 characters."
        If Len(Trim(txtUserName.Text)) < 4 Then
            MsgBox("Username " & Str, vbCritical)
            CheckValues = True
            Exit Function
        End If

        If Len(Trim(txtPassword.Text)) < 4 Then
            MsgBox("Password " & Str, vbCritical)
            CheckValues = True
            Exit Function
        End If

        If ComboBox1.SelectedIndex = -1 Then
            MsgBox("Please choose user level.", vbCritical)
            CheckValues = True
            Exit Function
        End If

        If InStr(1, txtUserName.Text, "'") <> 0 Then
            MsgBox("Invalid username character.", vbCritical)
            CheckValues = True
            Exit Function
        End If

        If InStr(1, txtPassword.Text, "'") <> 0 Then
            MsgBox("Invalid password character.", vbCritical)
            CheckValues = True
            Exit Function
        End If

        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        txtUserName.Clear()
        txtPassword.Clear()
        txtPassword.ReadOnly = False
        cmdPassword.Enabled = False
        CheckBox1.Checked = 1
        ComboBox1.SelectedIndex = -1
        txtFName.Clear()
        txtMidName.Clear()
        txtLName.Clear()
        cmdAdd.Enabled = False
        cmdFind.Text = "&Add"
        txtUserName.ReadOnly = False
        txtUserName.Focus()
        txtUserName.BackColor = Color.White
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Update" Or cmdFind.Text = "&Add" Or cmdFind.Text = "&Ok" Then
            cmdFind.Text = "&Find"
            Call ClearValues()
            If UserLevel = False Then
                cmdAdd.Enabled = False
            End If
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmdPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPassword.Click
        On Error GoTo xError
        Dim f As New frmDialogChangePassword

        dt = Query.PerformSelectQuery("SELECT UserLevel From SystemUsers Where UserName='" & UserName & "'", oConn2)
        If dt.Rows.Count() > 0 Then
            If dt.Rows(0).Item("UserLevel") = 1 Then
            Else
                If UserName <> txtUserName.Text Then
                    f.txtOldPassword.Visible = False
                    f.LblOldPassword.Visible = False
                End If
            End If


            f.Text1.Text = txtPassword.Text
            f.ShowDialog()
            If f.Success = True Then
                txtPassword.Text = f.pswd
                cmdFind.Text = "&Update"
                cmdPassword.Enabled = False
            End If

        Else
            MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdFindCheck()
        If cmdFind.Text = "&Ok" Then
            If cmdPassword.Enabled = False Then

            Else
                txtUserName.ReadOnly = True
                cmdFind.Text = "&Update"
                cmdAdd.Enabled = 0
            End If
        End If
    End Sub

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBox1.Click
        Call cmdFindCheck()
    End Sub

    Private Sub frmUserName_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If UserLevel = True Then   ' True or 1 = Admin else user only
            cmdAdd.Enabled = True
        Else
            cmdAdd.Enabled = False
        End If
    End Sub

    Private Sub txtFName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFName.TextChanged
        Call cmdFindCheck()
    End Sub

    Private Sub txtLName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLName.TextChanged
        Call cmdFindCheck()
    End Sub

    Private Sub txtMidName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMidName.TextChanged
        Call cmdFindCheck()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call cmdFindCheck()
    End Sub

    Private Sub frmUserName_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 432
        Me.Height = 350
    End Sub
End Class