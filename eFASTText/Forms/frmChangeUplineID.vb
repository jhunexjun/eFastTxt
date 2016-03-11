Public Class frmChangeUplineID
    Private Query As CPerformQuery
    Dim SQL As String, c As Int16

    Private Sub frmChangeUplineID_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        SQL = "Select (cClientID+'  '+RTRIM(LTRIM(cFamilyName))+', '+LTRIM(RTRIM(cFirstName)))[cClient] From tblClients WHERE cFlagCol=1 ORDER BY cFamilyName"

        Query = New CPerformQuery
        Call eFastTextDatabase() 'ConnectSmartDatabase()
        dt = Query.PerformSelectQuery(SQL, oConn2)

        If dt.Rows.Count > 0 Then
            c = 0
            Do While c < dt.Rows.Count
                cboFrom.Items.Add(dt.Rows(c).Item("cClient"))
                c += 1
            Loop
        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default

        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cboFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFrom.SelectedIndexChanged
        On Error GoTo xError

        cboTo.Items.Clear()
        txtToName.Text = ""
        cboTo.Enabled = False
        txtToName.Enabled = False


        Query = New CPerformQuery
        SQL = "SELECT (RTRIM(LTRIM(cFamilyName))+', '+LTRIM(RTRIM(cFirstName))+ ': '+LTRIM(RTRIM(cMobile)))[Name] From tblClients Where cClientID='" & Mid$(cboFrom.Text, 1, 19) & "' AND cFlagCol=1"
        dt = Query.PerformSelectQuery(SQL, oConn2)
        If dt.Rows.Count > 0 Then
            txtFromName.Text = dt.Rows(0).Item("Name")
            cboTo.Enabled = True
            txtToName.Enabled = True

            Query = New CPerformQuery
            SQL = "Select (cClientID+'  '+RTRIM(LTRIM(cFamilyName))+', '+LTRIM(RTRIM(cFirstName)))[cClient] From tblClients WHERE cFlagCol=1 AND cClientID<>'" & Mid$(cboFrom.Text, 1, 19) & "'  ORDER BY cFamilyName"
            dt = Query.PerformSelectQuery(SQL, oConn2)
            If dt.Rows.Count > 0 Then
                c = 0
                Do While c < dt.Rows.Count
                    cboTo.Items.Add(dt.Rows(c).Item("cClient"))
                    c += 1
                Loop
            Else
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            End If
        Else
            MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default

        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)

    End Sub

    Private Sub frmChangeUplineID_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 480
        Me.Height = 246
    End Sub

    Private Sub cboTo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTo.SelectedIndexChanged
        On Error GoTo xError

        Query = New CPerformQuery
        SQL = "SELECT (RTRIM(LTRIM(cFamilyName))+', '+LTRIM(RTRIM(cFirstName))+ ': '+RTRIM(LTRIM(cMobile)))[Name] From tblClients Where cClientID='" & Mid$(cboTo.Text, 1, 19) & "' AND cFlagCol=1"
        dt = Query.PerformSelectQuery(SQL, oConn2)
        If dt.Rows.Count > 0 Then
            txtToName.Text = dt.Rows(0).Item("Name")
            cmdOk.Text = "&Update"
        End If

        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default

        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        On Error GoTo xError
        Dim WriteLog As New CLogFile

        If cmdOk.Text = "&Ok" Then
            Me.Close()
        ElseIf cmdOk.Text = "&Update" Then
            If MsgBox("Are you sure you want to tag the downline of " & txtFromName.Text & " to " & txtToName.Text & " ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = vbYes Then
                Query = New CPerformQuery
                SQL = "Update tblClients SET cUplineID='" & Mid$(cboTo.Text, 1, 19) & "' WHERE cUplineID='" & Mid$(cboFrom.Text, 1, 19) & "' and cFlagLOADR=1"
                dt = Query.PerformSelectQuery(SQL, oConn2)


                WriteLog = New CLogFile
                WriteLog.LogWrite("Log: " & SQL)

                MsgBox("Update has been successful.", vbInformation)
                WriteLog = New CLogFile
                WriteLog.LogWrite("Log: Update has been successful.")

                txtFromName.Text = ""
                cboTo.Items.Clear()
                cboTo.Enabled = False
                txtToName.Text = ""
                txtToName.Enabled = False
                cmdOk.Text = "&Ok"
            End If
        End If

        Exit Sub
xError:
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default

        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class