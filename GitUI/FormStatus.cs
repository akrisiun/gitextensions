using System;
using System.Threading;
using System.Windows.Forms;
using GitCommands;
using GitUI.UserControls;
using JetBrains.Annotations;
using System.Drawing;

#if SKIN
using MaterialSkin;
#endif
// using Microsoft.WindowsAPICodePack.Taskbar;

namespace GitUI
{
    public partial class FormStatus : GitExtensionsForm
    {
        public delegate void ProcessStart(FormStatus form);
        public delegate void ProcessAbort(FormStatus form);

        // DispatcherFrameModalControler controler;
        private readonly bool UseDialogSettings = true;

        public FormStatus()
            : this(true)
        { }

        public FormStatus(bool useDialogSettings)
            : base(true)
        {
#if SKIN
			SetSkin();
            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = true; MaximizeBox = true;

            UseDialogSettings = useDialogSettings;

            // baseCreateParams = CreateParams;
            SuspendLayout();
#endif
            InitializeComponent();

            if (textMessage == null)
            {
                textMessage = new TextBox
                {
                    ReadOnly = true,
                    Text = "",
                    BorderStyle = BorderStyle.None,
                    Location = new Point(0, 3),
                    Size = new Size(tableLayoutPanelConsole.Width, 20),
                    Multiline = true
                };
                Controls.Add(textMessage);
                textMessage.Visible = true;
                tableLayoutPanel1.Anchor = AnchorStyles.Left;
                tableLayoutPanel1.Top = textMessage.Height;
                tableLayoutPanel1.Height = ClientSize.Height - textMessage.Height - 6;
                tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            }

#if SKIN
            ControlBox = false;
			if (SkinManager != null)
            {
                textMessage.Top = HeaderHeight + 4;
                textMessage.BackColor = BackColor;
                textMessage.ForeColor = Color.White;
                tableLayoutPanel1.Top = textMessage.Top + 30;

                this.Height = this.Height + HeaderHeight + 12;
                PerformLayout();
                tableLayoutPanel1.Height = this.Height - HeaderHeight - 30;
                tableLayoutPanel2.BackColor = BackColor;

                KeepDialogOpen.Size = new Size(130, 47);
                KeepDialogOpen.OnDrawText += (s, e) =>
                {
                    var Text = KeepDialogOpen.Text;
                    var Height = KeepDialogOpen.Height;
                    var g = e.Graphics;
                    SizeF stringSize = g.MeasureString(Text, KeepDialogOpen.Font);
                    g.DrawString(
                            "Keep",
                            SkinManager.ROBOTO_MEDIUM_10, Enabled ? SkinManager.GetPrimaryTextBrush() : SkinManager.GetDisabledOrHintBrush(),
                            40, 5); // Height / 2 - stringSize.Height / 2);

                    g.DrawString(
                            "dialog open",
                            SkinManager.ROBOTO_MEDIUM_10, SkinManager.GetPrimaryTextBrush(),
                            40, 25);
                };
            }
#else
            KeepDialogOpen.Size = new Size(130, 47);
			/*
            KeepDialogOpen.OnDrawText += (s, e) =>
            {
                var Text = KeepDialogOpen.Text;
                var Height = KeepDialogOpen.Height;
                var g = e.Graphics;
                using (Brush textBrush = new SolidBrush((KeepDialogOpen.ForeColor)))
                {
                    SizeF stringSize = g.MeasureString(Text, KeepDialogOpen.Font);
                    g.DrawString(
                            "Keep", 
                            KeepDialogOpen.Font, textBrush, 40, 5);

                    g.DrawString(
                            "dialog open",
                            KeepDialogOpen.Font, textBrush, 40, 25);
                }
            }; 
            */
#endif

            Translate();
            this.btnRetry.Text = "Retry";   // no &

            ConsoleOutput = ConsoleOutputControl.CreateInstance();
            if (ConsoleOutput != null)
            {
                ConsoleOutput.Visible = false;  // at first hidden, then show, too slow paint..
                if (ConsoleOutput.Parent == null && !(ConsoleOutput is ConsoleEmulatorOutputControl))
                    this.tableLayoutPanelConsole.Controls.Add(ConsoleOutput);
                // Delegates
                ConsoleOutputDelegate();
            }

            ResumeLayout(true);

            MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    textMessage.Visible = true;
                    textMessage.BringToFront();
                }
            };
            btnRetry.Click += (s, e) => Retry();

            if (UseDialogSettings)
                KeepDialogOpen.Checked = !GitCommands.AppSettings.CloseProcessDialog;
            else
                KeepDialogOpen.Hide();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            SuspendLayout();

            ConsoleOutput.Visible = true;
            textMessage.Left = tableLayoutPanel1.Left + tableLayoutPanelConsole.Left;
            textMessage.Width = tableLayoutPanelConsole.Width;
            textMessage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ResumeLayout(true);

            if (Width < 750)
            {
                Width = 750;
                PerformLayout();
            }

        }

#if !SKIN
        private int HeaderHeight = 0;
#else
        public override void SetSkin()
        {
            HeaderHeight = 32;
            this.SkinManager2 = new MaterialSkinManager
            {
                Theme = MaterialSkinManager.Themes.DARK,
                ColorScheme =  Scheme_ConEmu
            };

            base.SetSkin();
        }

        #region static colors

        public static Color BACKGROUND_DARK_ConEmu = ColorScheme.ToColor(0x10323B); // 0x00222B
        public static ColorScheme Scheme_ConEmu
        {
            get
            {
                return
                    new ColorScheme((int)0x85DF87, // Primary.Green200, // Green600,
                    (int)0, // 0x263232, 
                    (int)Primary.BlueGrey500,
                    (int)0x80D8FF, // Accent.LightBlue200, 
                    TextShade.WHITE);
            }
        }
        #endregion
#endif

        protected virtual void ConsoleOutputDelegate()
        {
            ConsoleOutput.Dock = DockStyle.Fill;
            ConsoleOutput.Terminated += delegate
            {
                if (!KeepDialogOpen.Checked)
                    Close();
                else
                {
                    btnAbort.Enabled = true;
                    btnRetry.Enabled = true;
                    btnRetry.Focus();
                }
            }; // This means the control is not visible anymore, no use in keeping. Expected scenario: user hits ESC in the prompt after the git process exits
        }

        public FormStatus(ProcessStart process, ProcessAbort abort)
            : this()
        {
            ProcessCallback = process;
            AbortCallback = abort;
        }

        // not readonly
        protected ConsoleOutputControl ConsoleOutput; // Naming: protected stuff must be CLS-compliant here
        public ProcessStart ProcessCallback;
        public ProcessAbort AbortCallback;
        private bool errorOccurred;
        private bool showOnError;

        /// <summary>
        /// Gets the logged output text. Note that this is a separate string from what you see in the console output control.
        /// For instance, progress messages might be skipped; other messages might be added manually.
        /// </summary>
        [NotNull]
        public readonly FormStatusOutputLog OutputLog = new FormStatusOutputLog();

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams mdiCp = base.CreateParams;
                mdiCp.ClassStyle = mdiCp.ClassStyle | NativeConstants.CP_NOCLOSE_BUTTON;
                return mdiCp;
            }
        }

        public bool ErrorOccurred()
        {
            return errorOccurred;
        }

        public void SetProgress(string text)
        {
            // This has to happen on the UI thread
            SendOrPostCallback method = o =>
                {
                    int index = text.LastIndexOf('%');
                    int progressValue;
                    if (index > 4 && int.TryParse(text.Substring(index - 3, 3), out progressValue) && progressValue >= 0)
                    {
                        if (ProgressBar.Style != ProgressBarStyle.Blocks)
                            ProgressBar.Style = ProgressBarStyle.Blocks;
                        ProgressBar.Value = Math.Min(100, progressValue);

#if !__MonoCS__ && !NET45
                        TaskBarLock.TryCatch(() => TaskBarLock.SetProgressStateValue(this, TaskbarProgressBarState.Normal, 100));
#endif
                    }
                    // Show last progress message in the title, unless it's showin in the control body already
                    if (!ConsoleOutput.IsDisplayingFullProcessOutput)
                        Text = text;
                };
            BeginInvoke(method, this);
        }

        /// <summary>
        /// Adds a message to the console display control ONLY, <see cref="GetOutputString" /> will not list it.
        /// </summary>
        public void AddMessage(string text)
        {
            ConsoleOutput.AppendMessageFreeThreaded(text);

            if (!textMessage.InvokeRequired)
            {
                textMessage.Text += "\n";
                textMessage.Text += text;
            }
            if (ConsoleOutput is ConsoleEmulatorOutputControl)
            {
                var terminal = (ConsoleOutput as ConsoleEmulatorOutputControl).Terminal;
                if (terminal != null && !terminal.Visible || terminal.RunningSession == null || terminal.RunningSession.IsConsoleProcessExited
                    && !this.textMessage.InvokeRequired)
                {
                    this.textMessage.Visible = true;
                    this.textMessage.BringToFront();
                    this.textMessage.Update();
                }
            }
        }

        /// <summary>
        /// Adds a message line to the console display control ONLY, <see cref="GetOutputString" /> will not list it.
        /// </summary>
        public void AddMessageLine(string text)
        {
            AddMessage(text + Environment.NewLine);
        }

        public virtual void Done(bool isSuccess)
        {
            if (isSuccess)
                AppendMessageCrossThread("Done");

            ProgressBar.Visible = false;
            btnOk.Enabled = true;
            btnRetry.Enabled = true;

            textMessage.Visible = true;
            textMessage.BringToFront();
            btnOk.Focus();

            AcceptButton = btnOk;
            btnAbort.Enabled = false;
#if !__MonoCS__ && !NET45
            if (progressStarted)
            {
                TaskBarLock.TryCatch(() => TaskBarLock.SetProgressStateValue(this,
                        isSuccess ? TaskbarProgressBarState.Normal : TaskbarProgressBarState.Error,
                        100));
            }
#endif

            if (textMessage.Width >= Width)
                textMessage.Width = Width - 100;

            if (isSuccess)
                picBoxSuccessFail.Image = GitUI.Properties.Resources.success;
            else
            {
                picBoxSuccessFail.Image = GitUI.Properties.Resources.error;

                var log = textMessage.Text; // this.OutputLog.GetString();
                if (log != null && log.Contains("/index.lock"))
                {
                    this.KeepDialogOpen.Checked = true;
                    this.btnRetry.Focus();
                }
            }

            errorOccurred = !isSuccess;

            if (showOnError && !isSuccess)
            {
                // For some reason setting the state to normal interferes with
                // proper parent centering...
                WindowState = FormWindowState.Normal;
                CenterToParent();
                Visible = true;
            }

            if (isSuccess && (showOnError || (UseDialogSettings && GitCommands.AppSettings.CloseProcessDialog)))
            {
                Close();
            }
        }

        public virtual void Retry()
        {
            Reset();
            KeepDialogOpen.Checked = true;
            ProcessCallback(this);
        }

        public void AppendMessageCrossThread(string text)
        {
            ConsoleOutput.AppendMessageFreeThreaded(text);

            textMessage.Text += Environment.NewLine;
            textMessage.Text += text;
        }

        public void Reset()
        {
            if (ConsoleOutput == null)  // ConEmu failure
            {
                this.ConsoleOutput = new EditboxBasedConsoleOutputControl();
                ConsoleOutputDelegate();
                ConsoleOutput.Reset();
            }
            else if (ConsoleOutput is ConsoleEmulatorOutputControl)
            {
                ConsoleOutput.Reset();
                var ctrl = (ConsoleOutput as ConsoleEmulatorOutputControl).Terminal;
                if (ConsoleOutput.Parent == null)
                    tableLayoutPanelConsole.Controls.Add(ConsoleOutput);

                if (ctrl.RunningSession == null || ctrl.RunningSession.IsConsoleProcessExited)
                {
                    // textMessage.Width = ctrl.Width;
                    // textMessage.Height += 22;
                    textMessage.BringToFront();
                    textMessage.Width = this.tableLayoutPanelConsole.Width;

                    //ConsoleOutput.Anchor = AnchorStyles.Left;
                    //ConsoleOutput.Top = textMessage.Height;
                    //ConsoleOutput.Visible = true;
                    ctrl.Size = ConsoleOutput.Parent.ClientSize;
                }
                ctrl.BringToFront();
            }

            OutputLog.Clear();
            ProgressBar.Visible = true;
            btnOk.Enabled = false;
            btnRetry.Enabled = false;
            ActiveControl = null;
        }

        public void ShowDialogOnError()
        {
            ShowDialogOnError(null);
        }

        public void ShowDialogOnError(IWin32Window owner)
        {
            Visible = false;
            KeepDialogOpen.Visible = false;
            btnAbort.Visible = false;
            showOnError = true;
            // Just hiding it still seems to draw one frame of the control
            WindowState = FormWindowState.Minimized;
            ShowDialog(owner);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.OK;
        }

        private void FormStatus_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (ProcessCallback == null)
            {
                throw new InvalidOperationException("You can't load the form without a ProcessCallback");
            }

            if (AbortCallback == null)
            {
                btnAbort.Visible = false;
            }
            StartPosition = FormStartPosition.CenterParent;

            Start();
        }

        private void FormStatus_FormClosed(object sender, FormClosedEventArgs e)
        {
#if !__MonoCS__ && !NET45
            if (progressStarted)
            {
                TaskBarLock.TryCatch(() => TaskBarLock.SetProgressState(this, TaskbarProgressBarState.NoProgress));
            }
#endif
        }

#if !__MonoCS__ && !NET45
        private bool progressStarted = false;
#endif
        private void Start()
        {
#if !__MonoCS__ && !NET45
            if (progressStarted)
            {
                TaskBarLock.TryCatch(() => TaskBarLock.SetProgressState(this, TaskbarProgressBarState.Indeterminate)
                    , ifSuccess: () => { progressStarted = true; });
            }
#endif
            Reset();
            ProcessCallback(this);
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            try
            {
                AbortCallback(this);
                OutputLog.Append(Environment.NewLine + "Aborted");  // TODO: write to display control also, if we pull the function up to this base class
                Done(false);
                DialogResult = DialogResult.Abort;
            }
            catch { }
        }

        public string GetOutputString()
        {
            return OutputLog.GetString();
        }

        private void KeepDialogOpen_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.CloseProcessDialog = !KeepDialogOpen.Checked;

            // Maintain the invariant: if changing to "don't keep" and conditions are such that the dialog would have closed in dont-keep mode, then close it
            if ((!KeepDialogOpen.Checked /* keep off */) && (btnOk.Enabled /* done */) && (!errorOccurred /* and successful */)) /* not checking for UseDialogSettings because checkbox is only visible with True */
                Close();
        }

        public void AfterClosed()
        { }
    }

    class DispatcherFrameModalControler
    {
        //private DispatcherFrame DispatcherFrame = new DispatcherFrame();
        private FormStatus FormStatus;
        private IWin32Window Owner;

        public DispatcherFrameModalControler(FormStatus aFormStatus, IWin32Window aOwner)
        {
            FormStatus = aFormStatus;
            Owner = aOwner;
        }

        public void BeginModal()
        {
            //FormStatus.Start();
            //Dispatcher.PushFrame(DispatcherFrame);
        }

        public void EndModal(bool success)
        {
            if (!success)
            {
                FormStatus.ShowDialog(Owner);
            }
            else
            {
                FormStatus.AfterClosed();
            }
            //DispatcherFrame.Continue = false;
        }
    }
}
