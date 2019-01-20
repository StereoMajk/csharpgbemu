namespace C_SharpGBEmu
{
    partial class VRAMForm
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
            this.VRAMTabControl = new System.Windows.Forms.TabControl();
            this.ScreenTabPage = new System.Windows.Forms.TabPage();
            this.vramPictureBox = new System.Windows.Forms.PictureBox();
            this.DownButton = new System.Windows.Forms.Button();
            this.UpButton = new System.Windows.Forms.Button();
            this.RightButton = new System.Windows.Forms.Button();
            this.LeftButton = new System.Windows.Forms.Button();
            this.BButton = new System.Windows.Forms.Button();
            this.AButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.TilemapTabPage = new System.Windows.Forms.TabPage();
            this.tilemapPictureBox = new System.Windows.Forms.PictureBox();
            this.VRAMTabControl.SuspendLayout();
            this.ScreenTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vramPictureBox)).BeginInit();
            this.TilemapTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tilemapPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // VRAMTabControl
            // 
            this.VRAMTabControl.Controls.Add(this.ScreenTabPage);
            this.VRAMTabControl.Controls.Add(this.TilemapTabPage);
            this.VRAMTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VRAMTabControl.Location = new System.Drawing.Point(0, 0);
            this.VRAMTabControl.Name = "VRAMTabControl";
            this.VRAMTabControl.SelectedIndex = 0;
            this.VRAMTabControl.Size = new System.Drawing.Size(558, 450);
            this.VRAMTabControl.TabIndex = 1;
            this.VRAMTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.VRAMTabControl_Selected);
            // 
            // ScreenTabPage
            // 
            this.ScreenTabPage.Controls.Add(this.vramPictureBox);
            this.ScreenTabPage.Controls.Add(this.DownButton);
            this.ScreenTabPage.Controls.Add(this.UpButton);
            this.ScreenTabPage.Controls.Add(this.RightButton);
            this.ScreenTabPage.Controls.Add(this.LeftButton);
            this.ScreenTabPage.Controls.Add(this.BButton);
            this.ScreenTabPage.Controls.Add(this.AButton);
            this.ScreenTabPage.Controls.Add(this.StartButton);
            this.ScreenTabPage.Location = new System.Drawing.Point(4, 22);
            this.ScreenTabPage.Name = "ScreenTabPage";
            this.ScreenTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScreenTabPage.Size = new System.Drawing.Size(550, 424);
            this.ScreenTabPage.TabIndex = 0;
            this.ScreenTabPage.Text = "Screen";
            this.ScreenTabPage.UseVisualStyleBackColor = true;
            // 
            // vramPictureBox
            // 
            this.vramPictureBox.Location = new System.Drawing.Point(269, 4);
            this.vramPictureBox.Name = "vramPictureBox";
            this.vramPictureBox.Size = new System.Drawing.Size(256, 256);
            this.vramPictureBox.TabIndex = 9;
            this.vramPictureBox.TabStop = false;
            // 
            // DownButton
            // 
            this.DownButton.Location = new System.Drawing.Point(90, 332);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(75, 23);
            this.DownButton.TabIndex = 8;
            this.DownButton.Text = "Down";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DownButton_MouseDown);
            this.DownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DownButton_MouseUp);
            // 
            // UpButton
            // 
            this.UpButton.Location = new System.Drawing.Point(90, 274);
            this.UpButton.Name = "UpButton";
            this.UpButton.Size = new System.Drawing.Size(75, 23);
            this.UpButton.TabIndex = 7;
            this.UpButton.Text = "Up";
            this.UpButton.UseVisualStyleBackColor = true;
            this.UpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UpButton_MouseDown);
            this.UpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UpButton_MouseUp);
            // 
            // RightButton
            // 
            this.RightButton.Location = new System.Drawing.Point(161, 303);
            this.RightButton.Name = "RightButton";
            this.RightButton.Size = new System.Drawing.Size(75, 23);
            this.RightButton.TabIndex = 6;
            this.RightButton.Text = "Right";
            this.RightButton.UseVisualStyleBackColor = true;
            this.RightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RightButton_MouseDown);
            this.RightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RightButton_MouseUp);
            // 
            // LeftButton
            // 
            this.LeftButton.Location = new System.Drawing.Point(18, 303);
            this.LeftButton.Name = "LeftButton";
            this.LeftButton.Size = new System.Drawing.Size(75, 23);
            this.LeftButton.TabIndex = 5;
            this.LeftButton.Text = "Left";
            this.LeftButton.UseVisualStyleBackColor = true;
            this.LeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftButton_MouseDown);
            this.LeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftButton_MouseUp);
            // 
            // BButton
            // 
            this.BButton.Location = new System.Drawing.Point(256, 332);
            this.BButton.Name = "BButton";
            this.BButton.Size = new System.Drawing.Size(75, 23);
            this.BButton.TabIndex = 4;
            this.BButton.Text = "B";
            this.BButton.UseVisualStyleBackColor = true;
            this.BButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button3_MouseDown);
            this.BButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button3_MouseUp);
            // 
            // AButton
            // 
            this.AButton.Location = new System.Drawing.Point(256, 303);
            this.AButton.Name = "AButton";
            this.AButton.Size = new System.Drawing.Size(75, 23);
            this.AButton.TabIndex = 3;
            this.AButton.Text = "A";
            this.AButton.UseVisualStyleBackColor = true;
            this.AButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            this.AButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button2_MouseUp);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(256, 266);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.StartButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button1_MouseUp);
            // 
            // TilemapTabPage
            // 
            this.TilemapTabPage.Controls.Add(this.tilemapPictureBox);
            this.TilemapTabPage.Location = new System.Drawing.Point(4, 22);
            this.TilemapTabPage.Name = "TilemapTabPage";
            this.TilemapTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TilemapTabPage.Size = new System.Drawing.Size(550, 424);
            this.TilemapTabPage.TabIndex = 1;
            this.TilemapTabPage.Text = "TileMap";
            this.TilemapTabPage.UseVisualStyleBackColor = true;
            // 
            // tilemapPictureBox
            // 
            this.tilemapPictureBox.Location = new System.Drawing.Point(286, 4);
            this.tilemapPictureBox.Name = "tilemapPictureBox";
            this.tilemapPictureBox.Size = new System.Drawing.Size(256, 256);
            this.tilemapPictureBox.TabIndex = 0;
            this.tilemapPictureBox.TabStop = false;
            // 
            // VRAMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 450);
            this.Controls.Add(this.VRAMTabControl);
            this.Name = "VRAMForm";
            this.Text = "VRAMForm";
            this.Load += new System.EventHandler(this.VRAMForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VRAMForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.VRAMForm_KeyUp);
            this.VRAMTabControl.ResumeLayout(false);
            this.ScreenTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vramPictureBox)).EndInit();
            this.TilemapTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tilemapPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl VRAMTabControl;
        private System.Windows.Forms.TabPage ScreenTabPage;
        private System.Windows.Forms.TabPage TilemapTabPage;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button BButton;
        private System.Windows.Forms.Button AButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.Button UpButton;
        private System.Windows.Forms.Button RightButton;
        private System.Windows.Forms.Button LeftButton;
        private System.Windows.Forms.PictureBox vramPictureBox;
        private System.Windows.Forms.PictureBox tilemapPictureBox;
    }
}