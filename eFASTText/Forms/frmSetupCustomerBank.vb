Imports System.Text

Public Class frmSetupCustomerBank
    Dim Query As CPerformQuery
    'Dim r As Integer
    Private SQL As StringBuilder
    Private rCount As Integer

    Private Sub frmSetupCustomerBank_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        Query = New CPerformQuery
        SQL = New StringBuilder
        SQL.Append("SELECT a.Customer COLLATE SQL_Latin1_General_CP1_CI_AS as [BPCode],a.Name[BPName],(CASE WHEN b.BankCode IS NULL THEN '' ELSE b.BankCode END)[Bank Code] ")
        SQL.Append("FROM [" & oConn.Database & "].dbo.ArCustomer a Left outer Join [" & oConn2.Database & "].dbo.BP_Bank b ON a.Customer=b.BPCode COLLATE SQL_Latin1_General_CP1_CI_AS ")
        SQL.Append("WHERE a.CustomerOnHold='N' ")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT b.BPCode COLLATE SQL_Latin1_General_CP1_CI_AS,''[BPName],(CASE WHEN b.BankCode IS NULL THEN '' ELSE b.BankCode END) ")
        SQL.Append("FROM [" & oConn2.Database & "].dbo.BP_Bank b ")
        SQL.Append("WHERE b.BPCode Like 'C%' ")

        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)

        r = 0
        rCount = dt.Rows.Count

        Do While r < rCount
            With DataGridView1
                .Rows.Add()
                .Item(0, r).Value = dt.Rows(r).Item("BPCode")
                .Item(1, r).Value = dt.Rows(r).Item("BPName")
                .Item(2, r).Value = dt.Rows(r).Item("Bank Code")

                .Item(0, r).ReadOnly = True
                .Item(1, r).ReadOnly = True
            End With

            r += 1
        Loop

        Query = New CPerformQuery
        SQL = New StringBuilder
        SQL.Append("SELECT ''[Bank Code]")
        SQL.Append("UNION ALL ")
        SQL.Append("SELECT BankCode FROM Bank ")
        colBank.DataSource = Query.PerformSelectQuery(SQL.ToString, oConn2)
        colBank.DisplayMember = "Bank Code"

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        On Error GoTo xError
        If MessageBox.Show("Are you sure you want to save now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Dim vBankCode, vBPCode As String, WithValueToSave As Boolean
        WithValueToSave = False

        r = 0
        rCount = DataGridView1.RowCount - 1
        SQL = New StringBuilder
        SQL.Append("DECLARE @intErrorCode as int ")
        SQL.Append("BEGIN TRAN ")
        SQL.Append("    DELETE BP_Bank ")
        Do While r < rCount
            vBankCode = modConvertNull(DataGridView1.Item(2, r).Value).Replace("'", "''")
            vBPCode = modConvertNull(DataGridView1.Item(0, r).Value).Replace("'", "''")

            If vBankCode <> "" Then
                WithValueToSave = True
                SQL.Append("    INSERT INTO BP_Bank(BPCode,BankCode) VALUES('" & vBPCode & "','" & vBankCode & "') ")
                SQL.Append("    SET @intErrorCode=@@ERROR ")
                SQL.Append("    IF (@intErrorCode<>0) GOTO PROBLEM ")
            End If
            r += 1
        Loop
        SQL.Append("COMMIT TRAN ")
        SQL.Append("PROBLEM: ")
        SQL.Append("IF (@intErrorCode<>0) ")
        SQL.Append("    BEGIN ")
        SQL.Append("        ROLLBACK TRAN ")
        SQL.Append("    END ")

        Query = New CPerformQuery
        If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
            MsgBox("Saving has been successful!", MsgBoxStyle.Information)
            Me.Close()
        Else
            If WithValueToSave = True Then
                MsgBox("Saving was unsuccessful!", MsgBoxStyle.Critical)
            Else
                MsgBox("No data to save.", MsgBoxStyle.Critical)
            End If

        End If

        Me.Cursor = Cursors.Default
        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If MessageBox.Show("Are you sure you want to exit without saving?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class