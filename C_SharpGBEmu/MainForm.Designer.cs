namespace C_SharpGBEmu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainSplitter = new System.Windows.Forms.SplitContainer();
            this.MainFormToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenDebuggerButton = new System.Windows.Forms.ToolStripButton();
            this.RunButton = new System.Windows.Forms.ToolStripButton();
            this.MainSplitter.Panel1.SuspendLayout();
            this.MainSplitter.SuspendLayout();
            this.MainFormToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainSplitter
            // 
            this.MainSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitter.IsSplitterFixed = true;
            this.MainSplitter.Location = new System.Drawing.Point(0, 0);
            this.MainSplitter.Name = "MainSplitter";
            this.MainSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitter.Panel1
            // 
            this.MainSplitter.Panel1.Controls.Add(this.MainFormToolStrip);
            // 
            // MainSplitter.Panel2
            // 
            this.MainSplitter.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.MainSplitter_Panel2_Paint);
            this.MainSplitter.Size = new System.Drawing.Size(309, 288);
            this.MainSplitter.SplitterDistance = 25;
            this.MainSplitter.TabIndex = 2;
            // 
            // MainFormToolStrip
            // 
            this.MainFormToolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFormToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainFormToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenDebuggerButton,
            this.RunButton});
            this.MainFormToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MainFormToolStrip.Name = "MainFormToolStrip";
            this.MainFormToolStrip.Size = new System.Drawing.Size(309, 25);
            this.MainFormToolStrip.TabIndex = 2;
            this.MainFormToolStrip.Text = "toolStrip2";
            // 
            // OpenDebuggerButton
            // 
            this.OpenDebuggerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenDebuggerButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenDebuggerButton.Image")));
            this.OpenDebuggerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenDebuggerButton.Name = "OpenDebuggerButton";
            this.OpenDebuggerButton.Size = new System.Drawing.Size(23, 22);
            this.OpenDebuggerButton.Text = "Disassembly";
            this.OpenDebuggerButton.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // RunButton
            // 
            this.RunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunButton.Image = ((System.Drawing.Image)(resources.GetObject("RunButton.Image")));
            this.RunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(23, 22);
            this.RunButton.Text = "RunButton";
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 288);
            this.Controls.Add(this.MainSplitter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MainSplitter.Panel1.ResumeLayout(false);
            this.MainSplitter.Panel1.PerformLayout();
            this.MainSplitter.ResumeLayout(false);
            this.MainFormToolStrip.ResumeLayout(false);
            this.MainFormToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitter;
        private System.Windows.Forms.ToolStrip MainFormToolStrip;
        private System.Windows.Forms.ToolStripButton OpenDebuggerButton;
        private System.Windows.Forms.ToolStripButton RunButton;


    }
}

