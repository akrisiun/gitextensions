namespace NBug.Core.UI.WinForms
{
	using NBug.Core.UI.WinForms.Panels;

	partial class Full
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Full));
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.sendAndQuitButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.warningLabel = new System.Windows.Forms.Label();
            this.exceptionTypeLabel = new System.Windows.Forms.Label();
            this.exceptionTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.errorDescriptionLabel = new System.Windows.Forms.Label();
            this.clrTextBox = new System.Windows.Forms.TextBox();
            this.clrLabel = new System.Windows.Forms.Label();
            this.dateTimeTextBox = new System.Windows.Forms.TextBox();
            this.dateTimeLabel = new System.Windows.Forms.Label();
            this.nbugTextBox = new System.Windows.Forms.TextBox();
            this.nbugLabel = new System.Windows.Forms.Label();
            this.applicationTextBox = new System.Windows.Forms.TextBox();
            this.applicationLabel = new System.Windows.Forms.Label();
            this.targetSiteTextBox = new System.Windows.Forms.TextBox();
            this.targetSiteLabel = new System.Windows.Forms.Label();
            this.exceptionMessageTextBox = new System.Windows.Forms.TextBox();
            this.warningPictureBox = new System.Windows.Forms.PictureBox();
            this.exceptionTabPage = new System.Windows.Forms.TabPage();
            this.exceptionDetails = new NBug.Core.UI.WinForms.Panels.ExceptionDetails();
            this.reportContentsTabPage = new System.Windows.Forms.TabPage();
            this.reportPreviewTextBox = new System.Windows.Forms.TextBox();
            this.previewLabel = new System.Windows.Forms.Label();
            this.contentsLabel = new System.Windows.Forms.Label();
            this.reportContentsListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mainTabs.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warningPictureBox)).BeginInit();
            this.exceptionTabPage.SuspendLayout();
            this.reportContentsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.generalTabPage);
            this.mainTabs.Controls.Add(this.exceptionTabPage);
            this.mainTabs.Controls.Add(this.reportContentsTabPage);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTabs.Location = new System.Drawing.Point(6, 6);
            this.mainTabs.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
			this.mainTabs.Size = new System.Drawing.Size(471, 376);
            this.mainTabs.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.sendAndQuitButton);
            this.generalTabPage.Controls.Add(this.quitButton);
            this.generalTabPage.Controls.Add(this.warningLabel);
            this.generalTabPage.Controls.Add(this.exceptionTypeLabel);
            this.generalTabPage.Controls.Add(this.exceptionTextBox);
            this.generalTabPage.Controls.Add(this.descriptionTextBox);
            this.generalTabPage.Controls.Add(this.errorDescriptionLabel);
            this.generalTabPage.Controls.Add(this.clrTextBox);
            this.generalTabPage.Controls.Add(this.clrLabel);
            this.generalTabPage.Controls.Add(this.dateTimeTextBox);
            this.generalTabPage.Controls.Add(this.dateTimeLabel);
            this.generalTabPage.Controls.Add(this.nbugTextBox);
            this.generalTabPage.Controls.Add(this.nbugLabel);
            this.generalTabPage.Controls.Add(this.applicationTextBox);
            this.generalTabPage.Controls.Add(this.applicationLabel);
            this.generalTabPage.Controls.Add(this.targetSiteTextBox);
            this.generalTabPage.Controls.Add(this.targetSiteLabel);
            this.generalTabPage.Controls.Add(this.exceptionMessageTextBox);
            this.generalTabPage.Controls.Add(this.warningPictureBox);
			this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
			this.generalTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.generalTabPage.Size = new System.Drawing.Size(463, 350);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // sendAndQuitButton
            // 
            this.sendAndQuitButton.Image = global::NBug.Properties.Resources.Send;
            this.sendAndQuitButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.sendAndQuitButton.Location = new System.Drawing.Point(348, 324);
            this.sendAndQuitButton.Name = "sendAndQuitButton";
			this.sendAndQuitButton.Size = new System.Drawing.Size(102, 22);
            this.sendAndQuitButton.TabIndex = 1;
            this.sendAndQuitButton.Text = "&Send and Quit";
            this.sendAndQuitButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sendAndQuitButton.UseVisualStyleBackColor = true;
            this.sendAndQuitButton.Click += new System.EventHandler(this.SendAndQuitButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.quitButton.Location = new System.Drawing.Point(267, 324);
            this.quitButton.Name = "quitButton";
			this.quitButton.Size = new System.Drawing.Size(75, 22);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "&Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // warningLabel
            // 
			this.warningLabel.Location = new System.Drawing.Point(64, 12);
            this.warningLabel.Name = "warningLabel";
			this.warningLabel.Size = new System.Drawing.Size(388, 42);
            this.warningLabel.TabIndex = 18;
            this.warningLabel.Text = resources.GetString("warningLabel.Text");
            // 
            // exceptionTypeLabel
            // 
            this.exceptionTypeLabel.Image = global::NBug.Properties.Resources.NBug_Icon_PNG_16;
            this.exceptionTypeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.exceptionTypeLabel.Location = new System.Drawing.Point(21, 66);
            this.exceptionTypeLabel.Name = "exceptionTypeLabel";
			this.exceptionTypeLabel.Size = new System.Drawing.Size(106, 16);
            this.exceptionTypeLabel.TabIndex = 17;
            this.exceptionTypeLabel.Text = "Exception Type:";
            this.exceptionTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // exceptionTextBox
            // 
			this.exceptionTextBox.Location = new System.Drawing.Point(135, 66);
            this.exceptionTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.exceptionTextBox.Name = "exceptionTextBox";
			this.exceptionTextBox.Size = new System.Drawing.Size(317, 20);
            this.exceptionTextBox.ReadOnly = true;
            this.exceptionTextBox.TabIndex = 2;
            // 
            // descriptionTextBox
            // 
			this.descriptionTextBox.Location = new System.Drawing.Point(13, 256);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.Size = new System.Drawing.Size(439, 58);
            this.descriptionTextBox.TabIndex = 15;
            // 
            // errorDescriptionLabel
            // 
            this.errorDescriptionLabel.AutoSize = true;
			this.errorDescriptionLabel.Location = new System.Drawing.Point(10, 242);
            this.errorDescriptionLabel.Name = "errorDescriptionLabel";
			this.errorDescriptionLabel.Size = new System.Drawing.Size(315, 13);
            this.errorDescriptionLabel.TabIndex = 14;
            this.errorDescriptionLabel.Text = "Please add a brief description of how we can reproduce the error:";
            // 
            // clrTextBox
            // 
			this.clrTextBox.Location = new System.Drawing.Point(307, 208);
            this.clrTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clrTextBox.Name = "clrTextBox";
			this.clrTextBox.Size = new System.Drawing.Size(145, 20);
            this.clrTextBox.ReadOnly = true;
            this.clrTextBox.TabIndex = 13;
            // 
            // clrLabel
            // 
            this.clrLabel.AutoSize = true;
			this.clrLabel.Location = new System.Drawing.Point(259, 210);
            this.clrLabel.Name = "clrLabel";
			this.clrLabel.Size = new System.Drawing.Size(31, 13);
            this.clrLabel.TabIndex = 12;
            this.clrLabel.Text = "CLR:";
            // 
            // dateTimeTextBox
            // 
			this.dateTimeTextBox.Location = new System.Drawing.Point(78, 208);
            this.dateTimeTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dateTimeTextBox.Name = "dateTimeTextBox";
			this.dateTimeTextBox.Size = new System.Drawing.Size(145, 20);
            this.dateTimeTextBox.ReadOnly = true;
            this.dateTimeTextBox.TabIndex = 11;
            // 
            // dateTimeLabel
            // 
            this.dateTimeLabel.AutoSize = true;
			this.dateTimeLabel.Location = new System.Drawing.Point(10, 210);
            this.dateTimeLabel.Name = "dateTimeLabel";
			this.dateTimeLabel.Size = new System.Drawing.Size(61, 13);
            this.dateTimeLabel.TabIndex = 10;
            this.dateTimeLabel.Text = "Date/Time:";
            // 
            // nbugTextBox
            // 
			this.nbugTextBox.Location = new System.Drawing.Point(307, 175);
            this.nbugTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.nbugTextBox.Name = "nbugTextBox";
			this.nbugTextBox.Size = new System.Drawing.Size(145, 20);
            this.nbugTextBox.ReadOnly = true;
            this.nbugTextBox.TabIndex = 9;
            // 
            // nbugLabel
            // 
            this.nbugLabel.AutoSize = true;
			this.nbugLabel.Location = new System.Drawing.Point(259, 178);
            this.nbugLabel.Name = "nbugLabel";
			this.nbugLabel.Size = new System.Drawing.Size(37, 13);
            this.nbugLabel.TabIndex = 8;
            this.nbugLabel.Text = "NBug:";
            // 
            // applicationTextBox
            // 
			this.applicationTextBox.Location = new System.Drawing.Point(78, 175);
            this.applicationTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.applicationTextBox.Name = "applicationTextBox";
			this.applicationTextBox.Size = new System.Drawing.Size(145, 20);
            this.applicationTextBox.ReadOnly = true;
            this.applicationTextBox.TabIndex = 7;
            // 
            // applicationLabel
            // 
            this.applicationLabel.AutoSize = true;
			this.applicationLabel.Location = new System.Drawing.Point(10, 178);
            this.applicationLabel.Name = "applicationLabel";
			this.applicationLabel.Size = new System.Drawing.Size(62, 13);
            this.applicationLabel.TabIndex = 6;
            this.applicationLabel.Text = "Application:";
            // 
            // targetSiteTextBox
            // 
			this.targetSiteTextBox.Location = new System.Drawing.Point(78, 142);
            this.targetSiteTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.targetSiteTextBox.Name = "targetSiteTextBox";
			this.targetSiteTextBox.Size = new System.Drawing.Size(374, 20);
            this.targetSiteTextBox.ReadOnly = true;
            this.targetSiteTextBox.TabIndex = 5;
            // 
            // targetSiteLabel
            // 
            this.targetSiteLabel.AutoSize = true;
			this.targetSiteLabel.Location = new System.Drawing.Point(10, 145);
            this.targetSiteLabel.Name = "targetSiteLabel";
			this.targetSiteLabel.Size = new System.Drawing.Size(62, 13);
            this.targetSiteLabel.TabIndex = 4;
            this.targetSiteLabel.Text = "Target Site:";
            // 
            // exceptionMessageTextBox
            // 
			this.exceptionMessageTextBox.Location = new System.Drawing.Point(13, 94);
            this.exceptionMessageTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.exceptionMessageTextBox.Multiline = true;
            this.exceptionMessageTextBox.Name = "exceptionMessageTextBox";
            this.exceptionMessageTextBox.ReadOnly = true;
            this.exceptionMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.exceptionMessageTextBox.Size = new System.Drawing.Size(439, 34);
            this.exceptionMessageTextBox.TabIndex = 3;
            // 
            // warningPictureBox
            // 
			this.warningPictureBox.Location = new System.Drawing.Point(16, 14);
            this.warningPictureBox.Name = "warningPictureBox";
			this.warningPictureBox.Size = new System.Drawing.Size(32, 31);
            this.warningPictureBox.TabIndex = 1;
            this.warningPictureBox.TabStop = false;
            // 
            // exceptionTabPage
            // 
            this.exceptionTabPage.Controls.Add(this.exceptionDetails);
			this.exceptionTabPage.Location = new System.Drawing.Point(4, 22);
            this.exceptionTabPage.Name = "exceptionTabPage";
			this.exceptionTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.exceptionTabPage.Size = new System.Drawing.Size(463, 350);
            this.exceptionTabPage.TabIndex = 2;
            this.exceptionTabPage.Text = "Exception";
            this.exceptionTabPage.UseVisualStyleBackColor = true;
            // 
            // exceptionDetails
            // 
            this.exceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exceptionDetails.InformationColumnWidth = 350;
			this.exceptionDetails.Location = new System.Drawing.Point(3, 3);
			this.exceptionDetails.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.exceptionDetails.Name = "exceptionDetails";
            this.exceptionDetails.PropertyColumnWidth = 101;
			this.exceptionDetails.Size = new System.Drawing.Size(461, 318);
            this.exceptionDetails.TabIndex = 0;
            // 
            // reportContentsTabPage
            // 
            this.reportContentsTabPage.Controls.Add(this.reportPreviewTextBox);
            this.reportContentsTabPage.Controls.Add(this.previewLabel);
            this.reportContentsTabPage.Controls.Add(this.contentsLabel);
            this.reportContentsTabPage.Controls.Add(this.reportContentsListView);
			this.reportContentsTabPage.Location = new System.Drawing.Point(4, 22);
            this.reportContentsTabPage.Name = "reportContentsTabPage";
			this.reportContentsTabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.reportContentsTabPage.Size = new System.Drawing.Size(463, 350);
            this.reportContentsTabPage.TabIndex = 3;
            this.reportContentsTabPage.Text = "Report Contents";
            this.reportContentsTabPage.UseVisualStyleBackColor = true;
            this.reportContentsTabPage.Enter += new System.EventHandler(this.ReportContentsTabPage_Enter);
            // 
            // reportPreviewTextBox
            // 
			this.reportPreviewTextBox.Location = new System.Drawing.Point(6, 142);
            this.reportPreviewTextBox.Multiline = true;
            this.reportPreviewTextBox.Name = "reportPreviewTextBox";
			this.reportPreviewTextBox.Size = new System.Drawing.Size(455, 166);
            this.reportPreviewTextBox.TabIndex = 5;
            // 
            // previewLabel
            // 
            this.previewLabel.AutoSize = true;
			this.previewLabel.Location = new System.Drawing.Point(6, 126);
            this.previewLabel.Name = "previewLabel";
			this.previewLabel.Size = new System.Drawing.Size(48, 13);
            this.previewLabel.TabIndex = 4;
            this.previewLabel.Text = "Preview:";
            // 
            // contentsLabel
            // 
            this.contentsLabel.AutoSize = true;
			this.contentsLabel.Location = new System.Drawing.Point(6, 6);
            this.contentsLabel.Name = "contentsLabel";
			this.contentsLabel.Size = new System.Drawing.Size(288, 13);
            this.contentsLabel.TabIndex = 3;
            this.contentsLabel.Text = "Double-click an item to open it with the associated program.";
            // 
            // reportContentsListView
            // 
            this.reportContentsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.descriptionColumnHeader,
            this.sizeColumnHeader});
			this.reportContentsListView.Location = new System.Drawing.Point(6, 23);
            this.reportContentsListView.Name = "reportContentsListView";
			this.reportContentsListView.Size = new System.Drawing.Size(455, 94);
            this.reportContentsListView.TabIndex = 0;
            this.reportContentsListView.UseCompatibleStateImageBehavior = false;
            this.reportContentsListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 120;
            // 
            // descriptionColumnHeader
            // 
            this.descriptionColumnHeader.Text = "Description";
            this.descriptionColumnHeader.Width = 240;
            // 
            // sizeColumnHeader
            // 
            this.sizeColumnHeader.Text = "Size";
            this.sizeColumnHeader.Width = 80;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // Full
            // 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.quitButton;
			this.ClientSize = new System.Drawing.Size(483, 388);
            this.Controls.Add(this.mainTabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Full";
			this.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{HostApplication} Error";
            this.TopMost = true;
            this.mainTabs.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.generalTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.warningPictureBox)).EndInit();
            this.exceptionTabPage.ResumeLayout(false);
            this.reportContentsTabPage.ResumeLayout(false);
            this.reportContentsTabPage.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl mainTabs;
		private System.Windows.Forms.TabPage generalTabPage;
		private System.Windows.Forms.Button sendAndQuitButton;
		private System.Windows.Forms.Button quitButton;
		private System.Windows.Forms.TabPage exceptionTabPage;
		private System.Windows.Forms.PictureBox warningPictureBox;
		private System.Windows.Forms.TextBox exceptionMessageTextBox;
		private System.Windows.Forms.TextBox exceptionTextBox;
		private System.Windows.Forms.TextBox targetSiteTextBox;
		private System.Windows.Forms.Label targetSiteLabel;
		private System.Windows.Forms.TextBox nbugTextBox;
		private System.Windows.Forms.Label nbugLabel;
		private System.Windows.Forms.TextBox applicationTextBox;
		private System.Windows.Forms.Label applicationLabel;
		private System.Windows.Forms.TextBox descriptionTextBox;
		private System.Windows.Forms.Label errorDescriptionLabel;
		private System.Windows.Forms.TextBox clrTextBox;
		private System.Windows.Forms.Label clrLabel;
		private System.Windows.Forms.TextBox dateTimeTextBox;
		private System.Windows.Forms.Label dateTimeLabel;
		private System.Windows.Forms.TabPage reportContentsTabPage;
		private System.Windows.Forms.TextBox reportPreviewTextBox;
		private System.Windows.Forms.Label previewLabel;
		private System.Windows.Forms.Label contentsLabel;
		private System.Windows.Forms.ListView reportContentsListView;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader descriptionColumnHeader;
		private System.Windows.Forms.ColumnHeader sizeColumnHeader;
		private System.Windows.Forms.Label exceptionTypeLabel;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Label warningLabel;
		private ExceptionDetails exceptionDetails;
	}
}