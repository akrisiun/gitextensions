// using MaterialSkin.Controls;
using System.Windows.Forms;
namespace GitUI
{
    partial class FormStatus
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.KeepDialogOpen = new MaterialSkin.Controls.MaterialCheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelConsole = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnRetry = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.picBoxSuccessFail = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSuccessFail)).BeginInit();
            this.SuspendLayout();
            // 
            // KeepDialogOpen
            // 
            this.KeepDialogOpen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.KeepDialogOpen.Depth = 0;
            this.KeepDialogOpen.Font = new System.Drawing.Font("Calibri", 10F);
            this.KeepDialogOpen.Location = new System.Drawing.Point(587, 248);
            this.KeepDialogOpen.Margin = new System.Windows.Forms.Padding(0);
            this.KeepDialogOpen.MouseLocation = new System.Drawing.Point(-1, -1);
            this.KeepDialogOpen.MouseState = MaterialSkin.MouseState.HOVER;
            this.KeepDialogOpen.Name = "KeepDialogOpen";
            this.KeepDialogOpen.Ripple = true;
            this.KeepDialogOpen.Size = new System.Drawing.Size(150, 52);
            this.KeepDialogOpen.TabIndex = 3;
            this.KeepDialogOpen.Text = "Keep dialog open";
            this.KeepDialogOpen.UseCompatibleTextRendering = true;
            this.KeepDialogOpen.UseVisualStyleBackColor = true;
            this.KeepDialogOpen.CheckedChanged += new System.EventHandler(this.KeepDialogOpen_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanelConsole, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.picBoxSuccessFail, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.KeepDialogOpen, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 11);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(737, 339);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanelConsole
            // 
            this.tableLayoutPanelConsole.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelConsole.ColumnCount = 1;
            this.tableLayoutPanelConsole.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelConsole.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelConsole.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelConsole.Name = "tableLayoutPanelConsole";
            this.tableLayoutPanelConsole.RowCount = 1;
            this.tableLayoutPanel1.SetRowSpan(this.tableLayoutPanelConsole, 2);
            this.tableLayoutPanelConsole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelConsole.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 294F));
            this.tableLayoutPanelConsole.Size = new System.Drawing.Size(581, 294);
            this.tableLayoutPanelConsole.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnRetry, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOk, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAbort, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.ProgressBar, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 303);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(731, 33);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnRetry
            // 
            this.btnRetry.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRetry.Location = new System.Drawing.Point(451, 4);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(91, 25);
            this.btnRetry.TabIndex = 4;
            this.btnRetry.Text = "&Retry";
            this.btnRetry.UseCompatibleTextRendering = true;
            this.btnRetry.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(645, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(83, 25);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseCompatibleTextRendering = true;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.Ok_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAbort.Location = new System.Drawing.Point(548, 4);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(91, 25);
            this.btnAbort.TabIndex = 3;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseCompatibleTextRendering = true;
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.Abort_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(3, 4);
            this.ProgressBar.MarqueeAnimationSpeed = 1;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(442, 25);
            this.ProgressBar.Step = 50;
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBar.TabIndex = 3;
            // 
            // picBoxSuccessFail
            // 
            this.picBoxSuccessFail.Dock = System.Windows.Forms.DockStyle.Right;
            this.picBoxSuccessFail.Image = global::GitUI.Properties.Resources.StatusHourglass;
            this.picBoxSuccessFail.Location = new System.Drawing.Point(668, 3);
            this.picBoxSuccessFail.Name = "picBoxSuccessFail";
            this.picBoxSuccessFail.Size = new System.Drawing.Size(66, 235);
            this.picBoxSuccessFail.TabIndex = 1;
            this.picBoxSuccessFail.TabStop = false;
            // 
            // FormStatus
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(106F, 106F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnAbort;
            this.ClientSize = new System.Drawing.Size(759, 361);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(550, 217);
            this.Name = "FormStatus";
            this.Padding = new System.Windows.Forms.Padding(11, 11, 11, 11);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Process";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormStatus_FormClosed);
            this.Load += new System.EventHandler(this.FormStatus_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSuccessFail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

#endregion

#if SKIN
        protected MaterialSkin.Controls.MaterialCheckBox
#else
        protected CheckBox 
#endif
                  KeepDialogOpen;

        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox picBoxSuccessFail;
        private TableLayoutPanel tableLayoutPanelConsole;
        private TableLayoutPanel tableLayoutPanel2;
        protected Button btnRetry;
        protected Button btnOk;
        protected Button btnAbort;
        private ProgressBar ProgressBar;
        protected TextBox textMessage;
    }
}