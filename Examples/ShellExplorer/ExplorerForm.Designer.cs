namespace ShellExplorer {
    partial class ShellExplorer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellExplorer));
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.fileMenu = new System.Windows.Forms.MenuItem();
            this.dummyMenuItem = new System.Windows.Forms.MenuItem();
            this.viewMenu = new System.Windows.Forms.MenuItem();
            this.refreshMenu = new System.Windows.Forms.MenuItem();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.backButton = new System.Windows.Forms.ToolBarButton();
            this.backButtonMenu = new System.Windows.Forms.ContextMenu();
            this.forwardButton = new System.Windows.Forms.ToolBarButton();
            this.forwardButtonMenu = new System.Windows.Forms.ContextMenu();
            this.upButton = new System.Windows.Forms.ToolBarButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.shellComboBox1 = new GongSolutions.Shell.ShellComboBox();
            this.shellView = new GongSolutions.Shell.ShellView();
            this.treeView = new GongSolutions.Shell.ShellTreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 638);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(961, 22);
            this.statusBar.TabIndex = 1;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.fileMenu, this.viewMenu });
            // 
            // fileMenu
            // 
            this.fileMenu.Index = 0;
            this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.dummyMenuItem });
            this.fileMenu.Text = "&File";
            this.fileMenu.Popup += new System.EventHandler(this.fileMenu_Popup);
            // 
            // dummyMenuItem
            // 
            this.dummyMenuItem.Index = 0;
            this.dummyMenuItem.Text = "Dummy";
            this.dummyMenuItem.Visible = false;
            // 
            // viewMenu
            // 
            this.viewMenu.Index = 1;
            this.viewMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.refreshMenu });
            this.viewMenu.Text = "&View";
            // 
            // refreshMenu
            // 
            this.refreshMenu.Index = 0;
            this.refreshMenu.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.refreshMenu.Text = "&Refresh";
            this.refreshMenu.Click += new System.EventHandler(this.refreshMenu_Click);
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] { this.backButton, this.forwardButton, this.upButton });
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageList;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(961, 28);
            this.toolBar.TabIndex = 2;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
            // 
            // backButton
            // 
            this.backButton.DropDownMenu = this.backButtonMenu;
            this.backButton.ImageIndex = 0;
            this.backButton.Name = "backButton";
            this.backButton.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            // 
            // backButtonMenu
            // 
            this.backButtonMenu.Popup += new System.EventHandler(this.backButton_Popup);
            // 
            // forwardButton
            // 
            this.forwardButton.DropDownMenu = this.forwardButtonMenu;
            this.forwardButton.ImageIndex = 1;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton;
            // 
            // forwardButtonMenu
            // 
            this.forwardButtonMenu.Popup += new System.EventHandler(this.forwardButton_Popup);
            // 
            // upButton
            // 
            this.upButton.ImageIndex = 2;
            this.upButton.Name = "upButton";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList.Images.SetKeyName(0, "Back.bmp");
            this.imageList.Images.SetKeyName(1, "Forward.bmp");
            this.imageList.Images.SetKeyName(2, "Up.bmp");
            // 
            // shellComboBox1
            // 
            this.shellComboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.shellComboBox1.Editable = true;
            this.shellComboBox1.Location = new System.Drawing.Point(0, 28);
            this.shellComboBox1.Name = "shellComboBox1";
            this.shellComboBox1.Size = new System.Drawing.Size(961, 23);
            this.shellComboBox1.TabIndex = 3;
            this.shellComboBox1.Text = "shellComboBox1";
            // 
            // shellView
            // 
            this.shellView.CustomContextMenuEnable = false;
            this.shellView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shellView.Location = new System.Drawing.Point(0, 0);
            this.shellView.Name = "shellView";
            this.shellView.Size = new System.Drawing.Size(259, 587);
            this.shellView.StatusBar = this.statusBar;
            this.shellView.TabIndex = 4;
            this.shellView.Text = "shellView1";
            this.shellView.View = GongSolutions.Shell.ShellViewStyle.Details;
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.ShellView = this.shellView;
            this.treeView.Size = new System.Drawing.Size(178, 587);
            this.treeView.TabIndex = 5;
            this.treeView.Text = "shellTreeView1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 51);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(961, 587);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.shellView);
            this.splitContainer2.Size = new System.Drawing.Size(779, 587);
            this.splitContainer2.SplitterDistance = 259;
            this.splitContainer2.TabIndex = 0;
            // 
            // ShellExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 660);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.shellComboBox1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "ShellExplorer";
            this.Text = "Shell Explorer";
            this.ResizeEnd += new System.EventHandler(this.ShellExplorer_ResizeEnd);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.SplitContainer splitContainer2;

        private System.Windows.Forms.SplitContainer splitContainer1;

        #endregion
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem fileMenu;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ToolBarButton backButton;
        private System.Windows.Forms.ToolBarButton forwardButton;
        private System.Windows.Forms.ToolBarButton upButton;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ContextMenu backButtonMenu;
        private System.Windows.Forms.ContextMenu forwardButtonMenu;
        private System.Windows.Forms.MenuItem dummyMenuItem;
        private System.Windows.Forms.MenuItem viewMenu;
        private System.Windows.Forms.MenuItem refreshMenu;
        private GongSolutions.Shell.ShellComboBox shellComboBox1;
        private GongSolutions.Shell.ShellView shellView;
        private GongSolutions.Shell.ShellTreeView treeView;
    }
}

