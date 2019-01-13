using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using SdlDotNet.Windows;
using System.Timers;

namespace C_SharpGBEmu
{
    public partial class VRAMForm : Form
    {
        public bool paused
        {
            set
            {
                m_paused=value;
            }
        }
        Gameboy m_gameboy;
        private SdlDotNet.Graphics.Surface m_screen_surf=null;
        private SdlDotNet.Graphics.Surface m_tilemap_surf = null;
        bool m_screen_selected = true;
        bool m_paused = false;
        Thread screenthread;
        SdlDotNet.Graphics.Surface m_screenbmp;
        int tWidth = 128;
        int tHeight = 256;
        int screenwidth = 256;
        int screenheight = 256;
        public static byte PIXEL1 = 0x80;       
        public static byte PIXEL2 = 0x40;       
        public static byte PIXEL3 = 0x20;       
        public static byte PIXEL4 = 0x10;       
        public static byte PIXEL5 = 0x08;       
        public static byte PIXEL6 = 0x04;       
        public static byte PIXEL7 = 0x02;       
        public static byte PIXEL8 = 0x01;       
        private Bitmap screenBitmap;
        private SurfaceControl m_screen_surface_control;
        private SurfaceControl m_tilemap_surface_control;
        private System.Timers.Timer m_ticktimer;

        public VRAMForm(Gameboy gb, Surface screen, Surface tilemap)
        {            
            InitializeComponent();
            m_gameboy = gb;
            m_screen_surf = screen;
            m_tilemap_surf = tilemap;
        }

        private void VRAMForm_Load(object sender, EventArgs e)
        {
            m_screen_surface_control = new SdlDotNet.Windows.SurfaceControl() { Width = 256, Height = 256 };
            m_screen_surface_control.Location = new System.Drawing.Point(0, 0);
            ScreenTabPage.Controls.Add(m_screen_surface_control);

            m_tilemap_surface_control = new SdlDotNet.Windows.SurfaceControl() { Width = 256, Height = 256 };
            m_tilemap_surface_control.Location = new System.Drawing.Point(0, 0);
            TilemapTabPage.Controls.Add(m_tilemap_surface_control);

            m_screenbmp = m_gameboy.m_screen;
            KeyPreview = true;
            m_ticktimer = new System.Timers.Timer();
            m_ticktimer.Elapsed += new System.Timers.ElapsedEventHandler(TickTimer_Tick);
            m_ticktimer.Interval = 160;
            m_ticktimer.Enabled = true;
        }

        private void TickTimer_Tick(object sender, ElapsedEventArgs e)
        {
            UpdateForm();
        }

        public void UpdateForm()
        {
            if (m_screen_selected)
                m_screen_surface_control.Blit(m_screen_surf);
            else
                m_tilemap_surface_control.Blit(m_tilemap_surf);

            
        }

        public void UpdateAll() 
        {
        //    ScreenPanel_Paint(null, null);
//            TileMapPanel_Paint(null, null);
        }
      
        private void VRAMTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (ScreenTabPage.Focused)
                m_screen_selected = true;
            else
                m_screen_selected = false;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.JOYPAD = 0xf7;
            m_gameboy.ButtonDown(Gameboy.BUTTON_START);
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.JOYPAD = 0xff;
            m_gameboy.ButtonUp(Gameboy.BUTTON_START);
            KeyPreview = true;
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonDown(Gameboy.BUTTON_A);
            m_gameboy.JOYPAD = 0xfe;
        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.JOYPAD = 0xff;
            m_gameboy.ButtonUp(Gameboy.BUTTON_A);
            KeyPreview = true;
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonDown(Gameboy.BUTTON_B);
            m_gameboy.JOYPAD = 0xfd;
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonUp(Gameboy.BUTTON_B);
            m_gameboy.JOYPAD = 0xff;
            KeyPreview = true;
        }

        private void VRAMForm_KeyDown(object sender, KeyEventArgs e)
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

        private void VRAMForm_KeyUp(object sender, KeyEventArgs e)
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

        private void VRAMForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void UpButton_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonDown(Gameboy.BUTTON_UP);
        }

        private void UpButton_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonUp(Gameboy.BUTTON_UP);
        }

        private void DownButton_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonDown(Gameboy.BUTTON_DOWN);
        }

        private void DownButton_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonUp(Gameboy.BUTTON_DOWN);
        }

        private void LeftButton_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonDown(Gameboy.BUTTON_LEFT);
        }

        private void LeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonUp(Gameboy.BUTTON_LEFT);
        }

        private void RightButton_MouseDown(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonDown(Gameboy.BUTTON_RIGHT);
        }

        private void RightButton_MouseUp(object sender, MouseEventArgs e)
        {
            m_gameboy.ButtonUp(Gameboy.BUTTON_RIGHT);
        }
    }
}