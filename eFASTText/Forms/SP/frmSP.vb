Public Class frmSP

    Private Query As New CPerformQuery

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        Dim EncryptedPassword As CPasswordEncrypt
        Dim EncryptedPasswordStr As String


        If cmdFind.Text = "&Find" Then

            Dim xs As Control, AsteriskChecker As Boolean = False, Str As String
            'AsteriskChecker = False

            Str = "SELECT RTRIM(LTRIM(cClientID))[Client ID],RTRIM(LTRIM(cFirstName))[First Name],RTRIM(LTRIM(cFamilyName))[Family Name],RTRIM(LTRIM(cMobile))[Mobile],RTRIM(LTRIM(Salesperson))[Salesperson],(CASE WHEN a.cActive=1 THEN 'True' ELSE 'False' END)[Active],cApprover[Approver] FROM tblClients a WHERE"

            For Each xs In Me.Controls
                If TypeOf xs Is TextBox Then
                    If InStr(1, xs.Text, "*") > 0 Then
                        If xs.Name = txtClientID.Name Then
                            Str = Str & " a.cClientID Like '" & Replace(txtClientID.Text, "*", "%") & "' AND "
                        ElseIf xs.Name = txtFName.Name Then
                            Str = Str & " a.cFirstName Like '" & Replace(txtFName.Text, "*", "%") & "' AND "
                        ElseIf xs.Name = txtFamName.Name Then
                            Str = Str & " a.cFamilyName Like '" & Replace(txtFamName.Text, "*", "%") & "' AND "
                        ElseIf xs.Name = txtMobile.Name Then
                            Str = Str & " a.cMobile Like '" & Replace(txtMobile.Text, "*", "%") & "' AND "
                        ElseIf xs.Name = txtSM.Name Then
                            Str = Str & " a.Salesperson Like '" & Replace(txtSM.Text, "*", "%") & "' AND "
                        ElseIf xs.Name = txtApprover.Name Then
                            Str = Str & " a.cApprover Like '" & Replace(txtApprover.Text, "*", "%") & "' AND "
                        End If
                        AsteriskChecker = True
                    End If
                End If
            Next xs
            If AsteriskChecker = False Then
                Str = Str & " a.cClientID='" & Trim(txtClientID.Text) & "'"
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
                    txtClientID.Text = f.Sel(0).ToString
                    txtFName.Clear()
                    txtFamName.Clear()
                    txtMobile.Clear()
                    txtSM.Clear()
                    txtApprover.Clear()
                    cmdFind_Click(sender, e)
                End If
            ElseIf dt.Rows.Count() = 1 Then
                txtClientID.Text = dt.Rows(0).Item("Client ID")
                txtFName.Text = dt.Rows(0).Item("First Name").ToString
                txtFamName.Text = dt.Rows(0).Item("Family Name").ToString
                txtMobile.Text = dt.Rows(0).Item("Mobile").ToString
                txtSM.Text = dt.Rows(0).Item("Salesperson").ToString
                CheckBox1.Checked = dt.Rows(0).Item("Active")
                txtApprover.Text = dt.Rows(0).Item("Approver").ToString

                txtClientID.ReadOnly = True
                txtClientID.BackColor = Color.LightGray

                cmdFind.Text = "&Ok"
            Else
                MsgBox("No matching record(s) found.", MsgBoxStyle.Exclamation)
                txtClientID.Focus()
            End If

        ElseIf cmdFind.Text = "&Ok" Then
            Me.Close()
        ElseIf cmdFind.Text = "&Update" Then
            If CheckValues() = True Then
                Exit Sub
            End If

            Query = New CPerformQuery
            If Query.PerformUpdateQuery("UPDATE tblClients Set cFirstName='" & txtFName.Text.Replace("'", "''").Trim & "',cFamilyName='" & txtFamName.Text.Replace("'", "''").Trim & "',cMobile='" & txtMobile.Text.Replace("'", "''").Trim & "',Salesperson='" & txtSM.Text.Replace("'", "''").Trim & "',cActive='" & IIf(CheckBox1.Checked = True, 1, 0) & "',UpdateDate='" & Now() & "',UpdatedBy='" & UserName & "',cApprover=" & txtApprover.Text.Trim & " WHERE cClientID='" & txtClientID.Text & "'", oConn2) > 0 Then
                MsgBox("Updating has been successful.", vbInformation)
                cmdFind.Text = "&Ok"
            End If

        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdFindCheck()
        If cmdFind.Text = "&Ok" Then
            txtClientID.ReadOnly = True
            cmdFind.Text = "&Update"
        End If
    End Sub

    Function CheckValues() As Boolean
        On Error GoTo xError
        CheckValues = True

        If txtFName.TextLength < 1 Then
            MsgBox("Please enter first name.", MsgBoxStyle.Critical)
            txtFName.Focus()
            Exit Function
        End If
        If txtFamName.TextLength < 1 Then
            MsgBox("Please enter family name.", MsgBoxStyle.Critical)
            txtFamName.Focus()
            Exit Function
        End If
        If txtMobile.TextLength < 1 Then
            MsgBox("Please enter mobile number.", MsgBoxStyle.Critical)
            txtMobile.Focus()
            Exit Function
        End If
        If Not IsNumeric(txtApprover.Text) = True Then
            MsgBox("Invalid number value.", MsgBoxStyle.Critical)
            txtApprover.Focus()
            Exit Function
        End If
        If Not txtMobile.Text.StartsWith("+639") Then
            MsgBox("Mobile number must start with +639.", MsgBoxStyle.Critical)
            txtMobile.Focus()
            Exit Function
        End If

        CheckValues = False
        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Update" Or cmdFind.Text = "&Add" Or cmdFind.Text = "&Ok" Then
            cmdFind.Text = "&Find"
            Call ClearValues()
        Else
            Me.Close()
        End If
    End Sub

    Private Sub ClearValues()

        txtClientID.Clear()
        txtFamName.Clear()
        txtFName.Clear()
        txtMobile.Clear()
        txtSM.Clear()
        txtApprover.Clear()

        CheckBox1.Checked = True

        txtClientID.ReadOnly = False
        txtClientID.BackColor = Color.White

        txtClientID.Focus()
    End Sub

    Private Sub txtFName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFName.TextChanged
        cmdFindCheck()
    End Sub

    Private Sub txtFamName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamName.TextChanged
        cmdFindCheck()
    End Sub

    Private Sub txtMobile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobile.TextChanged
        cmdFindCheck()
    End Sub

    Private Sub txtSM_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSM.TextChanged
        cmdFindCheck()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        cmdFindCheck()
    End Sub

    Private Sub txtApprover_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtApprover.TextChanged
        cmdFindCheck()
    End Sub
End Class