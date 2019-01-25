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
            this.VRAMTabControl.Size = new System.Drawing.Size(322, 325);
            this.VRAMTabControl.TabIndex = 1;
            this.VRAMTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.VRAMTabControl_Selected);
            // 
            // ScreenTabPage
            // 
            this.ScreenTabPage.Controls.Add(this.vramPictureBox);
            this.ScreenTabPage.Location = new System.Drawing.Point(4, 22);
            this.ScreenTabPage.Name = "ScreenTabPage";
            this.ScreenTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScreenTabPage.Size = new System.Drawing.Size(314, 299);
            this.ScreenTabPage.TabIndex = 0;
            this.ScreenTabPage.Text = "Screen";
            this.ScreenTabPage.UseVisualStyleBackColor = true;
            // 
            // vramPictureBox
            // 
            this.vramPictureBox.Location = new System.Drawing.Point(0, 0);
            this.vramPictureBox.Name = "vramPictureBox";
            this.vramPictureBox.Size = new System.Drawing.Size(256, 256);
            this.vramPictureBox.TabIndex = 9;
            this.vramPictureBox.TabStop = false;
            // 
            // TilemapTabPage
            // 
            this.TilemapTabPage.Controls.Add(this.tilemapPictureBox);
            this.TilemapTabPage.Location = new System.Drawing.Point(4, 22);
            this.TilemapTabPage.Name = "TilemapTabPage";
            this.TilemapTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TilemapTabPage.Size = new System.Drawing.Size(314, 299);
            this.TilemapTabPage.TabIndex = 1;
            this.TilemapTabPage.Text = "Tilemap";
            this.TilemapTabPage.UseVisualStyleBackColor = true;
            // 
            // tilemapPictureBox
            // 
            this.tilemapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.tilemapPictureBox.Name = "tilemapPictureBox";
            this.tilemapPictureBox.Size = new System.Drawing.Size(256, 256);
            this.tilemapPictureBox.TabIndex = 0;
            this.tilemapPictureBox.TabStop = false;
            // 
            // VRAMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 325);
            this.Controls.Add(this.VRAMTabControl);
            this.Name = "VRAMForm";
            this.Text = "VRAM";
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
        private System.Windows.Forms.PictureBox vramPictureBox;
        private System.Windows.Forms.PictureBox tilemapPictureBox;
    }
}