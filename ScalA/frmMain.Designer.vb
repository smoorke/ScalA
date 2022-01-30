<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.pnlButtons = New System.Windows.Forms.Panel()
        Me.btnMin = New System.Windows.Forms.Button()
        Me.btnMax = New System.Windows.Forms.Button()
        Me.btnQuit = New System.Windows.Forms.Button()
        Me.pbZoom = New System.Windows.Forms.PictureBox()
        Me.cboAlt = New System.Windows.Forms.ComboBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.tmrTick = New System.Windows.Forms.Timer(Me.components)
        Me.cmbResolution = New System.Windows.Forms.ComboBox()
        Me.chkDebug = New System.Windows.Forms.CheckBox()
        Me.pnlStartup = New System.Windows.Forms.Panel()
        Me.chkHideMessage = New System.Windows.Forms.CheckBox()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.btnAlt1 = New System.Windows.Forms.Button()
        Me.btnAlt2 = New System.Windows.Forms.Button()
        Me.btnAlt3 = New System.Windows.Forms.Button()
        Me.btnAlt4 = New System.Windows.Forms.Button()
        Me.tmrStartup = New System.Windows.Forms.Timer(Me.components)
        Me.btnStart = New System.Windows.Forms.Button()
        Me.tmrHotkey = New System.Windows.Forms.Timer(Me.components)
        Me.pnlButtons.SuspendLayout()
        CType(Me.pbZoom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStartup.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlButtons
        '
        Me.pnlButtons.Controls.Add(Me.btnMin)
        Me.pnlButtons.Controls.Add(Me.btnMax)
        Me.pnlButtons.Controls.Add(Me.btnQuit)
        Me.pnlButtons.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlButtons.Location = New System.Drawing.Point(667, 0)
        Me.pnlButtons.MaximumSize = New System.Drawing.Size(135, 25)
        Me.pnlButtons.MinimumSize = New System.Drawing.Size(135, 25)
        Me.pnlButtons.Name = "pnlButtons"
        Me.pnlButtons.Size = New System.Drawing.Size(135, 25)
        Me.pnlButtons.TabIndex = 7
        '
        'btnMin
        '
        Me.btnMin.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMin.FlatAppearance.BorderSize = 0
        Me.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMin.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMin.Location = New System.Drawing.Point(0, 0)
        Me.btnMin.Name = "btnMin"
        Me.btnMin.Size = New System.Drawing.Size(45, 25)
        Me.btnMin.TabIndex = 8
        Me.btnMin.TabStop = False
        Me.btnMin.Text = "⎯"
        Me.btnMin.UseVisualStyleBackColor = True
        '
        'btnMax
        '
        Me.btnMax.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnMax.FlatAppearance.BorderSize = 0
        Me.btnMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMax.Location = New System.Drawing.Point(45, 0)
        Me.btnMax.Name = "btnMax"
        Me.btnMax.Size = New System.Drawing.Size(45, 25)
        Me.btnMax.TabIndex = 9
        Me.btnMax.TabStop = False
        Me.btnMax.Text = "⧠"
        Me.btnMax.UseVisualStyleBackColor = True
        '
        'btnQuit
        '
        Me.btnQuit.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnQuit.FlatAppearance.BorderSize = 0
        Me.btnQuit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        Me.btnQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red
        Me.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuit.Location = New System.Drawing.Point(90, 0)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(45, 25)
        Me.btnQuit.TabIndex = 7
        Me.btnQuit.TabStop = False
        Me.btnQuit.Text = "╳"
        Me.btnQuit.UseVisualStyleBackColor = True
        '
        'pbZoom
        '
        Me.pbZoom.BackColor = System.Drawing.Color.Magenta
        Me.pbZoom.Location = New System.Drawing.Point(1, 27)
        Me.pbZoom.Name = "pbZoom"
        Me.pbZoom.Size = New System.Drawing.Size(800, 600)
        Me.pbZoom.TabIndex = 0
        Me.pbZoom.TabStop = False
        '
        'cboAlt
        '
        Me.cboAlt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAlt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAlt.FormattingEnabled = True
        Me.cboAlt.Items.AddRange(New Object() {"Someone"})
        Me.cboAlt.Location = New System.Drawing.Point(26, 3)
        Me.cboAlt.Name = "cboAlt"
        Me.cboAlt.Size = New System.Drawing.Size(160, 21)
        Me.cboAlt.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(268, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(71, 13)
        Me.lblTitle.TabIndex = 2
        Me.lblTitle.Text = "- ScalA beta -"
        '
        'tmrTick
        '
        Me.tmrTick.Interval = 16
        '
        'cmbResolution
        '
        Me.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbResolution.FormattingEnabled = True
        Me.cmbResolution.Location = New System.Drawing.Point(188, 3)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Size = New System.Drawing.Size(80, 21)
        Me.cmbResolution.TabIndex = 5
        '
        'chkDebug
        '
        Me.chkDebug.AutoSize = True
        Me.chkDebug.Location = New System.Drawing.Point(646, 7)
        Me.chkDebug.Name = "chkDebug"
        Me.chkDebug.Size = New System.Drawing.Size(15, 14)
        Me.chkDebug.TabIndex = 10
        Me.chkDebug.UseVisualStyleBackColor = True
        Me.chkDebug.Visible = False
        '
        'pnlStartup
        '
        Me.pnlStartup.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.pnlStartup.Controls.Add(Me.chkHideMessage)
        Me.pnlStartup.Controls.Add(Me.lblInfo)
        Me.pnlStartup.Controls.Add(Me.btnAlt1)
        Me.pnlStartup.Controls.Add(Me.btnAlt2)
        Me.pnlStartup.Controls.Add(Me.btnAlt3)
        Me.pnlStartup.Controls.Add(Me.btnAlt4)
        Me.pnlStartup.Location = New System.Drawing.Point(1, 27)
        Me.pnlStartup.Name = "pnlStartup"
        Me.pnlStartup.Size = New System.Drawing.Size(800, 600)
        Me.pnlStartup.TabIndex = 11
        Me.pnlStartup.Visible = False
        '
        'chkHideMessage
        '
        Me.chkHideMessage.AutoSize = True
        Me.chkHideMessage.Location = New System.Drawing.Point(277, 275)
        Me.chkHideMessage.Name = "chkHideMessage"
        Me.chkHideMessage.Size = New System.Drawing.Size(117, 17)
        Me.chkHideMessage.TabIndex = 0
        Me.chkHideMessage.Text = "Hide This Message"
        Me.chkHideMessage.UseVisualStyleBackColor = True
        '
        'lblInfo
        '
        Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(1, 1)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(399, 299)
        Me.lblInfo.TabIndex = 3
        Me.lblInfo.Text = "Please have an Astonia Client running in windowed mode and select it from the dro" &
    "pdown above or from this screen." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Press Ctrl-Tab to show this screen."
        '
        'btnAlt1
        '
        Me.btnAlt1.Location = New System.Drawing.Point(0, 0)
        Me.btnAlt1.Name = "btnAlt1"
        Me.btnAlt1.Size = New System.Drawing.Size(400, 300)
        Me.btnAlt1.TabIndex = 1
        Me.btnAlt1.Tag = "0"
        Me.btnAlt1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAlt1.UseVisualStyleBackColor = True
        Me.btnAlt1.Visible = False
        '
        'btnAlt2
        '
        Me.btnAlt2.Location = New System.Drawing.Point(400, 0)
        Me.btnAlt2.Name = "btnAlt2"
        Me.btnAlt2.Size = New System.Drawing.Size(400, 300)
        Me.btnAlt2.TabIndex = 2
        Me.btnAlt2.Tag = "1"
        Me.btnAlt2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAlt2.UseVisualStyleBackColor = True
        '
        'btnAlt3
        '
        Me.btnAlt3.Location = New System.Drawing.Point(0, 300)
        Me.btnAlt3.Name = "btnAlt3"
        Me.btnAlt3.Size = New System.Drawing.Size(400, 300)
        Me.btnAlt3.TabIndex = 3
        Me.btnAlt3.Tag = "2"
        Me.btnAlt3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAlt3.UseVisualStyleBackColor = True
        '
        'btnAlt4
        '
        Me.btnAlt4.Location = New System.Drawing.Point(400, 300)
        Me.btnAlt4.Name = "btnAlt4"
        Me.btnAlt4.Size = New System.Drawing.Size(400, 300)
        Me.btnAlt4.TabIndex = 4
        Me.btnAlt4.Tag = "3"
        Me.btnAlt4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAlt4.UseVisualStyleBackColor = True
        '
        'tmrStartup
        '
        Me.tmrStartup.Enabled = True
        Me.tmrStartup.Interval = 250
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(2, 2)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(23, 23)
        Me.btnStart.TabIndex = 12
        Me.btnStart.Text = "⊞"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'tmrHotkey
        '
        Me.tmrHotkey.Enabled = True
        Me.tmrHotkey.Interval = 99
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(802, 628)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.pnlStartup)
        Me.Controls.Add(Me.chkDebug)
        Me.Controls.Add(Me.pbZoom)
        Me.Controls.Add(Me.pnlButtons)
        Me.Controls.Add(Me.cmbResolution)
        Me.Controls.Add(Me.cboAlt)
        Me.Controls.Add(Me.lblTitle)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("TopMost", Global.ScalA.My.MySettings.Default, "topmost", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScalA Beta"
        Me.TopMost = Global.ScalA.My.MySettings.Default.topmost
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.pnlButtons.ResumeLayout(False)
        CType(Me.pbZoom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStartup.ResumeLayout(False)
        Me.pnlStartup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pbZoom As PictureBox
    Friend WithEvents cboAlt As ComboBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents tmrTick As Timer
    Friend WithEvents btnMin As Button
    Friend WithEvents btnQuit As Button
    Friend WithEvents btnMax As Button
    Friend WithEvents pnlButtons As Panel
    Friend WithEvents cmbResolution As ComboBox
    Friend WithEvents chkDebug As CheckBox
    Friend WithEvents pnlStartup As Panel
    Friend WithEvents btnAlt3 As Button
    Friend WithEvents btnAlt2 As Button
    Friend WithEvents btnAlt1 As Button
    Friend WithEvents lblInfo As Label
    Friend WithEvents tmrStartup As Timer
    Friend WithEvents chkHideMessage As CheckBox
    Friend WithEvents btnAlt4 As Button
    Friend WithEvents btnStart As Button
    Friend WithEvents tmrHotkey As Timer
End Class
