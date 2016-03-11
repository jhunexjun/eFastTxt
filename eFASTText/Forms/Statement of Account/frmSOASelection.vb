Imports System.Text
Public Class frmSOASelection
    Private SQL As StringBuilder

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub frmSOASelection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DTFrom.Value = Today
        DTTo.Value = Today
    End Sub

    Private Function GetSQL() As String

        Sql = New StringBuilder
        SQL.Append("DECLARE @CustCode as nvarchar(20),@DateFrom as DateTime,@DateTo as DateTime ")
        SQL.Append("SET @CustCode='" & txtCustomerCode.Text & "' ")
        SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
        Sql.Append("SELECT b.oDateTimeIn,a.[Name],b.oRepliedMSG FROM ")
        Sql.Append("[" & oConn.Database & "].dbo.ArCustomer a Inner Join tblsmsOut b ")
        Sql.Append("On substring(rCell,3,10) COLLATE DATABASE_DEFAULT = ")
        Sql.Append("substring(b.oMobileNo,4,10) COLLATE DATABASE_DEFAULT ")
        Sql.Append("WHERE len(a.rCell)>3 AND CommandUsed='BAL1' ")
        If ChkFilter.Checked = True Then
            Sql.Append("AND a.Customer=@CustCode ")
        End If
        Sql.Append("AND CONVERT(nvarchar(30),oDateTimeIn,101)>=@DateFrom AND CONVERT(nvarchar(30),oDateTimeIn,101)<=@DateTo ")
        Sql.Append("ORDER BY b.oDateTimeIn Desc ")

        GetSQL = SQL.ToString

        Return GetSQL

    End Function

    Private Sub cmdGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGenerate.Click
        On Error GoTo xError
        cmdGenerate.Enabled = False

        Dim xs As Control, WithAsterisk As Boolean


        If ChkFilter.Checked = False Then
            ViewReport()
        Else
            If (InStr(txtCustomerCode.Text, "*") OrElse InStr(txtCustomerName.Text, "*")) Then
                SQL = New StringBuilder
                SQL.Append("SELECT a.Customer,a.Name,('+' + rCell)[rCell] FROM ArCustomer a WHERE ")

                For Each xs In Me.Controls
                    If TypeOf xs Is TextBox Then
                        If InStr(xs.Text, "*") > 0 Then
                            If xs.Name = txtCustomerCode.Name Then
                                SQL.Append("a.Customer Like '" & txtCustomerCode.Text.Trim.Replace("*", "%").Replace("'", "''") & "' COLLATE SQL_Latin1_General_CP1_CI_AS    AND ")
                            End If
                            If xs.Name = txtCustomerName.Name Then
                                SQL.Append("a.Name Like '" & txtCustomerName.Text.Replace("*", "%").Replace("'", "''") & "' COLLATE SQL_Latin1_General_CP1_CI_AS    AND ")
                            End If

                            WithAsterisk = True
                        End If
                    End If
                Next xs


                If WithAsterisk = True Then
                    SQL.Remove(SQL.Length - 6, 6)

                    Dim f As New frmDialog
                    f.DatabaseConnection = 1
                    f.strToPerform = SQL.ToString
                    f.ShowDialog(Me)

                    If f.Sel(0).ToString.Length <> 0 Then
                        txtCustomerCode.Text = f.Sel(0).ToString.Trim
                        txtCustomerName.Text = f.Sel(1).ToString.Trim
                    End If

                End If
            Else
                ViewReport()
            End If
        End If

        cmdGenerate.Enabled = True

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub ViewReport()
        Dim frm As New frmSOASummary
        frm.StrToPerform = GetSQL()
        frm.MdiParent = frmMain
        frm.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkFilter.CheckedChanged
        If ChkFilter.Checked = True Then
            lblCode.Visible = True
            lblName.Visible = True
            txtCustomerCode.Visible = True
            txtCustomerName.Visible = True
        Else
            lblCode.Visible = False
            lblName.Visible = False
            txtCustomerCode.Visible = False
            txtCustomerName.Visible = False
        End If
    End Sub

    Private Function CheckValues() As Boolean
        CheckValues = True

        If ChkFilter.Checked = True AndAlso txtCustomerCode.TextLength <= 0 Then
            MsgBox("Please provide Customer Code.", MsgBoxStyle.Exclamation)
            txtCustomerCode.Focus()
            Exit Function
        End If

        Return CheckValues
    End Function
End Class