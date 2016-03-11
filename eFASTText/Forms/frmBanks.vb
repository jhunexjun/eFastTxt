Public Class frmBanks
    Private Query As New CPerformQuery

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        If cmdFind.Text = "&Find" Then

            Dim xs As Control, AsteriskChecker As Boolean = False, Str As String

            Str = "SELECT a.BankCode[Bank Code],a.BankName[Bank Name],a.Address,a.Active From Bank a Where"

            For Each xs In Me.Controls
                If TypeOf xs Is TextBox Then
                    If InStr(1, xs.Text, "*") > 0 Then
                        If xs.Name = txtBankCode.Name Then
                            Str = Str & " a.BankCode Like '" & Replace(txtBankCode.Text, "*", "%") & "' And "
                        ElseIf xs.Name = txtBankName.Name Then
                            Str = Str & " a.BankName Like '" & Replace(txtBankName.Text, "*", "%") & "' And "
                        ElseIf xs.Name = txtAddress.Name Then
                            Str = Str & " a.Address Like '" & Replace(txtAddress.Text, "*", "%") & "' And "
                        End If
                        AsteriskChecker = True
                    End If
                End If
            Next xs
            If AsteriskChecker = False Then
                Str = Str & " a.BankCode='" & Trim(txtBankCode.Text) & "'"
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

                txtBankCode.Text = f.Sel(0).ToString

            ElseIf dt.Rows.Count() = 1 Then
                txtBankCode.Text = dt.Rows(0).Item("Bank Code")
                txtBankName.Text = dt.Rows(0).Item("Bank Name")
                txtAddress.Text = dt.Rows(0).Item("Address")
                CheckActive.Checked = IIf(dt.Rows(0).Item("Active") = "Y", True, False)

                cmdFind.Text = "&Ok"
            Else
                MsgBox("No matching record(s) found.", MsgBoxStyle.Exclamation)
                txtBankCode.Focus()
            End If

        ElseIf cmdFind.Text = "&Add" Then

            If CheckValues() = True Then
                Exit Sub
            End If

            Query = New CPerformQuery
            If Query.PerformUpdateQuery("Insert Into Bank(BankCode,BankName,Address,Active,CreateDate,CreatedBy) " & _
                                        "Values('" & Trim$(txtBankCode.Text.Replace("'", "''")) & "','" & Trim(txtBankName.Text.Replace("'", "''")) & "','" & Trim(txtAddress.Text.Replace("'", "''")) & "','" & IIf(CheckActive.Checked = True, "Y", "N") & "','" & Now & "','" & UserName & "')", oConn2) > 0 Then
                '"Values('" & Trim$(txtBankCode.Text.Replace("'", "''")) & "','" & Trim(txtBankName.Text.Replace("'", "''")) & "','" & Trim(txtAddress.Text.Replace("'", "''")) & "','" & IIf(CheckActive.Checked = True, "Y", "N") & "',GetDate(),'" & UserName & "')", oConn2) > 0 Then
                MsgBox("Adding has been successful.", vbInformation)
                cmdAdd.Enabled = True
                Call ClearValues()
                cmdFind.Text = "&Find"
            Else
                MsgBox("Adding was unsuccessful!", MsgBoxStyle.Critical)
            End If

        ElseIf cmdFind.Text = "&Ok" Then
            Me.Close()
        ElseIf cmdFind.Text = "&Update" Then
            If CheckValues() = True Then
                Exit Sub
            End If

            Query = New CPerformQuery
            'If Query.PerformUpdateQuery("Update Bank Set BankName='" & Trim(txtBankName.Text.Replace("'", "''")) & "',Address='" & txtAddress.Text.Replace("'", "''") & "',UpdateDate=GetDate(),Active='" & IIf(CheckActive.Checked = True, "Y", "N") & "' WHERE BankCode='" & txtBankCode.Text.Trim & "'", oConn2) > 0 Then
            If Query.PerformUpdateQuery("Update Bank Set BankName='" & Trim(txtBankName.Text.Replace("'", "''")) & "',Address='" & txtAddress.Text.Replace("'", "''") & "',UpdateDate='" & Now & "',Active='" & IIf(CheckActive.Checked = True, "Y", "N") & "' WHERE BankCode='" & txtBankCode.Text.Trim & "'", oConn2) > 0 Then
                MsgBox("Updating has been successful.", vbInformation)
                txtBankCode.ReadOnly = False
                cmdFind.Text = "&Ok"
                cmdAdd.Enabled = True
            Else
                MsgBox("Updating was unsuccessful!", MsgBoxStyle.Critical)
            End If
        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub ClearValues()

        txtBankCode.Clear()
        txtBankName.Clear()
        txtAddress.Clear()
        txtBankCode.ReadOnly = False
        txtBankCode.Focus()

    End Sub

    Function CheckValues() As Boolean
        Dim Str As String
        CheckValues = False
        Str = "must at least 3 characters."
        If Len(Trim(txtBankCode.Text)) < 3 Then
            MsgBox("Username " & Str, vbCritical)
            CheckValues = True
            Exit Function
        End If

    End Function

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        txtBankCode.Clear()
        cmdAdd.Enabled = False
        txtBankName.Clear()
        txtAddress.Clear()
        cmdFind.Text = "&Add"
        txtBankCode.Focus()
        CheckActive.Checked = True
        txtBankCode.ReadOnly = False
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Update" Or cmdFind.Text = "&Add" Or cmdFind.Text = "&Ok" Then
            cmdFind.Text = "&Find"
            Call ClearValues()
            cmdAdd.Enabled = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmdFindCheck()
        If cmdFind.Text = "&Ok" Then
            txtBankCode.ReadOnly = True
            cmdFind.Text = "&Update"
            cmdAdd.Enabled = False
        End If
    End Sub

    Private Sub CheckActive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckActive.Click
        cmdFindCheck()
    End Sub

    Private Sub txtAddress_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAddress.TextChanged
        cmdFindCheck()
    End Sub

    Private Sub txtBankCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBankCode.TextChanged

    End Sub

    Private Sub txtBankName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBankName.TextChanged
        cmdFindCheck()
    End Sub

    Private Sub frmBanks_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtBankCode.Focus()
    End Sub
End Class