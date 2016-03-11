<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaggingOfRemittances
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.txtGrandTotal = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtSI = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtCheck = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.txtCash = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCNCM = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.coloID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colRemitted = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.colOnLinePymnt = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.colORNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCustomerName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSINo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colSIAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCNNo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCNAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colPaymentAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCheckDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colBank = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.colCheckPaymentAmt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColOverUnder = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.VoidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.txtSalesmanCode = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBranch = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdFind = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cboDivision = New System.Windows.Forms.ComboBox
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtGrandTotal
        '
        Me.txtGrandTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGrandTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrandTotal.Location = New System.Drawing.Point(810, 504)
        Me.txtGrandTotal.Name = "txtGrandTotal"
        Me.txtGrandTotal.ReadOnly = True
        Me.txtGrandTotal.Size = New System.Drawing.Size(146, 20)
        Me.txtGrandTotal.TabIndex = 50
        Me.txtGrandTotal.Text = "0.00"
        Me.txtGrandTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(741, 507)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(63, 13)
        Me.Label11.TabIndex = 51
        Me.Label11.Text = "Grand Total"
        '
        'txtSI
        '
        Me.txtSI.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSI.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSI.Location = New System.Drawing.Point(560, 461)
        Me.txtSI.Name = "txtSI"
        Me.txtSI.ReadOnly = True
        Me.txtSI.Size = New System.Drawing.Size(146, 20)
        Me.txtSI.TabIndex = 46
        Me.txtSI.Text = "0.00"
        Me.txtSI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(514, 463)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(25, 13)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "S.I."
        '
        'txtCheck
        '
        Me.txtCheck.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCheck.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheck.Location = New System.Drawing.Point(810, 482)
        Me.txtCheck.Name = "txtCheck"
        Me.txtCheck.ReadOnly = True
        Me.txtCheck.Size = New System.Drawing.Size(146, 20)
        Me.txtCheck.TabIndex = 47
        Me.txtCheck.Text = "0.00"
        Me.txtCheck.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(741, 485)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 13)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Check"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(836, 54)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "To"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(666, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 44
        Me.Label7.Text = "From"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(855, 50)
        Me.DateTimePicker2.MinDate = New Date(2009, 1, 1, 0, 0, 0, 0)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(101, 21)
        Me.DateTimePicker2.TabIndex = 34
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(713, 50)
        Me.DateTimePicker1.MinDate = New Date(2009, 1, 1, 0, 0, 0, 0)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(101, 21)
        Me.DateTimePicker1.TabIndex = 32
        '
        'txtCash
        '
        Me.txtCash.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCash.Location = New System.Drawing.Point(810, 461)
        Me.txtCash.Name = "txtCash"
        Me.txtCash.ReadOnly = True
        Me.txtCash.Size = New System.Drawing.Size(146, 20)
        Me.txtCash.TabIndex = 36
        Me.txtCash.Text = "0.00"
        Me.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(741, 464)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(31, 13)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Cash"
        '
        'txtCNCM
        '
        Me.txtCNCM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCNCM.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCNCM.Location = New System.Drawing.Point(560, 482)
        Me.txtCNCM.Name = "txtCNCM"
        Me.txtCNCM.ReadOnly = True
        Me.txtCNCM.Size = New System.Drawing.Size(146, 20)
        Me.txtCNCM.TabIndex = 37
        Me.txtCNCM.Text = "0.00"
        Me.txtCNCM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(514, 485)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "CN/CM"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeight = 24
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.coloID, Me.colRemitted, Me.colOnLinePymnt, Me.colORNo, Me.colCustomerName, Me.colSINo, Me.colSIAmt, Me.colCNNo, Me.colCNAmt, Me.colPaymentAmt, Me.colCheckDate, Me.colBank, Me.colCheckPaymentAmt, Me.ColOverUnder})
        Me.DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView1.Location = New System.Drawing.Point(12, 86)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.Size = New System.Drawing.Size(944, 367)
        Me.DataGridView1.TabIndex = 35
        '
        'coloID
        '
        Me.coloID.HeaderText = "Record #"
        Me.coloID.Name = "coloID"
        Me.coloID.ReadOnly = True
        Me.coloID.Width = 60
        '
        'colRemitted
        '
        Me.colRemitted.HeaderText = "Remitted"
        Me.colRemitted.Name = "colRemitted"
        Me.colRemitted.Width = 70
        '
        'colOnLinePymnt
        '
        Me.colOnLinePymnt.HeaderText = "Online Pay"
        Me.colOnLinePymnt.Name = "colOnLinePymnt"
        Me.colOnLinePymnt.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.colOnLinePymnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.colOnLinePymnt.Visible = False
        Me.colOnLinePymnt.Width = 70
        '
        'colORNo
        '
        Me.colORNo.HeaderText = "OR No."
        Me.colORNo.Name = "colORNo"
        Me.colORNo.ReadOnly = True
        Me.colORNo.Width = 60
        '
        'colCustomerName
        '
        Me.colCustomerName.HeaderText = "Customer Name"
        Me.colCustomerName.Name = "colCustomerName"
        Me.colCustomerName.ReadOnly = True
        Me.colCustomerName.Width = 170
        '
        'colSINo
        '
        Me.colSINo.HeaderText = "S.I. No."
        Me.colSINo.Name = "colSINo"
        Me.colSINo.ReadOnly = True
        Me.colSINo.Width = 60
        '
        'colSIAmt
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle1.Format = "N2"
        DataGridViewCellStyle1.NullValue = "-"
        Me.colSIAmt.DefaultCellStyle = DataGridViewCellStyle1
        Me.colSIAmt.HeaderText = "S.I. Amt"
        Me.colSIAmt.Name = "colSIAmt"
        Me.colSIAmt.ReadOnly = True
        '
        'colCNNo
        '
        Me.colCNNo.HeaderText = "CN/CM No."
        Me.colCNNo.Name = "colCNNo"
        Me.colCNNo.ReadOnly = True
        Me.colCNNo.Width = 70
        '
        'colCNAmt
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle2.Format = "N2"
        DataGridViewCellStyle2.NullValue = "-"
        Me.colCNAmt.DefaultCellStyle = DataGridViewCellStyle2
        Me.colCNAmt.HeaderText = "CN/CM Amt"
        Me.colCNAmt.Name = "colCNAmt"
        Me.colCNAmt.ReadOnly = True
        '
        'colPaymentAmt
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle3.Format = "N2"
        DataGridViewCellStyle3.NullValue = "-"
        Me.colPaymentAmt.DefaultCellStyle = DataGridViewCellStyle3
        Me.colPaymentAmt.HeaderText = "Cash Amt"
        Me.colPaymentAmt.Name = "colPaymentAmt"
        Me.colPaymentAmt.ReadOnly = True
        '
        'colCheckDate
        '
        Me.colCheckDate.HeaderText = "Check Date"
        Me.colCheckDate.Name = "colCheckDate"
        Me.colCheckDate.ReadOnly = True
        Me.colCheckDate.Width = 70
        '
        'colBank
        '
        Me.colBank.HeaderText = "Bank"
        Me.colBank.Name = "colBank"
        Me.colBank.ReadOnly = True
        '
        'colCheckPaymentAmt
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.Format = "N2"
        DataGridViewCellStyle4.NullValue = "-"
        Me.colCheckPaymentAmt.DefaultCellStyle = DataGridViewCellStyle4
        Me.colCheckPaymentAmt.HeaderText = "Check Amt"
        Me.colCheckPaymentAmt.Name = "colCheckPaymentAmt"
        Me.colCheckPaymentAmt.ReadOnly = True
        '
        'ColOverUnder
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle5.Format = "N2"
        DataGridViewCellStyle5.NullValue = "-"
        Me.ColOverUnder.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColOverUnder.HeaderText = "Over/Under Collection"
        Me.ColOverUnder.Name = "ColOverUnder"
        Me.ColOverUnder.ReadOnly = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VoidToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(110, 26)
        '
        'VoidToolStripMenuItem
        '
        Me.VoidToolStripMenuItem.Name = "VoidToolStripMenuItem"
        Me.VoidToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.VoidToolStripMenuItem.Text = "&Update"
        '
        'txtSalesmanCode
        '
        Me.txtSalesmanCode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesmanCode.Location = New System.Drawing.Point(99, 50)
        Me.txtSalesmanCode.Name = "txtSalesmanCode"
        Me.txtSalesmanCode.Size = New System.Drawing.Size(114, 21)
        Me.txtSalesmanCode.TabIndex = 27
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Salesman Code"
        '
        'txtBranch
        '
        Me.txtBranch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBranch.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.Location = New System.Drawing.Point(713, 24)
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.Size = New System.Drawing.Size(131, 21)
        Me.txtBranch.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Division"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(666, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Branch"
        '
        'cmdFind
        '
        Me.cmdFind.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdFind.Location = New System.Drawing.Point(800, 563)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.Size = New System.Drawing.Size(75, 23)
        Me.cmdFind.TabIndex = 38
        Me.cmdFind.Text = "&Find"
        Me.cmdFind.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(881, 563)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 41
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cboDivision
        '
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.FormattingEnabled = True
        Me.cboDivision.Items.AddRange(New Object() {"GROCERY", "ICE CREAM"})
        Me.cboDivision.Location = New System.Drawing.Point(99, 23)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.Size = New System.Drawing.Size(151, 21)
        Me.cboDivision.TabIndex = 26
        '
        'frmTaggingOfRemittances
        '
        Me.AcceptButton = Me.cmdFind
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(972, 598)
        Me.Controls.Add(Me.txtSI)
        Me.Controls.Add(Me.txtGrandTotal)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtCheck)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtCNCM)
        Me.Controls.Add(Me.txtCash)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSalesmanCode)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtBranch)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboDivision)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdFind)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmTaggingOfRemittances"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Tagging of Remittances"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtGrandTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtSI As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCheck As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCash As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCNCM As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents txtSalesmanCode As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBranch As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmdFind As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cboDivision As System.Windows.Forms.ComboBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents VoidToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents coloID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colRemitted As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colOnLinePymnt As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents colORNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCustomerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSINo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colSIAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCNNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCNAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colPaymentAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCheckDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colBank As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents colCheckPaymentAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColOverUnder As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
