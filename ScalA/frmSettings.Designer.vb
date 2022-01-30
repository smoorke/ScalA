<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Me.components = New System.ComponentModel.Container()
        Dim Label2 As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label3 As System.Windows.Forms.Label
        Me.chkTopMost = New System.Windows.Forms.CheckBox()
        Me.grpAlign = New System.Windows.Forms.GroupBox()
        Me.numYoffset = New System.Windows.Forms.NumericUpDown()
        Me.numXoffset = New System.Windows.Forms.NumericUpDown()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.chkDoAlign = New System.Windows.Forms.CheckBox()
        Me.tmrAlign = New System.Windows.Forms.Timer(Me.components)
        Me.chkAspect = New System.Windows.Forms.CheckBox()
        Me.cmbAnchor = New System.Windows.Forms.ComboBox()
        Label2 = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label3 = New System.Windows.Forms.Label()
        Me.grpAlign.SuspendLayout()
        CType(Me.numYoffset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numXoffset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Location = New System.Drawing.Point(6, 41)
        Label2.Name = "Label2"
        Label2.Size = New System.Drawing.Size(14, 13)
        Label2.TabIndex = 7
        Label2.Text = "Y"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Location = New System.Drawing.Point(6, 20)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(14, 13)
        Label1.TabIndex = 6
        Label1.Text = "X"
        '
        'Label3
        '
        Label3.AutoSize = True
        Label3.Location = New System.Drawing.Point(12, 52)
        Label3.Name = "Label3"
        Label3.Size = New System.Drawing.Size(66, 13)
        Label3.TabIndex = 8
        Label3.Text = "Maximize To"
        '
        'chkTopMost
        '
        Me.chkTopMost.AutoSize = True
        Me.chkTopMost.Location = New System.Drawing.Point(9, 9)
        Me.chkTopMost.Name = "chkTopMost"
        Me.chkTopMost.Size = New System.Drawing.Size(98, 17)
        Me.chkTopMost.TabIndex = 0
        Me.chkTopMost.Text = "Always On Top"
        Me.chkTopMost.UseVisualStyleBackColor = True
        '
        'grpAlign
        '
        Me.grpAlign.Controls.Add(Me.numYoffset)
        Me.grpAlign.Controls.Add(Me.numXoffset)
        Me.grpAlign.Controls.Add(Label2)
        Me.grpAlign.Controls.Add(Label1)
        Me.grpAlign.Enabled = False
        Me.grpAlign.Location = New System.Drawing.Point(9, 95)
        Me.grpAlign.Name = "grpAlign"
        Me.grpAlign.Size = New System.Drawing.Size(100, 62)
        Me.grpAlign.TabIndex = 1
        Me.grpAlign.TabStop = False
        '
        'numYoffset
        '
        Me.numYoffset.Location = New System.Drawing.Point(26, 39)
        Me.numYoffset.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.numYoffset.Minimum = New Decimal(New Integer() {4000, 0, 0, -2147483648})
        Me.numYoffset.Name = "numYoffset"
        Me.numYoffset.Size = New System.Drawing.Size(72, 20)
        Me.numYoffset.TabIndex = 9
        Me.numYoffset.Tag = "1"
        Me.numYoffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'numXoffset
        '
        Me.numXoffset.Location = New System.Drawing.Point(26, 17)
        Me.numXoffset.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.numXoffset.Minimum = New Decimal(New Integer() {4000, 0, 0, -2147483648})
        Me.numXoffset.Name = "numXoffset"
        Me.numXoffset.Size = New System.Drawing.Size(72, 20)
        Me.numXoffset.TabIndex = 8
        Me.numXoffset.Tag = "0"
        Me.numXoffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(9, 160)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(50, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(59, 160)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(50, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'chkDoAlign
        '
        Me.chkDoAlign.AutoSize = True
        Me.chkDoAlign.Location = New System.Drawing.Point(9, 95)
        Me.chkDoAlign.Name = "chkDoAlign"
        Me.chkDoAlign.Size = New System.Drawing.Size(72, 17)
        Me.chkDoAlign.TabIndex = 5
        Me.chkDoAlign.Text = "Alignment"
        Me.chkDoAlign.UseVisualStyleBackColor = True
        '
        'tmrAlign
        '
        '
        'chkAspect
        '
        Me.chkAspect.AutoSize = True
        Me.chkAspect.Enabled = False
        Me.chkAspect.Location = New System.Drawing.Point(9, 32)
        Me.chkAspect.Name = "chkAspect"
        Me.chkAspect.Size = New System.Drawing.Size(92, 17)
        Me.chkAspect.TabIndex = 6
        Me.chkAspect.Text = "Lock Ascpect"
        Me.chkAspect.UseVisualStyleBackColor = True
        '
        'cmbAnchor
        '
        Me.cmbAnchor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAnchor.DropDownWidth = 1
        Me.cmbAnchor.Enabled = False
        Me.cmbAnchor.FormattingEnabled = True
        Me.cmbAnchor.Items.AddRange(New Object() {"Top Left", "Center", "Bottom Right"})
        Me.cmbAnchor.Location = New System.Drawing.Point(20, 68)
        Me.cmbAnchor.Name = "cmbAnchor"
        Me.cmbAnchor.Size = New System.Drawing.Size(87, 21)
        Me.cmbAnchor.TabIndex = 7
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(117, 190)
        Me.Controls.Add(Label3)
        Me.Controls.Add(Me.cmbAnchor)
        Me.Controls.Add(Me.chkAspect)
        Me.Controls.Add(Me.chkDoAlign)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpAlign)
        Me.Controls.Add(Me.chkTopMost)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Scala Settings"
        Me.grpAlign.ResumeLayout(False)
        Me.grpAlign.PerformLayout()
        CType(Me.numYoffset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numXoffset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents chkTopMost As CheckBox
    Friend WithEvents grpAlign As GroupBox
    Friend WithEvents numYoffset As NumericUpDown
    Friend WithEvents numXoffset As NumericUpDown
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents tmrAlign As Timer
    Friend WithEvents chkDoAlign As CheckBox
    Friend WithEvents chkAspect As CheckBox
    Friend WithEvents cmbAnchor As ComboBox
End Class
