namespace C_SharpGBEmu
{
    partial class DisassembleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisassembleForm));
            XPTable.Models.Row row1 = new XPTable.Models.Row();
            this.DisassembleToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxAdress = new System.Windows.Forms.ToolStripTextBox();
            this.JumpToLocationButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.StepIntoButton = new System.Windows.Forms.ToolStripButton();
            this.StepOverButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.DisasmPanel = new BufferedPanel();
            this.DisasmScrollBar = new System.Windows.Forms.VScrollBar();
            this.DisassembleTable = new XPTable.Models.Table();
            this.DisasmColumnModel = new XPTable.Models.ColumnModel();
            this.PCColumn1 = new XPTable.Models.TextColumn();
            this.OPCodeColumn = new XPTable.Models.TextColumn();
            this.OPCodeColumnHex = new XPTable.Models.TextColumn();
            this.DisasmTableModel = new XPTable.Models.TableModel();
            this.textColumn1 = new XPTable.Models.TextColumn();
            this.PCColumn = new XPTable.Models.TextColumn();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.HLTextBox = new System.Windows.Forms.TextBox();
            this.AFTextBox = new System.Windows.Forms.TextBox();
            this.BCTextBox = new System.Windows.Forms.TextBox();
            this.DETextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.CcheckBox = new System.Windows.Forms.CheckBox();
            this.NcheckBox = new System.Windows.Forms.CheckBox();
            this.HcheckBox = new System.Windows.Forms.CheckBox();
            this.ZcheckBox = new System.Windows.Forms.CheckBox();
            this.PCTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.StackPanelScrollBar = new System.Windows.Forms.VScrollBar();
            this.StackPanel1 = new BufferedPanel();
            this.SPTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.AdressToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.AdressTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.MemoryPanel = new BufferedPanel();
            this.AdressScrollBar = new System.Windows.Forms.VScrollBar();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.ResetButton = new System.Windows.Forms.ToolStripButton();
            this.PauseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.RunButton = new System.Windows.Forms.ToolStripButton();
            this.VRAMButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TickTimer = new System.Windows.Forms.Timer(this.components);
            this.DisAsmPanelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RunToCursorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleBreakpointMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRomFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.DisassembleToolStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisassembleTable)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.AdressToolStrip.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.MainToolStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.DisAsmPanelContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisassembleToolStrip
            // 
            this.DisassembleToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBoxAdress,
            this.JumpToLocationButton,
            this.toolStripSeparator1,
            this.StepIntoButton,
            this.StepOverButton});
            this.DisassembleToolStrip.Location = new System.Drawing.Point(0, 0);
            this.DisassembleToolStrip.Name = "DisassembleToolStrip";
            this.DisassembleToolStrip.Size = new System.Drawing.Size(381, 25);
            this.DisassembleToolStrip.TabIndex = 1;
            this.DisassembleToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(25, 22);
            this.toolStripLabel1.Text = "PC:";
            // 
            // toolStripTextBoxAdress
            // 
            this.toolStripTextBoxAdress.Name = "toolStripTextBoxAdress";
            this.toolStripTextBoxAdress.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxAdress.TextChanged += new System.EventHandler(this.toolStripTextBoxAdress_TextChanged);
            // 
            // JumpToLocationButton
            // 
            this.JumpToLocationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.JumpToLocationButton.Image = ((System.Drawing.Image)(resources.GetObject("JumpToLocationButton.Image")));
            this.JumpToLocationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.JumpToLocationButton.Name = "JumpToLocationButton";
            this.JumpToLocationButton.Size = new System.Drawing.Size(37, 22);
            this.JumpToLocationButton.Text = "Goto";
            this.JumpToLocationButton.Click += new System.EventHandler(this.JumpToLocationButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // StepIntoButton
            // 
            this.StepIntoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StepIntoButton.Image = ((System.Drawing.Image)(resources.GetObject("StepIntoButton.Image")));
            this.StepIntoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StepIntoButton.Name = "StepIntoButton";
            this.StepIntoButton.Size = new System.Drawing.Size(23, 22);
            this.StepIntoButton.Text = "Step Into";
            this.StepIntoButton.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // StepOverButton
            // 
            this.StepOverButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StepOverButton.Image = ((System.Drawing.Image)(resources.GetObject("StepOverButton.Image")));
            this.StepOverButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StepOverButton.Name = "StepOverButton";
            this.StepOverButton.Size = new System.Drawing.Size(23, 22);
            this.StepOverButton.Text = "Step Over";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DisassembleToolStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer1.Panel2.Controls.Add(this.DisassembleTable);
            this.splitContainer1.Size = new System.Drawing.Size(381, 596);
            this.splitContainer1.SplitterDistance = 26;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.Controls.Add(this.DisasmPanel);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.DisasmScrollBar);
            this.splitContainer6.Panel2MinSize = 17;
            this.splitContainer6.Size = new System.Drawing.Size(381, 566);
            this.splitContainer6.SplitterDistance = 360;
            this.splitContainer6.TabIndex = 4;
            // 
            // DisasmPanel
            // 
            this.DisasmPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisasmPanel.Location = new System.Drawing.Point(0, 0);
            this.DisasmPanel.Name = "DisasmPanel";
            this.DisasmPanel.Size = new System.Drawing.Size(360, 566);
            this.DisasmPanel.TabIndex = 0;
            this.DisasmPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DisasmPanel_Paint);
            this.DisasmPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DisasmPanel_MouseDoubleClick);
            this.DisasmPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DisasmPanel_MouseClick);
            this.DisasmPanel.Resize += new System.EventHandler(this.DisasmPanel_Resize);
            // 
            // DisasmScrollBar
            // 
            this.DisasmScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisasmScrollBar.Location = new System.Drawing.Point(0, 0);
            this.DisasmScrollBar.Name = "DisasmScrollBar";
            this.DisasmScrollBar.Size = new System.Drawing.Size(17, 566);
            this.DisasmScrollBar.TabIndex = 0;
            this.DisasmScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.DisasmScrollBar_Scroll);
            // 
            // DisassembleTable
            // 
            this.DisassembleTable.AllowSelection = false;
            this.DisassembleTable.ColumnModel = this.DisasmColumnModel;
            this.DisassembleTable.Location = new System.Drawing.Point(132, 44);
            this.DisassembleTable.Name = "DisassembleTable";
            this.DisassembleTable.Size = new System.Drawing.Size(147, 209);
            this.DisassembleTable.TabIndex = 3;
            this.DisassembleTable.TableModel = this.DisasmTableModel;
            this.DisassembleTable.Text = "table1";
            this.DisassembleTable.Visible = false;
            // 
            // DisasmColumnModel
            // 
            this.DisasmColumnModel.Columns.AddRange(new XPTable.Models.Column[] {
            this.PCColumn1,
            this.OPCodeColumn,
            this.OPCodeColumnHex});
            // 
            // PCColumn1
            // 
            this.PCColumn1.Text = "PC";
            // 
            // OPCodeColumn
            // 
            this.OPCodeColumn.Text = "OPCode";
            this.OPCodeColumn.Width = 85;
            // 
            // OPCodeColumnHex
            // 
            this.OPCodeColumnHex.Text = "Hex";
            // 
            // DisasmTableModel
            // 
            this.DisasmTableModel.Rows.AddRange(new XPTable.Models.Row[] {
            row1});
            // 
            // textColumn1
            // 
            this.textColumn1.Text = "PC";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(965, 596);
            this.splitContainer2.SplitterDistance = 381;
            this.splitContainer2.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer8);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(580, 596);
            this.splitContainer3.SplitterDistance = 246;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer8
            // 
            this.splitContainer8.IsSplitterFixed = true;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer8.Panel1.Controls.Add(this.groupBox4);
            this.splitContainer8.Panel1.Controls.Add(this.PCTextBox);
            this.splitContainer8.Panel1.Controls.Add(this.label12);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.Controls.Add(this.StackPanelScrollBar);
            this.splitContainer8.Panel2.Controls.Add(this.StackPanel1);
            this.splitContainer8.Panel2.Controls.Add(this.SPTextBox);
            this.splitContainer8.Panel2.Controls.Add(this.label11);
            this.splitContainer8.Size = new System.Drawing.Size(577, 243);
            this.splitContainer8.SplitterDistance = 266;
            this.splitContainer8.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.HLTextBox);
            this.groupBox1.Controls.Add(this.AFTextBox);
            this.groupBox1.Controls.Add(this.BCTextBox);
            this.groupBox1.Controls.Add(this.DETextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(16, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 64);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GP Registers";
            // 
            // HLTextBox
            // 
            this.HLTextBox.Location = new System.Drawing.Point(161, 34);
            this.HLTextBox.Name = "HLTextBox";
            this.HLTextBox.Size = new System.Drawing.Size(40, 20);
            this.HLTextBox.TabIndex = 3;
            this.HLTextBox.Text = "0";
            // 
            // AFTextBox
            // 
            this.AFTextBox.Location = new System.Drawing.Point(23, 34);
            this.AFTextBox.Name = "AFTextBox";
            this.AFTextBox.Size = new System.Drawing.Size(40, 20);
            this.AFTextBox.TabIndex = 0;
            this.AFTextBox.Text = "0";
            this.AFTextBox.TextChanged += new System.EventHandler(this.AFTextBox_TextChanged);
            // 
            // BCTextBox
            // 
            this.BCTextBox.Location = new System.Drawing.Point(69, 34);
            this.BCTextBox.Name = "BCTextBox";
            this.BCTextBox.Size = new System.Drawing.Size(40, 20);
            this.BCTextBox.TabIndex = 1;
            this.BCTextBox.Text = "0";
            // 
            // DETextBox
            // 
            this.DETextBox.Location = new System.Drawing.Point(115, 34);
            this.DETextBox.Name = "DETextBox";
            this.DETextBox.Size = new System.Drawing.Size(40, 20);
            this.DETextBox.TabIndex = 2;
            this.DETextBox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "AF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "BC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "DE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(160, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "HL";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.CcheckBox);
            this.groupBox4.Controls.Add(this.NcheckBox);
            this.groupBox4.Controls.Add(this.HcheckBox);
            this.groupBox4.Controls.Add(this.ZcheckBox);
            this.groupBox4.Location = new System.Drawing.Point(17, 94);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(154, 58);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Flags";
            // 
            // CcheckBox
            // 
            this.CcheckBox.AutoSize = true;
            this.CcheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CcheckBox.Location = new System.Drawing.Point(103, 19);
            this.CcheckBox.Name = "CcheckBox";
            this.CcheckBox.Size = new System.Drawing.Size(18, 31);
            this.CcheckBox.TabIndex = 7;
            this.CcheckBox.Text = "C";
            this.CcheckBox.UseVisualStyleBackColor = true;
            // 
            // NcheckBox
            // 
            this.NcheckBox.AutoSize = true;
            this.NcheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.NcheckBox.Location = new System.Drawing.Point(53, 19);
            this.NcheckBox.Name = "NcheckBox";
            this.NcheckBox.Size = new System.Drawing.Size(19, 31);
            this.NcheckBox.TabIndex = 3;
            this.NcheckBox.Text = "N";
            this.NcheckBox.UseVisualStyleBackColor = true;
            // 
            // HcheckBox
            // 
            this.HcheckBox.AutoSize = true;
            this.HcheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.HcheckBox.Location = new System.Drawing.Point(78, 19);
            this.HcheckBox.Name = "HcheckBox";
            this.HcheckBox.Size = new System.Drawing.Size(19, 31);
            this.HcheckBox.TabIndex = 2;
            this.HcheckBox.Text = "H";
            this.HcheckBox.UseVisualStyleBackColor = true;
            // 
            // ZcheckBox
            // 
            this.ZcheckBox.AutoSize = true;
            this.ZcheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ZcheckBox.Location = new System.Drawing.Point(29, 19);
            this.ZcheckBox.Name = "ZcheckBox";
            this.ZcheckBox.Size = new System.Drawing.Size(18, 31);
            this.ZcheckBox.TabIndex = 0;
            this.ZcheckBox.Text = "Z";
            this.ZcheckBox.UseVisualStyleBackColor = true;
            // 
            // PCTextBox
            // 
            this.PCTextBox.Location = new System.Drawing.Point(38, 194);
            this.PCTextBox.Name = "PCTextBox";
            this.PCTextBox.Size = new System.Drawing.Size(40, 20);
            this.PCTextBox.TabIndex = 22;
            this.PCTextBox.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(35, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "PC";
            // 
            // StackPanelScrollBar
            // 
            this.StackPanelScrollBar.Location = new System.Drawing.Point(287, 0);
            this.StackPanelScrollBar.Name = "StackPanelScrollBar";
            this.StackPanelScrollBar.Size = new System.Drawing.Size(17, 240);
            this.StackPanelScrollBar.TabIndex = 1;
            this.StackPanelScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.StackPanelScrollBar_Scroll);
            // 
            // StackPanel1
            // 
            this.StackPanel1.Location = new System.Drawing.Point(3, 30);
            this.StackPanel1.Name = "StackPanel1";
            this.StackPanel1.Size = new System.Drawing.Size(286, 210);
            this.StackPanel1.TabIndex = 0;
            this.StackPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.StackPanel1_Paint);
            this.StackPanel1.Resize += new System.EventHandler(this.StackPanel1_Resize);
            // 
            // SPTextBox
            // 
            this.SPTextBox.Location = new System.Drawing.Point(36, 5);
            this.SPTextBox.Name = "SPTextBox";
            this.SPTextBox.Size = new System.Drawing.Size(40, 20);
            this.SPTextBox.TabIndex = 23;
            this.SPTextBox.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "SP";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.AdressToolStrip);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.AutoScroll = true;
            this.splitContainer4.Panel2.Controls.Add(this.splitContainer5);
            this.splitContainer4.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.MemoryPanel_Paint);
            this.splitContainer4.Panel2.Resize += new System.EventHandler(this.MemoryPanel_Resize);
            this.splitContainer4.Size = new System.Drawing.Size(580, 346);
            this.splitContainer4.SplitterDistance = 25;
            this.splitContainer4.TabIndex = 1;
            // 
            // AdressToolStrip
            // 
            this.AdressToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.AdressTextBox});
            this.AdressToolStrip.Location = new System.Drawing.Point(0, 0);
            this.AdressToolStrip.Name = "AdressToolStrip";
            this.AdressToolStrip.Size = new System.Drawing.Size(580, 25);
            this.AdressToolStrip.TabIndex = 0;
            this.AdressToolStrip.Text = "toolStrip1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel2.Text = "Adress:";
            // 
            // AdressTextBox
            // 
            this.AdressTextBox.Name = "AdressTextBox";
            this.AdressTextBox.Size = new System.Drawing.Size(100, 25);
            this.AdressTextBox.Text = "0";
            this.AdressTextBox.TextChanged += new System.EventHandler(this.AdressTextBox_TextChanged);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.MemoryPanel);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.AdressScrollBar);
            this.splitContainer5.Panel2MinSize = 17;
            this.splitContainer5.Size = new System.Drawing.Size(580, 317);
            this.splitContainer5.SplitterDistance = 559;
            this.splitContainer5.TabIndex = 1;
            // 
            // MemoryPanel
            // 
            this.MemoryPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MemoryPanel.Location = new System.Drawing.Point(0, 0);
            this.MemoryPanel.Name = "MemoryPanel";
            this.MemoryPanel.Size = new System.Drawing.Size(559, 317);
            this.MemoryPanel.TabIndex = 0;
            this.MemoryPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MemoryPanel_Paint);
            this.MemoryPanel.Resize += new System.EventHandler(this.MemoryPanel_Resize);
            // 
            // AdressScrollBar
            // 
            this.AdressScrollBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AdressScrollBar.LargeChange = 64;
            this.AdressScrollBar.Location = new System.Drawing.Point(0, 0);
            this.AdressScrollBar.Maximum = 4096;
            this.AdressScrollBar.Name = "AdressScrollBar";
            this.AdressScrollBar.Size = new System.Drawing.Size(17, 317);
            this.AdressScrollBar.TabIndex = 0;
            this.AdressScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.AdressScrollBar_Scroll);
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer7.IsSplitterFixed = true;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.MainToolStrip);
            this.splitContainer7.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer7.Size = new System.Drawing.Size(965, 650);
            this.splitContainer7.SplitterDistance = 53;
            this.splitContainer7.SplitterWidth = 1;
            this.splitContainer7.TabIndex = 5;
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ResetButton,
            this.PauseToolStripButton,
            this.RunButton,
            this.VRAMButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 28);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.Size = new System.Drawing.Size(965, 25);
            this.MainToolStrip.TabIndex = 0;
            this.MainToolStrip.Text = "toolStrip1";
            // 
            // ResetButton
            // 
            this.ResetButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ResetButton.Image = ((System.Drawing.Image)(resources.GetObject("ResetButton.Image")));
            this.ResetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(23, 22);
            this.ResetButton.Text = "ResetButton";
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // PauseToolStripButton
            // 
            this.PauseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PauseToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("PauseToolStripButton.Image")));
            this.PauseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PauseToolStripButton.Name = "PauseToolStripButton";
            this.PauseToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.PauseToolStripButton.Text = "toolStripButton1";
            this.PauseToolStripButton.Click += new System.EventHandler(this.PauseToolStripButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RunButton.Image = ((System.Drawing.Image)(resources.GetObject("RunButton.Image")));
            this.RunButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(23, 22);
            this.RunButton.Text = "Run";
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // VRAMButton
            // 
            this.VRAMButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.VRAMButton.Image = ((System.Drawing.Image)(resources.GetObject("VRAMButton.Image")));
            this.VRAMButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VRAMButton.Name = "VRAMButton";
            this.VRAMButton.Size = new System.Drawing.Size(23, 22);
            this.VRAMButton.Text = "VRAM";
            this.VRAMButton.Click += new System.EventHandler(this.VRAMButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(965, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRomToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // loadRomToolStripMenuItem
            // 
            this.loadRomToolStripMenuItem.Name = "loadRomToolStripMenuItem";
            this.loadRomToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.loadRomToolStripMenuItem.Text = "Load ROM...";
            this.loadRomToolStripMenuItem.Click += new System.EventHandler(this.loadRomToolStripMenuItem_Click);
            // 
            // TickTimer
            // 
            this.TickTimer.Interval = 160;
            this.TickTimer.Tick += new System.EventHandler(this.TickTimer_Tick);
            // 
            // DisAsmPanelContextMenuStrip
            // 
            this.DisAsmPanelContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunToCursorMenuItem,
            this.ToggleBreakpointMenuItem});
            this.DisAsmPanelContextMenuStrip.Name = "DisAsmPanelContextMenuStrip";
            this.DisAsmPanelContextMenuStrip.Size = new System.Drawing.Size(172, 48);
            // 
            // RunToCursorMenuItem
            // 
            this.RunToCursorMenuItem.Name = "RunToCursorMenuItem";
            this.RunToCursorMenuItem.Size = new System.Drawing.Size(171, 22);
            this.RunToCursorMenuItem.Text = "Run to cursor";
            this.RunToCursorMenuItem.Click += new System.EventHandler(this.RunToCursorMenuItem_Click);
            // 
            // ToggleBreakpointMenuItem
            // 
            this.ToggleBreakpointMenuItem.Name = "ToggleBreakpointMenuItem";
            this.ToggleBreakpointMenuItem.Size = new System.Drawing.Size(171, 22);
            this.ToggleBreakpointMenuItem.Text = "Toggle breakpoint";
            this.ToggleBreakpointMenuItem.Click += new System.EventHandler(this.ToggleBreakpointMenuItem_Click);
            // 
            // DisassembleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 650);
            this.Controls.Add(this.splitContainer7);
            this.Name = "DisassembleForm";
            this.Text = "DisassembleForm";
            this.Load += new System.EventHandler(this.DisassembleForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisassembleForm_FormClosing);
            this.DisassembleToolStrip.ResumeLayout(false);
            this.DisassembleToolStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DisassembleTable)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel1.PerformLayout();
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.Panel2.PerformLayout();
            this.splitContainer8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.AdressToolStrip.ResumeLayout(false);
            this.AdressToolStrip.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel1.PerformLayout();
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.ResumeLayout(false);
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.DisAsmPanelContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip DisassembleToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxAdress;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private XPTable.Models.TableModel DisasmTableModel;
        private XPTable.Models.ColumnModel DisasmColumnModel;
        private XPTable.Models.TextColumn PCColumn1;
        private XPTable.Models.TextColumn OPCodeColumn;
        private XPTable.Models.TextColumn OPCodeColumnHex;
        private XPTable.Models.TextColumn textColumn1;
        private XPTable.Models.TextColumn PCColumn;
        private XPTable.Models.Table DisassembleTable;
        private System.Windows.Forms.ToolStripButton JumpToLocationButton;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HLTextBox;
        private System.Windows.Forms.TextBox DETextBox;
        private System.Windows.Forms.TextBox BCTextBox;
        private System.Windows.Forms.TextBox AFTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox SPTextBox;
        private System.Windows.Forms.TextBox PCTextBox;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStrip AdressToolStrip;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox AdressTextBox;
        private System.Windows.Forms.VScrollBar AdressScrollBar;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.ToolStripButton StepIntoButton;
        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.VScrollBar DisasmScrollBar;
        private BufferedPanel DisasmPanel;
        private BufferedPanel MemoryPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton StepOverButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox NcheckBox;
        private System.Windows.Forms.CheckBox HcheckBox;
        private System.Windows.Forms.CheckBox ZcheckBox;
        private System.Windows.Forms.CheckBox CcheckBox;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton ResetButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton RunButton;
        private System.Windows.Forms.ToolStripButton VRAMButton;
        private System.Windows.Forms.ToolStripButton PauseToolStripButton;
        private System.Windows.Forms.Timer TickTimer;
        private System.Windows.Forms.ContextMenuStrip DisAsmPanelContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem RunToCursorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToggleBreakpointMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private BufferedPanel StackPanel1;
        private System.Windows.Forms.VScrollBar StackPanelScrollBar;
        private System.Windows.Forms.OpenFileDialog loadRomFileDialog1;
    }
}