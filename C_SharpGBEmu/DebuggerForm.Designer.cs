namespace C_SharpGBEmu
{
    partial class DebuggerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebuggerForm));
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder2 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer2 = new XPTable.Renderers.DragDropRenderer();
            XPTable.Models.Row row4 = new XPTable.Models.Row();
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PCTextBox = new System.Windows.Forms.TextBox();
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
            this.TickTimer = new System.Windows.Forms.Timer(this.components);
            this.DisAsmPanelContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RunToCursorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleBreakpointMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRomFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lcdGroupBox = new System.Windows.Forms.GroupBox();
            this.LCDCtextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LYtextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.STATtextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.IEtextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.IFtextBox = new System.Windows.Forms.TextBox();
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
            this.groupBox2.SuspendLayout();
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
            this.DisAsmPanelContextMenuStrip.SuspendLayout();
            this.lcdGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisassembleToolStrip
            // 
            this.DisassembleToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
            this.DisasmPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DisasmPanel_MouseClick);
            this.DisasmPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DisasmPanel_MouseDoubleClick);
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
            this.DisassembleTable.BorderColor = System.Drawing.Color.Black;
            this.DisassembleTable.ColumnModel = this.DisasmColumnModel;
            this.DisassembleTable.DataMember = null;
            this.DisassembleTable.DataSourceColumnBinder = dataSourceColumnBinder2;
            dragDropRenderer2.ForeColor = System.Drawing.Color.Red;
            this.DisassembleTable.DragDropRenderer = dragDropRenderer2;
            this.DisassembleTable.GridLinesContrainedToData = false;
            this.DisassembleTable.Location = new System.Drawing.Point(132, 44);
            this.DisassembleTable.Name = "DisassembleTable";
            this.DisassembleTable.Size = new System.Drawing.Size(147, 209);
            this.DisassembleTable.TabIndex = 3;
            this.DisassembleTable.TableModel = this.DisasmTableModel;
            this.DisassembleTable.Text = "table1";
            this.DisassembleTable.UnfocusedBorderColor = System.Drawing.Color.Black;
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
            this.PCColumn1.IsTextTrimmed = false;
            this.PCColumn1.Text = "PC";
            // 
            // OPCodeColumn
            // 
            this.OPCodeColumn.IsTextTrimmed = false;
            this.OPCodeColumn.Text = "OPCode";
            this.OPCodeColumn.Width = 85;
            // 
            // OPCodeColumnHex
            // 
            this.OPCodeColumnHex.IsTextTrimmed = false;
            this.OPCodeColumnHex.Text = "Hex";
            // 
            // DisasmTableModel
            // 
            row4.ChildIndex = 0;
            row4.ExpandSubRows = true;
            row4.Height = 15;
            this.DisasmTableModel.Rows.AddRange(new XPTable.Models.Row[] {
            row4});
            // 
            // textColumn1
            // 
            this.textColumn1.IsTextTrimmed = false;
            this.textColumn1.Text = "PC";
            // 
            // PCColumn
            // 
            this.PCColumn.IsTextTrimmed = false;
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
            this.splitContainer8.Panel1.Controls.Add(this.lcdGroupBox);
            this.splitContainer8.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer8.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer8.Panel1.Controls.Add(this.groupBox4);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PCTextBox);
            this.groupBox2.Location = new System.Drawing.Point(17, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(109, 53);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PC";
            // 
            // PCTextBox
            // 
            this.PCTextBox.Location = new System.Drawing.Point(6, 19);
            this.PCTextBox.Name = "PCTextBox";
            this.PCTextBox.Size = new System.Drawing.Size(87, 20);
            this.PCTextBox.TabIndex = 22;
            this.PCTextBox.Text = "0";
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
            this.HLTextBox.Location = new System.Drawing.Point(146, 34);
            this.HLTextBox.Name = "HLTextBox";
            this.HLTextBox.Size = new System.Drawing.Size(40, 20);
            this.HLTextBox.TabIndex = 3;
            this.HLTextBox.Text = "0";
            // 
            // AFTextBox
            // 
            this.AFTextBox.Location = new System.Drawing.Point(8, 34);
            this.AFTextBox.Name = "AFTextBox";
            this.AFTextBox.Size = new System.Drawing.Size(40, 20);
            this.AFTextBox.TabIndex = 0;
            this.AFTextBox.Text = "0";
            this.AFTextBox.TextChanged += new System.EventHandler(this.AFTextBox_TextChanged);
            // 
            // BCTextBox
            // 
            this.BCTextBox.Location = new System.Drawing.Point(54, 34);
            this.BCTextBox.Name = "BCTextBox";
            this.BCTextBox.Size = new System.Drawing.Size(40, 20);
            this.BCTextBox.TabIndex = 1;
            this.BCTextBox.Text = "0";
            // 
            // DETextBox
            // 
            this.DETextBox.Location = new System.Drawing.Point(100, 34);
            this.DETextBox.Name = "DETextBox";
            this.DETextBox.Size = new System.Drawing.Size(40, 20);
            this.DETextBox.TabIndex = 2;
            this.DETextBox.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "AF";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "BC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "DE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 16);
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
            this.groupBox4.Size = new System.Drawing.Size(109, 58);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Flags";
            // 
            // CcheckBox
            // 
            this.CcheckBox.AutoSize = true;
            this.CcheckBox.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.CcheckBox.Location = new System.Drawing.Point(79, 19);
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
            this.NcheckBox.Location = new System.Drawing.Point(29, 19);
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
            this.HcheckBox.Location = new System.Drawing.Point(54, 19);
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
            this.ZcheckBox.Location = new System.Drawing.Point(5, 19);
            this.ZcheckBox.Name = "ZcheckBox";
            this.ZcheckBox.Size = new System.Drawing.Size(18, 31);
            this.ZcheckBox.TabIndex = 0;
            this.ZcheckBox.Text = "Z";
            this.ZcheckBox.UseVisualStyleBackColor = true;
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
            this.AdressToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
            this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
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
            this.DisAsmPanelContextMenuStrip.Size = new System.Drawing.Size(171, 48);
            // 
            // RunToCursorMenuItem
            // 
            this.RunToCursorMenuItem.Name = "RunToCursorMenuItem";
            this.RunToCursorMenuItem.Size = new System.Drawing.Size(170, 22);
            this.RunToCursorMenuItem.Text = "Run to cursor";
            this.RunToCursorMenuItem.Click += new System.EventHandler(this.RunToCursorMenuItem_Click);
            // 
            // ToggleBreakpointMenuItem
            // 
            this.ToggleBreakpointMenuItem.Name = "ToggleBreakpointMenuItem";
            this.ToggleBreakpointMenuItem.Size = new System.Drawing.Size(170, 22);
            this.ToggleBreakpointMenuItem.Text = "Toggle breakpoint";
            this.ToggleBreakpointMenuItem.Click += new System.EventHandler(this.ToggleBreakpointMenuItem_Click);
            // 
            // lcdGroupBox
            // 
            this.lcdGroupBox.Controls.Add(this.label9);
            this.lcdGroupBox.Controls.Add(this.IFtextBox);
            this.lcdGroupBox.Controls.Add(this.label8);
            this.lcdGroupBox.Controls.Add(this.IEtextBox);
            this.lcdGroupBox.Controls.Add(this.label7);
            this.lcdGroupBox.Controls.Add(this.STATtextBox);
            this.lcdGroupBox.Controls.Add(this.label6);
            this.lcdGroupBox.Controls.Add(this.LYtextBox);
            this.lcdGroupBox.Controls.Add(this.label5);
            this.lcdGroupBox.Controls.Add(this.LCDCtextBox);
            this.lcdGroupBox.Location = new System.Drawing.Point(132, 94);
            this.lcdGroupBox.Name = "lcdGroupBox";
            this.lcdGroupBox.Size = new System.Drawing.Size(131, 146);
            this.lcdGroupBox.TabIndex = 25;
            this.lcdGroupBox.TabStop = false;
            this.lcdGroupBox.Text = "LCD Flags";
            // 
            // LCDCtextBox
            // 
            this.LCDCtextBox.Location = new System.Drawing.Point(8, 30);
            this.LCDCtextBox.Name = "LCDCtextBox";
            this.LCDCtextBox.Size = new System.Drawing.Size(40, 20);
            this.LCDCtextBox.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "LCDC";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "LY";
            // 
            // LYtextBox
            // 
            this.LYtextBox.Location = new System.Drawing.Point(63, 30);
            this.LYtextBox.Name = "LYtextBox";
            this.LYtextBox.Size = new System.Drawing.Size(40, 20);
            this.LYtextBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "STAT";
            // 
            // STATtextBox
            // 
            this.STATtextBox.Location = new System.Drawing.Point(8, 74);
            this.STATtextBox.Name = "STATtextBox";
            this.STATtextBox.Size = new System.Drawing.Size(40, 20);
            this.STATtextBox.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "IE";
            // 
            // IEtextBox
            // 
            this.IEtextBox.Location = new System.Drawing.Point(63, 74);
            this.IEtextBox.Name = "IEtextBox";
            this.IEtextBox.Size = new System.Drawing.Size(40, 20);
            this.IEtextBox.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "IF";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // IFtextBox
            // 
            this.IFtextBox.Location = new System.Drawing.Point(8, 113);
            this.IFtextBox.Name = "IFtextBox";
            this.IFtextBox.Size = new System.Drawing.Size(40, 20);
            this.IFtextBox.TabIndex = 12;
            this.IFtextBox.TextChanged += new System.EventHandler(this.IFtextBox_TextChanged);
            // 
            // DebuggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 650);
            this.Controls.Add(this.splitContainer7);
            this.Name = "DebuggerForm";
            this.Text = "Debugger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisassembleForm_FormClosing);
            this.Load += new System.EventHandler(this.DisassembleForm_Load);
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
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.Panel2.PerformLayout();
            this.splitContainer8.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
            this.DisAsmPanelContextMenuStrip.ResumeLayout(false);
            this.lcdGroupBox.ResumeLayout(false);
            this.lcdGroupBox.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox lcdGroupBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox IEtextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox STATtextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox LYtextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox LCDCtextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox IFtextBox;
    }
}