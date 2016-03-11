Imports System.Text
Public Class frmDPSProcessing
    Dim Query As New CPerformQuery, dt As DataTable
    Dim SQL As StringBuilder
    Dim r, c As Int16
    Dim EWT As Double
    Dim GridRow As Int16
    Private HT As New Hashtable

    Private Sub frmDPSProcessing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery("SELECT Top 1 EWTPrcnt FROM OADM", oConn2)
        EWT = (dt.Rows(0).Item(0)) / 100

        DTFrom.Value = Today
        DTTo.Value = Today

        cmdFind.Text = "&Add"
        GroupBox1.Enabled = False
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Add" Then
            GroupBox1.Enabled = True
            DTFrom.Value = Today
            DTTo.Value = Today
            DataGridView1.Rows.Clear()
            cmdFind.Text = "&Find"
            cmdAdd.Enabled = True
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.ReadOnly = True
            Call Reset()
            cmdFind.Enabled = True
            DataGridView1.Focus()
            HT.Clear()
        Else
            Me.Close()
        End If
    End Sub

    Private Function CheckValues() As Boolean
        On Error GoTo xError
        CheckValues = True

        If DataGridView1.RowCount - 1 <= 0 Then
            MsgBox("No data to save.", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        r = 0 : c = 0
        Do While r < DataGridView1.Rows.Count - 1
            Do While c < DataGridView1.Columns.Count - 1

                If Not IsDate(DataGridView1.Item(1, r).Value) Then
                    MsgBox("Invalid date value.", MsgBoxStyle.Exclamation)
                    Exit Function
                End If

                If Not IsNumeric(DataGridView1.Item(4, r).Value) Then
                    MsgBox("Please enter numeric value only.", MsgBoxStyle.Exclamation)
                    DataGridView1.Item(3, r).Selected = True
                    Exit Function
                End If
                If Not IsNumeric(DataGridView1.Item(5, r).Value) Then
                    MsgBox("Please enter numeric value only.", MsgBoxStyle.Exclamation)
                    DataGridView1.Item(5, r).Selected = True
                    Exit Function
                End If

                c += 1
            Loop
            c = 0
            r += 1
        Loop

        CheckValues = False
        Return CheckValues

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError
        Dim TotalEWT As Double = 0, TotalInvoice As Double = 0
        cmdFind.Enabled = False

        If cmdFind.Text = "&Find" Then
            TSPrintPrevw.Enabled = False
            DataGridView1.Rows.Clear()
            Call Reset()
            Query = New CPerformQuery
            dt = Query.PerformSelectQuery(GetSQL.ToString, oConn2)
            r = 0 : c = 0

            Do While r < dt.Rows.Count
                With DataGridView1
                    .Rows.Add()
                    .Item(0, r).Value = dt.Rows(r).Item("DocNum")
                    .Item(1, r).Value = dt.Rows(r).Item("DocDate")
                    .Item(2, r).Value = dt.Rows(r).Item("DocTyp")
                    .Item("colInvText", r).Value = dt.Rows(r).Item("InvTxt")

                    .Item("colInvAmt", r).Value = Format(dt.Rows(r).Item("InvAmt"), "Standard")
                    .Item("colEWTAmt", r).Value = Format(dt.Rows(r).Item("EWTAmt"), "standard")

                    .Item("colDeductTyp", r).Value = dt.Rows(r).Item("DeductTyp").ToString
                    .Item("colDeductTxt", r).Value = dt.Rows(r).Item("DeductTxt").ToString
                    .Item("colRefNo", r).Value = dt.Rows(r).Item("RefNo")

                    TotalInvoice += dt.Rows(r).Item("InvAmt")
                    TotalEWT += dt.Rows(r).Item("EWTAmt")

                End With

                c = 0
                r += 1
            Loop

            If DataGridView1.RowCount > 0 Then
                txtInvoice.Text = Format(TotalInvoice, "standard")
                txtEWT.Text = Format(TotalEWT, "standard")
                txtNetTotal.Text = Format(CDbl(txtInvoice.Text) - CDbl(txtEWT.Text), "standard")
                TSPrintPrevw.Enabled = True
            Else
                TSPrintPrevw.Enabled = False
                MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
            End If

        ElseIf cmdFind.Text = "&Add" Then
            If CheckValues() = False Then
                If MsgBox("Are you sure you want to add this document?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    cmdFind.Enabled = True
                    Exit Sub
                End If

                Dim RowCountTotal As Int16 = DataGridView1.RowCount - 1, RowCounter As Int16 = 0
                Dim vDocNum As String
                r = 0

                SQL = New StringBuilder
                SQL.Append("DECLARE @intErrorCode as int ")
                SQL.Append("BEGIN TRAN ")

                Do While r < DataGridView1.Rows.Count - 1
                    With DataGridView1
                        vDocNum = modConvertNull(.Item(0, r).Value)

                        SQL.Append("INSERT INTO ODPS(DocNum,DocDate,DocTyp,InvTxt,InvAmt,EWTAmt,DeductTyp,DeductTxt,CreateDate,CreatedBy) ")
                        SQL.Append("VALUES('" & vDocNum & "','" & .Item(1, r).Value & "','" & modConvertNull(.Item(2, r).Value) & "','" & modConvertNull(.Item(3, r).Value) & "'," & CDbl(.Item(4, r).Value) & "," & CDbl(.Item(5, r).Value) & ",'" & modConvertNull(.Item(6, r).Value) & "','" & modConvertNull(.Item(7, r).Value) & "',GetDate(),'" & UserName & "')  ")
                        SQL.Append("SELECT @intErrorCode = @@ERROR ")
                        SQL.Append("IF (@intErrorCode<>0) GOTO PROBLEM ")
                    End With

                    r += 1
                Loop
                SQL.Append("COMMIT TRAN ")
                SQL.Append("PROBLEM: ")
                SQL.Append("IF (@intErrorCode<>0) ")
                SQL.Append("    BEGIN ")
                SQL.Append("        ROLLBACK TRAN ")
                SQL.Append("    END")

                Query = New CPerformQuery
                If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                    MsgBox("Saving has been successful.", MsgBoxStyle.Information)
                    GroupBox1.Enabled = True
                    DTFrom.Value = Today
                    DTTo.Value = Today
                    DataGridView1.Rows.Clear()
                    cmdFind.Text = "&Find"
                    cmdAdd.Enabled = True
                    DataGridView1.AllowUserToAddRows = False
                    DataGridView1.ReadOnly = True
                    Call Reset()
                    cmdFind.Enabled = True
                    DataGridView1.Focus()
                    HT.Clear()
                End If

            End If
        ElseIf cmdFind.Text = "&Save" Then
            If MsgBox("Are you sure you want to save?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                SQL = New StringBuilder
                SQL.Append("DECLARE @intErrorCode as int ")
                SQL.Append("BEGIN TRAN ")
                For Each DE As DictionaryEntry In HT
                    SQL.Append("DELETE ODPS WHERE RefNo='" & DE.Key & "'")
                    SQL.Append("SET @intErrorCode=@@ERROR ")
                    SQL.Append("IF (@intErrorCode<>0) GOTO PROBLEM ")
                Next
                SQL.Append("COMMIT TRAN ")
                SQL.Append("PROBLEM: ")
                SQL.Append("IF (@intErrorCode<>0) ")
                SQL.Append("    BEGIN ")
                SQL.Append("        ROLLBACK TRAN ")
                SQL.Append("    END ")

                Query = New CPerformQuery

                If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                    MsgBox("Successfully deleted item(s).", MsgBoxStyle.Information)
                    HT.Clear()
                    cmdFind.Text = "&Find"
                    cmdFind.Enabled = True
                Else

                End If

            End If
        End If

        cmdFind.Enabled = True
        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
        cmdFind.Enabled = True
    End Sub

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If cmdFind.Text = "&Add" Then
            Call ComputeNetPay(e)
            Call Compute()
        End If
    End Sub

    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        If cmdFind.Text = "&Add" Then
            Call ComputeNetPay(e)
        End If
    End Sub

    Private Sub ComputeNetPay(ByVal f As System.Windows.Forms.DataGridViewCellEventArgs)
        On Error GoTo xError
        If f.ColumnIndex = 4 Or f.ColumnIndex = 5 Then
            If Trim(DataGridView1.Item(4, f.RowIndex).Value) = "" Then
                DataGridView1.Item(4, f.RowIndex).Value = "0.00"
            End If
            If Trim(DataGridView1.Item(5, f.RowIndex).Value) = "" Then
                DataGridView1.Item(5, f.RowIndex).Value = "0.00"
            End If

            DataGridView1.Item(4, f.RowIndex).Value = Format(DataGridView1.Item(4, f.RowIndex).Value, "standard")
            DataGridView1.Item(5, f.RowIndex).Value = Format(DataGridView1.Item(5, f.RowIndex).Value, "standard")
        End If

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub DataGridView1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DataGridView1.RowsAdded
        Call Reset()
        With DataGridView1
            .Item(1, e.RowIndex).Value = Today
            .Item(4, e.RowIndex).Value = "0.00"
            .Item(5, e.RowIndex).Value = "0.00"
        End With

    End Sub

    Private Sub Compute()
        On Error GoTo xError
        Call Reset()
        r = 0
        txtInvoice.Text = "0.00"
        txtEWT.Text = "0.00"
        txtNetTotal.Text = "0.00"
        Do While r < DataGridView1.RowCount - 1
            txtInvoice.Text = CDbl(txtInvoice.Text) + DataGridView1.Item(4, r).Value
            txtEWT.Text = CDbl(txtEWT.Text) + DataGridView1.Item(5, r).Value
            r += 1
        Loop

        txtInvoice.Text = Format(txtInvoice.Text, "standard")
        txtEWT.Text = Format(txtEWT.Text, "standard")
        txtNetTotal.Text = Format(CDbl(txtInvoice.Text) - CDbl(txtEWT.Text), "standard")

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub Reset()
        txtInvoice.Text = "0.00"
        txtEWT.Text = "0.00"
        txtNetTotal.Text = "0.00"
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        DataGridView1.AllowUserToAddRows = True
        DataGridView1.AllowUserToDeleteRows = True
        cmdAdd.Enabled = False
        TSPrintPrevw.Enabled = False
        cmdFind.Text = "&Add"

        GroupBox1.Enabled = False

        DataGridView1.ReadOnly = False
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Focus()

    End Sub

    Private Function GetSQL() As String
        SQL = New StringBuilder
        SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime ")
        SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
        SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
        SQL.Append("SET @DateFrom=CONVERT(nvarchar,@DateFrom,101) ")
        SQL.Append("SET @DateTo=CONVERT(nvarchar,@DateTo,101) ")
        SQL.Append("SELECT * FROM ODPS WHERE CONVERT(nvarchar,DocDate,101)>=@DateFrom AND CONVERT(nvarchar,DocDate,101)<=@DateTo AND (Void=0 OR Void IS NULL) ")
        SQL.Append("ORDER BY RefNo ")

        Return SQL.ToString
    End Function

    Private Sub ContxtMnuEWT_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContxtMnuEWT.Opening
        If cmdFind.Text = "&Add" Then
            ContxtComputeEWT.Enabled = True
        Else
            ContxtComputeEWT.Enabled = False
        End If
    End Sub

    Private Sub ContxtComputeEWT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ContxtComputeEWT.Click
        GridRow = DataGridView1.CurrentCellAddress.Y

        DataGridView1.Item(5, GridRow).Value = Format((CDbl(DataGridView1.Item(4, GridRow).Value) / 1.12) * EWT, "standard")
        txtEWT.Text = Format(CDbl(txtEWT.Text) + CDbl(DataGridView1.Item(5, GridRow).Value), "standard")

    End Sub

    Private Sub DeleteRowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteRowToolStripMenuItem.Click
        If cmdFind.Text = "&Find" Or cmdFind.Text = "&Save" Then
            Dim HTKey As String, HTValue As String
            Try
                GridRow = DataGridView1.CurrentCellAddress.Y
                HTKey = DataGridView1.Item("colRefNo", GridRow).Value
                HTValue = DataGridView1.Item(4, GridRow).Value

                HT.Add(HTKey, HTValue)

                DataGridView1.Rows.RemoveAt(GridRow)

                cmdFind.Text = "&Save"
                TSPrintPrevw.Enabled = False
            Catch ex As Exception
                Dim WriteLog As New CLogFile
                WriteLog.LogWrite("Error: " & sender.ToString & ": " & ex.Message & vbTab & Err.Number & Err.Description)
                MsgBox(Err.Number & Chr(13) & Err.Description, MsgBoxStyle.Critical)
            End Try

            Me.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub TSPrintPrevw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSPrintPrevw.Click
        On Error GoTo xError

        TSPrintPrevw.Enabled = False

        Me.Cursor = Cursors.WaitCursor

        Dim objExcel As New CExcelWriter
        Dim oFileName As String, iRow As Int32 = 9, RecCount As Int32
        r = 0

        oFileName = Application.StartupPath & "\Templates\DPS.xls"

        objExcel.OpenWorkSheet(oFileName)
        objExcel.CreateNewSheet("OR FORMAT", "DPS")

        dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
        RecCount = dt.Rows.Count

        Do While r < RecCount
            objExcel.WriteToSheet("DPS", iRow, 1, dt.Rows(r).Item("DocNum").ToString, "S")
            objExcel.WriteToSheet("DPS", iRow, 2, Format(dt.Rows(r).Item("DocDate"), "MM/dd/yyyy"), "S")
            objExcel.WriteToSheet("DPS", iRow, 3, dt.Rows(r).Item("DocTyp").ToString, "S")
            objExcel.WriteToSheet("DPS", iRow, 4, dt.Rows(r).Item("InvAmt").ToString, "N")
            objExcel.WriteToSheet("DPS", iRow, 5, dt.Rows(r).Item("InvTxt").ToString, "S")
            r += 1
            iRow = iRow + 1
        Loop

        If r > 0 Then
            Application.DoEvents()
            objExcel.WriteToSheet("DPS", iRow, 1, "SUBTOTAL", "S")
            objExcel.WriteToSheet("DPS", iRow, 4, txtInvoice.Text, "N")

            objExcel.WriteToSheet("DPS", iRow + 2, 1, "DEDUCTIONS: (COPY DETAILS FROM WEEKLY CLI SENT BY Mr RESTIE SANTOS)", "S")

            objExcel.WriteToSheet("DPS", iRow + 3, 1, "Doc number", "S")
            objExcel.WriteToSheet("DPS", iRow + 3, 2, "Doc Date", "S")
            objExcel.WriteToSheet("DPS", iRow + 3, 3, "Doc Type", "S")
            objExcel.WriteToSheet("DPS", iRow + 3, 4, "Amount", "S")
            objExcel.WriteToSheet("DPS", iRow + 3, 5, "Text", "S")

            SQL = New StringBuilder
            SQL.Append("Declare @DateFrom as DateTime,@DateTo as DateTime ")
            SQL.Append("SET @DateFrom='" & DTFrom.Value & "' ")
            SQL.Append("SET @DateTo='" & DTTo.Value & "' ")
            SQL.Append("SET @DateFrom=CONVERT(nvarchar,@DateFrom,101) ")
            SQL.Append("SET @DateTo=CONVERT(nvarchar,@DateTo,101) ")
            SQL.Append("SELECT DocNum,DocDate,DocTyp,EWTAmt,DeductTxt FROM ODPS WHERE CONVERT(nvarchar,DocDate,101)>=@DateFrom AND CONVERT(nvarchar,DocDate,101)<=@DateTo AND EWTAmt<>0")
            SQL.Append("ORDER BY RefNo ")

            Query = New CPerformQuery
            dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
            RecCount = dt.Rows.Count

            r = 0 : iRow += 4
            Do While r < RecCount
                objExcel.WriteToSheet("DPS", iRow, 1, dt.Rows(r).Item("DocNum").ToString, "S")
                objExcel.WriteToSheet("DPS", iRow, 2, Format(dt.Rows(r).Item("DocDate"), "MM/dd/yyyy"), "S")
                objExcel.WriteToSheet("DPS", iRow, 3, dt.Rows(r).Item("DocTyp").ToString, "S")
                objExcel.WriteToSheet("DPS", iRow, 4, dt.Rows(r).Item("EWTAmt").ToString, "N")
                objExcel.WriteToSheet("DPS", iRow, 5, dt.Rows(r).Item("DeductTxt").ToString, "S")
                r += 1
                iRow = iRow + 1
            Loop

            objExcel.WriteToSheet("DPS", iRow, 1, "SUBTOTAL", "S")
            objExcel.WriteToSheet("DPS", iRow, 4, txtEWT.Text, "N")

            objExcel.WriteToSheet("DPS", iRow + 2, 1, "OR NET AMOUNT", "S")
            objExcel.WriteToSheet("DPS", iRow + 2, 4, Format(txtNetTotal.Text, "standard"), "S")


            objExcel.HideSheet("OR FORMAT")
            oFileName = Application.StartupPath & "\Output Reports\DPS" & CStr(Format(Now, "MMddyyhhmmss")) & ".xls"
            objExcel.CloseWorkSheet(oFileName)
            objExcel.OpenWorkSheetTest(oFileName)
        End If

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

End Class