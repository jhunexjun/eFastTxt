<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewMessage
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblDateTime = New System.Windows.Forms.Label
        Me.txtInboxMessage = New System.Windows.Forms.TextBox
        Me.LblName = New System.Windows.Forms.Label
        Me.LblNumber = New System.Windows.Forms.Label
        Me.txtMsgIndex = New System.Windows.Forms.TextBox
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.cmdReply = New System.Windows.Forms.Button
        Me.LblTextCount = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Number:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Name:"
        '
        'LblDateTime
        '
        Me.LblDateTime.AutoSize = True
        Me.LblDateTime.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDateTime.Location = New System.Drawing.Point(214, 21)
        Me.LblDateTime.Name = "LblDateTime"
        Me.LblDateTime.Size = New System.Drawing.Size(56, 13)
        Me.LblDateTime.TabIndex = 2
        Me.LblDateTime.Text = "Date/Time"
        '
        'txtInboxMessage
        '
        Me.txtInboxMessage.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInboxMessage.Location = New System.Drawing.Point(51, 76)
        Me.txtInboxMessage.MaxLength = 480
        Me.txtInboxMessage.Multiline = True
        Me.txtInboxMessage.Name = "txtInboxMessage"
        Me.txtInboxMessage.ReadOnly = True
        Me.txtInboxMessage.Size = New System.Drawing.Size(318, 195)
        Me.txtInboxMessage.TabIndex = 2
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(65, 45)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(34, 13)
        Me.LblName.TabIndex = 5
        Me.LblName.Text = "Name"
        '
        'LblNumber
        '
        Me.LblNumber.AutoSize = True
        Me.LblNumber.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumber.Location = New System.Drawing.Point(65, 21)
        Me.LblNumber.Name = "LblNumber"
        Me.LblNumber.Size = New System.Drawing.Size(44, 13)
        Me.LblNumber.TabIndex = 4
        Me.LblNumber.Text = "Number"
        '
        'txtMsgIndex
        '
        Me.txtMsgIndex.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMsgIndex.Location = New System.Drawing.Point(12, 76)
        Me.txtMsgIndex.Name = "txtMsgIndex"
        Me.txtMsgIndex.Size = New System.Drawing.Size(33, 21)
        Me.txtMsgIndex.TabIndex = 6
        Me.txtMsgIndex.Visible = False
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.Location = New System.Drawing.Point(298, 292)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdReply
        '
        Me.cmdReply.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReply.Location = New System.Drawing.Point(217, 292)
        Me.cmdReply.Name = "cmdReply"
        Me.cmdReply.Size = New System.Drawing.Size(75, 23)
        Me.cmdReply.TabIndex = 0
        Me.cmdReply.Text = "Re&ply"
        Me.cmdReply.UseVisualStyleBackColor = True
        '
        'LblTextCount
        '
        Me.LblTextCount.AutoSize = True
        Me.LblTextCount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTextCount.Location = New System.Drawing.Point(52, 274)
        Me.LblTextCount.Name = "LblTextCount"
        Me.LblTextCount.Size = New System.Drawing.Size(35, 13)
        Me.LblTextCount.TabIndex = 21
        Me.LblTextCount.Text = "0/480"
        '
        'frmViewMessage
        '
        Me.AcceptButton = Me.cmdReply
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(396, 327)
        Me.Controls.Add(Me.LblTextCount)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdReply)
        Me.Controls.Add(Me.txtMsgIndex)
        Me.Controls.Add(Me.LblName)
        Me.Controls.Add(Me.LblNumber)
        Me.Controls.Add(Me.txtInboxMessage)
        Me.Controls.Add(Me.LblDateTime)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmViewMessage"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "View Message"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblDateTime As System.Windows.Forms.Label
    Friend WithEvents txtInboxMessage As System.Windows.Forms.TextBox
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents LblNumber As System.Windows.Forms.Label
    Friend WithEvents txtMsgIndex As System.Windows.Forms.TextBox
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents cmdReply As System.Windows.Forms.Button
    Friend WithEvents LblTextCount As System.Windows.Forms.Label
End Class
