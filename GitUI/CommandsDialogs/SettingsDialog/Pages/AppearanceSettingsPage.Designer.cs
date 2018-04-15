namespace GitUI.CommandsDialogs.SettingsDialog.Pages
{
    partial class AppearanceSettingsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbLanguages = new System.Windows.Forms.GroupBox();
            this.label49 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.Dictionary = new System.Windows.Forms.ComboBox();
            this.downloadDictionary = new System.Windows.Forms.LinkLabel();
            this.Language = new System.Windows.Forms.ComboBox();
            this.helpTranslate = new System.Windows.Forms.LinkLabel();
            this.gbFonts = new System.Windows.Forms.GroupBox();
            this.commitFontChangeButton = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.diffFontChangeButton = new System.Windows.Forms.Button();
            this.applicationFontChangeButton = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.gbGeneral = new System.Windows.Forms.GroupBox();
            this.chkShowRelativeDate = new System.Windows.Forms.CheckBox();
            this.chkShowCurrentBranchInVisualStudio = new System.Windows.Forms.CheckBox();
            this.chkEnableAutoScale = new System.Windows.Forms.CheckBox();
            this.truncateLongFilenames = new System.Windows.Forms.Label();
            this.truncatePathMethod = new System.Windows.Forms.ComboBox();
            this.gbAuthorImages = new System.Windows.Forms.GroupBox();
            this.NoImageService = new System.Windows.Forms.ComboBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this._NO_TRANSLATE_DaysToCacheImages = new System.Windows.Forms.NumericUpDown();
            this.label46 = new System.Windows.Forms.Label();
            this.ClearImageCache = new System.Windows.Forms.Button();
            this.ShowAuthorGravatar = new System.Windows.Forms.CheckBox();
            this.diffFontDialog = new System.Windows.Forms.FontDialog();
            this.applicationDialog = new System.Windows.Forms.FontDialog();
            this.commitFontDialog = new System.Windows.Forms.FontDialog();
            this.gbLanguages.SuspendLayout();
            this.gbFonts.SuspendLayout();
            this.gbGeneral.SuspendLayout();
            this.gbAuthorImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._NO_TRANSLATE_DaysToCacheImages)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLanguages
            // 
            this.gbLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.groupBox13.Controls.Add(this.label49);
            this.groupBox13.Controls.Add(this.label22);
            this.groupBox13.Controls.Add(this.Dictionary);
            this.groupBox13.Controls.Add(this.downloadDictionary);
            this.groupBox13.Controls.Add(this.Language);
            this.groupBox13.Controls.Add(this.helpTranslate);
            this.groupBox13.Location = new System.Drawing.Point(3, 443);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(1781, 84);
            this.groupBox13.TabIndex = 60;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Language";

            this.gbLanguages.Controls.Add(this.label49);
            this.gbLanguages.Controls.Add(this.label22);
            this.gbLanguages.Controls.Add(this.Dictionary);
            this.gbLanguages.Controls.Add(this.downloadDictionary);
            this.gbLanguages.Controls.Add(this.Language);
            this.gbLanguages.Controls.Add(this.helpTranslate);
            this.gbLanguages.Location = new System.Drawing.Point(3, 400);
            this.gbLanguages.Name = "gbLanguages";
            this.gbLanguages.Size = new System.Drawing.Size(1351, 84);
            this.gbLanguages.TabIndex = 3;
            this.gbLanguages.TabStop = false;
            this.gbLanguages.Text = "Language";

            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(8, 21);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(141, 13);

            this.label49.TabIndex = 30;

            this.label49.TabIndex = 0;

            this.label49.Text = "Language (restart required)";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(8, 50);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(150, 13);

            this.label22.TabIndex = 33;

            this.label22.TabIndex = 3;

            this.label22.Text = "Dictionary for spelling checker";
            // 
            // Dictionary
            // 
            this.Dictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dictionary.FormattingEnabled = true;
            this.Dictionary.Location = new System.Drawing.Point(283, 47);
            this.Dictionary.Name = "Dictionary";
            this.Dictionary.Size = new System.Drawing.Size(169, 21);

            this.Dictionary.TabIndex = 34;

            this.Dictionary.TabIndex = 4;

            this.Dictionary.DropDown += new System.EventHandler(this.Dictionary_DropDown);
            // 
            // downloadDictionary
            // 
            this.downloadDictionary.AutoSize = true;
            this.downloadDictionary.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.downloadDictionary.Location = new System.Drawing.Point(458, 50);
            this.downloadDictionary.Name = "downloadDictionary";
            this.downloadDictionary.Size = new System.Drawing.Size(104, 13);

            this.downloadDictionary.TabIndex = 35;

            this.downloadDictionary.TabIndex = 5;

            this.downloadDictionary.TabStop = true;
            this.downloadDictionary.Text = "Download dictionary";
            this.downloadDictionary.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadDictionary_LinkClicked);
            // 
            // Language
            // 
            this.Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Language.FormattingEnabled = true;
            this.Language.Location = new System.Drawing.Point(283, 18);
            this.Language.Name = "Language";
            this.Language.Size = new System.Drawing.Size(169, 21);

            this.Language.TabIndex = 31;

            this.Language.TabIndex = 1;

            // 
            // helpTranslate
            // 
            this.helpTranslate.AutoSize = true;
            this.helpTranslate.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.helpTranslate.Location = new System.Drawing.Point(458, 21);
            this.helpTranslate.Name = "helpTranslate";
            this.helpTranslate.Size = new System.Drawing.Size(74, 13);

            this.helpTranslate.TabIndex = 32;

            this.helpTranslate.TabIndex = 2;

            this.helpTranslate.TabStop = true;
            this.helpTranslate.Text = "Help translate";
            this.helpTranslate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpTranslate_LinkClicked);
            // 
            // gbFonts
            // 
            this.gbFonts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.groupBox15.Controls.Add(this.commitFontChangeButton);
            this.groupBox15.Controls.Add(this.label34);
            this.groupBox15.Controls.Add(this.diffFontChangeButton);
            this.groupBox15.Controls.Add(this.applicationFontChangeButton);
            this.groupBox15.Controls.Add(this.label26);
            this.groupBox15.Controls.Add(this.label56);
            this.groupBox15.Location = new System.Drawing.Point(3, 322);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(1777, 115);
            this.groupBox15.TabIndex = 59;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Fonts";

            this.gbFonts.Controls.Add(this.commitFontChangeButton);
            this.gbFonts.Controls.Add(this.label34);
            this.gbFonts.Controls.Add(this.diffFontChangeButton);
            this.gbFonts.Controls.Add(this.applicationFontChangeButton);
            this.gbFonts.Controls.Add(this.label26);
            this.gbFonts.Controls.Add(this.label56);
            this.gbFonts.Location = new System.Drawing.Point(3, 288);
            this.gbFonts.Name = "gbFonts";
            this.gbFonts.Size = new System.Drawing.Size(1351, 106);
            this.gbFonts.TabIndex = 2;
            this.gbFonts.TabStop = false;
            this.gbFonts.Text = "Fonts";

            // 
            // commitFontChangeButton
            // 
            this.commitFontChangeButton.AutoSize = true;
            this.commitFontChangeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.commitFontChangeButton.Location = new System.Drawing.Point(283, 75);
            this.commitFontChangeButton.Name = "commitFontChangeButton";
            this.commitFontChangeButton.Size = new System.Drawing.Size(66, 23);

            this.commitFontChangeButton.TabIndex = 25;

            this.commitFontChangeButton.TabIndex = 5;

            this.commitFontChangeButton.Text = "font name";
            this.commitFontChangeButton.UseVisualStyleBackColor = true;
            this.commitFontChangeButton.Click += new System.EventHandler(this.commitFontChangeButton_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(10, 80);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 13);

            this.label34.TabIndex = 24;

            this.label34.TabIndex = 4;

            this.label34.Text = "Commit font";
            // 
            // diffFontChangeButton
            // 
            this.diffFontChangeButton.AutoSize = true;
            this.diffFontChangeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.diffFontChangeButton.Location = new System.Drawing.Point(283, 17);
            this.diffFontChangeButton.Name = "diffFontChangeButton";
            this.diffFontChangeButton.Size = new System.Drawing.Size(66, 23);

            this.diffFontChangeButton.TabIndex = 21;

            this.diffFontChangeButton.TabIndex = 1;

            this.diffFontChangeButton.Text = "font name";
            this.diffFontChangeButton.UseVisualStyleBackColor = true;
            this.diffFontChangeButton.Click += new System.EventHandler(this.diffFontChangeButton_Click);
            // 
            // applicationFontChangeButton
            // 
            this.applicationFontChangeButton.AutoSize = true;
            this.applicationFontChangeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.applicationFontChangeButton.Location = new System.Drawing.Point(283, 46);
            this.applicationFontChangeButton.Name = "applicationFontChangeButton";
            this.applicationFontChangeButton.Size = new System.Drawing.Size(66, 23);

            this.applicationFontChangeButton.TabIndex = 23;

            this.applicationFontChangeButton.TabIndex = 3;

            this.applicationFontChangeButton.Text = "font name";
            this.applicationFontChangeButton.UseVisualStyleBackColor = true;
            this.applicationFontChangeButton.Click += new System.EventHandler(this.applicationFontChangeButton_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(10, 51);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 13);

            this.label26.TabIndex = 22;

            this.label26.Text = "Application font";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Location = new System.Drawing.Point(10, 22);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(55, 13);

            this.label56.TabIndex = 20;

            this.label56.Text = "Code font";
            // 
            // gbGeneral
            // 
            this.gbGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.groupBox14.Controls.Add(this.chkShowRelativeDate);
            this.groupBox14.Controls.Add(this.chkShowCurrentBranchInVisualStudio);
            this.groupBox14.Controls.Add(this.chkEnableAutoScale);
            this.groupBox14.Controls.Add(this.truncateLongFilenames);
            this.groupBox14.Controls.Add(this.truncatePathMethod);
            this.groupBox14.Location = new System.Drawing.Point(3, 3);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(1777, 136);
            this.groupBox14.TabIndex = 57;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "General";

            this.gbGeneral.Controls.Add(this.chkShowRelativeDate);
            this.gbGeneral.Controls.Add(this.chkShowCurrentBranchInVisualStudio);
            this.gbGeneral.Controls.Add(this.chkEnableAutoScale);
            this.gbGeneral.Controls.Add(this.truncateLongFilenames);
            this.gbGeneral.Controls.Add(this.truncatePathMethod);
            this.gbGeneral.Location = new System.Drawing.Point(3, 3);
            this.gbGeneral.Name = "gbGeneral";
            this.gbGeneral.Size = new System.Drawing.Size(1351, 136);
            this.gbGeneral.TabIndex = 0;
            this.gbGeneral.TabStop = false;
            this.gbGeneral.Text = "General";

            // 
            // chkShowRelativeDate
            // 
            this.chkShowRelativeDate.AutoSize = true;
            this.chkShowRelativeDate.Location = new System.Drawing.Point(10, 22);
            this.chkShowRelativeDate.Name = "chkShowRelativeDate";
            this.chkShowRelativeDate.Size = new System.Drawing.Size(209, 17);

            this.chkShowRelativeDate.TabIndex = 1;

            this.chkShowRelativeDate.Text = "Show relative date instead of full date";
            this.chkShowRelativeDate.UseVisualStyleBackColor = true;
            // 
            // chkShowCurrentBranchInVisualStudio
            // 
            this.chkShowCurrentBranchInVisualStudio.AutoSize = true;
            this.chkShowCurrentBranchInVisualStudio.Location = new System.Drawing.Point(10, 48);
            this.chkShowCurrentBranchInVisualStudio.Name = "chkShowCurrentBranchInVisualStudio";
            this.chkShowCurrentBranchInVisualStudio.Size = new System.Drawing.Size(200, 17);

            this.chkShowCurrentBranchInVisualStudio.TabIndex = 2;

            this.chkShowCurrentBranchInVisualStudio.Text = "Show current branch in Visual Studio";
            this.chkShowCurrentBranchInVisualStudio.UseVisualStyleBackColor = true;
            // 
            // chkEnableAutoScale
            // 
            this.chkEnableAutoScale.AutoSize = true;
            this.chkEnableAutoScale.Location = new System.Drawing.Point(10, 75);
            this.chkEnableAutoScale.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEnableAutoScale.Name = "chkEnableAutoScale";
            this.chkEnableAutoScale.Size = new System.Drawing.Size(254, 17);

            this.chkEnableAutoScale.TabIndex = 3;

            this.chkEnableAutoScale.Text = "Auto scale user interface when high DPI is used";
            this.chkEnableAutoScale.UseVisualStyleBackColor = true;
            // 
            // truncateLongFilenames
            // 
            this.truncateLongFilenames.AutoSize = true;
            this.truncateLongFilenames.Location = new System.Drawing.Point(10, 104);
            this.truncateLongFilenames.Name = "truncateLongFilenames";
            this.truncateLongFilenames.Size = new System.Drawing.Size(121, 13);

            this.truncateLongFilenames.TabIndex = 5;

            this.truncateLongFilenames.Text = "Truncate long filenames";
            // 
            // truncatePathMethod
            // 
            this.truncatePathMethod.FormattingEnabled = true;
            this.truncatePathMethod.Items.AddRange(new object[] {
            "None",
            "Compact",
            "Trim start",
            "Filename only"});
            this.truncatePathMethod.Location = new System.Drawing.Point(283, 101);
            this.truncatePathMethod.Name = "truncatePathMethod";
            this.truncatePathMethod.Size = new System.Drawing.Size(242, 21);

            this.truncatePathMethod.TabIndex = 6;

            // 
            // gbAuthorImages
            // 
            this.gbAuthorImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));

            this.groupBox6.Controls.Add(this.AuthorImageSize);
            this.groupBox6.Controls.Add(this.NoImageService);
            this.groupBox6.Controls.Add(this.label53);
            this.groupBox6.Controls.Add(this.label47);
            this.groupBox6.Controls.Add(this._NO_TRANSLATE_DaysToCacheImages);
            this.groupBox6.Controls.Add(this.label46);
            this.groupBox6.Controls.Add(this.label44);
            this.groupBox6.Controls.Add(this.ClearImageCache);
            this.groupBox6.Controls.Add(this.ShowAuthorGravatar);
            this.groupBox6.Location = new System.Drawing.Point(3, 145);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1777, 171);
            this.groupBox6.TabIndex = 58;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Author images";
            // 
            // AuthorImageSize
            // 
            this.AuthorImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AuthorImageSize.FormattingEnabled = true;
            this.AuthorImageSize.Items.AddRange(new object[] {
            "Small (80x80)",
            "Normal (160x160)",
            "Large (240x240)",
            "Extra Large (320x320)"});
            this.AuthorImageSize.Location = new System.Drawing.Point(221, 45);
            this.AuthorImageSize.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AuthorImageSize.Name = "AuthorImageSize";
            this.AuthorImageSize.Size = new System.Drawing.Size(142, 21);
            this.AuthorImageSize.TabIndex = 12;
            this.gbAuthorImages.Controls.Add(this.NoImageService);
            this.gbAuthorImages.Controls.Add(this.label53);
            this.gbAuthorImages.Controls.Add(this.label47);
            this.gbAuthorImages.Controls.Add(this._NO_TRANSLATE_DaysToCacheImages);
            this.gbAuthorImages.Controls.Add(this.label46);
            this.gbAuthorImages.Controls.Add(this.ClearImageCache);
            this.gbAuthorImages.Controls.Add(this.ShowAuthorGravatar);
            this.gbAuthorImages.Location = new System.Drawing.Point(3, 145);
            this.gbAuthorImages.Name = "gbAuthorImages";
            this.gbAuthorImages.Size = new System.Drawing.Size(1351, 137);
            this.gbAuthorImages.TabIndex = 1;
            this.gbAuthorImages.TabStop = false;
            this.gbAuthorImages.Text = "Author images";

            // 
            // NoImageService
            // 
            this.NoImageService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NoImageService.FormattingEnabled = true;
            this.NoImageService.Location = new System.Drawing.Point(283, 71);
            this.NoImageService.Name = "NoImageService";
            this.NoImageService.Size = new System.Drawing.Size(142, 21);

            this.NoImageService.TabIndex = 17;

            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Location = new System.Drawing.Point(10, 74);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(88, 13);

            this.label53.TabIndex = 16;

            this.label53.Text = "No image service";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(366, 45);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(30, 13);

            this.label47.TabIndex = 15;

            this.label47.Text = "days";
            // 
            // _NO_TRANSLATE_DaysToCacheImages
            // 
            this._NO_TRANSLATE_DaysToCacheImages.Location = new System.Drawing.Point(283, 44);
            this._NO_TRANSLATE_DaysToCacheImages.Maximum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this._NO_TRANSLATE_DaysToCacheImages.Name = "_NO_TRANSLATE_DaysToCacheImages";
            this._NO_TRANSLATE_DaysToCacheImages.Size = new System.Drawing.Size(77, 21);

            this._NO_TRANSLATE_DaysToCacheImages.TabIndex = 14;

            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(10, 46);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(73, 13);

            this.label46.TabIndex = 13;
            this.label46.Text = "Cache images";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(10, 48);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(58, 13);
            this.label44.TabIndex = 11;
            this.label44.Text = "Image size";
            // 
            this.label46.TabIndex = 1;
            this.label46.Text = "Cache images";
            // 

            // ClearImageCache
            // 
            this.ClearImageCache.Location = new System.Drawing.Point(11, 96);
            this.ClearImageCache.Name = "ClearImageCache";
            this.ClearImageCache.Size = new System.Drawing.Size(142, 25);
            this.ClearImageCache.TabIndex = 6;
            this.ClearImageCache.Text = "Clear image cache";
            this.ClearImageCache.UseVisualStyleBackColor = true;
            this.ClearImageCache.Click += new System.EventHandler(this.ClearImageCache_Click);
            // 
            // ShowAuthorGravatar
            // 
            this.ShowAuthorGravatar.AutoSize = true;
            this.ShowAuthorGravatar.Location = new System.Drawing.Point(10, 22);
            this.ShowAuthorGravatar.Name = "ShowAuthorGravatar";
            this.ShowAuthorGravatar.Size = new System.Drawing.Size(202, 17);

            this.ShowAuthorGravatar.TabIndex = 10;
            this.ShowAuthorGravatar.Text = "Get author image from gravatar.com";
            this.ShowAuthorGravatar.UseVisualStyleBackColor = true;
            // 
            // diffFontDialog
            // 
            this.diffFontDialog.AllowVerticalFonts = false;
            this.diffFontDialog.Color = System.Drawing.SystemColors.ControlText;
            this.diffFontDialog.FixedPitchOnly = true;
            // 
            // applicationDialog
            // 
            this.applicationDialog.AllowVerticalFonts = false;
            this.applicationDialog.Color = System.Drawing.SystemColors.ControlText;
            // 
            // commitFontDialog
            // 
            this.commitFontDialog.AllowVerticalFonts = false;
            this.commitFontDialog.Color = System.Drawing.SystemColors.ControlText;
            // 
            // AppearanceSettingsPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.gbLanguages);
            this.Controls.Add(this.gbFonts);
            this.Controls.Add(this.gbGeneral);
            this.Controls.Add(this.gbAuthorImages);
            this.MinimumSize = new System.Drawing.Size(515, 510);
            this.Name = "AppearanceSettingsPage";

            this.Size = new System.Drawing.Size(899, 550);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.Size = new System.Drawing.Size(1360, 773);
            this.gbLanguages.ResumeLayout(false);
            this.gbLanguages.PerformLayout();
            this.gbFonts.ResumeLayout(false);
            this.gbFonts.PerformLayout();
            this.gbGeneral.ResumeLayout(false);
            this.gbGeneral.PerformLayout();
            this.gbAuthorImages.ResumeLayout(false);
            this.gbAuthorImages.PerformLayout();

            ((System.ComponentModel.ISupportInitialize)(this._NO_TRANSLATE_DaysToCacheImages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbLanguages;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox Dictionary;
        private System.Windows.Forms.LinkLabel downloadDictionary;
        private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.LinkLabel helpTranslate;
        private System.Windows.Forms.GroupBox gbFonts;
        private System.Windows.Forms.Button diffFontChangeButton;
        private System.Windows.Forms.Button applicationFontChangeButton;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.GroupBox gbGeneral;
        private System.Windows.Forms.CheckBox chkShowRelativeDate;
        private System.Windows.Forms.CheckBox chkShowCurrentBranchInVisualStudio;
        private System.Windows.Forms.CheckBox chkEnableAutoScale;
        private System.Windows.Forms.Label truncateLongFilenames;
        private System.Windows.Forms.ComboBox truncatePathMethod;
        private System.Windows.Forms.GroupBox gbAuthorImages;
        private System.Windows.Forms.ComboBox NoImageService;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.NumericUpDown _NO_TRANSLATE_DaysToCacheImages;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Button ClearImageCache;
        private System.Windows.Forms.CheckBox ShowAuthorGravatar;
        private System.Windows.Forms.FontDialog diffFontDialog;
        private System.Windows.Forms.FontDialog applicationDialog;
        private System.Windows.Forms.Button commitFontChangeButton;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.FontDialog commitFontDialog;
    }
}
