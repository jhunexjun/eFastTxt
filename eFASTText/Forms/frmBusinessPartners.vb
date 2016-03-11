Imports System.Data.SqlClient
Imports System.Text
Public Class frmBusinessPartners
    Private Query As New CPerformQuery
    Dim SQL As StringBuilder
    Dim xs As Control
    Private SQLCmd As SqlCommand

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        Dim WithAsterisk As Boolean = False

        Me.Cursor = Cursors.WaitCursor
        SQL = New StringBuilder

        If cmdFind.Text = "&Find" Then

            SQL.Append("SELECT a.Customer,a.Name,a.rCell,(CASE WHEN a.CustomerOnHold='Y' THEN 'True' ELSE 'False' END)[CustomerOnHold] FROM ArCustomer a WHERE ")

            For Each Me.xs In Me.Controls
                If TypeOf xs Is TextBox Then
                    If InStr(xs.Text, "*") > 0 Then
                        If xs.Name = txtCustomerCode.Name Then
                            SQL.Append("a.Customer Like '" & txtCustomerCode.Text.Trim.Replace("*", "%") & "' COLLATE SQL_Latin1_General_CP1_CI_AS    AND ")
                        End If
                        If xs.Name = txtCustomerName.Name Then
                            SQL.Append("a.Name Like '" & txtCustomerName.Text.Replace("*", "%") & "' COLLATE SQL_Latin1_General_CP1_CI_AS    AND ")
                        End If
                        If xs.Name = txtMobile.Name Then
                            SQL.Append("a.rCell Like '" & txtMobile.Text.Replace("*", "%") & "'   AND ")
                        End If

                        WithAsterisk = True
                    End If
                End If
            Next xs

            If WithAsterisk = True Then
                SQL = SQL.Remove(SQL.Length - 6, 6)

                Dim f As New frmDialog
                f.DatabaseConnection = 1
                f.strToPerform = SQL.ToString
                f.ShowDialog(Me)

                If f.Sel(0).ToString.Length <> 0 Then
                    txtCustomerCode.Text = f.Sel(0).ToString
                    txtCustomerName.Text = ""
                    txtMobile.Text = ""
                    ChkActive.Checked = False
                End If

            Else
                SQL.Append("Customer='" & txtCustomerCode.Text.Trim & "' COLLATE SQL_Latin1_General_CP1_CI_AS")
                Query = New CPerformQuery
                dt = Query.PerformSelectQuery(SQL.ToString, oConn)
                If dt.Rows.Count >= 1 Then
                    txtCustomerCode.Text = Trim(dt.Rows(0).Item("Customer"))
                    txtCustomerName.Text = Trim(dt.Rows(0).Item("Name"))

                    txtMobile.Text = Trim(dt.Rows(0).Item("rCell").ToString)
                    ChkActive.Checked = dt.Rows(0).Item("CustomerOnHold")

                    Query = New CPerformQuery
                    SQL = New StringBuilder
                    SQL.Append("SELECT cClientID,cFamilyName,cFirstName,cUplineID,cUplineIDPrev,(CASE WHEN DocSubmitted IS NULL THEN '' ELSE DocSubmitted END)[DocSubmitted] FROM tblClients WHERE cMobile='+" & txtMobile.Text & "'")
                    
                    dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
                    If dt.Rows.Count > 0 Then
                        With dt.Rows(0)
                            txtInternalKey.Text = Trim(.Item("cClientID"))
                            txtFamilyName.Text = Trim(.Item("cFamilyName").ToString)
                            txtFirstName.Text = Trim(.Item("cFirstName").ToString)
                            txtUplineID.Text = Trim(.Item("cUplineID").ToString)
                            txtPrevUplineID.Text = Trim(.Item("cUplineIDPrev").ToString)
                            cboDocSubmitted.Text = .Item("DocSubmitted").ToString
                        End With
                    End If

                    cmdFind.Text = "&Ok"
                Else
                    MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
                End If

            End If

            Me.Cursor = Cursors.Default

        ElseIf cmdFind.Text = "&Update" Then
            If CheckValues() = False Then
                SQL = New StringBuilder
                SQL.Append("DECLARE @ErrorCode int,@txtCustomerCode nvarchar(20),@Name nvarchar(30),@txtLName nvarchar(40),@txtFName nvarchar(40),@txtMobileNo nvarchar(12),@PrevMobile nvarchar(12),@UplineID nvarchar(25),@PrevUplineID nvarchar(25),@txtInternalKey nvarchar(30),@DocSubmitted nvarchar(10) ")
                SQL.Append("SET @txtCustomerCode='" & txtCustomerCode.Text.ToString.Replace("'", "''") & "' ")
                SQL.Append("SET @Name='" & txtCustomerName.Text.ToString.Replace("'", "''") & "' ")
                SQL.Append("SET @txtLName='" & txtFamilyName.Text.Replace("'", "''").ToString & "' ")
                SQL.Append("SET @txtFName='" & txtFirstName.Text.Replace("'", "''").ToString & "' ")
                SQL.Append("SET @txtMobileNo='" & txtMobile.Text.ToString.Replace("'", "''") & "' ")
                SQL.Append("SET @txtInternalKey='" & txtInternalKey.Text.ToString.Replace("'", "''") & "' ")
                SQL.Append("SET @UplineID='" & txtUplineID.Text.ToString.Replace("'", "''") & "' ")
                SQL.Append("SET @DocSubmitted='" & cboDocSubmitted.Text & "' ")
                SQL.Append("SET @PrevMobile=(SELECT top 1 rCell FROM [" & oConn.Database & "].dbo.ArCustomer WHERE Customer=@txtCustomerCode) ")
                SQL.Append("BEGIN TRAN ")
                SQL.Append("    UPDATE [" & oConn.Database & "].dbo.ArCustomer SET rCell=@txtMobileNo,Name=@Name Where Customer=@txtCustomerCode ")
                SQL.Append("    SET @ErrorCode=@@ERROR ")
                SQL.Append("    IF (@ErrorCode<>0) GOTO PROBLEM ")
                SQL.Append("    SET @PrevUplineID=(SELECT cUplineID FROM tblClients WHERE cClientID=@txtInternalKey) ")
                SQL.Append("    IF (@UplineID<>@PrevUplineID) ")
                SQL.Append("        BEGIN ")
                SQL.Append("            UPDATE tblClients SET cMobile='+' + CAST(@txtMobileNo as nvarchar(20)),cUplineID=@UplineID,cUplineIDPrev=@PrevUplineID,cFamilyName=@txtLName,cFirstName=@txtFName,DocSubmitted=@DocSubmitted WHERE cMobile='+' + CAST(@PrevMobile as nvarchar(20)) ")
                SQL.Append("            SET @ErrorCode=@@ERROR ")
                SQL.Append("            IF (@ErrorCode<>0) GOTO PROBLEM ")
                SQL.Append("        END ")
                SQL.Append("    ELSE ")
                SQL.Append("        BEGIN ")
                SQL.Append("            UPDATE tblClients SET cMobile='+' + CAST(@txtMobileNo as nvarchar(20)),cFamilyName=@txtLName,cFirstName=@txtFName,DocSubmitted=@DocSubmitted WHERE cMobile='+' + CAST(@PrevMobile as nvarchar(20)) ")
                SQL.Append("            SET @ErrorCode=@@ERROR ")
                SQL.Append("            IF (@ErrorCode<>0) GOTO PROBLEM ")
                SQL.Append("        END ")
                SQL.Append("COMMIT TRAN ")
                SQL.Append("PROBLEM: ")
                SQL.Append("    IF (@ErrorCode<>0) ROLLBACK TRAN ")


                SQLCmd = New SqlCommand(SQL.ToString, oConn2)
                If SQLCmd.ExecuteNonQuery > 0 Then
                    MsgBox("Updating has been successful.", MsgBoxStyle.Information)
                    txtCustomerCode.ReadOnly = False
                    cmdFind.Text = "&Ok"

                    SQL = New StringBuilder
                    SQL.Append("SELECT cUplineIDPrev FROM tblClients WHERE cClientID='" & txtInternalKey.Text & "'")
                    Query = New CPerformQuery
                    dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
                    txtPrevUplineID.Text = dt.Rows(0).Item("cUplineIDPrev").ToString
                Else
                    MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
                End If
            End If

        ElseIf cmdFind.Text = "&Ok" Then
            Me.Close()
        End If

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Function CheckValues() As Boolean   'If true naa error
        On Error GoTo xError
        CheckValues = True

        On Error GoTo xError
        If Trim$(txtMobile.TextLength) < 12 Then
            MsgBox("Mobile number must be 12 characters.", MsgBoxStyle.Exclamation)
            txtMobile.Focus()
            Exit Function
        End If
        If Trim$(txtMobile.Text.Substring(0, 2)) <> "63" Then
            MsgBox("Number must be international format.", MsgBoxStyle.Exclamation)
            txtMobile.Focus()
            Exit Function
        End If

        Query = New CPerformQuery
        SQL.Append("SELECT Customer FROM ArCustomer WHERE rCell='" & txtMobile.Text & "' AND Customer<>'" & txtCustomerCode.Text & "'")
        dt = Query.PerformSelectQuery(SQL.ToString, oConn)
        If dt.Rows.Count > 0 Then
            MsgBox("Number already asigned to '" & dt.Rows(0).Item("Customer") & "'", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        If InStr(txtUplineID.Text, "*") > 0 Then
            SQL.Append("SELECT cClientID[Client ID],cUplineID[Upline ID],cFamilyName[Family Name],cFirstName[First Name],cMobile[Mobile] FROM tblClients WHERE cClientID Like '" & txtUplineID.Text.Trim.Replace("*", "%") & "'")
            Dim f As New frmDialog
            f.DatabaseConnection = 3
            f.strToPerform = SQL.ToString
            f.ShowDialog()
            If f.Sel(0).ToString.Length <> 0 Then
                txtUplineID.Text = f.Sel(0).ToString
            End If

            Exit Function
        End If

        CheckValues = False
        Exit Function
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & Me.Name & ": CheckValues() " & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Function

    Private Sub u_TextChange()
        If cmdFind.Text = "&Ok" Then
            cmdFind.Text = "&Update"
            txtCustomerCode.ReadOnly = True
        End If
    End Sub

    Private Sub txtMobile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMobile.TextChanged
        u_TextChange()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        'Me.Close()
        If cmdFind.Text = "&Find" Then
            Me.Close()
        Else
            ClearValues()

            cmdFind.Text = "&Find"
            txtCustomerCode.Focus()
            txtCustomerCode.ReadOnly = False
        End If
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call u_TextChange()
    End Sub

    Private Sub frmBusinessPartners_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMenuStrip1.Show(MousePosition)
        End If
    End Sub

    Private Sub frmBusinessPartners_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 414
        Me.Height = 416
    End Sub

    Private Sub ContextMenuStrip1_Opened(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContextMenuStrip1.Opened
        With ContextMenuStrip1
            If cmdFind.Text = "&Ok" Then
                .Items(0).Enabled = False
                .Items(1).Enabled = True
                .Items(2).Enabled = True
            ElseIf cmdFind.Text = "&Update" Then
                .Items(0).Enabled = True
                .Items(1).Enabled = False
                .Items(2).Enabled = True
            ElseIf cmdFind.Text = "&Find" Then
                .Items(0).Enabled = False
                .Items(1).Enabled = False
                .Items(2).Enabled = False
            End If
        End With
        
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        cmdFind_Click(sender, e)
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        u_TextChange()
    End Sub

    Private Sub CancelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelToolStripMenuItem.Click
        cmdCancel_Click(sender, e)
    End Sub

    Private Sub ClearValues()
        For Each Me.xs In Me.Controls
            If TypeOf xs Is TextBox Then
                xs.Text = ""
            End If
        Next
        ChkActive.Checked = False
        cboDocSubmitted.SelectedIndex = -1
    End Sub

    Private Sub txtUplineID_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUplineID.Leave
        If cmdFind.Text = "&Update" Then
            If InStr(txtUplineID.Text, "*") > 0 Then
                SQL.Append("SELECT cClientID[Client ID],cUplineID[Upline ID],cFamilyName[Family Name],cFirstName[First Name],cMobile[Mobile] FROM tblClients WHERE cClientID Like '" & txtUplineID.Text.Trim.Replace("*", "%") & "'")
                Dim f As New frmDialog
                f.DatabaseConnection = 3
                f.strToPerform = SQL.ToString
                f.ShowDialog()
                If f.Sel(0).ToString.Length <> 0 Then
                    txtUplineID.Text = f.Sel(0).ToString
                End If

                Exit Sub
            End If
        End If
    End Sub

    Private Sub txtUplineID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUplineID.TextChanged
        u_TextChange()
    End Sub

    Private Sub txtCustomerName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerName.TextChanged
        u_TextChange()
    End Sub

    Private Sub txtFamilyName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFamilyName.TextChanged
        u_TextChange()
    End Sub

    Private Sub txtFirstName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFirstName.TextChanged
        u_TextChange()
    End Sub

    Private Sub cboDocSubmitted_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDocSubmitted.SelectedIndexChanged
        u_TextChange()
    End Sub
End Class