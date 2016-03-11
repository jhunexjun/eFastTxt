Imports System.Text
Public Class frmMiscPayment
    Private SQL As StringBuilder
    Dim r, c As Int16
    Private Query As CPerformQuery

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        'DataGridView1.Rows.Clear()

        'Exit Sub

        If cmdFind.Text = "&Add" Then
            If CheckValues() = False Then
                If MsgBox("Are you sure you want to add this item(s)?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    r = 0 : c = 0

                    Dim vSalesman, vOR, vAmount As String, RowCounterAdded As Int16 = 0, RowCountTotal As Int16 = DataGridView1.RowCount - 1

                    Do While r < DataGridView1.Rows.Count - 1
                        vSalesman = DataGridView1.Item(0, r).Value
                        vOR = DataGridView1.Item(1, r).Value
                        vAmount = DataGridView1.Item(2, r).Value

                        Dim CMD As New StringBuilder
                        CMD.Append("PAYCX ")
                        CMD.Append(vSalesman)
                        CMD.Append(" ")
                        CMD.Append(vOR)
                        CMD.Append(" ")
                        CMD.Append("x ")
                        CMD.Append(vAmount)

                        Query = New CPerformQuery
                        SQL = New StringBuilder
                        SQL.Append("Declare @Num as char(20) ")
                        SQL.Append("SET @Num=(SELECT cMobile FROM tblClients WHERE Salesperson='C01' AND cApprover=7) ")
                        SQL.Append("EXEC sp_ValidateSMSIn1 @Num,'" & CMD.ToString & "'")

                        If Query.PerformUpdateQuery(SQL.ToString, oConn2) <= 0 Then
                            MsgBox("Adding has not been successful. Please try again.", MsgBoxStyle.Critical)

                        Else
                            RowCounterAdded += 1
                            DataGridView1.Rows.RemoveAt(r)
                        End If

                        r += 1
                    Loop

CountAdded:
                    MsgBox(RowCounterAdded & " out of " & RowCountTotal & " has been added successfully.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                    If RowCounterAdded = RowCountTotal Then

                    End If

                End If
            End If
        ElseIf cmdFind.Text = "&Ok" Then

        End If
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True

        If DataGridView1.RowCount - 1 <= 0 Then
            MsgBox("No data to save.", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        r = 0 : c = 0
        Do While r < DataGridView1.Rows.Count - 1
            Do While c < DataGridView1.Columns.Count - 1
                If DataGridView1.Item(c, r).Value = "" Then
                    MsgBox("Invalid data in item(row=" & r & ",col=" & c & ")", MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
                    Exit Function
                End If
                If Not IsNumeric(DataGridView1.Item(2, r).Value) = True Then
                    MsgBox("Please enter numeric values only.", MsgBoxStyle.Exclamation)
                    Exit Function
                End If

                c += 1
            Loop
            c = 0
            r += 1
        Loop

        CheckValues = False
        Return CheckValues
    End Function

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub
End Class