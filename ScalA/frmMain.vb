Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Class frmMain

    Private zooms() As Size = {New Size(800, 600),
                               New Size(1200, 900),
                               New Size(1600, 1200),
                               New Size(2000, 1500),
                               New Size(2400, 1800),
                               New Size(2800, 2100),
                               New Size(3200, 2400),
                               New Size(3600, 2700),
                               New Size(4000, 3000),
                               New Size(4400, 3300)}
    Public AltPP As Process = Nothing
    Private WndClass() As String = {"MAINWNDMOAC", "䅍义乗䵄䅏C"}
#Region " Alt Dropdown "
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Sub GetClassName(ByVal hWnd As System.IntPtr, ByVal lpClassName As System.Text.StringBuilder, ByVal nMaxCount As Integer)
    End Sub
    Public Function GetWindowClass(ByVal hwnd As Long) As String
        Dim sClassName As New System.Text.StringBuilder("", 256)
        Call GetClassName(hwnd, sClassName, 256)
        Return sClassName.ToString
    End Function
    Private Sub popDropDown()
        Dim lstAst As New List(Of String)
        Dim Name As String = ""
        lstAst.Add("Someone")
        For Each p As Process In Process.GetProcesses
            If p.MainWindowTitle IsNot Nothing AndAlso
             WndClass.Contains(GetWindowClass(p.MainWindowHandle)) AndAlso
             Not p.MainWindowTitle.StartsWith("Someone") Then
                Name = p.Name
                lstAst.Add(Name)
                If Not cboAlt.Items.Contains(Name) Then
                    cboAlt.Items.Add(Name)
                End If
            End If
        Next
        ' clean ComboBox of idled clients
        Dim i As Integer = 1
        Do While i < cboAlt.Items.Count
            If Not lstAst.Contains(cboAlt.Items(i)) Then
                If cboAlt.SelectedIndex = i Then
                    cboAlt.SelectedIndex = 0
                End If
                cboAlt.Items.RemoveAt(i)
                i -= 1
            End If
            i += 1
        Loop
        lstAst = Nothing
        Name = Nothing
    End Sub
    Private Sub cboAlt_DropDown(sender As Object, e As EventArgs) Handles cboAlt.DropDown
        popDropDown()
    End Sub
#End Region
    Private Sub cboAlt_SelectedIndexChanged(sender As ComboBox, e As EventArgs) Handles cboAlt.SelectedIndexChanged
        Debug.Print("cboAlt_SelectedIndexChanged")

        If btnAlt1.Visible Then
            btnAlt1.Focus()
        ElseIf btnAlt2.Visible Then
            btnAlt2.Focus()
        End If

        For Each pair In startThumbsDict
            DwmUnregisterThumbnail(pair.Value)
        Next
        startThumbsDict.Clear()

        Debug.Print("restorePos")
        restorePos(AltPP)
        AltPP = Nothing
        If sender.SelectedIndex = 0 Then
            pnlStartup.Show()
            tmrStartup.Enabled = True
        Else
            pnlStartup.Hide()
            tmrStartup.Enabled = False
        End If

        Debug.Print("listProcesses")
        For Each pp In listProcesses()
            If pp.MainWindowTitle.StartsWith(sender.Text & " - ") Then
                AltPP = pp
            End If
        Next
        If AltPP IsNot Nothing Then
            Debug.Print("GetWindowRect")
            GetWindowRect(AltPP.MainWindowHandle, rcW)
            Debug.Print("GetClientRect")
            GetClientRect(AltPP.MainWindowHandle, rcC)

            Dim ptt As Point

            Debug.Print("ClientToScreen")
            ClientToScreen(AltPP.MainWindowHandle, ptt)

            AstClientOffset = New Size(ptt.X - rcW.Left, ptt.Y - rcW.Top)

            Debug.Print("updateTitle")
            updateTitle()

            newX = Me.Left + pbZoom.Left - AstClientOffset.Width - My.Settings.offset.X
            newY = Me.Top + pbZoom.Top - AstClientOffset.Height - My.Settings.offset.Y

            Debug.Print("SetWindowLong")
            Dim retv As Integer = 0
            Try
                retv = SetWindowLong(Me.Handle, GWL_HWNDPARENT, AltPP.MainWindowHandle) ' have Client always be beneath ScalA (set Scala to be owner of client)
                '                                                                  note SetParent() doesn't work.
            Catch ex As Exception
                Debug.Print("*** Exception on SetWindowLong ***")
                Debug.Print(ex.Message)
                Debug.Print("*** StackTrace ***")
                Debug.Print(ex.StackTrace)
            End Try
            Debug.Print("retv:" & retv)

            Debug.Print("SetWindowPos")
            SetWindowPos(AltPP.MainWindowHandle, Me.Handle, newX, newY, -1, -1, SetWindowPosFlags.IgnoreResize) ' + SetWindowPosFlags.DoNotActivate)

            Debug.Print("createThumb")
            createThumb()
            Debug.Print("updateThumb")
            updateThumb(255)
        Else
            Debug.Print("DwmUnregisterThumbnail")
            DwmUnregisterThumbnail(thumb)
        End If
        Debug.Print("tmrTick.Enabled")
        tmrTick.Enabled = True
        updateTitle()
    End Sub
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Public Shared Function SetParent(ByVal hWndChild As IntPtr, ByVal hWndNewParent As IntPtr) As IntPtr
    End Function
    Dim AstClientOffset As Size = New Size(0, 0)
    Public Sub restorePos(altPP As Process)
        If altPP.isRunning() Then
            SetWindowPos(altPP.MainWindowHandle, Me.Handle, rcW.Left, rcW.Top, -1, -1, SetWindowPosFlags.IgnoreResize + SetWindowPosFlags.DoNotActivate)
        End If
    End Sub

    Private Sub updateTitle()
        Dim titleSuff As String = String.Empty
        If AltPP.isRunning Then
            AltPP.Refresh()
            titleSuff = " - " & AltPP.MainWindowTitle
        End If
        Me.Text = "ScalA" & titleSuff
        With My.Application.Info.Version
            lblTitle.Text = "- ScalA v" & .Major & "." & .Minor & "." & .Build & titleSuff
        End With
    End Sub
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim args() As String = Environment.GetCommandLineArgs()
        Dim strAltName As String = ""
        Dim intFound As Integer = 0
        For Each p As Process In Process.GetProcesses
            If WndClass.Contains(GetWindowClass(p.MainWindowHandle)) Then
                strAltName = Strings.Left(p.MainWindowTitle, p.MainWindowTitle.IndexOf(" - "))
                If strAltName <> "Someone" Then
                    cboAlt.Items.Add(strAltName)
                    intFound += 1
                End If
            End If
        Next

        Debug.Print("location " & My.Settings.location.ToString)
        Me.Location = My.Settings.location
        Debug.Print("topmost " & My.Settings.topmost)
        Me.TopMost = My.Settings.topmost
        Me.chkHideMessage.Checked = My.Settings.hideMessage

        For Each ss As Size In zooms
            Debug.Print("Adding " & ss.ToString & " to reslist")
            cmbResolution.Items.Add(ss.Width & "x" & ss.Height)
        Next

        cmbResolution.SelectedIndex = My.Settings.zoom

        If args.Count > 1 Then
            cboAlt.SelectedItem = args(1)
        Else
            cboAlt.SelectedIndex = If(intFound = 1, 1, 0)
        End If
        Debug.Print("doSysMenu")
        doSysMenu()
        Debug.Print("updateTitle")
        updateTitle()
#If DEBUG Then
        chkDebug.Visible = True
#End If
    End Sub

    <DllImport("user32.dll")>
    Private Shared Function SetWindowLong(ByVal hwnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As UInteger) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowLong(ByVal hwnd As IntPtr, ByVal nIndex As Integer) As UInteger
    End Function
    Const GWL_STYLE As Integer = -16
    Const GWL_HWNDPARENT As Integer = -8

    Const WS_SYSMENU As UInteger = &H80000L
    Const WS_MINIMIZEBOX As UInteger = &H20000L
    Const WS_MAXIMIZEBOX As UInteger = &H10000L
    Const WS_SIZEBOX As UInteger = &H40000L 'bugs out reZoom
    Const WS_POPUP As UInteger = &H80000000L
    Const WS_DLGFRAME As UInteger = &H400000L
    Const WS_BORDER As UInteger = &H800000L 'buggy

#Region " DWMapi "
    <Flags()> Public Enum DwmThumbnailFlags As UInteger
        DWM_TNP_RECTDESTINATION = &H1
        DWM_TNP_RECTSOURCE = &H2
        DWM_TNP_OPACITY = &H4
        DWM_TNP_VISIBLE = &H8
        DWM_TNP_SOURCECLIENTAREAONLY = &H10
    End Enum

    <StructLayout(LayoutKind.Sequential)>
    Public Structure DWM_THUMBNAIL_PROPERTIES
        Public dwFlags As DwmThumbnailFlags
        Public rcDestination As Rectangle
        Public rcSource As Rectangle
        Public opacity As Byte
        Public fVisible As Boolean
        Public fSourceClientAreaOnly As Boolean
    End Structure

    Declare Function DwmRegisterThumbnail Lib "dwmapi.dll" (ByVal Dest As IntPtr, ByVal Src As IntPtr, ByRef Thumb As IntPtr) As Integer
    Public Declare Function DwmUpdateThumbnailProperties Lib "dwmapi.dll" (ByVal hThumbnail As IntPtr, ByRef props As DWM_THUMBNAIL_PROPERTIES) As Integer
    Public Declare Function DwmUnregisterThumbnail Lib "dwmapi.dll" (ByVal Thumb As IntPtr) As Integer
#End Region

#Region " Thumb "
    Dim thumb As IntPtr = IntPtr.Zero


    Private Sub createThumb()

        If AltPP IsNot Nothing Then
            DwmUnregisterThumbnail(thumb)
            DwmRegisterThumbnail(Me.Handle, AltPP.MainWindowHandle, thumb)
        End If

    End Sub

    Public Sub updateThumb(opacity As Byte)
        Dim twp As DWM_THUMBNAIL_PROPERTIES
        twp.dwFlags = DwmThumbnailFlags.DWM_TNP_OPACITY + DwmThumbnailFlags.DWM_TNP_RECTDESTINATION + DwmThumbnailFlags.DWM_TNP_SOURCECLIENTAREAONLY + DwmThumbnailFlags.DWM_TNP_VISIBLE
        twp.opacity = opacity
        twp.fVisible = True
        twp.rcDestination = New Rectangle(pbZoom.Left, pbZoom.Top, pbZoom.Right, pbZoom.Bottom)
        twp.fSourceClientAreaOnly = True

        DwmUpdateThumbnailProperties(thumb, twp)
    End Sub
#End Region

    Public Function listProcesses() As List(Of Process)
        Dim lst As List(Of Process) = New List(Of Process)()
        For Each pp As Process In Process.GetProcesses
            If pp.MainWindowTitle IsNot Nothing AndAlso
             Not pp.MainWindowTitle.StartsWith("Someone") AndAlso
             WndClass.Contains(GetWindowClass(pp.MainWindowHandle)) Then
                lst.Add(pp)
            End If
        Next
        Return lst
    End Function
#Region " Move Self "

#If 0 Then
    Private Sub MoveSelf(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown, lblTitle.MouseDown
        If e.Button = MouseButtons.Left Then
            Me.Capture = False   'change this to the control you will use to move the window
            Me.lblTitle.Capture = False
            ' Create and send a WM_NCLBUTTONDOWN message.
            Const WM_NCLBUTTONDOWN As Integer = &HA1S
            Const HTCAPTION As Integer = 2
            Dim msg As Message =
                Message.Create(Me.Handle, WM_NCLBUTTONDOWN,
                    New IntPtr(HTCAPTION), IntPtr.Zero)
            Me.DefWndProc(msg)
        End If

    End Sub
#Else
    Private MovingForm As Boolean
    Private MoveForm_MousePosition As Point

    Public Sub MoveForm_MouseDown(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseDown, lblTitle.MouseDown ' Add more handles here (Example: PictureBox1.MouseDown)
        'Me.TopMost = True
        If Me.WindowState <> FormWindowState.Maximized AndAlso e.Button = MouseButtons.Left Then
            MovingForm = True
            MoveForm_MousePosition = e.Location
        End If
    End Sub

    Public Sub MoveForm_MouseMove(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseMove, lblTitle.MouseMove ' Add more handles here (Example: PictureBox1.MouseMove)
        If MovingForm Then
            If AltPP IsNot Nothing AndAlso Not frmSettings.chkDoAlign.Checked Then
                newX = Me.Left + pbZoom.Left - AstClientOffset.Width - My.Settings.offset.X
                newY = Me.Top + pbZoom.Top - AstClientOffset.Height - My.Settings.offset.Y
                SetWindowPos(AltPP.MainWindowHandle, Me.Handle, newX, newY, -1, -1, SetWindowPosFlags.IgnoreResize + SetWindowPosFlags.ASyncWindowPosition) ' + SetWindowPosFlags.DoNotActivate)
            End If
            Dim newoffset As Point = e.Location - MoveForm_MousePosition
            Me.Location = Me.Location + newoffset
            If frmSettings.chkDoAlign.Checked Then
                frmSettings.ScalaMoved += newoffset
            End If
        End If
    End Sub

    Public Sub MoveForm_MouseUp(sender As Object, e As MouseEventArgs) Handles _
    MyBase.MouseUp, lblTitle.MouseUp  ' Add more handles here (Example: PictureBox1.MouseUp)

        If e.Button = MouseButtons.Left Then
            MovingForm = False
            If AltPP.isRunning Then
                Debug.Print("Mouseup")
                'newX = Me.Left + pbZoom.Left - AstClientOffset.Width
                'newY = Me.Top + pbZoom.Top - AstClientOffset.Height
                'SetWindowPos(AltPP.MainWindowHandle, Me.Handle, newX, newY, -1, -1, SetWindowPosFlags.IgnoreResize) ' + SetWindowPosFlags.DoNotActivate)
            End If
        End If
    End Sub
#End If
#End Region


    Private Sub frmMain_Closing(sender As Object, e As EventArgs) Handles Me.Closing
        restorePos(AltPP)
        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.location = Me.Location
        End If
        Hotkey.unregHotkey(Me)
    End Sub

    <DllImport("user32.dll")>
    Public Shared Function GetWindowRect(ByVal hWnd As IntPtr, ByRef lpRect As Rectangle) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetClientRect(ByVal hWnd As IntPtr, ByRef lpRect As Rectangle) As Boolean
    End Function
    <DllImport("user32.dll")>
    Private Shared Function ClientToScreen(ByVal hWnd As IntPtr, ByRef lpPoint As Point) As Boolean
    End Function
    <DllImport("user32.dll")>
    Public Shared Function GetForegroundWindow() As IntPtr
    End Function
    <DllImport("user32.dll")>
    Public Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, <Out()> ByRef lpdwProcessId As UInteger) As UInteger
    End Function
#Region " SetWindowPos "
    <Flags>
    Public Enum SetWindowPosFlags As UInteger
        ''' <summary>If the calling thread and the thread that owns the window are attached to different input queues,
        ''' the system posts the request to the thread that owns the window. This prevents the calling thread from
        ''' blocking its execution while other threads process the request.</summary>
        ''' <remarks>SWP_ASYNCWINDOWPOS</remarks>
        ASyncWindowPosition = &H4000
        ''' <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        ''' <remarks>SWP_DEFERERASE</remarks>
        DeferErase = &H2000
        ''' <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        ''' <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = &H20
        ''' <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
        ''' the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
        ''' is sent only when the window's size is being changed.</summary>
        ''' <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = &H20
        ''' <summary>Hides the window.</summary>
        ''' <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = &H80
        ''' <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
        ''' top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        ''' parameter).</summary>
        ''' <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = &H10
        ''' <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
        ''' contents of the client area are saved and copied back into the client area after the window is sized or
        ''' repositioned.</summary>
        ''' <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = &H100
        ''' <summary>Retains the current position (ignores X and Y parameters).</summary>
        ''' <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = &H2
        ''' <summary>Does not change the owner window's position in the Z order.</summary>
        ''' <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = &H200
        ''' <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
        ''' the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
        ''' window uncovered as a result of the window being moved. When this flag is set, the application must
        ''' explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        ''' <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = &H8
        ''' <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        ''' <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = &H200
        ''' <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        ''' <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = &H400
        ''' <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        ''' <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = &H1
        ''' <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        ''' <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = &H4
        ''' <summary>Displays the window.</summary>
        ''' <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = &H40
    End Enum
    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As SetWindowPosFlags) As Boolean
    End Function
#End Region

    Public Function GetActiveProcessID() As UInteger
        Dim hWnd As IntPtr = GetForegroundWindow()
        Dim ProcessID As UInteger = 0

        GetWindowThreadProcessId(hWnd, ProcessID)

        Return ProcessID
    End Function

    Dim rcW As Rectangle ' windowrect
    Dim rcC As Rectangle ' clientrect
    Public newX As Integer
    Public newY As Integer

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles tmrTick.Tick
        updateTitle()


        If AltPP Is Nothing OrElse AltPP.HasExited Then
            tmrTick.Enabled = False
            cboAlt.SelectedIndex = 0
            Exit Sub
        End If
        If Me.WindowState = FormWindowState.Minimized Then
            Exit Sub
        End If

        If pbZoom.Contains(MousePosition) Then

            Dim ptZ As Point = pbZoom.Location
            ClientToScreen(Me.Handle, ptZ)
            newX = MousePosition.X.Map(ptZ.X, ptZ.X + pbZoom.Width, ptZ.X, ptZ.X + pbZoom.Width - rcC.Width) - AstClientOffset.Width - My.Settings.offset.X
            newY = MousePosition.Y.Map(ptZ.Y, ptZ.Y + pbZoom.Height, ptZ.Y, ptZ.Y + pbZoom.Height - rcC.Height) - AstClientOffset.Height - My.Settings.offset.Y
        End If
        If Me.WindowState <> FormWindowState.Minimized Then
            SetWindowPos(AltPP.MainWindowHandle, Me.Handle, newX, newY, -1, -1, SetWindowPosFlags.IgnoreResize + SetWindowPosFlags.DoNotActivate + SetWindowPosFlags.ASyncWindowPosition)
        End If

    End Sub

    Public Sub cmbResolution_SelectedIndexChanged(sender As ComboBox, e As EventArgs) Handles cmbResolution.SelectedIndexChanged
        Debug.Print("cboResolution_SelectedIndexChanged")
        My.Settings.zoom = sender.SelectedIndex
        If AltPP.isRunning Then
            newX = Me.Left + pbZoom.Left - AstClientOffset.Width - My.Settings.offset.X
            newY = Me.Top + pbZoom.Top - AstClientOffset.Height - My.Settings.offset.Y
            Debug.Print("SetWindowPos")
            SetWindowPos(AltPP.MainWindowHandle, Me.Handle, newX, newY, -1, -1, SetWindowPosFlags.IgnoreResize + SetWindowPosFlags.DoNotActivate)
        End If
        Debug.Print("reZoom")
        reZoom(zooms(My.Settings.zoom))
    End Sub

    Private Sub reZoom(newSize As Size)
        If Me.WindowState <> FormWindowState.Maximized Then
            Me.Size = New Size(newSize.Width + 2, newSize.Height + 28)
            pbZoom.Left = 1
            pbZoom.Size = newSize
            cmbResolution.Enabled = True
        Else
            pbZoom.Left = 0
            pbZoom.Width = newSize.Width + 1
            pbZoom.Height = newSize.Height - 27
            cmbResolution.Enabled = False
        End If
        updateThumb(255)
    End Sub



#Region " SysMenu "

    Dim hSysMenu As IntPtr = GetSystemMenu(Me.Handle, False)
    Public Sub doSysMenu()
        Debug.Print("SetWindowLong")
        SetWindowLong(Me.Handle, GWL_STYLE, GetWindowLong(Handle, GWL_STYLE) Or WS_SYSMENU Or WS_MINIMIZEBOX) 'Enable SysMenu and MinimizeBox 
        If hSysMenu Then
            ModifyMenuA(hSysMenu, SC_CLOSE, MF_BYCOMMAND, SC_CLOSE, "&Close") 'remove Alt-F4
            SetMenuItemBitmaps(hSysMenu, SC_CLOSE, MF_BYCOMMAND, 5, IntPtr.Zero) 're-add close icon
            SetMenuDefaultItem(hSysMenu, SC_CLOSE, MF_BYCOMMAND)
            RemoveMenu(hSysMenu, SC_SIZE, MF_BYCOMMAND) 'remove size menuitem
            InsertMenuA(hSysMenu, 0, MF_SEPARATOR Or MF_BYPOSITION, 0, String.Empty)
            InsertMenuA(hSysMenu, 0, MF_BYPOSITION, 666, "Settings")
        End If
    End Sub

    Public Sub ShowSysMenu(sender As Control, e As MouseEventArgs) Handles Me.MouseUp, lblTitle.MouseUp, btnMin.MouseUp, btnMax.MouseUp
        If e.Button = MouseButtons.Right Then
            Debug.Print("ShowSysMenu")
            Dim cmd As Integer = TrackPopupMenuEx(hSysMenu, &H100L, MousePosition.X, MousePosition.Y, Me.Handle, IntPtr.Zero)
            If cmd > 0 Then
                Debug.Print("SendMessage " & cmd)
                SendMessage(Me.Handle, WM_SYSCOMMAND, cmd, IntPtr.Zero)
            End If
        End If
    End Sub



    Private Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Integer, ByVal bRevert As Integer) As Integer
    Private Declare Function ModifyMenuA Lib "user32" (hMenu As Integer, uItem As Integer, fByPos As Integer, newID As Integer, lpNewIem As String) As Boolean
    Private Declare Function SetMenuItemBitmaps Lib "user32" (hMenu As Integer, uitem As Integer, fByPos As Integer, hBitmapUnchecked As Integer, hBitmapChecked As Integer) As Boolean

    Private Declare Function InsertMenuA Lib "user32" (ByVal hMenu As Integer, ByVal nPosition As Integer, ByVal wFlags As Integer, uIDNewItem As Integer, lpNewItem As String) As Boolean
    Private Declare Function RemoveMenu Lib "user32" (ByVal hMenu As Integer, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
    Private Declare Function SetMenuDefaultItem Lib "user32" (hMenu As Integer, uItem As Integer, fByPos As Integer) As Boolean

    Const MF_STRING = &H0
    Const MF_SEPARATOR = &H800
    Const MF_REMOVE = &H1000&

    Const MF_BYCOMMAND = &H0
    Const MF_BYPOSITION = &H400

    Const WM_SYSCOMMAND = &H112

    Const SC_SIZE As Integer = &HF000
    Const SC_MOVE As Integer = &HF010
    Const SC_MINIMIZE As Integer = &HF020
    Const SC_MAXIMIZE As Integer = &HF030
    Const SC_CLOSE As Integer = &HF060
    Const SC_RESTORE As Integer = &HF120

    <DllImport("User32.Dll")>
    Private Shared Function TrackPopupMenuEx(ByVal hmenu As IntPtr, ByVal fuFlags As UInt32, ByVal x As Integer, ByVal y As Integer, ByVal hwnd As IntPtr, ByVal lptpm As Integer) As Integer
    End Function
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    End Function
#End Region

    Dim wasMaximized As Boolean = False
    Dim minByMenu As Boolean = False

    Const WM_GETMINMAXINFO = &H24
    <StructLayout(LayoutKind.Sequential)>
    Private Structure MINMAXINFO
        Dim ptReserved As Point
        Dim ptMaxSize As Point
        Dim ptMaxPosition As Point
        Dim ptMinTrackSize As Point
        Dim ptMaxTrackSize As Point
    End Structure
    Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (<[In](), MarshalAs(UnmanagedType.I4)> hpvDest As IntPtr, <[In](), Out()> hpvSource As IntPtr, ByVal cbCopy As Long)

    Protected Overrides Sub WndProc(ByRef m As Message)
        'If m.Msg = &H2E0 Then
        '    Debug.Print("&H2E0")
        '    Dim b As Rectangle = Screen.FromControl(Me).WorkingArea ' //where mForm Is Application.OpenForms[0], this Is specific to my applications use case.
        '    b.X = 0 '; //The bounds will Try To base it On Monitor 1 For example, 1920x1080, To another 1920x1080 monitor On right, will Set X To 1080, making it show up On the monitor On right, outside its bounds.
        '    b.Y = 0 '; //same As b.X
        '    MaximizedBounds = b '; //Apply it To MaximizedBounds
        '    m.Result = New IntPtr(0) '; //Tell WndProc it did stuff

        'Else


        Select Case m.Msg
            Case WM_GETMINMAXINFO
                Debug.Print("WM_GETMINMAXINFO")
                If Me.WindowState = FormWindowState.Maximized Then
                    'Dim mmi As MINMAXINFO = m.GetLParam(mmi.GetType)
                    'Debug.Print("mmi: MaxSize " & mmi.ptMaxSize.ToString & " MaxPos " & mmi.ptMaxPosition.ToString & " minTsize " & mmi.ptMinTrackSize.ToString & " maxTsize " & mmi.ptMaxTrackSize.ToString)
                    Debug.Print("maxBound: " & Me.MaximizedBounds.ToString)
                    Debug.Print("scrn.bounds: " & Screen.GetBounds(Me).ToString)
                    Debug.Print("scrn.workarea: " & Screen.GetWorkingArea(Me).ToString)
                    Dim bounds As Rectangle = Screen.GetBounds(Me)
                    Dim workingarea As Rectangle = Screen.GetWorkingArea(Me)

                    'mmi.ptMaxPosition.X = Math.Abs(workingarea.Left - bounds.Left)
                    'mmi.ptMaxPosition.Y = Math.Abs(workingarea.Top - bounds.Top)
                    'mmi.ptMaxSize.X = workingarea.Width
                    'mmi.ptMaxSize.Y = workingarea.Height
                    Dim newMB As Rectangle = New Rectangle(workingarea.Left - bounds.Left, workingarea.Top - bounds.Top, workingarea.Width, workingarea.Height)
                    Debug.Print("newMaxBound " & newMB.ToString)

                    'If Me.Size <> newMB.Size Then
                    '    Me.MaximizedBounds = newMB
                    '    SetWindowPos(Me.Handle, vbNull, newMB.X, newMB.Y, newMB.Width, newMB.Height, SetWindowPosFlags.ASyncWindowPosition Or SetWindowPosFlags.DoNotSendChangingEvent)
                    '    reZoom(newMB.Size)
                    'End If
                    'Marshal.StructureToPtr(mmi, m.LParam, True)

                End If
            Case Hotkey.WM_HOTKEY
                If m.WParam = 1 Then
                    'only preform switch when astonia or scala Is active
                    Dim activeID = GetActiveProcessID()
                    Debug.Print("aID " & activeID & " selfPID " & scalaPID)
                    If WndClass.Contains(GetWindowClass(Process.GetProcessById(activeID).MainWindowHandle)) OrElse activeID = scalaPID Then
                        btnStart.PerformClick()
                    End If
                End If
            Case WM_SYSCOMMAND
                Select Case m.WParam
                    Case SC_RESTORE
                        Debug.Print("SC_RESTORE")
                        Debug.Print("wasMax " & wasMaximized)
                        If minByMenu Then
                            Me.Location = restoreLoc
                        End If
                    Case SC_MAXIMIZE
                        Debug.Print("SC_MAXIMIZE " & m.LParam.ToString)
                        Debug.Print("wasMax " & wasMaximized)
                        btnMax.PerformClick()
                        wasMaximized = True
                        Debug.Print("wasMax " & wasMaximized)
                        'm = Message.Create(m.HWnd, m.Msg, SC_RESTORE, m.LParam) 'Replace Maximize event for now
                        'Exit Sub
                        m.Result = 0
                    Case SC_MINIMIZE
                        Debug.Print("SC_MINIMIZE")
                        minByMenu = True
                        wasMaximized = (WindowState = FormWindowState.Maximized)
                        If Not wasMaximized Then
                            restoreLoc = Me.Location
                        End If
                        Debug.Print("wasMax " & wasMaximized)
                        restorePos(AltPP)
                    Case 666
                        frmSettings.Show()
                End Select


        End Select




        MyBase.WndProc(m)  ' allow form to process this message
    End Sub

    Private Sub chkDebug_CheckedChanged(sender As CheckBox, e As EventArgs) Handles chkDebug.CheckedChanged
        Debug.Print(Screen.GetWorkingArea(sender).ToString)
        updateThumb(If(sender.Checked, 122, 255))
    End Sub


    Dim startThumbsDict As Dictionary(Of String, IntPtr) = New Dictionary(Of String, IntPtr)
    Dim opaDict As Dictionary(Of String, Byte) = New Dictionary(Of String, Byte)
    Const dimmed As Byte = 230
    Private Sub tmrStartup_Tick(sender As Timer, e As EventArgs) Handles tmrStartup.Tick
        Dim i As Integer = 0
        Dim alts As List(Of Process) = listProcesses()
        Dim activeNameList As List(Of String) = New List(Of String)

        For Each ctrl As Button In pnlStartup.Controls.OfType(Of Button)
            If Not ctrl.Visible Then
                Continue For
            End If
            If i < alts.Count Then
                ctrl.Text = alts(i).Name
                activeNameList.Add(alts(i).Name)
                If Not startThumbsDict.ContainsKey(alts(i).Name) Then
                    startThumbsDict(alts(i).Name) = IntPtr.Zero
                    DwmRegisterThumbnail(Me.Handle, alts(i).MainWindowHandle, startThumbsDict(alts(i).Name))
                    Debug.Print("registered thumb " & startThumbsDict(alts(i).Name).ToString)
                End If
                Dim prp As DWM_THUMBNAIL_PROPERTIES = New DWM_THUMBNAIL_PROPERTIES
                With prp
                    .dwFlags = DwmThumbnailFlags.DWM_TNP_OPACITY Or DwmThumbnailFlags.DWM_TNP_SOURCECLIENTAREAONLY Or DwmThumbnailFlags.DWM_TNP_VISIBLE Or DwmThumbnailFlags.DWM_TNP_RECTDESTINATION
                    'Dim clear As Integer
                    'opa.TryGetValue(alts(i).Name, clear)
                    'Debug.Print("opa(" & alts(i).Name & ")=" & clear)
                    '.opacity = If(clear = 0, 230, clear)
                    .opacity = opaDict.GetValueOrDefault(alts(i).Name, dimmed)
                    .fSourceClientAreaOnly = True
                    .fVisible = True
                    .rcDestination = New Rectangle(pnlStartup.Left + ctrl.Left + 3, pnlStartup.Top + ctrl.Top + 20, ctrl.Right - 2, pnlStartup.Top + ctrl.Bottom - 3)
                End With
                DwmUpdateThumbnailProperties(startThumbsDict(alts(i).Name), prp)
            Else
                ctrl.Text = ""
            End If
            i += 1
        Next
        Dim purgeList As List(Of String) = New List(Of String)
        For Each name As String In startThumbsDict.Keys
            If Not activeNameList.Contains(name) Then
                purgeList.Add(name)
            End If
        Next
        For Each name As String In purgeList
            DwmUnregisterThumbnail(startThumbsDict(name))
            startThumbsDict.Remove(name)
            For Each ctrl As Button In pnlStartup.Controls.OfType(Of Button)
                If ctrl.Text = name Then
                    ctrl.Text = String.Empty
                End If
            Next
        Next
    End Sub

    Private Sub btnQuit_MouseEnter(sender As Button, e As EventArgs) Handles btnQuit.MouseEnter
        sender.ForeColor = SystemColors.Control
    End Sub

    Private Sub btnQuit_MouseLeave(sender As Button, e As EventArgs) Handles btnQuit.MouseLeave
        sender.ForeColor = SystemColors.ControlText
    End Sub
    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub btnMin_Click(sender As Object, e As EventArgs) Handles btnMin.Click
        Debug.Print("btnMin_Click")
        wasMaximized = Me.WindowState = FormWindowState.Maximized
        If Not wasMaximized Then
            restoreLoc = Me.Location
        End If
        Me.WindowState = FormWindowState.Minimized

        restorePos(AltPP)
    End Sub
    Private Sub btnAlt_Click(sender As Button, e As EventArgs) Handles btnAlt1.Click, btnAlt2.Click, btnAlt3.Click, btnAlt4.Click
        tmrStartup.Enabled = False
        popDropDown()
        cboAlt.SelectedItem = sender.Text
    End Sub
    Private Sub btnAlt_MouseEnter(sender As Button, e As EventArgs) Handles btnAlt1.MouseEnter, btnAlt2.MouseEnter, btnAlt3.MouseEnter, btnAlt4.MouseEnter
        opaDict(sender.Text) = &HFF
    End Sub
    Private Sub btnAlt_MouseLeave(sender As Button, e As EventArgs) Handles btnAlt1.MouseLeave, btnAlt2.MouseLeave, btnAlt3.MouseLeave, btnAlt4.MouseLeave
        opaDict(sender.Text) = dimmed
    End Sub

    Private Sub chkHideMessage_CheckedChanged(sender As CheckBox, e As EventArgs) Handles chkHideMessage.CheckedChanged
        Debug.Print("chkHideMessage " & sender.Checked)
        If sender.Checked Then
            btnAlt1.Visible = True
            lblInfo.Hide()
            chkHideMessage.Hide()
            My.Settings.hideMessage = True
        Else
            'btnAlt4.Visible = False
            'lblInfo.Show()
            'chkHideMessage.Show()
        End If
    End Sub

    Dim restoreLoc As Point
    Private Sub btnMax_Click(sender As Object, e As EventArgs) Handles btnMax.Click
        Debug.Print("btnMax_Click")
        '🗖,🗗,⧠
        If Me.WindowState <> FormWindowState.Maximized Then
            'go maximized
            sender.text = "🗗"
            For Each scrn As Screen In Screen.AllScreens
                If scrn.Bounds.Contains(MousePosition) Then
                    Debug.Print("screen workarea " & scrn.WorkingArea.ToString)
                    Debug.Print("screen bounds " & scrn.Bounds.ToString)
                    Me.MaximizedBounds = New Rectangle(scrn.WorkingArea.Left - scrn.Bounds.Left, scrn.WorkingArea.Top - scrn.Bounds.Top, scrn.WorkingArea.Width, scrn.WorkingArea.Height)
                    Debug.Print("new maxbound " & MaximizedBounds.ToString)
                    restoreLoc = Me.Location
                    Me.Location = scrn.WorkingArea.Location
                    Me.WindowState = FormWindowState.Maximized
                    Exit For
                End If
            Next
            reZoom(New Size(Me.MaximizedBounds.Width, Me.MaximizedBounds.Height))
        Else 'go normal
            Me.WindowState = FormWindowState.Normal
            sender.text = "⧠"
            Me.Location = restoreLoc
            reZoom(zooms(cmbResolution.SelectedIndex))
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        cboAlt.SelectedIndex = 0
        If btnAlt1.Visible Then
            btnAlt1.Focus()
        ElseIf btnAlt2.Visible Then
            btnAlt2.Focus()
        End If
    End Sub

    Dim scalaPID = Process.GetCurrentProcess().Id
    ' todo: move to thread
    Private Sub tmrHotkey_Tick(sender As Object, e As EventArgs) Handles tmrHotkey.Tick
        Dim activeID = GetActiveProcessID()

        If WndClass.Contains(GetWindowClass(Process.GetProcessById(activeID).MainWindowHandle)) OrElse activeID = scalaPID Then
            Hotkey.registerHotkey(Me, 1, Hotkey.KeyModifier.Control, Keys.Tab)
        Else
            Hotkey.unregHotkey(Me, 1)
        End If
    End Sub

    Private Sub frmMain_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick, lblTitle.MouseDoubleClick
        Debug.Print("frmMain_MouseDoubleClick")
        btnMax.PerformClick()
    End Sub

    Private Sub frmMain_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        Debug.Print("ResizeEnd")
    End Sub


    Public Sub runAsAdmin()
        Dim procStartInfo As New ProcessStartInfo

        With procStartInfo
            .UseShellExecute = True
            .FileName = Environment.GetCommandLineArgs()(0)
            .Arguments = """" & Me.cboAlt.SelectedItem & """"
            .WindowStyle = ProcessWindowStyle.Normal
            .Verb = "runas" 'add this to prompt for elevation
        End With

        If Me.WindowState = FormWindowState.Normal Then
            My.Settings.location = Me.Location
        End If
        My.Settings.Save()

        Try
            Process.Start(procStartInfo).WaitForInputIdle()
        Catch e As System.ComponentModel.Win32Exception
            'operation cancelled
        Catch e As InvalidOperationException
            'wait for inputidle is needed
        Catch e As Exception
            Throw e
        End Try

    End Sub


End Class


Public Class Hotkey

#Region "Declarations - WinAPI, Hotkey constant and Modifier Enum"
    ''' <summary>
    ''' Declaration of winAPI function wrappers. The winAPI functions are used to register / unregister a hotkey
    ''' </summary>
    Private Declare Function RegisterHotKey Lib "user32" _
        (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer

    Private Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer

    Public Const WM_HOTKEY As Integer = &H312

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
        NoRepeat = &H4000
    End Enum 'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.
#End Region
    '
    ' handle kotkey in wndProc
#Region "Hotkey registration, unregistration"
    Private Shared _HotkeyList As List(Of Integer) = New List(Of Integer)
    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal hotkeyID As Integer, ByVal modifier As KeyModifier, ByVal triggerKey As Keys)
        Try
            If Not _HotkeyList.Contains(hotkeyID) Then
                RegisterHotKey(sourceForm.Handle, hotkeyID, modifier, triggerKey)
                _HotkeyList.Add(hotkeyID)
            End If
        Catch ex As Exception
            Debug.Print("registerHotkey failed")
            UnregisterHotKey(sourceForm.Handle, hotkeyID)
        End Try
    End Sub

    Public Shared Sub unregHotkey(ByRef sourceForm As Form, Optional ByRef hotKeyID As Integer = 0)
        'Debug.Print("Unregister Hotkey " & hotKeyID)
        If hotKeyID = 0 Then
            For Each id In _HotkeyList
                UnregisterHotKey(sourceForm.Handle, id)  'Remember to call unregisterHotkeys() when closing your application.
            Next
            _HotkeyList.Clear()
            Exit Sub
        End If
        If _HotkeyList.Contains(hotKeyID) Then
            Debug.Print("unregisterHotkey " & hotKeyID)
            UnregisterHotKey(sourceForm.Handle, hotKeyID)  'Remember to call unregisterHotkeys() when closing your application.
            _HotkeyList.Remove(hotKeyID)
        End If
    End Sub
#End Region


End Class

Module Extensions
    <Extension()>
    Public Function GetValueOrDefault(Of TKey, TValue)(source As Dictionary(Of TKey, TValue), Key As TKey, Optional defaultValue As TValue = CType(Nothing, TValue)) As TValue
        Dim found As TValue
        If source.TryGetValue(Key, found) Then
            Return found
        Else
            Return defaultValue
        End If
    End Function
    <Extension()>
    Public Function isRunning(AltPP As Process) As Boolean
        Try
            Return AltPP IsNot Nothing AndAlso Not AltPP.HasExited
        Catch e As Exception
            frmMain.runAsAdmin()
            End
        End Try
    End Function
    <Extension()>
    Public Function Name(altPP As Process) As String
        Try
            Return Strings.Left(altPP.MainWindowTitle, altPP.MainWindowTitle.IndexOf(" - "))
        Catch
            Return String.Empty
        End Try
    End Function
    <Extension()>
    Public Function Contains(ctrl As Control, screenPt As Point) As Boolean
        Return New Rectangle(ctrl.Location, ctrl.Size).Contains(ctrl.FindForm.PointToClient(screenPt))
    End Function
    <Extension()>
    Public Function Map(this As Integer, fromMin As Integer, fromMax As Integer, toMin As Integer, toMax As Integer) As Integer
        Return toMin + ((this - fromMin) * (toMax - toMin) / (fromMax - fromMin))
    End Function
End Module