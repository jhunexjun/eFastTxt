Imports System.Data.SqlClient
Imports System.Text

Public Class frmbroadcast
    Private dt2 As DataTable
    Private Query As CPerformQuery
    Dim SQL As StringBuilder
    Dim c As Integer

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdSend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSend.Click
        On Error GoTo xError

        If ListView1.CheckedItems.Count <= 0 Then
            MsgBox("Please select recipient.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtMessage.MaxLength = 32767

        If MsgBox("Send message to " & ListView1.CheckedItems.Count & " recipient(s)?", MsgBoxStyle.YesNo + MsgBoxStyle.Information) = MsgBoxResult.Yes Then
            Dim f As New frmAuthentication
            f.ShowDialog()
            If f.CorrectPassword = False Then
                Exit Sub
            End If

            Dim c As Int32

            SQL = New StringBuilder
            SQL.Append("Declare @msg nvarchar(320),@intErrorCode int ")
            SQL.Append("BEGIN TRAN ")

            Do While c < ListView1.CheckedItems.Count
                SQL.Append("SET @msg='" & txtMessage.Text.Trim.Replace("'", "''").ToString & "' ")
                SQL.Append("INSERT INTO [dbo].[tblSMSBroadcast] (bClientID,bMobileNo,bSMSMessage,flgSend) ")
                SQL.Append("Values('" & ListView1.Items(c).SubItems(1).Text.Replace("'", "''") & "','" & "+" & ListView1.Items(c).Text & "',@msg,0) ")
                SQL.Append("SET @intErrorCode=@@ERROR ")
                SQL.Append("IF (@intErrorCode<>0) GOTO PROBLEM ")
                c += 1
            Loop

            SQL.Append("COMMIT TRAN ")
            SQL.Append("PROBLEM: ")
            SQL.Append("IF (@intErrorCode<>0) ")
            SQL.Append("    BEGIN ")
            SQL.Append("       ROLLBACK TRAN ")
            SQL.Append("    END ")

            Query = New CPerformQuery
            If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                MsgBox("Message was successfully sent to all recipient(s).", MsgBoxStyle.Information)
            Else
                MsgBox("No message was sent to recipient(s).", MsgBoxStyle.Information)
            End If

        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub txtMessage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMessage.TextChanged
        Label3.Text = txtMessage.Text.Length & "/160"
    End Sub

    Private Sub frmBroadCast_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Dim c As Int16

        cboSOType.Items.Add("All")
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("Select (OrderType+'-'+Description)[SOTypes] FROM TblSoTypes Order by OrderType", oConn)
        If dt.Rows.Count() > 0 Then
            Do While c < dt.Rows.Count
                cboSOType.Items.Add(dt.Rows(c).Item("SOTypes"))
                c += 1
            Loop
        End If

        cboCustType.Items.Add("All")
        c = 0
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("Select (Branch+ '-'+Description)[Branch] FROM SalBranch Order By Branch", oConn)
        If dt.Rows.Count() > 0 Then
            Do While c < dt.Rows.Count
                cboCustType.Items.Add(dt.Rows(c).Item("Branch"))
                c += 1
            Loop
        End If

        cboPromoType.Items.Add("All")
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT Distinct SubGroupCode FROM NPICustGrp_Members Order by SubGroupCode ", oConn)
        c = 0
        Do While c < dt.Rows.Count
            cboPromoType.Items.Add(dt.Rows(c).Item("SubGroupCode").ToString)
            c += 1
        Loop

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub frmBroadCast_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 679
        Me.Height = 453
    End Sub

    Private Sub cboSOType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSOType.SelectedIndexChanged
        If cboCustType.SelectedIndex > -1 And cboPromoType.SelectedIndex > -1 Then
            Call RetrieveCustomers()
        End If
    End Sub

    Private Sub RetrieveCustomers()
        Me.Cursor = Cursors.WaitCursor

        On Error GoTo xError
        ListView1.Items.Clear()
        LblRecords.Text = "0 record(s) found."

        Dim ListItemVar As System.Windows.Forms.ListViewItem
        SQL = New StringBuilder
        SQL.Append("Select DISTINCT Ltrim(rtrim(rCell))[rCell],ltrim(rtrim(Name))[Name] FROM ArCustomer a Inner Join NPICustGrp_Members b On a.Customer=b.Customer WHERE LEN(RTRIM(LTRIM(rCell)))=12 AND CustomerOnHold='N' AND b.DateFrom<=GetDate() AND b.DateTo>=GetDate() ")

        If cboCustType.SelectedIndex > 0 And cboPromoType.Text = "All" Then
            If cboCustType.Text.Substring(0, InStr(cboCustType.Text, "-") - 1) = "AE" Then
                SQL = New StringBuilder
                SQL.Append("Select DISTINCT Ltrim(rtrim(rCell))[rCell],ltrim(rtrim(Name))[Name] FROM ArCustomer a WHERE LEN(RTRIM(LTRIM(rCell)))=12 AND CustomerOnHold='N' ")
            End If
        End If


        If cboCustType.Text = "All" And cboSOType.Text = "All" And cboPromoType.Text = "All" Then
            'All customer not onhold and rCell=2
        ElseIf cboCustType.Text = "All" And cboSOType.Text = "All" And cboPromoType.Text <> "All" Then
            SQL.Append(" AND SubGroupCode='" & cboPromoType.Text & "'  ")
        ElseIf cboCustType.Text = "All" And cboSOType.Text <> "All" And cboPromoType.Text = "All" Then
            SQL.Append(" AND DefaultOrdType='" & cboSOType.Text.Substring(0, InStr(cboSOType.Text, "-") - 1) & "' ")
        ElseIf cboCustType.Text = "All" And cboSOType.Text <> "All" And cboPromoType.Text <> "All" Then
            SQL.Append(" AND DefaultOrdType='" & cboSOType.Text.Substring(0, InStr(cboSOType.Text, "-") - 1) & "' AND SubGroupCode='" & cboPromoType.Text & "' ")

        ElseIf cboCustType.Text <> "All" And cboSOType.Text = "All" And cboPromoType.Text = "All" Then
            SQL.Append(" AND a.Branch='" & cboCustType.Text.Substring(0, InStr(cboCustType.Text, "-") - 1) & "' ")
        ElseIf cboCustType.Text <> "All" And cboSOType.Text = "All" And cboPromoType.Text <> "All" Then
            SQL.Append(" AND a.Branch='" & cboCustType.Text.Substring(0, InStr(cboCustType.Text, "-") - 1) & "' AND SubGroupCode='" & cboPromoType.Text & "' ")
        ElseIf cboCustType.Text <> "All" And cboSOType.Text <> "All" And cboPromoType.Text = "All" Then
            SQL.Append(" AND a.Branch='" & cboCustType.Text.Substring(0, InStr(cboCustType.Text, "-") - 1) & "' AND DefaultOrdType='" & cboSOType.Text.Substring(0, InStr(cboSOType.Text, "-") - 1) & "' ")
        ElseIf cboCustType.Text <> "All" And cboSOType.Text <> "All" And cboPromoType.Text <> "All" Then
            SQL.Append(" AND a.Branch='" & cboCustType.Text.Substring(0, InStr(cboCustType.Text, "-") - 1) & "' AND DefaultOrdType='" & cboSOType.Text.Substring(0, InStr(cboSOType.Text, "-") - 1) & "' AND SubGroupCode='" & cboPromoType.Text & "' ")
        End If

        Dim Query As New CPerformQuery
        dt2 = New DataTable
        dt2 = Query.PerformSelectQuery(SQL.ToString, oConn)

        If dt2.Rows.Count > 0 Then
            c = 0
            Do While c < dt2.Rows.Count
                ListItemVar = ListView1.Items.Add(dt2.Rows(c).Item("rCell"))
                ListItemVar.SubItems.Add(dt2.Rows(c).Item("Name"))
                ListView1.Items(c).Checked = True
                c += 1
            Loop
        End If

        LblRecords.Text = ListView1.Items.Count & " record(s) found."
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cboCustType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCustType.SelectedIndexChanged
        If cboSOType.SelectedIndex > -1 And cboPromoType.SelectedIndex > -1 Then
            Call RetrieveCustomers()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        c = 0

        If CheckBox1.Checked = True Then
            Do While c < ListView1.Items.Count
                ListView1.Items(c).Checked = True
                c += 1
            Loop
        Else
            Do While c < ListView1.Items.Count
                ListView1.Items(c).Checked = False
                c += 1
            Loop
        End If
    End Sub

    Private Sub cboPromoType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPromoType.SelectedIndexChanged
        If cboCustType.SelectedIndex > -1 And cboSOType.SelectedIndex > -1 Then
            Call RetrieveCustomers()
        End If
    End Sub
End Class