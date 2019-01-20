using System.Collections.Generic;

namespace C_SharpGBEmu
{
    public class Gameboy
    {        
        public class ReadMemoryHandler
        {
            public ushort startAdress;
            public ushort endAdress;
            public Z80Cpu.ReadRAMHandler handler;
        }
        public class WriteMemoryHandler
        {
            public ushort startAdress;
            public ushort endAdress;
            public Z80Cpu.WriteRAMHandler handler;
        }
        List<ReadMemoryHandler> m_readMemoryHandlers;
        List<WriteMemoryHandler> m_writeMemoryHandlers;       
        int Width;
        int Height;
        private Z80Cpu m_cpu;
        public byte[] RAM;
        const int CLOCK_HZ = 41943040;
        const float TIME_PER_CYCLE = 1.0f / (float)CLOCK_HZ;
        const int NR_CYCLES_IN_4096HZ = (int)(((1.0f) / (4096.0f)) / TIME_PER_CYCLE);
        const int NR_CYCLES_IN_262144HZ = (int)(((1.0f) / (262144.0f)) / TIME_PER_CYCLE);
        const int NR_CYCLES_IN_65536HZ = (int)(((1.0f) / (65536.0f)) / TIME_PER_CYCLE);
        const int NR_CYCLES_IN_16384HZ = (int)(((1.0f) / (16384.0f)) / TIME_PER_CYCLE);
        const int NR_CYCLES_IN_MODE0 = 204;
        const int NR_CYCLES_IN_MODE1 = 4560;
        const int NR_CYCLES_IN_MODE2 = 80;
        const int NR_CYCLES_IN_MODE3 = 172;
        const int NR_CYCLES_PER_SCANLINE = NR_CYCLES_IN_MODE0 + NR_CYCLES_IN_MODE2 + NR_CYCLES_IN_MODE3; 
        const int NR_CYCLES_PER_DIV = 2560;
        const byte MODE0 = 0;
        const byte MODE1 = 1;
        const byte MODE2 = 2;
        const byte MODE3 = 3;
        const int SCANLINE_MAX = 0x99;
        const int SCANLINE_VBLANK = 0x90;

        public static byte JOYPAD_DIRECTIONS_BIT = 0x10;
        public static byte JOYPAD_BUTTONS_BIT = 0x20;
        

        public static byte PIXEL1 = 0x80;      
        public static byte PIXEL2 = 0x40;      
        public static byte PIXEL3 = 0x20;      
        public static byte PIXEL4 = 0x10;      
        public static byte PIXEL5 = 0x08;      
        public static byte PIXEL6 = 0x04;      
        public static byte PIXEL7 = 0x02;      
        public static byte PIXEL8 = 0x01;

        public static byte LCDC_DISPLAY_ENABLE = 0x80;
        public static byte LCDC_WINDOW_TILEMAP_DISPLAY_SELECT = 0x40;
        public static byte LCDC_WINDOW_DISPLAY_ENABLE = 0x20;
        public static byte LCDC_BG_WINDOW_TILE_DATA_SELECT = 0x10;
        public static byte LCDC_BG_TILE_DATA_SELECT = 0x08;
        public static byte LCDC_OBJ_SIZE = 0x04;
        public static byte LCDC_OBJ_DISPLAY_ENABLE = 0x02;
        public static byte LCDC_BG_DISPLAY = 0x01;      

        public static byte LCD_STAT_LYC_INTERRUPT_BIT = 0x40;  
        public static byte LCD_STAT_OAM_INTERRUPT_BIT = 0x20;  
        public static byte LCD_STAT_VBLANK_INTERRUPT_BIT = 0x10;
        public static byte LCD_STAT_HBLANK_INTERRUPT_BIT = 0x08;
        public static byte LCD_STAT_LYC_COINCIDENCE_FLAG = 0x04;

        int cycle_counter = 0;
        int hblank_counter = 0;
        int DIV_counter = 0;
        int TIMA_counter = 0;
        public byte JOYPAD;
        byte JoypadMode = 0;
        public byte LY;
        public byte LYC;
        public byte LCDC;
        public byte SCY;
        public byte SCX;
        public byte BGP;
        public byte OBP0;
        public byte OBP1;
        public byte WX;
        public byte WY;
        byte DIV;
        byte TIMA;
        byte TMA;
        byte TAC;
        byte IE;
        byte IF;
        byte STAT;
        public int[] m_background_palette;
        public int[] m_object_palette0;
        public int[] m_object_palette1;
        byte previousLCDMode;
        int totalnrcyclesrun = 0;
        int scanline_cycle_counter = NR_CYCLES_PER_SCANLINE;

        public static int BUTTON_START = 0;
        public static int BUTTON_SELECT = 1;
        public static int BUTTON_B = 2;
        public static int BUTTON_A = 3;
        public static int BUTTON_DOWN = 4;
        public static int BUTTON_UP = 5;
        public static int BUTTON_LEFT = 6;
        public static int BUTTON_RIGHT = 7;
        
        

        int[] m_buttons;
        public class InvalidReadAccessException : System.Exception
        {
        }
        public class InvalidWriteAccessException : System.Exception
        {
        }
        public Z80Cpu cpu
        {
            get
            {
                return m_cpu;
            }            
        }
        public Gameboy()
        {
            m_buttons = new int[8];
            for (int i = 0; i < m_buttons.Length; i++)
                m_buttons[i] = 1;
            m_background_palette = new int[4];
            m_object_palette0 = new int[4];
            m_object_palette1 = new int[4];

            m_background_palette[0] = 0xAFCB46;
            m_background_palette[1] = 0x226F5F;
            m_background_palette[2] = 0x79B26D;
            m_background_palette[3] = 0x082952;

            m_object_palette0[0] = m_object_palette1[0] = m_background_palette[0];
            m_object_palette0[1] = m_object_palette1[1] = m_background_palette[1];
            m_object_palette0[2] = m_object_palette1[2] = m_background_palette[2];
            m_object_palette0[3] = m_object_palette1[3] = m_background_palette[3];
            
            m_readMemoryHandlers=new List<ReadMemoryHandler>();
            ReadMemoryHandler rmemhandler = new ReadMemoryHandler();
            rmemhandler.startAdress = 0xff00;
            rmemhandler.endAdress = 0xff00;
            rmemhandler.handler = new Z80Cpu.ReadRAMHandler(ReadJoystick);
            m_readMemoryHandlers.Add(rmemhandler);

            m_writeMemoryHandlers = new List<WriteMemoryHandler>();
            WriteMemoryHandler dmaTransferhandler = new WriteMemoryHandler();
            dmaTransferhandler.startAdress = 0xff46;
            dmaTransferhandler.endAdress = 0xff46;
            dmaTransferhandler.handler = new Z80Cpu.WriteRAMHandler(DoDMATransfer);
            m_writeMemoryHandlers.Add(dmaTransferhandler);
            
            WriteMemoryHandler echoRAMhandler1 = new WriteMemoryHandler();
            echoRAMhandler1.startAdress = 0xe000;
            echoRAMhandler1.endAdress = 0xfe00;
            echoRAMhandler1.handler = new Z80Cpu.WriteRAMHandler(EchoRamHandling1);
            m_writeMemoryHandlers.Add(echoRAMhandler1);

            WriteMemoryHandler echoRAMhandler2 = new WriteMemoryHandler();
            echoRAMhandler2.startAdress = 0xc000;
            echoRAMhandler2.endAdress = 0xde00;
            echoRAMhandler2.handler = new Z80Cpu.WriteRAMHandler(EchoRamHandling2);
            m_writeMemoryHandlers.Add(echoRAMhandler2);

            Width = 160;
            Height = 144;
            
            RAM = new byte[0xffff+1];
            m_cpu = new Z80Cpu(new Z80Cpu.ReadRAMHandler(ReadRAM), new Z80Cpu.WriteRAMHandler(WriteRAM), RAM);            
        }
        public bool ReadCart(string path)
        {
            m_cpu.ReadCart(path);
            //lets set the starting point
            Reset();
            return true;
        }
        public void ButtonDown(int button)
        {
            m_buttons[button] = 0;
            RequestInterrupt(Z80Cpu.JOYPAD_INTERRUPT);
        }
        public void ButtonUp(int button)
        {
            m_buttons[button] = 1;
        }
        public void Run(int cycles)
        {
            int nr_cycles_run=m_cpu.Run(cycles);

            if (LYC == LY)
                STAT |= LCD_STAT_LYC_COINCIDENCE_FLAG;
            else
                STAT &= (byte)~LCD_STAT_LYC_COINCIDENCE_FLAG;

            CheckTimers(nr_cycles_run);
            CheckGraphics(nr_cycles_run);
            CheckInterrupts();

            totalnrcyclesrun += nr_cycles_run;
        }

        private void CheckGraphics(int nr_cycles_run)
        {
            //if the display is disabled we set to mode 1
            //if ((LCDC & LCDC_DISPLAY_ENABLE) == 0)
            //{
            //    scanline_cycle_counter = NR_CYCLES_PER_SCANLINE;
            //    hblank_counter = NR_CYCLES_IN_MODE0;
            //    cycle_counter = NR_CYCLES_IN_MODE1;
            //    SetLCDMode(MODE1);
            //    LY = 0;                
            //}
            scanline_cycle_counter -= nr_cycles_run;
            if(scanline_cycle_counter<= 0)
            {
                DrawScanLine();
                LY++;
            }

            byte prevMode = GetLCDMode();
            cycle_counter -= nr_cycles_run;
            switch (GetLCDMode())
            {
                case MODE0:
                    //HBLANK mode                                       
                    if (cycle_counter <= 0 && LY < 143)
                    {
                        cycle_counter = NR_CYCLES_IN_MODE2;
                        SetLCDMode(MODE2);                                               
                    }
                    else if (LY >= 143)
                    {
                        hblank_counter = NR_CYCLES_IN_MODE0;
                        cycle_counter = NR_CYCLES_IN_MODE1;
                        SetLCDMode(MODE1);
                    }
                   
                    break;
                case MODE1:
                    //VBLANK

                    //lets count the hblank counter even during VBLANK
                    hblank_counter -= nr_cycles_run;
                    if (hblank_counter <= 0)
                    {
                        hblank_counter = NR_CYCLES_IN_MODE2;                                                
                    }                   
                    if (cycle_counter <= 0)
                    {
                        cycle_counter = NR_CYCLES_IN_MODE2;
                        SetLCDMode(MODE2);                        
                    }
                    
                    break;
                case MODE2:
                    //Reading from OAM                    
                    if (cycle_counter <= 0)
                    {
                        cycle_counter = NR_CYCLES_IN_MODE3;
                        SetLCDMode(MODE3);
                    }                    
                    break;
                case MODE3:
                    //Reading from both VRAM and OAM                    
                    if (cycle_counter <= 0)
                    {
                        cycle_counter = NR_CYCLES_IN_MODE0;
                        SetLCDMode(MODE0);
                    }
                   
                    break;
            }            
        }
        public byte ReadRAM(int adress)
        {
            foreach (ReadMemoryHandler memhandler in m_readMemoryHandlers)
            {
                if (adress >= memhandler.startAdress && adress <= memhandler.endAdress)
                    return memhandler.handler(adress);
            }
            if (adress == 0xff04)
                return (DIV);
            else if (adress == 0xff05)
                return TIMA;
            else if (adress == 0xff06)
                return TMA;
            else if (adress == 0xff07)
                return TAC;
            else if (adress == 0xff40)
                return LCDC;
            else if (adress == 0xff41)
                return STAT;
            else if (adress == 0xff42)
                return SCY;
            else if (adress == 0xff43)
                return SCX;
            else if (adress == 0xff44)
                return LY;
            else if (adress == 0xff45)
                return LYC;
            else if (adress == 0xff47)
                return BGP;
            else if (adress == 0xff48)
                return OBP0;
            else if (adress == 0xff48)
                return OBP1;
            else if (adress == 0xff4a)
                return WX;
            else if (adress == 0xff4b)
                return WY;
            else if (adress == 0xffff)
                return IE;
            else
            {
                //if (adress >= 0 && adress < 65536)
                {                    
                    return RAM[adress];
                }
                /*else
                    throw new InvalidReadAccessException();*/
            }
                
        }
        public void WriteRAM(int adress, byte value)
        {
            foreach (WriteMemoryHandler memhandler in m_writeMemoryHandlers)
            {
                if (adress >= memhandler.startAdress && adress <= memhandler.endAdress)
                {
                    memhandler.handler(adress, value);
                    return;
                }
            }
            if (adress == 0xff00)
                JoypadMode = value;
            if (adress == 0xff04)
                DIV = 0;
            else if (adress == 0xff40)
                LCDC = value;
            else if (adress == 0xff41)
                STAT = value;
            else if (adress == 0xff42)
                SCY = value;
            else if (adress == 0xff43)
                SCX = value;
            else if (adress == 0xff44)
                LY = value;
            else if (adress == 0xff45)
                LYC = value;
            else if (adress == 0xff47)
                BGP = value;
            else if (adress == 0xff48)
                OBP0 = value;
            else if (adress == 0xff49)
                OBP1 = value;
            else if (adress == 0xff4A)
                WX = value;
            else if (adress == 0xff4B)
                WY = value;
            else if (adress == 0xffff)
                IE = value;
            else
            {
                if(adress >= 0 && adress < 65536)
                {
                    if (adress > 0x8000)
                        RAM[adress] = value;
                    else
                    {
                        RAM[adress] = value;
                      //  throw new InvalidWriteAccessException();
                    }
                }
                else
                    throw new InvalidWriteAccessException();
            }
        }
        public byte ReadJoystick(int adress)
        {
            if (JoypadMode == JOYPAD_BUTTONS_BIT)
            {
                int complete = (int)(0xef & ((m_buttons[BUTTON_DOWN] << 3) | (m_buttons[BUTTON_UP] << 2)
                                          | (m_buttons[BUTTON_LEFT] << 1) | (m_buttons[BUTTON_RIGHT])));

                return (byte)complete;
                
                
            }
            else if (JoypadMode == JOYPAD_DIRECTIONS_BIT)
            {
                int complete = (int)(0xdf & ((m_buttons[BUTTON_START] << 3) | (m_buttons[BUTTON_SELECT] << 2)
                                           | (m_buttons[BUTTON_B] << 1)  | (m_buttons[BUTTON_A])));

                return (byte)complete;

            }
                

            return JOYPAD;
        }
        public void DoDMATransfer(int adress, byte value)
        {
            int startsource=value * 0x100;
            int dest = 0xFE * 0x100;
            for (int i = 0; i < 0x9f; i++)
            {
                RAM[dest+i]=RAM[startsource+i];
            }
        }
        public void EchoRamHandling1(int adress, byte value)
        {
            RAM[adress] = value;
            RAM[0xc000 + (adress - 0xe000)] = value;
        }
        public void EchoRamHandling2(int adress, byte value)
        {
            RAM[adress] = value;
            RAM[0xe000 + (adress - 0xc000)] = value;
        }
        public void CheckTimers(int nr_cycles_run)
        {
            DIV_counter -= nr_cycles_run;
            if (DIV_counter <= 0)
            {
                DIV++;
                DIV_counter = NR_CYCLES_PER_DIV;
            }
            int timer_enabled = TAC & 0x4;
            if (timer_enabled > 0)
            {
                TIMA_counter += nr_cycles_run;
                int input_clock_select = TAC & 0x3;

                int nr_cycles_per_tick = 0;
                switch (input_clock_select)
                {
                    case 0:
                        nr_cycles_per_tick = NR_CYCLES_IN_4096HZ;
                        break;
                    case 1:
                        nr_cycles_per_tick = NR_CYCLES_IN_262144HZ;
                        break;
                    case 2:
                        nr_cycles_per_tick = NR_CYCLES_IN_65536HZ;
                        break;
                    case 3:
                        nr_cycles_per_tick = NR_CYCLES_IN_16384HZ;
                        break;
                }
                if (TIMA_counter >= nr_cycles_per_tick)
                {
                    if (TIMA == 0xff)
                    {
                        //overflow
                        TIMA = TMA;
                        RequestInterrupt(Z80Cpu.TIMER_INTERRUPT);
                    }
                    else
                        TIMA++;

                    TIMA_counter = 0;
                }
            }
        }
        public void CheckInterrupts()
        {
            if (m_cpu.IME == 1)
            {              
                switch (GetLCDMode())
                {
                    case MODE0:
                        if (m_cpu.InterruptEnabled(Z80Cpu.LCD_STAT_INTERRUPT) && (STAT & LCD_STAT_HBLANK_INTERRUPT_BIT) > 0)
                            RequestInterrupt(Z80Cpu.LCD_STAT_INTERRUPT);
                        break;
                    case MODE1:
                        if (((m_cpu.InterruptEnabled(Z80Cpu.LCD_STAT_INTERRUPT) && (STAT & LCD_STAT_VBLANK_INTERRUPT_BIT) > 0)))
                            RequestInterrupt(Z80Cpu.LCD_STAT_INTERRUPT);
                        if (m_cpu.InterruptEnabled(Z80Cpu.VBLANK_INTERRUPT))
                            RequestInterrupt(Z80Cpu.VBLANK_INTERRUPT);
                        break;
                    case MODE2:
                        if (m_cpu.InterruptEnabled(Z80Cpu.LCD_STAT_INTERRUPT) && (STAT & LCD_STAT_OAM_INTERRUPT_BIT) > 0)
                            RequestInterrupt(Z80Cpu.LCD_STAT_INTERRUPT);
                        break;
                }
                if ((STAT & LCD_STAT_LYC_COINCIDENCE_FLAG) > 0)
                {
                    if (LYC == LY && m_cpu.InterruptEnabled(Z80Cpu.LCD_STAT_INTERRUPT) && (STAT & LCD_STAT_LYC_INTERRUPT_BIT) > 0)
                        RequestInterrupt(Z80Cpu.LCD_STAT_INTERRUPT);
                }
                else if ((STAT & LCD_STAT_LYC_COINCIDENCE_FLAG) == 0)
                {
                    if (LYC != LY && m_cpu.InterruptEnabled(Z80Cpu.LCD_STAT_INTERRUPT) && (STAT & LCD_STAT_LYC_INTERRUPT_BIT) > 0)
                        RequestInterrupt(Z80Cpu.LCD_STAT_INTERRUPT);
                }

                //lets handle all the requests
                if (m_cpu.InterruptEnabled(Z80Cpu.VBLANK_INTERRUPT) && ((IF & Z80Cpu.VBLANK_INTERRUPT) > 0))
                {
                    //Console.WriteLine("VBLANK_INTERRUPT");
                    m_cpu.Interrupt(0x040);
                    IF = (byte)((int)IF & ~(1));
                }
                if (m_cpu.InterruptEnabled(Z80Cpu.LCD_STAT_INTERRUPT) && ((IF & Z80Cpu.LCD_STAT_INTERRUPT) > 0))
                {
                    //Console.WriteLine("LCD_STAT_INTERRUPT");
                    m_cpu.Interrupt(0x048);
                    IF = (byte)((int)IF & ~(1 << 1));
                }
                if (m_cpu.InterruptEnabled(Z80Cpu.TIMER_INTERRUPT) && ((IF & Z80Cpu.TIMER_INTERRUPT) > 0))
                {
                    //Console.WriteLine("TIMER_INTERRUPT");
                    m_cpu.Interrupt(0x050);
                    IF = (byte)((int)IF & ~(1 << 2));
                }
                if (m_cpu.InterruptEnabled(Z80Cpu.SERIAL_INTERRUPT) && ((IF & Z80Cpu.SERIAL_INTERRUPT) > 0))
                {
                    m_cpu.Interrupt(0x058);
                    IF = (byte)((int)IF & ~(1 << 3));
                }                
            }
        }
        public void RequestInterrupt(int which)
        {
            IF = (byte)((int)IF | (which));
        }
        public void Reset()
        {
            m_cpu.Reset();
            
            m_cpu.SP = 0xfffe;
            m_cpu.AF.w = 0x01b0;
            m_cpu.BC.w = 0x0013;
            m_cpu.DE.w = 0x00d8;
            m_cpu.HL.w = 0x014D;            

            m_cpu.PC = 0x100;
            JOYPAD = 0xcf;
            LCDC = 0x91;
            SCY = 0x00;
            SCX = 0x00;
            LY = 0x00;
            LYC = 0x00;
            BGP = 0xFC;
            OBP0 = 0xFF;
            OBP1 = 0xFF;
            WX = 0x00;
            WY = 0x00;
            IE = 0x00;
            STAT = 0x84;
            TIMA = 0x00;
            TMA = 0x00;
            TAC = 0x00;
            DIV = 0;
            cycle_counter = NR_CYCLES_IN_MODE0;
            DIV_counter = NR_CYCLES_PER_DIV;
            previousLCDMode = 0;
        }
        public void SetLCDMode(byte mode)
        {
            unchecked
            {
                STAT = (byte)(STAT & 0xfc | (STAT & ~0x2) | (STAT & ~0x1));
                STAT = (byte)(STAT & 0xfc | mode);
            }
        }
        public byte GetLCDMode()
        {
            return (byte)((STAT & 0x2) | (STAT & 0x1));
        }
        public void StartDraw()
        {
/*            Locked =
                        m_screen.LockBits(
                               new Rectangle(0, 0, Width, Height),
                                ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);*/

        }
        public void EndDraw()
        {
//            m_screen.UnlockBits(Locked);
        }
        public void DrawScanLine()
        {
            scanline_cycle_counter = NR_CYCLES_PER_SCANLINE;
            if ((LCDC & LCDC_DISPLAY_ENABLE) == 0)
                return;

            if (LY > SCANLINE_MAX)
                LY = 0;

            if (LY < SCANLINE_VBLANK)
            {
                /*m_screen.Lock();
                unsafe
                {
                    // Get an int pointer to the start of the bitmap:
                    int* p = (int*)m_screen.Pixels;

                    const int nr_y_tiles = 32;
                    const int nr_x_tiles = 32;
                    const int tile_y_size = 8;
                    const int tile_x_size = 8;
                    const int bytes_per_tile = 16;
                    int tiley = LY / tile_y_size;
                    int screenmodypos = LY % tile_y_size;
                    int screenxpos = 0;
                    int tilemapadress = ((LCDC & LCDC_WINDOW_TILEMAP_DISPLAY_SELECT) > 0) ? 0x9C00 : 0x9800;
                    int tiledataadress = ((LCDC & LCDC_BG_TILE_DATA_SELECT) > 0) ? 0x8000 : 0x8800;
                    while (screenxpos < 160)
                    {
                        int tilex = screenxpos / tile_x_size;

                        int tileid = ReadRAM(tilemapadress + (tiley * nr_x_tiles + tilex));
                        int startadress = 0x8000 + (tileid) * bytes_per_tile;
                        byte current_row = ReadRAM(startadress + screenmodypos * 2);
                        byte current_row2 = ReadRAM(startadress + screenmodypos * 2 + 1);

                        int current_pixel = ((current_row & PIXEL1) >> 6) + ((current_row2 & PIXEL1) >> 7);
                        int color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 0] = color;

                        current_pixel = ((current_row & PIXEL2) >> 5) + ((current_row2 & PIXEL2) >> 6);
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 1] = color;

                        current_pixel = ((current_row & PIXEL3) >> 4) + ((current_row2 & PIXEL3) >> 5);
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 2] = color;

                        current_pixel = ((current_row & PIXEL4) >> 3) + ((current_row2 & PIXEL4) >> 4);
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 3] = color;

                        current_pixel = ((current_row & PIXEL5) >> 2) + ((current_row2 & PIXEL5) >> 3);
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 4] = color;

                        current_pixel = ((current_row & PIXEL6) >> 1) + ((current_row2 & PIXEL6) >> 2);
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 5] = color;

                        current_pixel = ((current_row & PIXEL7)) + ((current_row2 & PIXEL7) >> 1);
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 6] = color;

                        current_pixel = ((current_row & PIXEL8 << 1)) + ((current_row2 & PIXEL8));
                        color = m_background_palette[current_pixel];
                        p[Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 7] = color;
                        screenxpos += tile_x_size;
                        //                    System.Console.WriteLine("Drawing from startpos " + startadress+" tile nr:"+tileid+ " currentrow:"+current_row+" at " + (Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 0));
                    }
                    //              System.Console.WriteLine("Drawing at line " +(Width * (tile_y_size * tiley + screenmodypos) + (screenxpos) + 0));

                }
                m_screen.Unlock();*/
            }
           
        }
    }
}
