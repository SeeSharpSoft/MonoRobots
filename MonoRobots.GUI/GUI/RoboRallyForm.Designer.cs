namespace SeeSharpSoft.MonoRobots.GUI
{
    partial class RoboRallyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoboRallyForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.roboRallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.gameSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSpeedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBoardAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.showEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeAssetDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadKIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.editControl = new SeeSharpSoft.MonoRobots.GUI.EditControl();
            this.boardPlayerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.boardControl = new SeeSharpSoft.MonoRobots.GUI.BoardControl();
            this.playerPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStripRight = new System.Windows.Forms.ToolStrip();
            this.playerPanelToggleButton = new System.Windows.Forms.ToolStripButton();
            this.openKIFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip.SuspendLayout();
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.RightToolStripPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boardPlayerSplitContainer)).BeginInit();
            this.boardPlayerSplitContainer.Panel1.SuspendLayout();
            this.boardPlayerSplitContainer.Panel2.SuspendLayout();
            this.boardPlayerSplitContainer.SuspendLayout();
            this.toolStripRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roboRallyToolStripMenuItem,
            this.editorToolStripMenuItem,
            this.playerToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(815, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // roboRallyToolStripMenuItem
            // 
            this.roboRallyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolStripSeparator4,
            this.gameSpeedToolStripMenuItem,
            this.gameSpeedToolStripMenuItem1,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.roboRallyToolStripMenuItem.Name = "roboRallyToolStripMenuItem";
            this.roboRallyToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.roboRallyToolStripMenuItem.Text = "Game";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startGameToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(210, 6);
            // 
            // gameSpeedToolStripMenuItem
            // 
            this.gameSpeedToolStripMenuItem.Name = "gameSpeedToolStripMenuItem";
            this.gameSpeedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.gameSpeedToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.gameSpeedToolStripMenuItem.Text = "Game Speed +";
            this.gameSpeedToolStripMenuItem.Click += new System.EventHandler(this.gameSpeedPlusToolStripMenuItem_Click);
            // 
            // gameSpeedToolStripMenuItem1
            // 
            this.gameSpeedToolStripMenuItem1.Name = "gameSpeedToolStripMenuItem1";
            this.gameSpeedToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.gameSpeedToolStripMenuItem1.Size = new System.Drawing.Size(213, 22);
            this.gameSpeedToolStripMenuItem1.Text = "Game Speed -";
            this.gameSpeedToolStripMenuItem1.Click += new System.EventHandler(this.gameSpeedMinusToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(210, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editorToolStripMenuItem
            // 
            this.editorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadBoardToolStripMenuItem,
            this.newBoardToolStripMenuItem,
            this.toolStripSeparator2,
            this.saveBoardToolStripMenuItem,
            this.saveBoardAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.showEditorToolStripMenuItem,
            this.changeAssetDirectoryToolStripMenuItem});
            this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            this.editorToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.editorToolStripMenuItem.Text = "Board";
            // 
            // loadBoardToolStripMenuItem
            // 
            this.loadBoardToolStripMenuItem.Name = "loadBoardToolStripMenuItem";
            this.loadBoardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.loadBoardToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.loadBoardToolStripMenuItem.Text = "Load Board";
            this.loadBoardToolStripMenuItem.Click += new System.EventHandler(this.loadBoardToolStripMenuItem_Click);
            // 
            // newBoardToolStripMenuItem
            // 
            this.newBoardToolStripMenuItem.Name = "newBoardToolStripMenuItem";
            this.newBoardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newBoardToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.newBoardToolStripMenuItem.Text = "New board";
            this.newBoardToolStripMenuItem.Click += new System.EventHandler(this.newBoardToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(227, 6);
            // 
            // saveBoardToolStripMenuItem
            // 
            this.saveBoardToolStripMenuItem.Name = "saveBoardToolStripMenuItem";
            this.saveBoardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveBoardToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.saveBoardToolStripMenuItem.Text = "Save board";
            this.saveBoardToolStripMenuItem.Click += new System.EventHandler(this.saveBoardToolStripMenuItem_Click);
            // 
            // saveBoardAsToolStripMenuItem
            // 
            this.saveBoardAsToolStripMenuItem.Name = "saveBoardAsToolStripMenuItem";
            this.saveBoardAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveBoardAsToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.saveBoardAsToolStripMenuItem.Text = "Save board as ...";
            this.saveBoardAsToolStripMenuItem.Click += new System.EventHandler(this.saveBoardAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(227, 6);
            // 
            // showEditorToolStripMenuItem
            // 
            this.showEditorToolStripMenuItem.Name = "showEditorToolStripMenuItem";
            this.showEditorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.showEditorToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.showEditorToolStripMenuItem.Text = "Show Editor";
            this.showEditorToolStripMenuItem.Click += new System.EventHandler(this.showEditorToolStripMenuItem_Click);
            // 
            // changeAssetDirectoryToolStripMenuItem
            // 
            this.changeAssetDirectoryToolStripMenuItem.Name = "changeAssetDirectoryToolStripMenuItem";
            this.changeAssetDirectoryToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.changeAssetDirectoryToolStripMenuItem.Text = "Change Asset Directory";
            this.changeAssetDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changeAssetDirectoryToolStripMenuItem_Click);
            // 
            // playerToolStripMenuItem
            // 
            this.playerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadKIToolStripMenuItem,
            this.toolStripSeparator3});
            this.playerToolStripMenuItem.Name = "playerToolStripMenuItem";
            this.playerToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.playerToolStripMenuItem.Text = "Player";
            // 
            // loadKIToolStripMenuItem
            // 
            this.loadKIToolStripMenuItem.Name = "loadKIToolStripMenuItem";
            this.loadKIToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.loadKIToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadKIToolStripMenuItem.Text = "Load AI";
            this.loadKIToolStripMenuItem.Click += new System.EventHandler(this.loadKIToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // toolStripContainer
            // 
            this.toolStripContainer.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStripContainer.ContentPanel.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(791, 506);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Margin = new System.Windows.Forms.Padding(2);
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // toolStripContainer.RightToolStripPanel
            // 
            this.toolStripContainer.RightToolStripPanel.Controls.Add(this.toolStripRight);
            this.toolStripContainer.Size = new System.Drawing.Size(815, 530);
            this.toolStripContainer.TabIndex = 2;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.editControl);
            this.splitContainer.Panel1Collapsed = true;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.boardPlayerSplitContainer);
            this.splitContainer.Size = new System.Drawing.Size(791, 506);
            this.splitContainer.SplitterDistance = 246;
            this.splitContainer.TabIndex = 2;
            // 
            // editControl
            // 
            this.editControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editControl.Location = new System.Drawing.Point(0, 0);
            this.editControl.MaximumSize = new System.Drawing.Size(255, 0);
            this.editControl.MinimumSize = new System.Drawing.Size(255, 300);
            this.editControl.Name = "editControl";
            this.editControl.Size = new System.Drawing.Size(255, 300);
            this.editControl.TabIndex = 1;
            // 
            // boardPlayerSplitContainer
            // 
            this.boardPlayerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardPlayerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.boardPlayerSplitContainer.IsSplitterFixed = true;
            this.boardPlayerSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.boardPlayerSplitContainer.Name = "boardPlayerSplitContainer";
            // 
            // boardPlayerSplitContainer.Panel1
            // 
            this.boardPlayerSplitContainer.Panel1.Controls.Add(this.boardControl);
            // 
            // boardPlayerSplitContainer.Panel2
            // 
            this.boardPlayerSplitContainer.Panel2.Controls.Add(this.playerPanel);
            this.boardPlayerSplitContainer.Panel2MinSize = 300;
            this.boardPlayerSplitContainer.Size = new System.Drawing.Size(791, 506);
            this.boardPlayerSplitContainer.SplitterDistance = 487;
            this.boardPlayerSplitContainer.TabIndex = 1;
            // 
            // boardControl
            // 
            this.boardControl.AllowDrop = true;
            this.boardControl.BackColor = System.Drawing.Color.Black;
            this.boardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boardControl.Location = new System.Drawing.Point(0, 0);
            this.boardControl.Margin = new System.Windows.Forms.Padding(2);
            this.boardControl.Name = "boardControl";
            this.boardControl.Size = new System.Drawing.Size(487, 506);
            this.boardControl.TabIndex = 0;
            this.boardControl.Paint += new System.Windows.Forms.PaintEventHandler(this.boardControl_Paint);
            // 
            // playerPanel
            // 
            this.playerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.playerPanel.Location = new System.Drawing.Point(0, 0);
            this.playerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(300, 506);
            this.playerPanel.TabIndex = 0;
            this.playerPanel.WrapContents = false;
            // 
            // toolStripRight
            // 
            this.toolStripRight.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripRight.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerPanelToggleButton});
            this.toolStripRight.Location = new System.Drawing.Point(0, 0);
            this.toolStripRight.Name = "toolStripRight";
            this.toolStripRight.Size = new System.Drawing.Size(24, 506);
            this.toolStripRight.Stretch = true;
            this.toolStripRight.TabIndex = 0;
            // 
            // playerPanelToggleButton
            // 
            this.playerPanelToggleButton.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerPanelToggleButton.Image = ((System.Drawing.Image)(resources.GetObject("playerPanelToggleButton.Image")));
            this.playerPanelToggleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playerPanelToggleButton.Name = "playerPanelToggleButton";
            this.playerPanelToggleButton.Size = new System.Drawing.Size(22, 61);
            this.playerPanelToggleButton.Text = "Players";
            this.playerPanelToggleButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            this.playerPanelToggleButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.playerPanelToggleButton.Click += new System.EventHandler(this.playerPanelToggleButton_Click);
            // 
            // openKIFileDialog
            // 
            this.openKIFileDialog.InitialDirectory = ".";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // RoboRallyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 530);
            this.Controls.Add(this.toolStripContainer);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RoboRallyForm";
            this.Text = "MonoRobots";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.RightToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.RightToolStripPanel.PerformLayout();
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.boardPlayerSplitContainer.Panel1.ResumeLayout(false);
            this.boardPlayerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boardPlayerSplitContainer)).EndInit();
            this.boardPlayerSplitContainer.ResumeLayout(false);
            this.toolStripRight.ResumeLayout(false);
            this.toolStripRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem roboRallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private BoardControl boardControl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripMenuItem newBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem saveBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBoardAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private EditControl editControl;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem showEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openKIFileDialog;
        private System.Windows.Forms.ToolStripMenuItem loadKIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem loadBoardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSpeedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripMenuItem changeAssetDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripRight;
        private System.Windows.Forms.ToolStripButton playerPanelToggleButton;
        private System.Windows.Forms.SplitContainer boardPlayerSplitContainer;
        private System.Windows.Forms.FlowLayoutPanel playerPanel;
    }
}