Public Class frmSettings
    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        storeZoom = My.Settings.zoom
        Me.Owner = frmMain

        Me.TopMost = My.Settings.topmost
        chkTopMost.Checked = My.Settings.topmost

        chkAspect.Checked = My.Settings.lockAspect
        cmbAnchor.SelectedIndex = My.Settings.anchor

        numXoffset.Value = My.Settings.offset.X
        numYoffset.Value = My.Settings.offset.Y

    End Sub


    Dim storeZoom As Integer
    Private Sub chkDoAlign_CheckedChanged(sender As CheckBox, e As EventArgs) Handles chkDoAlign.CheckedChanged
        If sender.Checked AndAlso frmMain.AltPP Is Nothing Then
            MessageBox.Show(frmMain, "To perform alignment an alt needs to be selected.", "ScalA Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            sender.Checked = False
            Exit Sub
        End If
        grpAlign.Enabled = sender.Checked
        frmMain.tmrTick.Enabled = Not sender.Checked
        frmMain.cmbResolution.SelectedIndex = If(sender.Checked, 0, storeZoom)
        frmMain.updateThumb(If(sender.Checked, 122, 255))

        If sender.Checked Then
            frmMain.SetWindowPos(frmMain.AltPP.MainWindowHandle, frmMain.Handle, frmMain.newX, frmMain.newY, -1, -1, frmMain.SetWindowPosFlags.IgnoreResize + frmMain.SetWindowPosFlags.DoNotActivate)
            frmMain.GetWindowRect(frmMain.AltPP.MainWindowHandle, rcAstOffsetBase)
            Debug.Print(rcAstOffsetBase.ToString)
        End If
        tmrAlign.Enabled = sender.Checked
        chkDoAlign.Enabled = Not sender.Checked
    End Sub



    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.chkDoAlign.Checked = False
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'save settings
        Me.chkDoAlign.Checked = False
        My.Settings.topmost = chkTopMost.Checked
        My.Settings.offset = New Point(numXoffset.Value, numYoffset.Value)
        Me.Close()
    End Sub

    Dim rcAstOffsetBase As Rectangle
    Public ScalaMoved As Point
    Dim rcAstOffsetNew As Rectangle
    Private Sub tmrAlign_Tick(sender As Object, e As EventArgs) Handles tmrAlign.Tick
        frmMain.GetWindowRect(frmMain.AltPP.MainWindowHandle, rcAstOffsetNew)
        manualNumUpdate = False
        numXoffset.Value = My.Settings.offset.X + ScalaMoved.X - rcAstOffsetNew.Left + rcAstOffsetBase.Left
        numYoffset.Value = My.Settings.offset.Y + ScalaMoved.Y - rcAstOffsetNew.Top + rcAstOffsetBase.Top
        manualNumUpdate = True
    End Sub

    Public manualNumUpdate As Boolean = True

    Private Sub numXYoffsets_ValueChanged(sender As NumericUpDown, e As EventArgs) Handles numYoffset.ValueChanged, numXoffset.ValueChanged

        If manualNumUpdate Then
            Dim ptMove As Point = New Point(0, 0)
            If sender.Tag Then 'Y
                ptMove.Y += sender.Text - sender.Value
            Else 'X
                ptMove.X += sender.Text - sender.Value
            End If

            frmMain.SetWindowPos(frmMain.AltPP.MainWindowHandle, frmMain.Handle, rcAstOffsetNew.Left + ptMove.X, rcAstOffsetNew.Top + ptMove.Y, -1, -1, frmMain.SetWindowPosFlags.IgnoreResize + frmMain.SetWindowPosFlags.DoNotActivate)

        End If

    End Sub
End Class