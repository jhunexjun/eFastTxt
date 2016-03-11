<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDPSProcessing
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.colDocNum = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDocDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDocType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colInvText = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colInvAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colEWTAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ContxtMnuEWT = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ContxtComputeEWT = New System.Windows.Forms.ToolStripMenuItem
        Me.colDeductTyp = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colDeductTxt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRefNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ContxtMnuGrid = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteRowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Label4 = New System.Windows.Forms.Label
        Me.DTTo = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.DTFrom = New System.Windows.Forms.DateTimePicker
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdFind = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtEWT = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtNetTotal = New System.Windows.Forms.TextBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtInvoice = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.TSPrintPrevw = New System.Windows.Forms.ToolStripMenuItem
        Me.TSQuit = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContxtMnuEWT.SuspendLayout()
        Me.ContxtMnuGrid.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colDocNum, Me.colDocDate, Me.colDocType, Me.colInvText, Me.colInvAmt, Me.colEWTAmt, Me.colDeductTyp, Me.colDeductTxt, Me.colRefNo})
        Me.DataGridView1.ContextMenuStrip = Me.ContxtMnuGrid
        Me.DataGridView1.Location = New System.Drawing.Point(11, 86)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(862, 322)
        Me.DataGridView1.TabIndex = 0
        '
        'colDocNum
        '
        Me.colDocNum.HeaderText = "Doc. Num"
        Me.colDocNum.Name = "colDocNum"
        Me.colDocNum.Width = 80
        '
        'colDocDate
        '
        DataGridViewCellStyle5.Format = "d"
        Me.colDocDate.DefaultCellStyle = DataGridViewCellStyle5
        Me.colDocDate.HeaderText = "Doc. Date"
        Me.colDocDate.Name = "colDocDate"
        Me.colDocDate.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colDocDate.Width = 80
        '
        'colDocType
        '
        Me.colDocType.HeaderText = "Doc. Type"
        Me.colDocType.Name = "colDocType"
        Me.colDocType.Width = 90
        '
        'colInvText
        '
        DataGridViewCellStyle6.NullValue = Nothing
        Me.colInvText.DefaultCellStyle = DataGridViewCellStyle6
        Me.colInvText.HeaderText = "Invoice Text"
        Me.colInvText.Name = "colInvText"
        '
        'colInvAmt
        '
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle7.Format = "N2"
        DataGridViewCellStyle7.NullValue = "0.00"
        Me.colInvAmt.DefaultCellStyle = DataGridViewCellStyle7
        Me.colInvAmt.HeaderText = "Inv Amt."
        Me.colInvAmt.Name = "colInvAmt"
        Me.colInvAmt.Width = 90
        '
        'colEWTAmt
        '
        Me.colEWTAmt.ContextMenuStrip = Me.ContxtMnuEWT
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle8.Format = "N2"
        DataGridViewCellStyle8.NullValue = "0.00"
        Me.colEWTAmt.DefaultCellStyle = DataGridViewCellStyle8
        Me.colEWTAmt.HeaderText = "EWT Amt"
        Me.colEWTAmt.Name = "colEWTAmt"
        '
        'ContxtMnuEWT
        '
        Me.ContxtMnuEWT.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContxtComputeEWT})
        Me.ContxtMnuEWT.Name = "ContxtMnu"
        Me.ContxtMnuEWT.Size = New System.Drawing.Size(143, 26)
        '
        'ContxtComputeEWT
        '
        Me.ContxtComputeEWT.Name = "ContxtComputeEWT"
        Me.ContxtComputeEWT.Size = New System.Drawing.Size(142, 22)
        Me.ContxtComputeEWT.Text = "Compute EWT"
        '
        'colDeductTyp
        '
        Me.colDeductTyp.HeaderText = "Deduct Type"
        Me.colDeductTyp.Name = "colDeductTyp"
        Me.colDeductTyp.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'colDeductTxt
        '
        Me.colDeductTxt.HeaderText = "Deduction Text"
        Me.colDeductTxt.Name = "colDeductTxt"
        Me.colDeductTxt.Width = 110
        '
        'colRefNo
        '
        Me.colRefNo.HeaderText = "Ref. Number"
        Me.colRefNo.Name = "colRefNo"
        Me.colRefNo.ReadOnly = True
        Me.colRefNo.Visible = False
        '
        'ContxtMnuGrid
        '
        Me.ContxtMnuGrid.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteRowToolStripMenuItem})
        Me.ContxtMnuGrid.Name = "ContxtMnu"
        Me.ContxtMnuGrid.Size = New System.Drawing.Size(130, 26)
        '
        'DeleteRowToolStripMenuItem
        '
        Me.DeleteRowToolStripMenuItem.Name = "DeleteRowToolStripMenuItem"
        Me.DeleteRowToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.DeleteRowToolStripMenuItem.Text = "Delete Row"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 45)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(19, 13)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "To"
        '
        'DTTo
        '
        Me.DTTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTTo.Location = New System.Drawing.Point(47, 41)
        Me.DTTo.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.DTTo.MinDate = New Date(2009, 1, 1, 0, 0, 0, 0)
        Me.DTTo.Name = "DTTo"
        Me.DTTo.Size = New System.Drawing.Size(101, 21)
        Me.DTTo.TabIndex = 63
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 18)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 62
        Me.Label8.Text = "From"
        '
        'DTFrom
        '
        Me.DTFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFrom.Location = New System.Drawing.Point(47, 13)
        Me.DTFrom.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.DTFrom.MinDate = New Date(2009, 1, 1, 0, 0, 0, 0)
        Me.DTFrom.Name = "DTFrom"
        Me.DTFrom.Size = New System.Drawing.Size(101, 21)
        Me.DTFrom.TabIndex = 61
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(789, 520)
        Me.cmdCancel.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(74, 22)
        Me.cmdCancel.TabIndex = 67
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdFind
        '
        Me.cmdFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdFind.Location = New System.Drawing.Point(630, 520)
        Me.cmdFind.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(74, 22)
        Me.cmdFind.TabIndex = 65
        Me.cmdFind.Text = "&Find"
        Me.cmdFind.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(670, 447)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 13)
        Me.Label6.TabIndex = 73
        Me.Label6.Text = "EWT"
        '
        'txtEWT
        '
        Me.txtEWT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEWT.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWT.Location = New System.Drawing.Point(723, 443)
        Me.txtEWT.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtEWT.Name = "txtEWT"
        Me.txtEWT.ReadOnly = True
        Me.txtEWT.Size = New System.Drawing.Size(140, 21)
        Me.txtEWT.TabIndex = 72
        Me.txtEWT.Text = "0.00"
        Me.txtEWT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(670, 471)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 69
        Me.Label3.Text = "Net Total"
        '
        'txtNetTotal
        '
        Me.txtNetTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNetTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetTotal.Location = New System.Drawing.Point(723, 467)
        Me.txtNetTotal.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtNetTotal.Name = "txtNetTotal"
        Me.txtNetTotal.ReadOnly = True
        Me.txtNetTotal.Size = New System.Drawing.Size(140, 21)
        Me.txtNetTotal.TabIndex = 68
        Me.txtNetTotal.Text = "0.00"
        Me.txtNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Enabled = False
        Me.cmdAdd.Location = New System.Drawing.Point(711, 520)
        Me.cmdAdd.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(74, 22)
        Me.cmdAdd.TabIndex = 74
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DTTo)
        Me.GroupBox1.Controls.Add(Me.DTFrom)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(701, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(162, 68)
        Me.GroupBox1.TabIndex = 75
        Me.GroupBox1.TabStop = False
        '
        'txtInvoice
        '
        Me.txtInvoice.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtInvoice.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoice.Location = New System.Drawing.Point(723, 419)
        Me.txtInvoice.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.txtInvoice.Name = "txtInvoice"
        Me.txtInvoice.ReadOnly = True
        Me.txtInvoice.Size = New System.Drawing.Size(140, 21)
        Me.txtInvoice.TabIndex = 81
        Me.txtInvoice.Text = "0.00"
        Me.txtInvoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(670, 423)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 82
        Me.Label1.Text = "Invoice"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(884, 24)
        Me.MenuStrip1.TabIndex = 83
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'FileToolStripMenuItem1
        '
        Me.FileToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator2, Me.TSPrintPrevw, Me.TSQuit})
        Me.FileToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.FileToolStripMenuItem1.Name = "FileToolStripMenuItem1"
        Me.FileToolStripMenuItem1.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem1.Text = "&File"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(149, 6)
        '
        'TSPrintPrevw
        '
        Me.TSPrintPrevw.Enabled = False
        Me.TSPrintPrevw.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSPrintPrevw.Name = "TSPrintPrevw"
        Me.TSPrintPrevw.Size = New System.Drawing.Size(152, 22)
        Me.TSPrintPrevw.Text = "Export to Excel"
        '
        'TSQuit
        '
        Me.TSQuit.Image = Global.eFastText.My.Resources.Resources.Quit
        Me.TSQuit.MergeAction = System.Windows.Forms.MergeAction.MatchOnly
        Me.TSQuit.Name = "TSQuit"
        Me.TSQuit.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.TSQuit.Size = New System.Drawing.Size(152, 22)
        Me.TSQuit.Text = "&Quit"
        '
        'frmDPSProcessing
        '
        Me.AcceptButton = Me.cmdFind
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(884, 568)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.txtInvoice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtNetTotal)
        Me.Controls.Add(Me.txtEWT)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdFind)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.MaximizeBox = False
        Me.Name = "frmDPSProcessing"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DPS Processing"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContxtMnuEWT.ResumeLayout(False)
        Me.ContxtMnuGrid.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DTFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtEWT As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ContxtMnuGrid As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteRowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtInvoice As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ContxtMnuEWT As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContxtComputeEWT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents colDocNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDocDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDocType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvText As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colInvAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colEWTAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDeductTyp As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colDeductTxt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRefNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSPrintPrevw As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSQuit As System.Windows.Forms.ToolStripMenuItem
End Class
