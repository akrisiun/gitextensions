# C# Git Extensions project clone

Debug build, optimized for touch screen, net451
```
cd gitextensions
git submodule update --init
GitExtensions.VS2015.build.cmd
```
## MONO macOS problem

```
$ mono -V
Mono JIT compiler version 5.0.1.1 (2017-02/5077205 Thu May 18 16:11:37 EDT 2017)
Copyright (C) 2002-2014 Novell, Inc, Xamarin Inc and Contributors. www.mono-project.com
        TLS:           normal
        SIGSEGV:       altstack
        Notification:  kqueue
        Architecture:  x86
        Disabled:      none
        Misc:          softdebug
        LLVM:          yes(3.6.0svn-mono-master/8b1520c)
        GC:            sgen (concurrent by default)

$ mono GitExtensions/bin/Debug/GitExtensions.exe
System.ArgumentException: A null reference or invalid value was found [GDI+ status: InvalidParameter]
  at System.Drawing.GDIPlus.CheckStatus (System.Drawing.Status status) [0x00098] in <fe4961a9ba4b4f60ab173107fe5721c9>:0
  at System.Drawing.Drawing2D.LinearGradientBrush..ctor (System.Drawing.Rectangle rect, System.Drawing.Color color1, System.Drawing.Color color2, System.Drawing.Drawing2D.LinearGradientMode linearGradientMode) [0x00024] in <fe4961a9ba4b4f60ab173107fe5721c9>:0
  at (wrapper remoting-invoke-with-check) System.Drawing.Drawing2D.LinearGradientBrush:.ctor (System.Drawing.Rectangle,System.Drawing.Color,System.Drawing.Color,System.Drawing.Drawing2D.LinearGradientMode)
  at System.Windows.Forms.ToolStripProfessionalRenderer.OnRenderToolStripPanelBackground (System.Windows.Forms.ToolStripPanelRenderEventArgs e) [0x0002c] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ToolStripRenderer.DrawToolStripPanelBackground (System.Windows.Forms.ToolStripPanelRenderEventArgs e) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ToolStripPanel.OnPaintBackground (System.Windows.Forms.PaintEventArgs e) [0x00019] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control.WmPaint (System.Windows.Forms.Message& m) [0x0005e] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control.WndProc (System.Windows.Forms.Message& m) [0x001a4] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ScrollableControl.WndProc (System.Windows.Forms.Message& m) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ContainerControl.WndProc (System.Windows.Forms.Message& m) [0x00029] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control+ControlWindowTarget.OnMessage (System.Windows.Forms.Message& m) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control+ControlNativeWindow.WndProc (System.Windows.Forms.Message& m) [0x0000b] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.NativeWindow.WndProc (System.IntPtr hWnd, System.Windows.Forms.Msg msg, System.IntPtr wParam, System.IntPtr lParam) [0x00085]in <6673d7101e044ca5890b94a28ed667e8>:0
System.ObjectDisposedException: Cannot access a disposed object.
Object name: 'GitUI.CommandsDialogs.FormCommit'.
  at System.Windows.Forms.Control.CreateHandle () [0x00013] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Form.CreateHandle () [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control.get_Handle () [0x00022] in <6673d7101e044ca5890b94a28ed667e8>:0
  at (wrapper remoting-invoke-with-check) System.Windows.Forms.Control:get_Handle ()
  at System.Windows.Forms.Application.RunLoop (System.Boolean Modal, System.Windows.Forms.ApplicationContext context) [0x00090] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Form.ShowDialog (System.Windows.Forms.IWin32Window owner) [0x001b7] in <6673d7101e044ca5890b94a28ed667e8>:0
  at (wrapper remoting-invoke-with-check) System.Windows.Forms.Form:ShowDialog (System.Windows.Forms.IWin32Window)
  at GitUI.GitUICommands+<>c__DisplayClass328_0.<StartCommitDialog>b__0 () [0x00027] in <55a5cb75642b43f0ac9cee744a249780>:0
  at GitUI.GitUICommands.DoActionOnRepo (System.Windows.Forms.IWin32Window owner, System.Boolean requiresValidWorkingDir, System.Boolean changesRepo, GitUIPluginInterfaces.GitUIEventHandler preEvent, GitUIPluginInterfaces.GitUIPostActionEventHandler postEvent, System.Func`1[TResult] action) [0x0003e] in <55a5cb75642b43f0ac9cee744a249780>:0
  at GitUI.GitUICommands.StartCommitDialog (System.Windows.Forms.IWin32Window owner, System.Boolean showOnlyWhenChanges) [0x00029] in <55a5cb75642b43f0ac9cee744a249780>:0
  at GitUI.GitUICommands.StartCommitDialog (System.Windows.Forms.IWin32Window owner) [0x00001] in <55a5cb75642b43f0ac9cee744a249780>:0
  at GitUI.CommandsDialogs.FormBrowse.CommitToolStripMenuItemClick (System.Object sender, System.EventArgs e) [0x00007] in <55a5cb75642b43f0ac9cee744a249780>:0
  at GitUI.CommandsDialogs.FormBrowse.StatusClick (System.Object sender, System.EventArgs e) [0x00001] in <55a5cb75642b43f0ac9cee744a249780>:0
  at System.Windows.Forms.ToolStripItem.OnClick (System.EventArgs e) [0x00019] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ToolStripMenuItem.OnClick (System.EventArgs e) [0x00090] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ToolStripMenuItem.HandleClick (System.Int32 mouse_clicks, System.EventArgs e) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ToolStripItem.FireEvent (System.EventArgs e, System.Windows.Forms.ToolStripItemEventType met) [0x00054] in <6673d7101e044ca5890b94a28ed667e8>:0
  at (wrapper remoting-invoke-with-check) System.Windows.Forms.ToolStripItem:FireEvent (System.EventArgs,System.Windows.Forms.ToolStripItemEventType)
  at System.Windows.Forms.ToolStrip.OnMouseUp (System.Windows.Forms.MouseEventArgs mea) [0x00048] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control.WmLButtonUp (System.Windows.Forms.Message& m) [0x00078] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control.WndProc (System.Windows.Forms.Message& m) [0x001b4] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ScrollableControl.WndProc (System.Windows.Forms.Message& m) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.ToolStrip.WndProc (System.Windows.Forms.Message& m) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at GitUI.ToolStripEx.WndProc (System.Windows.Forms.Message& m) [0x00001] in <55a5cb75642b43f0ac9cee744a249780>:0
  at System.Windows.Forms.Control+ControlWindowTarget.OnMessage (System.Windows.Forms.Message& m) [0x00000] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.Control+ControlNativeWindow.WndProc (System.Windows.Forms.Message& m) [0x0000b] in <6673d7101e044ca5890b94a28ed667e8>:0
  at System.Windows.Forms.NativeWindow.WndProc (System.IntPtr hWnd, System.Windows.Forms.Msg msg, System.IntPtr wParam, System.IntPtr lParam) [0x00085]in <6673d7101e044ca5890b94a28ed667e8>:0
  ```

## Original introduction

GitExtensions is a standalone Git repository tool, a Visual Studio 2008 / 2010 / 2012 / 2013 plugin and a shell extension.

Build status: master [![Status](http://teamcity.codebetter.com/app/rest/builds/buildType:\(id:GitExtensions_Master\)/statusIcon)](http://teamcity.codebetter.com/viewType.html?buildTypeId=GitExtensions_Master)

Mono Build status: master
[![Build Status](https://travis-ci.org/gitextensions/gitextensions.svg?branch=master)](https://travis-ci.org/gitextensions/gitextensions)
* Source code: [http://github.com/gitextensions/gitextensions](http://github.com/gitextensions/gitextensions)
* Wiki: [https://github.com/gitextensions/gitextensions/wiki](https://github.com/gitextensions/gitextensions/wiki)
