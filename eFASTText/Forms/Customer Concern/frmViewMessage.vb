Option Explicit On
Imports System.Text

Public Class frmViewMessage
    Dim FromName, Msg, DateTime As String
    Dim Query As CPerformQuery
    Public ReplyMessage As String = ""
    Private SQL As StringBuilder

    Private Sub cmdReply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReply.Click

        If cmdReply.Text = "Re&ply" Then

            FromName = LblName.Text
            DateTime = LblDateTime.Text
            Msg = txtInboxMessage.Text

            Label2.Text = "To:"
            LblDateTime.Text = ""
            txtInboxMessage.Text = ""
            txtInboxMessage.ReadOnly = False

            cmdReply.Text = "&Send"
        Else
            If MsgBox("Send this message to " & LblName.Text & vbTab & LblNumber.Text & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Try
                    SQL = New StringBuilder
                    SQL.Append("Declare @intErrorCode int,@reply as nvarchar(480) ")
                    SQL.Append("BEGIN TRAN ")
                    SQL.Append("    INSERT INTO dbo.tblSMSBroadcast(bClientID,bMobileNo,bSMSMessage,flgSend)  ")
                    SQL.Append("    VALUES(1,'" & Trim(LblNumber.Text) & "','" & Replace(Trim(txtInboxMessage.Text), "'", "''") & "',0) ")
                    SQL.Append("    SET @intErrorCode=@@ERROR ")
                    SQL.Append("    IF (@intErrorCode<>0) GOTO PROBLEM ")

                    SQL.Append("    SET @reply='" & Replace(Trim(txtInboxMessage.Text), "'", "''") & "' + '\\' ")
                    SQL.Append("    UPDATE tblConcern SET cReply=(CASE WHEN cReply IS NULL THEN ''+@reply ELSE cReply + @reply END) WHERE cID=" & Trim(txtMsgIndex.Text))
                    SQL.Append("    SET @intErrorCode=@@ERROR ")
                    SQL.Append("    IF (@intErrorCode<>0) GOTO PROBLEM ")
                    SQL.Append("    IF (SELECT Date1stReply FROM tblConcern WHERE cID=" & Trim(txtMsgIndex.Text) & ") IS NULL ")
                    SQL.Append("        SET @intErrorCode=@@ERROR ")
                    SQL.Append("        IF (@intErrorCode<>0) GOTO PROBLEM ")
                    SQL.Append("        BEGIN ")
                    'SQL.Append("            UPDATE tblConcern SET Date1stReply=GetDate() WHERE cID=" & Trim(txtMsgIndex.Text))
                    SQL.Append("            UPDATE tblConcern SET Date1stReply='" & Now & "' WHERE cID=" & Trim(txtMsgIndex.Text))
                    SQL.Append("            SET @intErrorCode=@@ERROR ")
                    SQL.Append("            IF (@intErrorCode<>0) GOTO PROBLEM ")
                    SQL.Append("        END ")
                    SQL.Append("COMMIT TRAN ")
                    SQL.Append("PROBLEM: ")
                    SQL.Append("IF (@intErrorCode<>0) ROLLBACK TRAN ")

                    Query = New CPerformQuery
                    If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                        ReplyMessage = ReplyMessage & Trim(txtInboxMessage.Text) & "\\"
                        MsgBox("Message sent.", MsgBoxStyle.Information)
                    Else
                        MsgBox("Sending message was not successful!", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly)
                    End If

                Catch ex As Exception

                    modWriteLog("Error", Me, ex.Source, ex.Message)
                    MsgBox(ex, MsgBoxStyle.Critical)
                End Try
            End If
        End If

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        On Error GoTo xError

        If cmdReply.Text = "&Send" Then
            LblName.Text = FromName
            LblDateTime.Text = DateTime
            txtInboxMessage.Text = Msg
            Label2.Text = "From:"
            txtInboxMessage.ReadOnly = True

            cmdReply.Text = "Re&ply"
        Else
            Me.Close()
        End If
        Exit Sub
xError:
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtInboxMessage_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInboxMessage.TextChanged
        LblTextCount.Text = txtInboxMessage.Text.Length & "/480"
    End Sub

    Private Sub frmViewMessage_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Width = 404
        Me.Height = 361
    End Sub
End Class