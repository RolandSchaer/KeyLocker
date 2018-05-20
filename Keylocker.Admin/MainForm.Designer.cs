namespace Keylocker.Admin
{
	partial class MainForm
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
			if(disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.mainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mainMenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuOptionsPassword = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuOptionsSalt = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenuOptionsIterations = new System.Windows.Forms.ToolStripMenuItem();
			this.keyDataGridView = new System.Windows.Forms.DataGridView();
			this.buttonAddKey = new System.Windows.Forms.Button();
			this.buttonDeleteKey = new System.Windows.Forms.Button();
			this.statusStripMain = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.mainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.keyDataGridView)).BeginInit();
			this.statusStripMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFile,
            this.mainMenuOptions});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(784, 24);
			this.mainMenu.TabIndex = 0;
			this.mainMenu.Text = "Main Menu";
			// 
			// mainMenuFile
			// 
			this.mainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFileNew,
            this.mainMenuFileOpen,
            this.toolStripSeparator2,
            this.mainMenuFileSave,
            this.mainMenuFileSaveAs,
            this.toolStripSeparator1,
            this.mainMenuFileExit});
			this.mainMenuFile.Name = "mainMenuFile";
			this.mainMenuFile.Size = new System.Drawing.Size(37, 20);
			this.mainMenuFile.Text = "File";
			// 
			// mainMenuFileNew
			// 
			this.mainMenuFileNew.Name = "mainMenuFileNew";
			this.mainMenuFileNew.ShortcutKeyDisplayString = "F1";
			this.mainMenuFileNew.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.mainMenuFileNew.Size = new System.Drawing.Size(133, 22);
			this.mainMenuFileNew.Text = "New";
			this.mainMenuFileNew.Click += new System.EventHandler(this.mainMenuFileNew_Click);
			// 
			// mainMenuFileOpen
			// 
			this.mainMenuFileOpen.Name = "mainMenuFileOpen";
			this.mainMenuFileOpen.ShortcutKeyDisplayString = "F2";
			this.mainMenuFileOpen.ShortcutKeys = System.Windows.Forms.Keys.F2;
			this.mainMenuFileOpen.Size = new System.Drawing.Size(133, 22);
			this.mainMenuFileOpen.Text = "Open";
			this.mainMenuFileOpen.Click += new System.EventHandler(this.mainMenuFileOpen_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
			// 
			// mainMenuFileSave
			// 
			this.mainMenuFileSave.Name = "mainMenuFileSave";
			this.mainMenuFileSave.ShortcutKeyDisplayString = "F5";
			this.mainMenuFileSave.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.mainMenuFileSave.Size = new System.Drawing.Size(133, 22);
			this.mainMenuFileSave.Text = "Save";
			this.mainMenuFileSave.Click += new System.EventHandler(this.mainMenuFileSave_Click);
			// 
			// mainMenuFileSaveAs
			// 
			this.mainMenuFileSaveAs.Name = "mainMenuFileSaveAs";
			this.mainMenuFileSaveAs.ShortcutKeyDisplayString = "F6";
			this.mainMenuFileSaveAs.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.mainMenuFileSaveAs.Size = new System.Drawing.Size(133, 22);
			this.mainMenuFileSaveAs.Text = "Save As";
			this.mainMenuFileSaveAs.Click += new System.EventHandler(this.mainMenuFileSaveAs_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
			// 
			// mainMenuFileExit
			// 
			this.mainMenuFileExit.Name = "mainMenuFileExit";
			this.mainMenuFileExit.ShortcutKeyDisplayString = "F10";
			this.mainMenuFileExit.ShortcutKeys = System.Windows.Forms.Keys.F10;
			this.mainMenuFileExit.Size = new System.Drawing.Size(133, 22);
			this.mainMenuFileExit.Text = "Exit";
			this.mainMenuFileExit.Click += new System.EventHandler(this.mainMenuFileExit_Click);
			// 
			// mainMenuOptions
			// 
			this.mainMenuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuOptionsPassword,
            this.mainMenuOptionsSalt,
            this.mainMenuOptionsIterations});
			this.mainMenuOptions.Name = "mainMenuOptions";
			this.mainMenuOptions.Size = new System.Drawing.Size(61, 20);
			this.mainMenuOptions.Text = "Options";
			// 
			// mainMenuOptionsPassword
			// 
			this.mainMenuOptionsPassword.Name = "mainMenuOptionsPassword";
			this.mainMenuOptionsPassword.Size = new System.Drawing.Size(124, 22);
			this.mainMenuOptionsPassword.Text = "Password";
			this.mainMenuOptionsPassword.Click += new System.EventHandler(this.mainMenuOptionsPassword_Click);
			// 
			// mainMenuOptionsSalt
			// 
			this.mainMenuOptionsSalt.Name = "mainMenuOptionsSalt";
			this.mainMenuOptionsSalt.Size = new System.Drawing.Size(124, 22);
			this.mainMenuOptionsSalt.Text = "Salt";
			this.mainMenuOptionsSalt.Click += new System.EventHandler(this.mainMenuOptionsSalt_Click);
			// 
			// mainMenuOptionsIterations
			// 
			this.mainMenuOptionsIterations.Name = "mainMenuOptionsIterations";
			this.mainMenuOptionsIterations.Size = new System.Drawing.Size(124, 22);
			this.mainMenuOptionsIterations.Text = "Iterations";
			this.mainMenuOptionsIterations.Click += new System.EventHandler(this.mainMenuOptionsIterations_Click);
			// 
			// keyDataGridView
			// 
			this.keyDataGridView.AllowUserToAddRows = false;
			this.keyDataGridView.AllowUserToDeleteRows = false;
			this.keyDataGridView.AllowUserToResizeRows = false;
			this.keyDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.keyDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.keyDataGridView.Location = new System.Drawing.Point(12, 27);
			this.keyDataGridView.MultiSelect = false;
			this.keyDataGridView.Name = "keyDataGridView";
			this.keyDataGridView.Size = new System.Drawing.Size(744, 463);
			this.keyDataGridView.TabIndex = 1;
			// 
			// buttonAddKey
			// 
			this.buttonAddKey.Location = new System.Drawing.Point(12, 499);
			this.buttonAddKey.Name = "buttonAddKey";
			this.buttonAddKey.Size = new System.Drawing.Size(75, 23);
			this.buttonAddKey.TabIndex = 2;
			this.buttonAddKey.Text = "Add";
			this.buttonAddKey.UseVisualStyleBackColor = true;
			this.buttonAddKey.Click += new System.EventHandler(this.buttonAddKey_Click);
			// 
			// buttonDeleteKey
			// 
			this.buttonDeleteKey.Location = new System.Drawing.Point(93, 498);
			this.buttonDeleteKey.Name = "buttonDeleteKey";
			this.buttonDeleteKey.Size = new System.Drawing.Size(75, 24);
			this.buttonDeleteKey.TabIndex = 3;
			this.buttonDeleteKey.Text = "Delete";
			this.buttonDeleteKey.UseVisualStyleBackColor = true;
			this.buttonDeleteKey.Click += new System.EventHandler(this.buttonDeleteKey_Click);
			// 
			// statusStripMain
			// 
			this.statusStripMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
			this.statusStripMain.Location = new System.Drawing.Point(0, 533);
			this.statusStripMain.Name = "statusStripMain";
			this.statusStripMain.Size = new System.Drawing.Size(784, 22);
			this.statusStripMain.TabIndex = 4;
			this.statusStripMain.Text = "Status";
			// 
			// toolStripStatusLabel
			// 
			this.toolStripStatusLabel.Name = "toolStripStatusLabel";
			this.toolStripStatusLabel.Size = new System.Drawing.Size(71, 17);
			this.toolStripStatusLabel.Text = "Current File:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 555);
			this.Controls.Add(this.statusStripMain);
			this.Controls.Add(this.buttonDeleteKey);
			this.Controls.Add(this.buttonAddKey);
			this.Controls.Add(this.keyDataGridView);
			this.Controls.Add(this.mainMenu);
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "Keylocker Administration";
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.keyDataGridView)).EndInit();
			this.statusStripMain.ResumeLayout(false);
			this.statusStripMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFile;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileNew;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileSave;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileExit;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mainMenuFileSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.DataGridView keyDataGridView;
		private System.Windows.Forms.Button buttonAddKey;
		private System.Windows.Forms.Button buttonDeleteKey;
		private System.Windows.Forms.StatusStrip statusStripMain;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
		private System.Windows.Forms.ToolStripMenuItem mainMenuOptions;
		private System.Windows.Forms.ToolStripMenuItem mainMenuOptionsPassword;
		private System.Windows.Forms.ToolStripMenuItem mainMenuOptionsSalt;
		private System.Windows.Forms.ToolStripMenuItem mainMenuOptionsIterations;
	}
}

