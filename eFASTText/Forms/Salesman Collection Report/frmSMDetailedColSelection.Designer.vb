<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSMDetailedColSelection
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
        Me.lblSalesperson = New System.Windows.Forms.Label
        Me.txtSalesperson = New System.Windows.Forms.TextBox
        Me.ChckDue = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.DTTo = New System.Windows.Forms.DateTimePicker
        Me.DTFrom = New System.Windows.Forms.DateTimePicker
        Me.ChckFilter = New System.Windows.Forms.CheckBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdGenerate = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblSalesperson
        '
        Me.lblSalesperson.AutoSize = True
        Me.lblSalesperson.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesperson.Location = New System.Drawing.Point(35, 103)
        Me.lblSalesperson.Name = "lblSalesperson"
        Me.lblSalesperson.Size = New System.Drawing.Size(65, 13)
        Me.lblSalesperson.TabIndex = 45
        Me.lblSalesperson.Tag = "1"
        Me.lblSalesperson.Text = "Salesperson"
        Me.lblSalesperson.Visible = False
        '
        'txtSalesperson
        '
        Me.txtSalesperson.Location = New System.Drawing.Point(106, 99)
        Me.txtSalesperson.Name = "txtSalesperson"
        Me.txtSalesperson.Size = New System.Drawing.Size(100, 21)
        Me.txtSalesperson.TabIndex = 3
        Me.txtSalesperson.Tag = "1"
        Me.txtSalesperson.Visible = False
        '
        'ChckDue
        '
        Me.ChckDue.AutoSize = True
        Me.ChckDue.Checked = True
        Me.ChckDue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChckDue.Location = New System.Drawing.Point(38, 126)
        Me.ChckDue.Name = "ChckDue"
        Me.ChckDue.Size = New System.Drawing.Size(98, 17)
        Me.ChckDue.TabIndex = 4
        Me.ChckDue.Tag = "1"
        Me.ChckDue.Text = "Due check only"
        Me.ChckDue.UseVisualStyleBackColor = True
        Me.ChckDue.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 41)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(19, 13)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "To"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "From"
        '
        'DTTo
        '
        Me.DTTo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTTo.Location = New System.Drawing.Point(49, 37)
        Me.DTTo.MinDate = New Date(2009, 1, 1, 0, 0, 0, 0)
        Me.DTTo.Name = "DTTo"
        Me.DTTo.Size = New System.Drawing.Size(101, 21)
        Me.DTTo.TabIndex = 1
        '
        'DTFrom
        '
        Me.DTFrom.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DTFrom.Location = New System.Drawing.Point(49, 10)
        Me.DTFrom.MinDate = New Date(2009, 1, 1, 0, 0, 0, 0)
        Me.DTFrom.Name = "DTFrom"
        Me.DTFrom.Size = New System.Drawing.Size(101, 21)
        Me.DTFrom.TabIndex = 0
        '
        'ChckFilter
        '
        Me.ChckFilter.AutoSize = True
        Me.ChckFilter.Location = New System.Drawing.Point(15, 83)
        Me.ChckFilter.Name = "ChckFilter"
        Me.ChckFilter.Size = New System.Drawing.Size(50, 17)
        Me.ChckFilter.TabIndex = 2
        Me.ChckFilter.Text = "Filter"
        Me.ChckFilter.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(119, 183)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 6
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdGenerate
        '
        Me.cmdGenerate.Location = New System.Drawing.Point(38, 183)
        Me.cmdGenerate.Name = "cmdGenerate"
        Me.cmdGenerate.Size = New System.Drawing.Size(75, 23)
        Me.cmdGenerate.TabIndex = 5
        Me.cmdGenerate.Text = "&Generate"
        Me.cmdGenerate.UseVisualStyleBackColor = True
        '
        'frmSMDetailedColSelection
        '
        Me.AcceptButton = Me.cmdGenerate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(221, 218)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdGenerate)
        Me.Controls.Add(Me.ChckFilter)
        Me.Controls.Add(Me.lblSalesperson)
        Me.Controls.Add(Me.txtSalesperson)
        Me.Controls.Add(Me.ChckDue)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.DTTo)
        Me.Controls.Add(Me.DTFrom)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmSMDetailedColSelection"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detailed Salesman Collection - Selection"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSalesperson As System.Windows.Forms.Label
    Friend WithEvents txtSalesperson As System.Windows.Forms.TextBox
    Friend WithEvents ChckDue As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DTTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents ChckFilter As System.Windows.Forms.CheckBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdGenerate As System.Windows.Forms.Button
End Class
