using SdlDotNet.Graphics;
using SdlDotNet.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace C_SharpGBEmu
{
    public partial class MainForm : Form
    {
        string[] m_args;
        const int c_screenwidth = 256;
        const int c_screenheight = 256;
        const int tWidth = 128;
        DebuggerForm m_dsfrm;
        Gameboy m_gameboy;

        System.Timers.Timer m_ticktimer;
        System.Timers.Timer m_screentimer;
        EmulationState m_currentemulationstate;
        private string m_currentRomFile;
        public static byte PIXEL1 = 0x80;
        public static byte PIXEL2 = 0x40;
        public static byte PIXEL3 = 0x20;
        public static byte PIXEL4 = 0x10;
        public static byte PIXEL5 = 0x08;
        public static byte PIXEL6 = 0x04;
        public static byte PIXEL7 = 0x02;
        public static byte PIXEL8 = 0x01;
        private VRAMForm m_vramform;

        delegate void UpdatePictureBoxDelegate();

        uint[] _screen_pixels { get; set; }
        Bitmap _screen_bitmap { get; set; }
        GCHandle _screen_pixels_handle { get; set; }
        IntPtr _screen_pixels_addr { get; set; }

        uint[] _tilemap_pixels { get; set; }
        Bitmap _tilemap_bitmap { get; set; }
        GCHandle _tilemap_pixels_handle { get; set; }
        IntPtr _tilemap_pixels_addr { get; set; }

        public MainForm(string[] args)
        {
            m_args = args;
            InitializeComponent();

            InitializeScreenBitmap();
            InitializeTilemapBitmap();
        }

        private void InitializeScreenBitmap()
        {
            int imageWidth = 256; ;
            int imageHeight = 256; ;

            PixelFormat fmt = PixelFormat.Format32bppRgb;
            int pixelFormatSize = Image.GetPixelFormatSize(fmt);
            int stride = imageWidth * pixelFormatSize;
            int padding = 32 - (stride % 32);

            if (padding < 32)
            {
                stride += padding;
            }
            _screen_pixels = new uint[(stride / 32) * imageHeight + 1];
            _screen_pixels_handle = GCHandle.Alloc(_screen_pixels, GCHandleType.Pinned);
            _screen_pixels_addr = Marshal.UnsafeAddrOfPinnedArrayElement(_screen_pixels, 0);
            _screen_bitmap = new Bitmap(imageWidth, imageHeight, stride / 8, fmt, _screen_pixels_addr);
            screenPictureBox.Image = _screen_bitmap;
        }
        private void InitializeTilemapBitmap()
        {
            int imageWidth = 128; ;
            int imageHeight = 256; ;

            PixelFormat fmt = PixelFormat.Format32bppRgb;
            int pixelFormatSize = Image.GetPixelFormatSize(fmt);
            int stride = imageWidth * pixelFormatSize;
            int padding = 32 - (stride % 32);

            if (padding < 32)
            {
                stride += padding;
            }
            _tilemap_pixels = new uint[(stride / 32) * imageHeight + 1];
            _tilemap_pixels_handle = GCHandle.Alloc(_tilemap_pixels, GCHandleType.Pinned);
            _tilemap_pixels_addr = Marshal.UnsafeAddrOfPinnedArrayElement(_tilemap_pixels, 0);
            _tilemap_bitmap = new Bitmap(imageWidth, imageHeight, stride / 8, fmt, _tilemap_pixels_addr);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            m_currentemulationstate = new EmulationState();
            m_currentemulationstate.IsPaused = true;
            m_gameboy = new Gameboy();
            m_gameboy.ReadCart(m_args[0]);
            m_currentRomFile = m_args[0];

            m_ticktimer = new System.Timers.Timer();
            m_ticktimer.Elapsed += new System.Timers.ElapsedEventHandler(TickTimer_Tick);
            m_ticktimer.Interval = 160;
            m_ticktimer.Enabled = false;

            m_screentimer = new System.Timers.Timer();
            m_screentimer.Elapsed += new System.Timers.ElapsedEventHandler(Screen_Events_Tick);
            m_screentimer.Elapsed += new System.Timers.ElapsedEventHandler(Tilemap_Events_Tick);
            m_screentimer.Interval = 1;
            m_screentimer.Enabled = true;
        }        

        private void Events_MouseMotion(object sender, SdlDotNet.Input.MouseMotionEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void UpdatePictureBox()
        {
            screenPictureBox.Refresh();
        }

        private void TickTimer_Tick(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 70224; i++)
            {
                foreach (BreakPoint bp in m_currentemulationstate.Breakpoints)
                {
                    if (!bp.enabled)
                        continue;
                    if (bp.adress == m_gameboy.cpu.PC)
                    {
                        m_currentemulationstate.IsPaused = true;
                        m_ticktimer.Enabled = false;
                        ShowAndUpdateDisassembly();
                        break;
                    }
                }
                if (m_currentemulationstate.RunToCursorBreakpoint != -1)
                {
                    Debug.WriteLine(m_gameboy.cpu.BC.w.ToString("X4"));
                    if (m_currentemulationstate.RunToCursorBreakpoint == m_gameboy.cpu.PC)
                    {
                        m_currentemulationstate.RunToCursorBreakpoint = -1;
                        m_currentemulationstate.IsPaused = true;
                        m_ticktimer.Enabled = false;
                        ShowAndUpdateDisassembly();
                        break;
                    }
                }
                if (!m_currentemulationstate.IsPaused)
                {
                    try
                    {
                        m_gameboy.Run(1);
                    }
                    catch (Gameboy.InvalidWriteAccessException ie)
                    {
                        m_currentemulationstate.RunToCursorBreakpoint = -1;
                        m_currentemulationstate.IsPaused = true;
                        m_ticktimer.Enabled = false;
                        ShowAndUpdateDisassembly();
                        break;
                    }
                    catch (Gameboy.InvalidReadAccessException ie)
                    {
                        m_currentemulationstate.RunToCursorBreakpoint = -1;
                        m_currentemulationstate.IsPaused = true;
                        m_ticktimer.Enabled = false;
                        ShowAndUpdateDisassembly();
                        break;
                    }
                    catch (Z80Cpu.InvalidOpCodeException ie)
                    {
                        m_currentemulationstate.RunToCursorBreakpoint = -1;
                        m_currentemulationstate.IsPaused = true;
                        m_ticktimer.Enabled = false;
                        ShowAndUpdateDisassembly();
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private void ShowAndUpdateDisassembly()
        {
            if (m_dsfrm == null)
            {
                ShowDisassemblyThreaded();
            }

            m_dsfrm.Invoke(new DebuggerForm.UpdateFormCallback(m_dsfrm.UpdateAll), new object[] { });
        }

        private void ShowDisassemblyThreaded()
        {
            this.Invoke(new UpdatePictureBoxDelegate(this.ShowDisassembly), new object[] { });
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            ShowDisassembly();
        }

        private void ShowDisassembly()
        {
            m_dsfrm = new DebuggerForm(m_gameboy, _screen_bitmap, _tilemap_bitmap, m_currentemulationstate, m_ticktimer);
            m_dsfrm.Show(this);
        }

        private void RunButton_Click_1(object sender, EventArgs e)
        {
            RunEmulator();
        }

        private void RunEmulator()
        {
            m_currentemulationstate.IsPaused = false;
            m_ticktimer.Enabled = true;
        }

        private void OnKeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (e.Key == SdlDotNet.Input.Key.LeftArrow)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_LEFT);
            }
            if (e.Key == SdlDotNet.Input.Key.RightArrow)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_RIGHT);
            }
            if (e.Key == SdlDotNet.Input.Key.UpArrow)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_UP);
            }
            if (e.Key == SdlDotNet.Input.Key.DownArrow)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_DOWN);
            }
            if (e.Key == SdlDotNet.Input.Key.X)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_START);
            }
            if (e.Key == SdlDotNet.Input.Key.Z)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_SELECT);
            }
            if (e.Key == SdlDotNet.Input.Key.A)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_A);
            }
            if (e.Key == SdlDotNet.Input.Key.B)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_B);
            }
        }
        private void OnKeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (e.Key == SdlDotNet.Input.Key.LeftArrow)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_LEFT);
            }
            if (e.Key == SdlDotNet.Input.Key.RightArrow)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_RIGHT);
            }
            if (e.Key == SdlDotNet.Input.Key.UpArrow)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_UP);
            }
            if (e.Key == SdlDotNet.Input.Key.DownArrow)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_DOWN);
            }
            if (e.Key == SdlDotNet.Input.Key.X)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_START);
            }
            if (e.Key == SdlDotNet.Input.Key.Z)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_SELECT);
            }
            if (e.Key == SdlDotNet.Input.Key.A)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_A);
            }
            if (e.Key == SdlDotNet.Input.Key.B)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_B);
            }
        }
        private void Screen_Events_Tick(object sender, ElapsedEventArgs e)
        {
            for (int currentscanline = 0; currentscanline < c_screenheight; currentscanline++)
            {
                const int tile_y_size = 8;
                const int tile_x_size = 8;
                const int bytes_per_tile = 16;
                const int nr_y_tiles = 32;
                const int nr_x_tiles = 32;

                int tiley = currentscanline / tile_y_size;
                int screenmodypos = currentscanline % tile_y_size;
                int screenxpos = 0;
                int sWidth = c_screenwidth;
                int tilemapadress = ((m_gameboy.LCDC & Gameboy.LCDC_BG_TILE_DATA_SELECT) > 0) ? 0x9C00 : 0x9800;
                int tiledataadress = ((m_gameboy.LCDC & Gameboy.LCDC_BG_WINDOW_TILE_DATA_SELECT) > 0) ? 0x8000 : 0x8800;
                // int tilemapadress = 0x9800;
                //int tiledataadress = 0x8000;
                while (screenxpos < c_screenwidth)
                {
                    int tilex = screenxpos / tile_x_size;

                    int tileid = m_gameboy.ReadRAM(tilemapadress + (tiley * nr_x_tiles + tilex));
                    int startadress = tiledataadress + (tileid) * bytes_per_tile;
                    byte current_row = m_gameboy.ReadRAM(startadress + screenmodypos * 2);
                    byte current_row2 = m_gameboy.ReadRAM(startadress + screenmodypos * 2 + 1);
                    //                    if (current_row != 0)
                    //                      current_row = 0xffffff;

                    //for(int i=0;i<144;i++)
                    int current_pixel = ((current_row & PIXEL1) >> 6) + ((current_row2 & PIXEL1) >> 7);
                    int color = m_gameboy.m_background_palette[current_pixel];
                    int current_pixel_pos = sWidth * (tile_y_size * tiley + screenmodypos) + (screenxpos);
                    _screen_pixels[current_pixel_pos + 0] = (uint)color;

                    current_pixel = ((current_row & PIXEL2) >> 5) + ((current_row2 & PIXEL2) >> 6);
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 1] = (uint)color;

                    current_pixel = ((current_row & PIXEL3) >> 4) + ((current_row2 & PIXEL3) >> 5);
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 2] = (uint)color;

                    current_pixel = ((current_row & PIXEL4) >> 3) + ((current_row2 & PIXEL4) >> 4);
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 3] = (uint)color;

                    current_pixel = ((current_row & PIXEL5) >> 2) + ((current_row2 & PIXEL5) >> 3);
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 4] = (uint)color;

                    current_pixel = ((current_row & PIXEL6) >> 1) + ((current_row2 & PIXEL6) >> 2);
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 5] = (uint)color;

                    current_pixel = ((current_row & PIXEL7)) + ((current_row2 & PIXEL7) >> 1);
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 6] = (uint)color;

                    current_pixel = ((current_row & PIXEL8 << 1)) + ((current_row2 & PIXEL8));
                    color = m_gameboy.m_background_palette[current_pixel];
                    _screen_pixels[current_pixel_pos + 7] = (uint)color;
                    screenxpos += tile_x_size;
                    //                    System.Console.WriteLine("Drawing from startpos " + startadress+" tile nr:"+tileid+ " currentrow:"+current_row+" at " + (Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 0));
                }
                //              System.Console.WriteLine("Drawing at line " +(Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 0));
                int spritecount = 0;
                for (int i = 0; i < 40; i++)
                {
                    byte ypos = (byte)(m_gameboy.ReadRAM(0xfe00 + i * 4) - (byte)16);
                    byte xpos = (byte)(m_gameboy.ReadRAM(0xfe00 + i * 4 + 1) - (byte)8);
                    byte tilenr = m_gameboy.ReadRAM(0xfe00 + i * 4 + 2);
                    byte attr = m_gameboy.ReadRAM(0xfe00 + i * 4 + 3);
                    int newypos = ypos;
                    int newxpos = xpos;

                    if (newxpos != 0 && newxpos < 168 && newypos != 0 && newypos < 160)
                    {
                        //  System.Console.WriteLine("Drawing sprite " + tilenr + " xpos:" + xpos + " ypos:" + ypos);
                        spritecount++;

                        int startadress = 0x8000 + (tilenr) * bytes_per_tile;
                        //                            System.Console.WriteLine("Drawing sprite " + tilenr + " xpos:" + xpos + " ypos:" + ypos + " adress:"+startadress);
                        //                    if (current_row != 0)
                        //                      current_row = 0xffffff;

                        //for(int i=0;i<144;i++)                                                        
                        int current_pixel_pos = sWidth * (newypos) + (newxpos);
                        int color = 0;
                        int transparent_color = 0;
                        for (int yrow = 0; yrow < tile_y_size; yrow++)
                        {
                            current_pixel_pos = sWidth * (newypos + yrow) + (newxpos);
                            byte current_row = m_gameboy.ReadRAM(startadress + yrow * 2);
                            byte current_row2 = m_gameboy.ReadRAM(startadress + yrow * 2 + 1);

                            int current_pixel = ((current_row & PIXEL1) >> 6) + ((current_row2 & PIXEL1) >> 7);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 0] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL2) >> 5) + ((current_row2 & PIXEL2) >> 6);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 1] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL3) >> 4) + ((current_row2 & PIXEL3) >> 5);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 2] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL4) >> 3) + ((current_row2 & PIXEL4) >> 4);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 3] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL5) >> 2) + ((current_row2 & PIXEL5) >> 3);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 4] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL6) >> 1) + ((current_row2 & PIXEL6) >> 2);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 5] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL7)) + ((current_row2 & PIXEL7) >> 1);
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 6] = (uint)color;
                            }

                            current_pixel = ((current_row & PIXEL8 << 1)) + ((current_row2 & PIXEL8));
                            if (current_pixel != transparent_color)
                            {
                                color = m_gameboy.m_object_palette0[current_pixel];
                                _screen_pixels[current_pixel_pos + 7] = (uint)color;
                            }
                        }
                    }
                }
                spritecount = spritecount;
                for (int i = 0; i < 160; i++)
                {
                    //System.Console.WriteLine("Drawing line " + " WX:" + m_gameboy.WX + " WY:" + m_gameboy.WY);
                    _screen_pixels[m_gameboy.SCX + i + m_gameboy.SCY * 256] = 0xE0F8D0;
                    _screen_pixels[m_gameboy.SCX + i + (m_gameboy.SCY + 144) * 256] = 0xE0F8D0;
                }
                for (int i = 0; i < 144; i++)
                {
                    _screen_pixels[m_gameboy.SCX + (m_gameboy.SCY + i) * 256] = 0xE0F8D0;
                    _screen_pixels[m_gameboy.SCX + 160 + (m_gameboy.SCY + i) * 256] = 0xE0F8D0;
                }
            }
            this.UpdateForm();
        }
        private void Tilemap_Events_Tick(object sender, ElapsedEventArgs e)
        {
            const int bytes_per_tile = 16;
            const int tile_rows = 32;
            const int tile_columns = 16;
            const int pixels_per_row = 8;
            for (int ytile = 0; ytile < tile_rows; ytile++)
            {
                for (int xtile = 0; xtile < tile_columns; xtile++)
                {
                    int startadress = 0x8000 + (ytile * tile_columns + xtile) * bytes_per_tile;
                    for (int i = 0; i < bytes_per_tile / 2; i++)
                    {
                        int current_row = m_gameboy.RAM[startadress + i * 2];
                        int current_row2 = m_gameboy.RAM[startadress + i * 2 + 1];

                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 0] = (uint)(0xE0F8D0 & (current_row & PIXEL1) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 1] = (uint)(0xE0F8D0 & (current_row & PIXEL2) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 2] = (uint)(0xE0F8D0 & (current_row & PIXEL3) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 3] = (uint)(0xE0F8D0 & (current_row & PIXEL4) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 4] = (uint)(0xE0F8D0 & (current_row & PIXEL5) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 5] = (uint)(0xE0F8D0 & (current_row & PIXEL6) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 6] = (uint)(0xE0F8D0 & (current_row & PIXEL7) * 0xffffff);
                        _tilemap_pixels[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 7] = (uint)(0xE0F8D0 & (current_row & PIXEL8) * 0xffffff);
                    }
                }
            }

            this.UpdateForm();
        }

        public void UpdateForm()
        {
            this.Invoke(new UpdatePictureBoxDelegate(this.UpdatePictureBox), new object[] { });
        }

        private void loadRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    m_currentemulationstate = new EmulationState();
                    m_currentRomFile = dialog.FileName;
                    m_gameboy = new Gameboy();
                    m_gameboy.ReadCart(dialog.FileName);
                    RunEmulator();
                }
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDisassembly();
        }

        private void vRAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_vramform = new VRAMForm(m_gameboy, _screen_bitmap, _tilemap_bitmap);
            m_vramform.Show(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_ticktimer.Stop();
            m_ticktimer.Enabled = false;

            _screen_pixels_addr = IntPtr.Zero;
            if (_screen_pixels_handle.IsAllocated)
            {
                _screen_pixels_handle.Free();
            }
            _screen_bitmap.Dispose();
            _screen_bitmap = null;
            _screen_pixels = null;

            _tilemap_pixels_addr = IntPtr.Zero;
            if (_tilemap_pixels_handle.IsAllocated)
            {
                _tilemap_pixels_handle.Free();
            }
            _tilemap_bitmap.Dispose();
            _tilemap_bitmap = null;
            _tilemap_pixels = null;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_gameboy = new Gameboy();
            m_gameboy.ReadCart(m_currentRomFile);
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Left)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_LEFT);
            }
            if (e.KeyCode == Keys.Right)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_RIGHT);
            }
            if (e.KeyCode == Keys.Up)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_UP);
            }
            if (e.KeyCode == Keys.Down)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_DOWN);
            }
            if (e.KeyCode == Keys.X)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_START);
            }
            if (e.KeyCode == Keys.Z)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_SELECT);
            }
            if (e.KeyCode == Keys.A)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_A);
            }
            if (e.KeyCode == Keys.B)
            {
                m_gameboy.ButtonDown(Gameboy.BUTTON_B);
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.KeyCode == Keys.Left)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_LEFT);
            }
            if (e.KeyCode == Keys.Right)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_RIGHT);
            }
            if (e.KeyCode == Keys.Up)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_UP);
            }
            if (e.KeyCode == Keys.Down)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_DOWN);
            }
            if (e.KeyCode == Keys.X)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_START);
            }
            if (e.KeyCode == Keys.Z)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_SELECT);
            }
            if (e.KeyCode == Keys.A)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_A);
            }
            if (e.KeyCode == Keys.B)
            {
                m_gameboy.ButtonUp(Gameboy.BUTTON_B);
            }
        }
    }
}