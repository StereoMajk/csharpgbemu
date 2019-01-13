using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using XPTable.Models;
using SdlDotNet.Graphics;

namespace C_SharpGBEmu
{
    //public class BufferedPanel : Panel
    //{
    //    BufferedPanel()
    //    {
    //        this.SetStyle(ControlStyles.AllPaintingInWmPaint |
    //                 ControlStyles.DoubleBuffer |
    //                 ControlStyles.ResizeRedraw |
    //                 ControlStyles.UserPaint,
    //                 true);

    //    }
    //}   
    public partial class DisassembleForm : Form
    {

        public class BreakPoint
        {
            public BreakPoint(int aadress) { adress = aadress; enabled = true; }
            public int adress;
            public bool enabled;
        };

        private EmulationState m_emulationstate;
        System.Timers.Timer m_ticktimer;
        Z80Cpu m_cpu;
        Gameboy m_gameboy;
        private Surface m_screen_surface;
        private Surface m_tilemap_surface;
        System.Drawing.Font m_memoryfnt;
        System.Drawing.Font m_disasmfnt;
        Point m_memdrawpnt;
        Point m_disasmdrawpnt;
        int m_disasmlinesvisible;
        int m_startadress;
        int m_adresslinesvisible;
        int m_maxmemorywidth;
        int m_maxmemoryheight;
        int m_maxmemoryadress;
        int m_selecteddisasmlocation;
        int m_mouseselecteddisasmlocation;
        long m_runToCursorBreakpoint;        
        bool m_runningSimulation = false;
        int m_stacklinesvisible;
        VRAMForm m_vramform=null;
        List<BreakPoint> m_breakpointlist;
        private enum Accelerators
         { Unspecified = 0, StepIn, StepOver };
        Hashtable m_accelHash;
        public DisassembleForm(Gameboy gameboy, Surface screen, Surface tilemap, EmulationState emulationState, System.Timers.Timer tickTimer)
        {
            m_emulationstate = emulationState;
            m_ticktimer = tickTimer;
            m_ticktimer.Elapsed += TickTimer_Tick;
            m_breakpointlist=new List<BreakPoint>();
            m_memoryfnt = new System.Drawing.Font("Microsoft Sans Serif", 8);
            m_disasmfnt = new System.Drawing.Font("Microsoft Sans Serif", 8);
            m_memdrawpnt = new Point();
            m_disasmdrawpnt = new Point();
            m_cpu = gameboy.cpu;
            m_gameboy = gameboy;
            m_screen_surface = screen;
            m_tilemap_surface = tilemap;

            m_accelHash =  new Hashtable();
            m_accelHash.Add(new AcceleratorKey(Keys.F7),
                                  Accelerators.StepIn);
            m_accelHash.Add(new AcceleratorKey(Keys.F3),
                                  Accelerators.StepOver);

            InitializeComponent();
            InitializePanels(); 
        }
        protected override bool ProcessCmdKey(ref Message msg,
                                       Keys keyData)
        {
            // Check this key...
            bool bHandled = false;

            // Look up value
            Accelerators accel = Accelerators.Unspecified;
            if (m_accelHash.ContainsKey(keyData))
            {
                accel = (Accelerators)m_accelHash[keyData];

                switch (accel)
                {
                    case Accelerators.StepIn:
//                        DisplayHome();
                        RunOnce();
                        break;

                    case Accelerators.StepOver:
//                        Save();
                        bHandled = true;
                        break;

                    case Accelerators.Unspecified:
                    default:
                        break;

                } // switch
            } // if

            return bHandled;
        }
        private void InitializePanels()
        {
            m_startadress = 0;
            m_maxmemorywidth = 510;
            m_maxmemoryheight = 600;
            m_maxmemoryadress = 0;
            m_selecteddisasmlocation = 0;
            m_mouseselecteddisasmlocation = 0;
            m_runToCursorBreakpoint = -1;
            m_adresslinesvisible = MemoryPanel.Height / m_memoryfnt.Height;
            m_disasmlinesvisible = DisasmPanel.Height / m_disasmfnt.Height;
            m_stacklinesvisible = StackPanel1.Height / m_disasmfnt.Height;
            AdressScrollBar.LargeChange = m_adresslinesvisible - 1;
            DisasmScrollBar.LargeChange = m_disasmlinesvisible - 1;
            StackPanelScrollBar.LargeChange = m_stacklinesvisible - 1;
        }
        private void DisassembleForm_Load(object sender, EventArgs e)
        {          
            for (int i = 0; i < 100; i++)
            {
                Row rw = new Row();

                rw.Cells.Add(new Cell("cc"));
                rw.Cells.Add(new Cell("cdd"));
                DisasmTableModel.Rows.Add(rw);
            }
            ArrayList linelist = m_cpu.GetDisassembly();
            DisasmScrollBar.Maximum = linelist.Count;
            m_maxmemoryadress = (int)((Z80DisassembledLine)linelist[linelist.Count - 1]).PC;
  
            toolStripTextBoxAdress.Text = m_cpu.PC.ToString();

            LoadBreakpoints();

            UpdateAll();
        }
        void LoadBreakpoints()
        {
            XMLSettings.Settings settings = new XMLSettings.Settings();
            int nr_breakpoints = settings.GetSetting("Disassembly/BreakPoints/Count", 0);
            int counter = 0;
            for (int i = 0; i < nr_breakpoints; i++)
            {
                int adress = settings.GetSetting("Disassembly/BreakPoints/BreakPoint" + counter + "/Adress", 0);
                int enabled = settings.GetSetting("Disassembly/BreakPoints/BreakPoint" + counter + "/Enabled", 0);
                BreakPoint bp = new BreakPoint(adress);
                bp.enabled = (enabled == 1);
                m_breakpointlist.Add(bp);                
                counter++;
            }
        }
        void SaveBreakpoints()
        {
            XMLSettings.Settings settings = new XMLSettings.Settings();
            settings.PutSetting("Disassembly/BreakPoints/Count", m_breakpointlist.Count);
            int counter = 0;
            foreach (BreakPoint bp in m_breakpointlist)
            {
                settings.PutSetting("Disassembly/BreakPoints/BreakPoint" + counter + "/Adress", bp.adress);
                settings.PutSetting("Disassembly/BreakPoints/BreakPoint" + counter + "/Enabled", bp.enabled ? 1:0);
                counter++;
            }
        }
        private void toolStripTextBoxAdress_TextChanged(object sender, EventArgs e)
        {

            //if (toolStripTextBoxAdress.Text.Length > 0)
            //{
            //    AddDisassembly(Convert.ToInt32(toolStripTextBoxAdress.Text), Convert.ToInt32(toolStripTextBoxAdress.Text) + 255);
            //}
        }
        private void JumpToLocation(int loc)
        {
            if (m_maxmemoryadress >= loc)
            {
                ArrayList linelist=m_cpu.GetDisassembly();
                for (int i = 0; i < linelist.Count; i++)
                {
                    Z80DisassembledLine currentline = (Z80DisassembledLine)linelist[i];
                    if (currentline.PC == loc)
                        m_selecteddisasmlocation = i;
                }                
            }
            int locationdiff = 10;
            if (m_selecteddisasmlocation - locationdiff < 0)
                DisasmScrollBar.Value = 0;
            else
                DisasmScrollBar.Value = m_selecteddisasmlocation - locationdiff;
            DisasmPanel.Invalidate();
            //if (m_maxmemoryadress >= loc)
            //{
            //    int i=0;
            //    while (i < DisasmTableModel.Rows.Count && (i) >= 0)
            //    {
            //        if (i < DisasmTableModel.Rows.Count && ((Z80DisassembledLine)DisasmTableModel.Rows[i].Cells[0].Data).PC == loc)
            //        {
            //            DisasmTableModel.Selections.SelectCells(new CellPos(i, 0), new CellPos(i, 1));
            //            DisassembleTable.EnsureVisible(new CellPos(i,0));
            //            break;
            //        }
            //        i++;
            //    }
            //}
        }
        private void ClearRows()
        {
            DisasmTableModel.Rows.Clear();
        }
        private void AddRow(Row rw)
        {
            DisasmTableModel.Rows.Add(rw);
        }
        public delegate void ClearRowsCallback();
        public delegate void AddRowCallback(Row rw);

        private void AddDisassembly(int startadress, int endadress)
        {
            lock (this)
            {
                DisassembleTable.Invoke(new ClearRowsCallback(this.ClearRows));
            
                ArrayList linelist = m_cpu.GetDisassembly();
                int realstartadress = startadress;
                while (startadress < ((Z80DisassembledLine)linelist[realstartadress]).PC)
                    realstartadress++;
                
                int linecounter = 0;
                while (realstartadress <= endadress && linecounter < linelist.Count)
                {
                    //if (realstartadress > m_cpu.PC && not_jumped)
                    //{
                    //    not_jumped = false;
                    //    JumpToLocation(m_cpu.PC);
                    //}

                    Thread.Sleep(2);
                    Z80DisassembledLine line = (Z80DisassembledLine)linelist[linecounter];

                    realstartadress=(int)line.PC;
                    linecounter++;
                    byte[] bytes = BitConverter.GetBytes(line.PC);
                    string firsthex = bytes[1].ToString("X");
                    if (firsthex.Length == 1)
                        firsthex = "0" + firsthex;
                    string secondhex = bytes[0].ToString("X");
                    if (secondhex.Length == 1)
                        secondhex = "0" + secondhex;
                    string pc = firsthex + secondhex;
                    Row rw = new Row();

                    rw.Cells.Add(new Cell(pc));
                    rw.Cells.Add(new Cell(line.OPCode));
                    rw.Cells[0].Data = line;
                    DisassembleTable.Invoke(new AddRowCallback(this.AddRow), (object)rw);
                    m_maxmemoryadress = realstartadress;
                }
            }
        }

        private void DisassembleTable_MouseClick(object sender, MouseEventArgs e)
        {
            //Z80DisassembledLine line = (Z80DisassembledLine)DisassembleTable.TopItem.Cells[0].Data;
            //ushort visibleidx = (ushort)line.PC;
            //AddDisassembly(visibleidx, visibleidx + 256);
        }

        private void JumpToLocationButton_Click(object sender, EventArgs e)
        {
            JumpToLocation(m_cpu.PC);
        }

        private void MemoryPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            dc.Clear(Color.White);
            dc.TranslateTransform(MemoryPanel.AutoScrollPosition.X, MemoryPanel.AutoScrollPosition.Y);

            int height = m_memoryfnt.Height;
//            int currypos = -height;
            int startx = 45;
            int width = 19;
            int currypos = 0;
            int currxpos = startx;
            int currcharxpos = 0;

           
            int charswidth = 8;
            m_startadress = AdressScrollBar.Value*16;
            for (int i = 0; i < 16; i++)
            {
                string currentmemvalue = i.ToString("X");
                if (currentmemvalue.Length == 1)
                    currentmemvalue = "0" + currentmemvalue;
                m_memdrawpnt.X = currxpos;
                m_memdrawpnt.Y = currypos;
                dc.DrawString(currentmemvalue,
                        m_memoryfnt, System.Drawing.Brushes.Black, m_memdrawpnt);
                currxpos += width;
            }
            currypos = 8;
            currxpos = 0;
            for (int i = m_startadress; i < (m_startadress + m_adresslinesvisible*16) && i < 0xffff; i++)
            {
                if (i % 16 == 0)
                {
                    string currentmempos = i.ToString("X");
                    int strlength = currentmempos.Length;
                    for (int j = 0; j < 4-strlength; j++)
                        currentmempos = "0" + currentmempos;
                    currypos += height;
                    m_memdrawpnt.X = 0;
                    m_memdrawpnt.Y = currypos;
                    dc.DrawString(currentmempos,
                        m_memoryfnt, System.Drawing.Brushes.Black, m_memdrawpnt);
                    currxpos = startx;
                    currcharxpos = startx+325;
                }
                string currentmemvalue=m_cpu.ReadRAM(i).ToString("X");
                if (currentmemvalue.Length == 1)
                    currentmemvalue = "0" + currentmemvalue;
                m_memdrawpnt.X = currxpos;
                m_memdrawpnt.Y = currypos;
                dc.DrawString(currentmemvalue,
                        m_memoryfnt, System.Drawing.Brushes.Black, m_memdrawpnt);
                currxpos += width;
                
                char tester = Convert.ToChar(m_cpu.ReadRAM(i));
                string currentmemcharvalue="";
                if (Char.IsLetterOrDigit(tester) || Char.IsPunctuation(tester) ||
                   Char.IsSeparator(tester) || Char.IsWhiteSpace(tester))
                    currentmemcharvalue = tester.ToString();
                else
                    currentmemcharvalue = ".";

                m_memdrawpnt.X = currcharxpos;
                m_memdrawpnt.Y = currypos;
                dc.DrawString(currentmemcharvalue,
                        m_memoryfnt, System.Drawing.Brushes.Black, m_memdrawpnt);
                currcharxpos += charswidth;
                
            }
            if (currcharxpos+charswidth > m_maxmemorywidth)
            {
                m_maxmemorywidth = currcharxpos+charswidth;
            }
            if (currypos+height > m_maxmemoryheight)
            {
                m_maxmemoryheight = currypos+height;
            }
        }
        

        private void MemoryPanel_Resize(object sender, EventArgs e)
        {
          //  splitContainer4.Panel2.AutoScrollMinSize = new Size(m_maxmemorywidth, m_maxmemoryheight);
            m_adresslinesvisible = MemoryPanel.Height / m_memoryfnt.Height;
            AdressScrollBar.LargeChange = m_adresslinesvisible -1;
            MemoryPanel.Invalidate();
        }
        private void AdressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (AdressTextBox.Text.Length != 0)
            {
                if (AdressTextBox.Text.IndexOf("x") != -1 || AdressTextBox.Text.IndexOf("X") != -1)
                    m_startadress = Convert.ToInt32(AdressTextBox.Text, 16);
                else
                    m_startadress = Convert.ToInt32(AdressTextBox.Text);

                AdressScrollBar.Value = m_startadress / 16;
                MemoryPanel.Invalidate();
            }
        }

        private void AdressScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            MemoryPanel.Invalidate();
        }
        public delegate void UpdateFormCallback();

        private void UpdateAll()
        {
            AFTextBox.Text = PadHexString(m_cpu.AF.w.ToString("X"));
            BCTextBox.Text = PadHexString(m_cpu.BC.w.ToString("X"));
            DETextBox.Text = PadHexString(m_cpu.DE.w.ToString("X"));
            HLTextBox.Text = PadHexString(m_cpu.HL.w.ToString("X"));

            PCTextBox.Text = PadHexString(m_cpu.PC.ToString("X"));
            SPTextBox.Text = PadHexString(m_cpu.SP.ToString("X"));

            ZcheckBox.Checked = m_cpu.GetFlag(Z80Cpu.Z_FLAG);
            HcheckBox.Checked = m_cpu.GetFlag(Z80Cpu.H_FLAG);
            NcheckBox.Checked = m_cpu.GetFlag(Z80Cpu.N_FLAG);
            CcheckBox.Checked = m_cpu.GetFlag(Z80Cpu.C_FLAG);

            JumpToLocation(m_cpu.PC);
            MemoryPanel.Invalidate();
            StackPanel1.Invalidate();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            RunOnce();
        }
        private void RunOnce()
        {
            m_gameboy.Run(1);
            UpdateAll();
            if (m_vramform!=null)
                m_vramform.UpdateAll();
        }
        private string PadHexString(string inpstring)
        {
            string outstring = inpstring;
            int strlength = inpstring.Length;
            int nr_zeroes = 4;
            for (int i = 0; i < nr_zeroes - strlength; i++)
            {
                outstring = "0" + outstring;
            }
            return outstring;
        }
        private string PadHexString(string inpstring, int nr_zeroes)
        {
             string outstring=inpstring;
            int strlength=inpstring.Length;
            for(int i=0;i<nr_zeroes-strlength;i++)
            {
                outstring="0"+outstring;
            }
            return outstring;
        }

        private void DisasmPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics dc = e.Graphics;            
            dc.Clear(Color.White);
            ArrayList linelist = m_cpu.GetDisassembly();
            m_disasmdrawpnt.X = 0;
            m_disasmdrawpnt.Y = 0;
            string PCString="";
            Brush currentbrush=Brushes.Black;
            Pen blackPen = new Pen(Color.Black, 1);
            Pen redPen = new Pen(Color.Red, 1);
            for (int i = DisasmScrollBar.Value; i < DisasmScrollBar.Value+m_disasmlinesvisible && i<linelist.Count; i++)
            {
                m_disasmdrawpnt.X = 0;
                Z80DisassembledLine currentline = (Z80DisassembledLine)linelist[i];
                bool isBreakpoint=false;
                foreach (BreakPoint bp in m_breakpointlist)
                {
                    if (currentline.PC == bp.adress)
                    {
                        isBreakpoint=true;
                    }
                }
                if (i == m_mouseselecteddisasmlocation)
                {
                    dc.DrawRectangle(blackPen, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    dc.FillRectangle(Brushes.LightBlue, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    currentbrush = Brushes.White;
                }
                if (i == m_runToCursorBreakpoint)
                {
                    dc.DrawRectangle(blackPen, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    dc.FillRectangle(Brushes.Khaki, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    currentbrush = Brushes.White;
                }
                if (isBreakpoint)
                {
                    dc.DrawRectangle(redPen, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    dc.FillRectangle(Brushes.Red, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    currentbrush = Brushes.White;
                }
                if (i == m_selecteddisasmlocation)
                {
                    dc.DrawRectangle(blackPen, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    dc.FillRectangle(Brushes.Black, m_disasmdrawpnt.X, m_disasmdrawpnt.Y, DisasmPanel.Width, m_disasmfnt.Height);
                    currentbrush = Brushes.White;
                }
                else
                    currentbrush = Brushes.Black;

                PCString = currentline.PC.ToString("X");
                int strlength = PCString.Length;
                for (int j = 0; j < 4 - strlength; j++)
                    PCString = "0" + PCString;
                
               
                dc.DrawString(PCString,
                       m_disasmfnt, currentbrush, m_disasmdrawpnt);

                m_disasmdrawpnt.X = 40;
                for (int j = 0; j < currentline.nr_bytes; j++)
                {
                    string op = currentline.OPCodeBytes[j].ToString("X");
                    if (op.Length == 1)
                        op = "0" + op;

                    dc.DrawString(op,
                       m_disasmfnt, currentbrush, m_disasmdrawpnt);
                    m_disasmdrawpnt.X += 18;
                }               
                

                m_disasmdrawpnt.X = 120;

                dc.DrawString(currentline.OPCode,
                       m_disasmfnt, currentbrush, m_disasmdrawpnt);
                m_disasmdrawpnt.Y += m_disasmfnt.Height;
            }

        }   

        private void DisasmPanel_Resize(object sender, EventArgs e)
        {
            m_disasmlinesvisible = DisasmPanel.Height / m_memoryfnt.Height;
            DisasmScrollBar.LargeChange = m_disasmlinesvisible-1;
            DisasmPanel.Invalidate();
        }

        private void DisasmScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            DisasmPanel.Invalidate();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            m_emulationstate.IsPaused = true;
            m_ticktimer.Enabled = false;

            if (m_vramform != null)
                m_vramform.paused = true; 
           
            m_gameboy.Reset();
            UpdateAll();
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            m_ticktimer.Enabled = true;
            m_emulationstate.IsPaused = false;

            if (m_vramform != null)
                m_vramform.paused = false;
        }

        private void VRAMButton_Click(object sender, EventArgs e)
        {
            m_vramform = new VRAMForm(m_gameboy, m_screen_surface, m_tilemap_surface);
            m_vramform.Show(this);
        }

        private void AFTextBox_TextChanged(object sender, EventArgs e)
        {
            m_gameboy.cpu.AF.w = (ushort)Convert.ToInt16(AFTextBox.Text, 16);
        }

        private void PauseToolStripButton_Click(object sender, EventArgs e)
        {
            m_emulationstate.IsPaused = true;
            m_ticktimer.Enabled = false;

            if (m_vramform!=null)
                m_vramform.paused = true;
            UpdateAll();
        }

        private void DisassembleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveBreakpoints();            
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {            
            if (!m_runningSimulation)
            {                
                m_runningSimulation = true;
                for (int i = 0; i < 70224; i++)
                {
                    foreach (BreakPoint bp in m_breakpointlist)
                    {
                        if (!bp.enabled)
                            continue;
                        if (bp.adress == m_gameboy.cpu.PC)
                        {
                            m_emulationstate.IsPaused = true;
                            m_ticktimer.Enabled = false;
                            this.Invoke(new UpdateFormCallback(this.UpdateAll), new object[] { });
                            //UpdateAll();
                            break;
                        }
                    }
                    if (m_runToCursorBreakpoint != -1)
                    {
                        if (m_runToCursorBreakpoint == m_gameboy.cpu.PC)
                        {
                            m_runToCursorBreakpoint = -1;
                            m_emulationstate.IsPaused = true;
                            m_ticktimer.Enabled = false;
                            this.Invoke(new UpdateFormCallback(this.UpdateAll), new object[] { });
                            break;
                        }
                    }
                    if (!m_emulationstate.IsPaused)
                    {
                        try
                        {
                            m_gameboy.Run(1);
                        }
                        catch (Gameboy.InvalidWriteAccessException ie)
                        {
                            m_runToCursorBreakpoint = -1;
                            m_emulationstate.IsPaused = true;
                            m_ticktimer.Enabled = false;
                            this.Invoke(new UpdateFormCallback(this.UpdateAll), new object[] { });
                            break;
                        }
                        catch (Gameboy.InvalidReadAccessException ie)
                        {
                            m_runToCursorBreakpoint = -1;
                            m_emulationstate.IsPaused = true;
                            m_ticktimer.Enabled = false;
                            this.Invoke(new UpdateFormCallback(this.UpdateAll), new object[] { });
                            break;
                        }
                        catch (Z80Cpu.InvalidOpCodeException ie)
                        {
                            m_runToCursorBreakpoint = -1;
                            m_emulationstate.IsPaused = true;
                            m_ticktimer.Enabled = false;
                            this.Invoke(new UpdateFormCallback(this.UpdateAll), new object[] { });
                            break;
                        }
                    }
                    else
                        break;
                }
                m_runningSimulation = false;
            }
        }

        private void RunToCursorMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList linelist = m_cpu.GetDisassembly();
            
            Z80DisassembledLine selectedLine = (Z80DisassembledLine)linelist[m_mouseselecteddisasmlocation];
            m_runToCursorBreakpoint = selectedLine.PC;
            DisasmPanel.Invalidate();
            m_ticktimer.Enabled = true;
            m_emulationstate.IsPaused = false;            
            if (m_vramform != null)
                m_vramform.paused = false;
        }

        private void DisasmPanel_MouseClick(object sender, MouseEventArgs e)
        {            
            m_mouseselecteddisasmlocation = DisasmScrollBar.Value + e.Y / m_disasmfnt.Height;
            DisasmPanel.Invalidate();

            if (e.Button == MouseButtons.Right)
                DisAsmPanelContextMenuStrip.Show(DisasmPanel.PointToScreen(e.Location));            
        }

        private void ToggleBreakpointMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList linelist = m_cpu.GetDisassembly();

            Z80DisassembledLine selectedLine = (Z80DisassembledLine)linelist[m_mouseselecteddisasmlocation];
            bool found = false;
            BreakPoint oldBreakpoint = new BreakPoint(0);
            foreach (BreakPoint bp in m_breakpointlist)
            {
                if (bp.adress == selectedLine.PC)
                {
                    found = true;
                    oldBreakpoint = bp;
                }
            }
            if (found)
            {
                m_mouseselecteddisasmlocation = -1;
                m_breakpointlist.Remove(oldBreakpoint);
            }
            else
                m_breakpointlist.Add(new BreakPoint((int)selectedLine.PC));

            SaveBreakpoints();
            DisasmPanel.Invalidate();
        }

        private void DisasmPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_mouseselecteddisasmlocation = DisasmScrollBar.Value + e.Y / m_disasmfnt.Height;
            DisasmPanel.Invalidate();
            ToggleBreakpointMenuItem_Click(sender, e);
        }

        private void StackPanel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics dc = e.Graphics;
            dc.Clear(Color.White);                              
            Brush currentbrush = Brushes.Black;
            Pen blackPen = new Pen(Color.Black, 1);
            Pen redPen = new Pen(Color.Red, 1);
            Point stackPaneldrawpnt = new Point(0,0);
            int stackValue = m_cpu.SP - StackPanelScrollBar.Value;            
            for (int i = StackPanelScrollBar.Value; i < StackPanelScrollBar.Value + m_stacklinesvisible; i++)
            {
                if (stackValue > 0)
                {
                    stackPaneldrawpnt.X = 0;

                    currentbrush = Brushes.Black;

                    string stackposition = (stackValue).ToString("X");
                    if (stackposition.Length == 1)
                        stackposition = "0" + stackposition;

                    dc.DrawString(stackposition, m_disasmfnt, currentbrush, stackPaneldrawpnt);

                    stackPaneldrawpnt.X = 40;
                    string currentmemvalue = m_cpu.ReadRAM(stackValue).ToString("X");
                    if (currentmemvalue.Length == 1)
                        currentmemvalue = "0" + currentmemvalue;
                    string nextmemvalue = m_cpu.ReadRAM(stackValue - 1).ToString("X");
                    if (nextmemvalue.Length == 1)
                        nextmemvalue = "0" + nextmemvalue;

                    currentmemvalue = currentmemvalue + nextmemvalue;

                    dc.DrawString(currentmemvalue, m_disasmfnt, currentbrush, stackPaneldrawpnt);

                    stackPaneldrawpnt.Y += m_disasmfnt.Height;
                    stackValue -= 2;
                }
            }
        }

        private void StackPanelScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            StackPanel1.Invalidate();
        }

        private void StackPanel1_Resize(object sender, EventArgs e)
        {
            m_stacklinesvisible = StackPanel1.Height / m_disasmfnt.Height;
            StackPanelScrollBar.LargeChange = m_stacklinesvisible - 1;
            StackPanel1.Invalidate();
        }

        private void loadRomToolStripMenuItem_Click(object sender, EventArgs e)
        {                            
            if (loadRomFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_gameboy.ReadCart(loadRomFileDialog1.FileName);
                m_emulationstate.IsPaused = true;
                m_ticktimer.Enabled = false;

                if (m_vramform != null)
                    m_vramform.paused = true;

                m_gameboy.Reset();
                InitializePanels();

                ArrayList linelist = m_cpu.GetDisassembly();
                DisasmScrollBar.Maximum = linelist.Count;
                m_maxmemoryadress = (int)((Z80DisassembledLine)linelist[linelist.Count - 1]).PC;
  
                UpdateAll();
            }
        }
    }
}