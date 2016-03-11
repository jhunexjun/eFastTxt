Imports System.Text
Imports System.Windows.Forms.ListViewItem
Public Class frmStocksRequisitionSlip
    Private Query As CPerformQuery
    Dim r As Integer, rCount As Integer, SQL As StringBuilder
    Dim xs As Control, v As String
    Dim vStockCode As String, vItemDesc, v_LoadUoM As String

    Private Sub frmStocksRequisitionSlip_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmMain.ToolStrip1.Enabled = False
    End Sub

    Private Function GetSQL() As String
        Dim str As String

        str = "SELECT b.Code,b.Dscription,b.Suggstd,b.Inventory,b.ActualLoad,(CASE WHEN b.LoadUoM IS NULL THEN 'CS' ELSE b.LoadUoM END)[LoadUoM] FROM StockReqLine b WHERE b.DocNum='" & txtDocNum.Text & "' ORDER BY b.LineNum "

        Return str
    End Function

    Private Sub cmdFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFind.Click
        On Error GoTo xError
        Me.Cursor = Cursors.WaitCursor

        If cmdFind.Text = "&Find" Then
            Call Reset()

            If txtDocNum.TextLength <= 0 Then
                Me.Cursor = Cursors.Default
                MsgBox("Please enter document number.", MsgBoxStyle.Critical)
                txtDocNum.Focus()
                Exit Sub
            End If

            SQL = New StringBuilder
            SQL.Append("SELECT DocNum,Branch,SellDays,Provision,WHouse FROM StockReqHeader WHERE DocNum ")

            If txtDocNum.Text.Contains("*") = True Then
                SQL.Append("LIKE '%" & txtDocNum.Text.Replace("*", "%") & "%'")

                Dim f As New frmDialog
                f.DatabaseConnection = 3
                f.strToPerform = SQL.ToString
                f.ShowDialog(Me)

                If f.Sel(0).ToString.Length <> 0 Then
                    txtDocNum.Text = f.Sel(0).ToString
                    cmdFind_Click(sender, e)
                End If

            Else
                SQL.Append("='" & txtDocNum.Text & "' ")

                Query = New CPerformQuery
                dt = Query.PerformSelectQuery(SQL.ToString, oConn2)
                If dt.Rows.Count >= 1 Then
                    txtBranch.Text = Trim(dt.Rows(0).Item("Branch"))
                    txtSellingDays.Text = dt.Rows(0).Item("SellDays")
                    txtProvision.Text = dt.Rows(0).Item("Provision")
                    cboWhouse.Text = dt.Rows(0).Item("WHouse")

                    Query = New CPerformQuery
                    dt = Query.PerformSelectQuery(GetSQL, oConn2)

                    rCount = dt.Rows.Count
                    r = 0
                    Do While r < rCount
                        DataGridView1.Rows.Add()
                        With DataGridView1
                            .Item(0, r).Value = r + 1
                            .Item(1, r).Value = dt.Rows(r).Item("Code")
                            .Item(2, r).Value = dt.Rows(r).Item("Dscription")
                            .Item(3, r).Value = dt.Rows(r).Item("Suggstd")
                            .Item(4, r).Value = dt.Rows(r).Item("Inventory")
                            .Item(5, r).Value = dt.Rows(r).Item("ActualLoad")
                            .Item(6, r).Value = dt.Rows(r).Item("LoadUoM")

                            .Item(0, Me.r).ReadOnly = True
                            .Item(1, Me.r).ReadOnly = True
                            .Item(2, Me.r).ReadOnly = True
                            .Item(3, Me.r).ReadOnly = True
                            .Item(4, Me.r).ReadOnly = True
                            .Item(5, Me.r).ReadOnly = True
                            .Item(6, Me.r).ReadOnly = True

                            .Item(0, Me.r).Style.BackColor = Color.LightGray
                            .Item(1, Me.r).Style.BackColor = Color.LightGray
                            .Item(2, Me.r).Style.BackColor = Color.LightGray
                            .Item(3, Me.r).Style.BackColor = Color.LightGray
                            .Item(4, Me.r).Style.BackColor = Color.LightGray
                            .Item(5, Me.r).Style.BackColor = Color.LightGray
                            .Item(6, Me.r).Style.BackColor = Color.LightGray
                        End With

                        r += 1
                    Loop

                    cmdFind.Text = "&Ok"
                    cmdPrint.Enabled = True
                    TSPrintPrevw.Enabled = True
                    txtDocNum.ReadOnly = True
                Else
                    MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)
                    txtDocNum.Focus()
                End If

            End If

            Me.Cursor = Cursors.Default

        ElseIf cmdFind.Text = "&Save" Then
            If CheckValues() = False AndAlso AllZeros() = False Then
                If MessageBox.Show("Are you sure you want to save now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                    SQL = New StringBuilder
                    SQL.Append("DECLARE @intErrorCode as int,@StockReqNum as nvarchar(10) ")
                    SQL.Append("BEGIN TRAN ")
                    SQL.Append("    SET @StockReqNum=(SELECT '" & cboWhouse.Text & "' + '-' + (CASE WHEN CAST(MAX(CAST(SUBSTRING(DocNum,CHARINDEX('-',DocNum)+1,LEN(DocNum)) as numeric)) as nvarchar) is NULL then '1' ELSE CAST(MAX(CAST(SUBSTRING(DocNum,CHARINDEX('-',DocNum)+1,LEN(DocNum)) as numeric))+1 as nvarchar) END) FROM StockReqHeader WHERE WHouse Like substring('" & cboWhouse.Text & "',0,CHARINDEX('-',DocNum)) + '%') ")
                    SQL.Append("    SELECT @intErrorCode = @@ERROR ")
                    SQL.Append("    IF (@intErrorCode <> 0) GOTO PROBLEM ")
                    SQL.Append("    INSERT INTO StockReqHeader(DocNum,Branch,SellDays,Provision,WHouse) VALUES(@StockReqNum,'" & txtBranch.Text.Trim.Replace("'", "''") & "'," & Int(txtSellingDays.Text) & "," & Int(txtProvision.Text) & ",'" & cboWhouse.Text & "') ")
                    SQL.Append("    SELECT @intErrorCode = @@ERROR ")
                    SQL.Append("    IF (@intErrorCode <> 0) GOTO PROBLEM ")
                    r = 0
                    rCount = DataGridView1.Rows.Count - 1

                    Dim vSuggstd, vInventory As Integer

                    Do While r < rCount
                        v = Trim(DataGridView1.Item(5, r).Value)

                        vSuggstd = CDbl(DataGridView1.Item(3, r).Value)
                        vInventory = CDbl(DataGridView1.Item(4, r).Value)

                        SetDefaultValue()

                        If IsNumeric(v) AndAlso v <> 0 Then
                            SQL.Append("INSERT INTO StockReqLine(DocNum,LineNum,Code,Dscription,Suggstd,Inventory,ActualLoad,LoadUoM) ")
                            SQL.Append("VALUES(@StockReqNum," & r + 1 & ",'" & DataGridView1.Item(1, r).Value & "','" & DataGridView1.Item(2, r).Value & "'," & vSuggstd & "," & vInventory & "," & v & ",'" & v_LoadUoM & "') ")
                            SQL.Append("SELECT @intErrorCode = @@ERROR ")
                            SQL.Append("IF (@intErrorCode <> 0) GOTO PROBLEM ")
                        End If

                        r += 1
                    Loop
                    SQL.Append("        SELECT @intErrorCode = @@ERROR ")
                    SQL.Append("        IF (@intErrorCode <> 0) GOTO PROBLEM ")
                    SQL.Append("COMMIT TRAN ")
                    SQL.Append("PROBLEM: ")
                    SQL.Append("IF (@intErrorCode<>0) ")
                    SQL.Append("    BEGIN ")
                    SQL.Append("        ROLLBACK TRAN ")
                    SQL.Append("    END ")

                    Query = New CPerformQuery
                    If Query.PerformUpdateQuery(SQL.ToString, oConn2) > 0 Then
                        MsgBox("Saving was successful!", MsgBoxStyle.Information)
                        Reset()
                        DataGridView1.Rows.Clear()
                        cmdFind.Text = "&Find"
                    Else
                        MsgBox("Saving was unsuccessful!", MsgBoxStyle.Critical)
                    End If
                End If
            End If
        ElseIf cmdFind.Text = "&Ok" Then
            Me.Close()
        ElseIf cmdFind.Text = "&Update" Then
            If CheckValues() = False AndAlso AllZeros() = False Then
                If MessageBox.Show("Are you sure you want to update now?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    SQL = New StringBuilder
                    SQL.Append("DECLARE @intErrorCode int,@StockReqNum as nvarchar(10),@maxLineNum int ")
                    SQL.Append("BEGIN TRAN ")
                    SQL.Append("    SET @maxLineNum=(SELECT max(LineNum) FROM StockReqLine WHERE DocNum='" & txtDocNum.Text.Trim & "') ")
                    SQL.Append("    SELECT @intErrorCode = @@ERROR ")
                    SQL.Append("    IF (@intErrorCode <> 0) GOTO PROBLEM ")

                    r = 0
                    rCount = DataGridView1.Rows.Count - 1

                    Dim vSuggstd, vInventory As Integer, vLineNum As String

                    Do While r < rCount
                        vLineNum = Trim(DataGridView1.Item(0, r).Value) 'check last number
                        vSuggstd = CDbl(DataGridView1.Item(3, r).Value)
                        vInventory = CDbl(DataGridView1.Item(4, r).Value)
                        v = Trim(DataGridView1.Item(5, r).Value)

                        SetDefaultValue()

                        If IsNumeric(v) AndAlso v <> 0 And vLineNum.Length <= 0 Then
                            SQL.Append("INSERT INTO StockReqLine(DocNum,LineNum,Code,Dscription,Suggstd,Inventory,ActualLoad,LoadUoM) ")
                            SQL.Append("VALUES('" & txtDocNum.Text & "',@maxLineNum+1,'" & DataGridView1.Item(1, r).Value & "','" & DataGridView1.Item(2, r).Value & "'," & vSuggstd & "," & vInventory & "," & v & ",'" & v_LoadUoM & "') ")
                            SQL.Append("SELECT @intErrorCode = @@ERROR ")
                            SQL.Append("IF (@intErrorCode <> 0) GOTO PROBLEM ")
                            SQL.Append("SET @maxLineNum=@maxLineNum+1 ")
                            SQL.Append("SELECT @intErrorCode = @@ERROR ")
                            SQL.Append("IF (@intErrorCode <> 0) GOTO PROBLEM ")
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
                        MsgBox("Updating has been successful!", MsgBoxStyle.Information)
                        cmdFind.Text = "&Ok"
                    Else
                        MsgBox("Updating was not successful!", MsgBoxStyle.Critical)
                    End If

                End If
            End If
        End If

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Private Sub SetDefaultValue()
        v_LoadUoM = Trim(DataGridView1.Item(6, r).Value)
        If v_LoadUoM Is Nothing Then
            v_LoadUoM = "CS"
        ElseIf v_LoadUoM.Length <= 0 Then
            v_LoadUoM = "CS"
        End If
    End Sub

    Private Sub Reset()
        txtBranch.Text = ""
        txtProvision.Text = ""
        txtSellingDays.Text = ""
        cboWhouse.SelectedIndex = -1
        cmdPrint.Enabled = False
        TSPrintPrevw.Enabled = False
        TSSave.Enabled = False
    End Sub

    Private Function AllZeros() As Boolean
        AllZeros = True   'True = items are all zeros

        r = 0
        rCount = DataGridView1.Rows.Count - 1
        Do While r < rCount
            v = Trim(DataGridView1.Item(5, r).Value)
            If v = "" Then
                v = 0
            End If

            If CDbl(v) <> CDbl(0) Then
                AllZeros = False
                Exit Do
            End If

            r += 1
        Loop

        If AllZeros = True Then
            MsgBox("No data to save.", MsgBoxStyle.Exclamation)
        End If

        Return AllZeros
    End Function

    Private Function CheckValues() As Boolean
        On Error GoTo xError
        CheckValues = True

        If txtBranch.TextLength <= 0 Then
            MessageBox.Show("Please indicate branch name.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtBranch.Focus()
            Exit Function
        End If

        If txtSellingDays.TextLength <= 0 Then
            MessageBox.Show("Please indicate number of days.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSellingDays.Focus()
            Exit Function
        End If

        If Not IsNumeric(txtSellingDays.Text) Then
            MessageBox.Show("Invalid number value.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtSellingDays.Focus()
            Exit Function
        End If
        If Not IsNumeric(txtProvision.Text) Then
            MessageBox.Show("Please indicate provision in percent.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtProvision.Focus()
            Exit Function
        End If
        If cboWhouse.SelectedIndex = -1 Then
            MessageBox.Show("Please choose warehouse.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboWhouse.Focus()
            Exit Function
        End If

        Dim HT As New Hashtable

        r = 0
        rCount = DataGridView1.RowCount - 1
        Do While r < rCount
            v = Trim(DataGridView1.Item(5, r).Value)
            If v = "" Then
                v = 0
            End If

            If Not IsNumeric(v) Then
                MessageBox.Show("Invalid number value in item(Column=6,Row=" & r + 1 & ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DataGridView1.Item(5, r).Selected = True
                Exit Function
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            v = Trim(DataGridView1.Item(1, r).Value)
            If v.Length <= 0 Then
                MessageBox.Show("Invalid Stock Code in item(Column=2,Row=" & r + 1 & ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DataGridView1.Item(1, r).Selected = True
                Exit Function
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            v = Trim(DataGridView1.Item(3, r).Value)
            If v = "" Then
                v = 0
            End If

            If Not IsNumeric(v) Then
                MessageBox.Show("Invalid number value in item(Column=4,Row=" & r + 1 & ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DataGridView1.Item(3, r).Selected = True
                Exit Function
            End If

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            v = Trim(DataGridView1.Item(4, r).Value)
            If v = "" Then
                v = 0
            End If

            If Not IsNumeric(v) Then
                MessageBox.Show("Invalid number value in item(Column=5,Row=" & r + 1 & ")", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DataGridView1.Item(4, r).Selected = True
                Exit Function
            End If
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            vStockCode = Trim(DataGridView1.Item(1, r).Value)
            vItemDesc = Trim(DataGridView1.Item(2, r).Value)

            HT.Add(vStockCode, vItemDesc)
            r += 1
        Loop

        CheckValues = False
        Return CheckValues

        Exit Function
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Function

    Private Sub txtBranch_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBranch.LostFocus
        txtBranch.Text = txtBranch.Text.ToUpper()
    End Sub

    Private Sub frmStocksRequisitionSlip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        On Error GoTo xError

        SQL = New StringBuilder
        SQL.Append("Select Warehouse from FDCWarehouse /*Where Warehouse Like 'X%'*/ Group by Warehouse Order by Warehouse")
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn)
        r = 0
        rCount = dt.Rows.Count
        Do While r < rCount
            cboWhouse.Items.Add(dt.Rows(r).Item("Warehouse").ToString)
            r += 1
        Loop

        txtDocNum.Focus()

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)

    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        If cmdFind.Text = "&Find" Then
            Me.Close()
        ElseIf cmdFind.Text = "&Save" Then
            If MessageBox.Show("Are you sure you want to cancel saving? This will delete inputed values.", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Reset()
                cmdFind.Text = "&Find"
                DataGridView1.Rows.Clear()
                txtDocNum.Clear()
                txtDocNum.Focus()
                txtDocNum.ReadOnly = False
            End If
        ElseIf cmdFind.Text = "&Ok" Then
            Reset()
            txtDocNum.Focus()
            txtDocNum.Clear()
            cmdFind.Text = "&Find"
            DataGridView1.Rows.Clear()
            txtDocNum.ReadOnly = False
        Else
            Me.Close()
        End If
    End Sub

    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click
        On Error GoTo xError

        Me.Cursor = Cursors.WaitCursor
        Dim Report As New crStockReqSlip

        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(GetSQL, oConn2)

        With Report
            .SetDataSource(dt)
            .SetParameterValue(0, txtBranch.Text)
            .SetParameterValue(1, cboWhouse.Text)
            .SetParameterValue(2, txtSellingDays.Text)
            .SetParameterValue(3, txtProvision.Text)
            .SetParameterValue(4, txtDocNum.Text)
            .PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            .PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
            '.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
        End With

        '''''''''''''''''''''''''''before (in order to effect this, adjust the paper settings also in GUI of Crystal Report)
        'Dim i As Integer
        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
        'doctoprint.PrinterSettings.PrinterName = "\\RD2\Epson LX-300+"
        'doctoprint.PrinterSettings.PrinterName = DefaultPrinterName()  '"\\fdcit\BrotherH"
        'Dim rawKind As Integer
        'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
        ''If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "10x10" Then
        'If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "8.5 x 5.5 inc" Then
        'rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
        'Exit For
        'End If
        'Next
        'Report.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
        '''''''''''''''''''''''''''

        frmCrystalViewer.CrystalReportViewer1.ReportSource = Report
        frmCrystalViewer.MdiParent = frmMain
        frmCrystalViewer.Text = "Stock Requisition"
        frmCrystalViewer.Show()

        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub

    Public Shared Function DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings

        Try
            DefaultPrinterName = oPS.PrinterName
        Catch ex As System.Exception
            DefaultPrinterName = ""
        Finally
            oPS = Nothing
        End Try
    End Function

    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If cmdFind.Text = "&Ok" Then
            cmdFind.Text = "&Update"
        End If

        On Error GoTo xError

        Me.Cursor = Cursors.WaitCursor
        vStockCode = Trim(modConvertNull(DataGridView1.Item(1, e.RowIndex).Value))
        vItemDesc = Trim(modConvertNull(DataGridView1.Item(2, e.RowIndex).Value))

        If vStockCode.Length <= 0 AndAlso vItemDesc.Length <= 0 Then
            Me.Cursor = Cursors.Default
            Exit Sub
        End If

        vStockCode = vStockCode.Replace("'", "''")
        vStockCode = vStockCode.Replace("*", "%")

        vItemDesc = vItemDesc.Replace("'", "''")
        vItemDesc = vItemDesc.Replace("*", "%")

        SQL = New StringBuilder
        SQL.Append("SELECT LTRIM(RTRIM(StockCode))[Stock Code],LTRIM(RTRIM(Description))[Description],StockUom[UOM],ConvFactAltUom[Conversion] from InvMaster WHERE ")

        If e.ColumnIndex = 1 AndAlso DataGridView1.Item(1, e.RowIndex).ReadOnly = False Then
            SQL.Append("StockCode Like '" & vStockCode & "'")
            GoTo RetrieveData
        ElseIf e.ColumnIndex = 2 AndAlso DataGridView1.Item(2, e.RowIndex).ReadOnly = False Then
            SQL.Append("Description Like '" & vItemDesc & "'")
            GoTo RetrieveData
        End If

        Me.Cursor = Cursors.Default
        Exit Sub
RetrieveData:
        Query = New CPerformQuery
        dt = Query.PerformSelectQuery(SQL.ToString, oConn)
        rCount = dt.Rows.Count

        If rCount > 1 Then
            Dim frm As New frmDialog
            frm.DatabaseConnection = 1
            frm.strToPerform = SQL.ToString
            frm.ShowDialog(Me)

            If frm.Sel(0).ToString.Length <> 0 Then
                DataGridView1.Item(1, e.RowIndex).Value = frm.Sel(0).ToString
                DataGridView1.Item(2, e.RowIndex).Value = frm.Sel(1).ToString
                DataGridView1.Item(6, e.RowIndex).Value = "CS"
            End If

        ElseIf rCount <= 0 Then
            MsgBox("No matching record(s) found.", MsgBoxStyle.Critical)

            DataGridView1.Item(0, e.RowIndex).Value = ""
            DataGridView1.Item(1, e.RowIndex).Value = ""
            DataGridView1.Item(2, e.RowIndex).Value = ""
            DataGridView1.Item(3, e.RowIndex).Value = "0"
            DataGridView1.Item(4, e.RowIndex).Value = "0"
            DataGridView1.Item(5, e.RowIndex).Value = ""
            DataGridView1.Item(6, e.RowIndex).Value = "CS"

        ElseIf rCount = 1 Then
            DataGridView1.Item(1, e.RowIndex).Value = dt.Rows(0).Item(0)
            DataGridView1.Item(2, e.RowIndex).Value = dt.Rows(0).Item(1)
            DataGridView1.Item(6, e.RowIndex).Value = "CS"
        End If

        Call CheckActualLoading(sender, e)
        Me.Cursor = Cursors.Default

        Exit Sub
xError:
        modWriteLog("Error", Me, Err.Number, Err.Description)
    End Sub


    Private Sub DataGridView1_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellLeave
        Call CheckActualLoading(sender, e)
    End Sub

    Private Sub CheckActualLoading(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If cmdFind.Text = "&Save" Then
            If e.ColumnIndex = 5 Then
                Dim v As String = Trim(DataGridView1.Item(5, e.RowIndex).Value)
                If v = "" Then
                    v = 0
                End If

                If Not IsNumeric(v) Then
                    MsgBox("Please enter numeric values only.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        frmMain.ToolStrip1.Enabled = True

    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cmdFind_Click(sender, e)
    End Sub

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmMain.frmMain_FormClosing(sender, e)
    End Sub

    Private Sub TSSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSSave.Click
        cmdFind_Click(sender, e)
    End Sub

    Private Sub TSPrintPrevw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSPrintPrevw.Click
        cmdPrint_Click(sender, e)
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.F Then
            frmSearchItem.ShowDialog(Me)

            If frmSearchItem.vSearch = True Then
                Dim vItemFound As Boolean = False
                Dim vColumnIndex As Int16 = frmSearchItem.vC
                Dim vItem As String = frmSearchItem.vItem

                r = 0 : rCount = DataGridView1.RowCount - 1
                Do While r < rCount
                    If DataGridView1.Item(vColumnIndex, r).Value.ToString.ToUpper = vItem.ToUpper Then
                        MsgBox("Item found.", MsgBoxStyle.Information)
                        vItemFound = True
                        DataGridView1.Item(vColumnIndex, r).Selected = True
                        Exit Do
                    End If

                    r += 1
                Loop

                If vItemFound = False Then
                    MsgBox("Item not found.", MsgBoxStyle.Critical)
                End If

            End If
        End If
    End Sub

End Class