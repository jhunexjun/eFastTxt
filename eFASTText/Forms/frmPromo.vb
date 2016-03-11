Imports System.Text
Public Class frmPromo
    Private Query As CPerformQuery
    Private SQL As StringBuilder
    Dim xs As Control

    Private Sub frmPromo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmPromo_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.Width = 348
        Me.Height = 203
    End Sub

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError

        Dim WithAsterisk As Boolean = False
        Me.Cursor = Cursors.WaitCursor

        SQL = New StringBuilder
        If cmdFind.Text = "&Find" Then
            SQL.Append("Select a.PromoCode,a.PromoName FROM u_Promo a WHERE ")

            For Each Me.xs In Me.Controls
                If TypeOf xs Is TextBox Then
                    If InStr(xs.Text, "*") > 0 Then
                        If xs.Name = txtPromoCode.Name Then
                            SQL.Append("a.PromoCode Like '" & txtPromoCode.Text.Trim.Replace("*", "%") & "' COLLATE SQL_Latin1_General_CP1_CI_AS    AND ")
                        End If
                        If xs.Name = txtPromoName.Name Then
                            SQL.Append("a.PromoName Like '" & txtPromoName.Text.Replace("*", "%") & "' COLLATE SQL_Latin1_General_CP1_CI_AS    AND ")
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
                    txtPromoCode.Text = f.Sel(0).ToString
                    txtPromoName.Text = ""
                End If
            Else
                SQL.Append("PromoCode='" & txtPromoCode.Text.Trim & "' COLLATE SQL_Latin1_General_CP1_CI_AS")
                Query = New CPerformQuery
                dt = Query.PerformSelectQuery(SQL.ToString, oConn)
                If dt.Rows.Count >= 1 Then
                    txtPromoCode.Text = Trim(dt.Rows(0).Item("PromoCode"))
                    txtPromoName.Text = Trim(dt.Rows(0).Item("PromoName"))

                    cmdFind.Text = "&Ok"
                Else
                    MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
                End If

            End If

            Me.Cursor = Cursors.Default

        ElseIf cmdFind.Text = "&Add" Then
            If CheckValues() = False Then
                SQL.Append("INSERT INTO u_Promo(PromoCode,PromoName) VALUES('" & Trim(txtPromoCode.Text) & "','" & Trim(txtPromoName.Text) & "')")
                Query = New CPerformQuery
                If Query.PerformUpdateQuery(SQL.ToString, oConn) > 0 Then
                    MsgBox("Adding has been successful.", MsgBoxStyle.Information)
                    cmdFind.Text = "&Ok"
                    cmdAdd.Enabled = True
                Else
                    MsgBox("Invalid values. Item already found in u_Promo table. Please check.", MsgBoxStyle.Critical)
                End If
            End If

        ElseIf cmdFind.Text = "&Update" Then
            If CheckValues() = False Then
                SQL.Append("UPDATE u_Promo SET PromoName='" & Trim$(txtPromoName.Text) & "' Where PromoCode='" & Trim$(txtPromoCode.Text) & "'")
                If Query.PerformUpdateQuery(SQL.ToString, oConn) > 0 Then

                    MsgBox("Updating has been successful.", MsgBoxStyle.Information)
                    txtPromoCode.ReadOnly = False
                    cmdFind.Text = "&Ok"
                    cmdAdd.Enabled = True
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
        Dim WriteLog As New CLogFile
        WriteLog.LogWrite("Error: " & sender.ToString & ": " & e.ToString & vbTab & Err.Number & Err.Description)
        Me.Cursor = Cursors.Default

        MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub ClearValues()
        txtPromoCode.Clear()
        txtPromoName.Clear()
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True
        If txtPromoCode.TextLength <= 0 Or txtPromoName.TextLength <= 0 Then
            MsgBox("Please fill-up the form completely.", MsgBoxStyle.Critical)
            txtPromoCode.Focus()
            Exit Function
        End If

        CheckValues = False
    End Function

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        cmdFind.Text = "&Add"
        cmdAdd.Enabled = False
        txtPromoCode.Clear()
        txtPromoName.Clear()
        txtPromoCode.Focus()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Find" Then
            Me.Close()
        Else
            ClearValues()

            cmdFind.Text = "&Find"
            txtPromoCode.Focus()
            txtPromoCode.ReadOnly = False
            cmdAdd.Enabled = True
        End If
    End Sub

    Private Sub u_TextChange()
        If cmdFind.Text = "&Ok" Then
            cmdFind.Text = "&Update"
            txtPromoCode.ReadOnly = True
        End If
    End Sub

    Private Sub txtPromoName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPromoName.TextChanged
        u_TextChange()
    End Sub
End Class