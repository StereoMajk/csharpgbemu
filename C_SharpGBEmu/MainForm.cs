using SdlDotNet.Graphics;
using SdlDotNet.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace C_SharpGBEmu
{
    public partial class MainForm : Form
    {
        string[] m_args;
        private SdlDotNet.Graphics.Surface m_screen_surf = null;
        private SdlDotNet.Graphics.Surface m_tilemap_surf = null;
        private SurfaceControl m_screen_surface_control;
        const int c_screenwidth = 256;
        const int c_screenheight = 256;
        const int tWidth = 128;
        DisassembleForm m_dsfrm;
        Gameboy m_gameboy;
        private Thread m_screenthread;
        System.Timers.Timer m_ticktimer;
        EmulationState m_currentemulationstate;

        public static byte PIXEL1 = 0x80;
        public static byte PIXEL2 = 0x40;
        public static byte PIXEL3 = 0x20;
        public static byte PIXEL4 = 0x10;
        public static byte PIXEL5 = 0x08;
        public static byte PIXEL6 = 0x04;
        public static byte PIXEL7 = 0x02;
        public static byte PIXEL8 = 0x01;

        public MainForm(string[] args)
        {
            m_args = args;
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            m_currentemulationstate = new EmulationState();
            m_currentemulationstate.IsPaused = true;
            m_gameboy = new Gameboy();
            m_gameboy.ReadCart(m_args[0]);
            m_screen_surface_control = new SdlDotNet.Windows.SurfaceControl() { Width = 256, Height = 256 };
            m_screen_surface_control.Location = new System.Drawing.Point(0, 0);            
            MainSplitter.Panel2.Controls.Add(m_screen_surface_control);

            m_screenthread = new Thread(new ThreadStart(SdlDotNet.Core.Events.Run));
            m_screenthread.IsBackground = true;
            m_screenthread.Name = "Screenthread";
            m_screenthread.Priority = ThreadPriority.Normal;
            m_screenthread.Start();

            SdlDotNet.Core.Events.Tick += new EventHandler<SdlDotNet.Core.TickEventArgs>(Screen_Events_Tick);
            SdlDotNet.Core.Events.Tick += new EventHandler<SdlDotNet.Core.TickEventArgs>(Tilemap_Events_Tick);
            SdlDotNet.Core.Events.KeyboardDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(this.OnKeyboardDown);
            SdlDotNet.Core.Events.KeyboardUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(this.OnKeyboardUp);

            m_ticktimer = new System.Timers.Timer();
            m_ticktimer.Elapsed += new System.Timers.ElapsedEventHandler(TickTimer_Tick);
            m_ticktimer.Interval = 160;
            m_ticktimer.Enabled = false;
        }

        private void TickTimer_Tick(object sender, ElapsedEventArgs e)
        {
            if (!m_currentemulationstate.IsPaused)
            {
                for (int i = 0; i < 70224; i++)
                {
                    m_gameboy.Run(1);
                }
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            m_dsfrm = new DisassembleForm(m_gameboy, m_screen_surf, m_tilemap_surf, m_currentemulationstate, m_ticktimer);
            m_dsfrm.Show(this);
        }

        private void RunButton_Click_1(object sender, EventArgs e)
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
        private void Screen_Events_Tick(object sender, SdlDotNet.Core.TickEventArgs e)
        {
            if (m_screen_surf == null)
                m_screen_surf = new Surface(c_screenwidth, c_screenheight);

            m_screen_surf.Lock();
            for (int currentscanline = 0; currentscanline < c_screenheight; currentscanline++)
            {                
                unsafe
                {

                    // Get an int pointer to the start of the bitmap:
                    int* rawPixels = (int*)m_screen_surf.Pixels;
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
                        rawPixels[current_pixel_pos + 0] = color;

                        current_pixel = ((current_row & PIXEL2) >> 5) + ((current_row2 & PIXEL2) >> 6);
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 1] = color;

                        current_pixel = ((current_row & PIXEL3) >> 4) + ((current_row2 & PIXEL3) >> 5);
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 2] = color;

                        current_pixel = ((current_row & PIXEL4) >> 3) + ((current_row2 & PIXEL4) >> 4);
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 3] = color;

                        current_pixel = ((current_row & PIXEL5) >> 2) + ((current_row2 & PIXEL5) >> 3);
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 4] = color;

                        current_pixel = ((current_row & PIXEL6) >> 1) + ((current_row2 & PIXEL6) >> 2);
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 5] = color;

                        current_pixel = ((current_row & PIXEL7)) + ((current_row2 & PIXEL7) >> 1);
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 6] = color;

                        current_pixel = ((current_row & PIXEL8 << 1)) + ((current_row2 & PIXEL8));
                        color = m_gameboy.m_background_palette[current_pixel];
                        rawPixels[current_pixel_pos + 7] = color;
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
                                    rawPixels[current_pixel_pos + 0] = color;
                                }

                                current_pixel = ((current_row & PIXEL2) >> 5) + ((current_row2 & PIXEL2) >> 6);
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 1] = color;
                                }

                                current_pixel = ((current_row & PIXEL3) >> 4) + ((current_row2 & PIXEL3) >> 5);
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 2] = color;
                                }

                                current_pixel = ((current_row & PIXEL4) >> 3) + ((current_row2 & PIXEL4) >> 4);
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 3] = color;
                                }

                                current_pixel = ((current_row & PIXEL5) >> 2) + ((current_row2 & PIXEL5) >> 3);
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 4] = color;
                                }

                                current_pixel = ((current_row & PIXEL6) >> 1) + ((current_row2 & PIXEL6) >> 2);
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 5] = color;
                                }

                                current_pixel = ((current_row & PIXEL7)) + ((current_row2 & PIXEL7) >> 1);
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 6] = color;
                                }

                                current_pixel = ((current_row & PIXEL8 << 1)) + ((current_row2 & PIXEL8));
                                if (current_pixel != transparent_color)
                                {
                                    color = m_gameboy.m_object_palette0[current_pixel];
                                    rawPixels[current_pixel_pos + 7] = color;
                                }
                            }
                        }
                    }
                    spritecount = spritecount;
                    for (int i = 0; i < 160; i++)
                    {
                        //System.Console.WriteLine("Drawing line " + " WX:" + m_gameboy.WX + " WY:" + m_gameboy.WY);
                        rawPixels[m_gameboy.SCX + i + m_gameboy.SCY * 256] = 0xE0F8D0;
                        rawPixels[m_gameboy.SCX + i + (m_gameboy.SCY + 144) * 256] = 0xE0F8D0;
                    }
                    for (int i = 0; i < 144; i++)
                    {
                        rawPixels[m_gameboy.SCX + (m_gameboy.SCY + i) * 256] = 0xE0F8D0;
                        rawPixels[m_gameboy.SCX + 160 + (m_gameboy.SCY + i) * 256] = 0xE0F8D0;
                    }
                }
            }

            m_screen_surf.Unlock();
            this.UpdateForm();
        }
        private void Tilemap_Events_Tick(object sender, SdlDotNet.Core.TickEventArgs e)
        {
            if (m_tilemap_surf == null)
                m_tilemap_surf = new Surface(128, 256);

            //for(int i=0;i<456;i++)
            //                m_gameboy.Run(1);

            m_tilemap_surf.Lock();

            unsafe
            {
                int* p = (int*)m_tilemap_surf.Pixels;
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
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 0] = 0xE0F8D0 & (current_row & PIXEL1) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 1] = 0xE0F8D0 & (current_row & PIXEL2) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 2] = 0xE0F8D0 & (current_row & PIXEL3) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 3] = 0xE0F8D0 & (current_row & PIXEL4) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 4] = 0xE0F8D0 & (current_row & PIXEL5) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 5] = 0xE0F8D0 & (current_row & PIXEL6) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 6] = 0xE0F8D0 & (current_row & PIXEL7) * 0xffffff;
                            p[tWidth * (pixels_per_row * ytile + i) + (pixels_per_row * xtile) + 7] = 0xE0F8D0 & (current_row & PIXEL8) * 0xffffff;
                        }
                    }
                }
            }
            m_tilemap_surf.Unlock();

            this.UpdateForm();
        }

        public void UpdateForm()
        {
            m_screen_surface_control.Blit(m_screen_surf);          
        }

        private void MainSplitter_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}