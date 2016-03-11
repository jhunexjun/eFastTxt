Imports System.Text
Public Class frmSuccessfulPayment

    Dim Query As CPerformQuery
    Dim ListItemVar As System.Windows.Forms.ListViewItem
    Dim SQL As StringBuilder, c As Int16

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        ListView1.Items.Clear()
        cmdFind.Enabled = False
        SQL = New StringBuilder
        SQL.Append("Declare @From as DateTime,@To as DateTime ")
        SQL.Append("SET @From='" & DTFrom.Value & "'")
        SQL.Append("SET @To='" & DTTo.Value & "' ")
        SQL.Append("SET @From=CONVERT(nvarchar(30),@From,101) ")
        SQL.Append("SET @To=CONVERT(nvarchar(30),@To,101) ")
        SQL.Append("SELECT b.oID,b.oDateTimeIn,b.CommandUsed,b.oVoid,a.Salesperson FROM tblsmsOUT b Inner Join tblClients a On a.cClientID=b.oClientID where CONVERT(nvarchar(30),oDateTimeIn,101)>=@From AND CONVERT(nvarchar(30),oDateTimeIn,101)<=@To AND (CommandUsed Like 'PAYCH %' OR CommandUsed Like 'PAYCK %') AND oRepliedMSG Like '%successful%' ORDER BY oID ")

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
        c = 0
        Do While c < dt.Rows.Count
            ListItemVar = ListView1.Items.Add(c + 1)
            With ListItemVar.SubItems
                .Add(dt.Rows(c).Item("oID"))
                .Add(dt.Rows(c).Item("oDateTimeIn"))
                .Add(dt.Rows(c).Item("CommandUsed"))
                .Add(dt.Rows(c).Item("oVoid"))
                .Add(dt.Rows(c).Item("Salesperson"))
            End With

            c += 1
        Loop

        cmdFind.Enabled = True

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub frmSuccessfulPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.Value = Today
        DTFrom.Value = Today
    End Sub

    Private Sub ListView1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            ContextMnuVoid.Show(MousePosition)
            If ListView1.FocusedItem.SubItems.Item(4).Text = "False" Then
                ContextMnuVoid.Items(0).Text = "Set as Void"
            Else
                ContextMnuVoid.Items(0).Text = "Set as valid"
            End If
        End If
    End Sub

    Private Sub VoidToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles VoidToolStripMenuItem.Click
        On Error GoTo xError

        Dim Action As String, WriteLog As CLogFile
        SQL = New StringBuilder
        Query = New CPerformQuery


        If ListView1.FocusedItem.SubItems.Item(4).Text = "False" Then
            Action = "void"
        Else
            Action = "valid"
        End If

        Dim xoID As String = ListView1.FocusedItem.SubItems(1).Text

        If MsgBox("Are you sure you want to set as  " & Action & ", ID # '" & xoID & "'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then
            Exit Sub
        End If

        Dim f As New frmAuthentication
        If UserLevel = False Then
            f.Owner = Me
            f.ShowDialog()
        Else
            f.CorrectPassword = True
        End If
        
        If f.CorrectPassword = True Then
            If ListView1.FocusedItem.SubItems(2).Text.Substring(0, 5).ToString = "PAYCK" AndAlso ListView1.FocusedItem.SubItems(3).Text = "False" Then
                SQL.Append("Declare @intErrorCode int,@RowsAffected int ")
                SQL.Append("DECLARE @CustCode varchar(30),@ORNo VARCHAR(8),@CheckAmt numeric(18,6) ")
                SQL.Append("BEGIN TRAN ")
                SQL.Append("    SELECT @CustCode=dbo.fn_GetCustCode(oRegName),@ORNo=dbo.fn_GetOR(CommandUsed),@CheckAmt=dbo.fn_GetCheckPayment(CommandUsed) FROM tblsmsOUT WHERE oID=" & xoID)
                SQL.Append("    DELETE [" & oConn.Database & "].dbo.ArPostDatedCh WHERE Customer=@CustCode AND Cheque LIKE @ORNo AND Amount=CheckAmt ")
                SQL.Append("    SELECT @intErrorCode=@@ERROR,@RowsAffected=@@ROWCOUNT ")
                SQL.Append("    IF (@intErrorCode<>0) GOTO PROBLEM ")
                SQL.Append("    IF (@RowsAffected>0) ")
                SQL.Append("    BEGIN ")
                SQL.Append("        UPDATE [" & oConn.Database & "].dbo.tblsmsOUT SET oVoid=" & IIf(Action = "void", 1, 0) & " WHERE oID=" & xoID)
                SQL.Append("        SET @intErrorCode=@@ERROR ")
                SQL.Append("        IF (@intErrorCode<>0) GOTO PROBLEM ")
                SQL.Append("    END ")
                SQL.Append("COMMIT TRAN ")
                SQL.Append("PROBLEM: ")
                SQL.Append("IF (@intErrorCode<>0) ROLLBACK TRAN ")

                If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                    MsgBox("Transaction has been successful.", MsgBoxStyle.Information)
                End If
            End If
        End If



        'Upgraded
        'Dim Action As String, WriteLog As CLogFile
        'SQL = New StringBuilder
        'Query = New CPerformQuery

        'SQL.Append("Update tblsmsOUT SET ")
        'If ListView1.FocusedItem.SubItems.Item(4).Text = "False" Then
        'Action = "void"
        'SQL.Append("oVoid=1")
        'Else
        'Action = "valid"
        'SQL.Append("oVoid=0")
        'End If

        'Dim xoID As String = ListView1.FocusedItem.SubItems(1).Text
        'SQL.Append("WHERE oID=" & xoID)

        'If MsgBox("Are you sure you want to set as  " & Action & ", ID # '" & xoID & "'?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        'Dim f As New frmAuthentication

        'If UserLevel = False Then
        'f.Owner = Me
        'f.ShowDialog()
        'Else
        'f.CorrectPassword = True
        'End If

        'If f.CorrectPassword = True Then
        'If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then

        'If ListView1.FocusedItem.SubItems(2).Text.Substring(0, 5).ToString = "PAYCK" Then
        'If ListView1.FocusedItem.SubItems(3).Text = "False" Then
        'SQL.Append("Declare @intErrorCode int ")
        'SQL.Append("DECLARE @CustCode varchar(30),@ORNo VARCHAR(8),@CheckAmt numeric(18,6) ")
        'SQL.Append("BEGIN TRAN ")
        'SQL.Append("    SELECT @CustCode=dbo.fn_GetCustCode(oRegName),@ORNo=dbo.fn_GetOR(CommandUsed),@CheckAmt=dbo.fn_GetCheckPayment(CommandUsed) FROM tblsmsOUT WHERE oID=" & xoID)
        'SQL.Append("    DELETE ArPostDatedCh WHERE Customer=@CustCode AND Cheque LIKE @ORNo AND Amount=CheckAmt ")
        'SQL.Append("    SET @intErrorCode=@@ERROR ")
        'SQL.Append("    IF (@intErrorCode<>0) GOTO PROBLEM ")
        'SQL.Append("COMMIT TRAN ")
        'SQL.Append("PROBLEM: ")
        'SQL.Append("IF (@intErrorCode<>0) ROLLBACK TRAN ")



        'SQL.Append("SELECT dbo.fn_GetCustCode(oRegName)[CustCode],dbo.fn_GetOR(CommandUsed)[ORNumber],dbo.fn_GetCheckPayment(CommandUsed)[CheckAmount] FROM tblsmsOUT WHERE oID=" & xoID)
        'Query = New CPerformQuery
        'dt = Query.PerformSelectQuery(SQL.ToString, oConn2)

        'SQL = New StringBuilder
        'SQL.Append("DELETE ArPostDatedCh WHERE Customer='" & dt.Rows(0).Item("CustCode").ToString & "' AND Cheque LIKE '%" & dt.Rows(0).Item("ORNumber").ToString & "%' AND Amount=" & dt.Rows(0).Item("CheckAmount"))
        'Query = New CPerformQuery
        'If Query.PerformUpdateQuery(SQL.ToString, oConn) > 0 Then
        'MsgBox("Void transaction and deleting from check maintenance has been successful.", MsgBoxStyle.Information)
        'WriteLog = New CLogFile
        'WriteLog.LogWrite("Log: Void transaction and deleting from check maintenance has been successful. " & sender.ToString)
        'Else
        'Query = New CPerformQuery
        'If Query.PerformUpdateQuery("UPDATE tblsmsOUT SET oVoid=0 WHERE oID=" & xoID, oConn2) > 0 Then
        'MsgBox("Deleting record in ArPostDatedCh was not successful. Void transaction has been reversed." & Chr(13) & "If you want to void, please have it maintained in Syspro first having same Customer,Official Receipt, and Amount.", MsgBoxStyle.Critical)
        'WriteLog = New CLogFile
        'WriteLog.LogWrite("Log: Deleting record in ArPostDatedCh was not successful. Void transaction has been reversed. " & sender.ToString)
        'Else
        'MsgBox("Deleting record and void transaction reversal was unsuccessful. ", MsgBoxStyle.Critical)
        'WriteLog = New CLogFile
        'WriteLog.LogWrite("Log: Deleting record and void transaction reversal was unsuccessful. " & sender.ToString)
        'End If
        'End If
        'Else
        'GoTo Successful
        'End If
        'Else
        'GoTo Successful
        'End If
        'Else
        'MsgBox("Something went wrong. Please reprocess item.", MsgBoxStyle.Critical)
        'End If
        'End If
        'End If

        'Exit Sub
        'Successful:
        'MsgBox("Item successfully " & Action & ".", MsgBoxStyle.Information)
        'With ListView1.FocusedItem.SubItems
        'If Action = "void" Then
        '.Item(4).Text = "True"
        'Else
        '.Item(4).Text = "False"
        'End If
        'End With

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub TSExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSExport.Click
        On Error GoTo xError

        SaveFileDialog1.Filter = "Text File(*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName <> "" Then
            Dim ExportToText As New CExportToText

            Dim c = 0, RowText As String = ""
            r = 0
            Dim LVColumnCnt As Int16 = ListView1.Columns.Count, LVRCnt As Integer = ListView1.Items.Count

            Do While c < LVColumnCnt
                RowText &= ListView1.Columns(c).Text & IIf(c = ListView1.Columns.Count - 1, "", vbTab)
                c += 1
            Loop

            ExportToText.CreateFile(SaveFileDialog1.FileName)
            ExportToText.WriteText(RowText)

            c = 0
            RowText = ""

            Do While r < LVRCnt
                Do While c < LVColumnCnt
                    RowText &= ListView1.Items(r).SubItems(c).Text & IIf(c = LVColumnCnt - 1, "", vbTab)
                    c += 1
                Loop

                ExportToText.WriteText(RowText)

                c = 0
                r += 1
                RowText = ""
            Loop

            ExportToText.CloseFile()
            SaveFileDialog1.FileName = ""

            MessageBox.Show("Export has been successful!", My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ContextMnuVoid_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMnuVoid.Opening
        If ListView1.FocusedItem.SubItems(2).Text.Substring(0, 5).ToString = "PAYCK" AndAlso ListView1.FocusedItem.SubItems(3).Text.ToUpper = "True".ToUpper Then
            ContextMnuVoid.Items(0).Enabled = False
        Else
            ContextMnuVoid.Items(0).Enabled = True
        End If
    End Sub
End Class