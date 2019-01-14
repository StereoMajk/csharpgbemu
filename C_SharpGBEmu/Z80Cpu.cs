#define GAMEBOY

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Runtime.InteropServices;


namespace C_SharpGBEmu
{
    public class Z80DisassembledLine
    {

        public string OPCode;
        public string Arguments;
        public byte[] OPCodeBytes;
        public int nr_bytes;
        public long PC;
    }
    [StructLayout(LayoutKind.Explicit)]
    public struct Word
    {
        [FieldOffset(0)] public ushort w;
        [FieldOffset(0)] public byte l;
        [FieldOffset(1)] public byte h;
    }
    public class Z80Cpu
    {
        byte[] Cycles =
        {
          4,12, 8, 8, 4, 4, 8, 8, 20, 8, 8, 8, 4, 4, 8, 8,
          4,12, 8, 8, 4, 4, 8, 8,  8, 8, 8, 8, 4, 4, 8, 8,
          8,12, 8, 8, 4, 4, 8, 4,  8, 8, 8, 8, 4, 4, 8, 4,
          8,12, 8, 8,12,12,12, 4,  8, 8, 8, 8, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          8, 8, 8, 8, 8, 8, 4, 8,  4, 4, 4, 4, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          4, 4, 4, 4, 4, 4, 8, 4,  4, 4, 4, 4, 4, 4, 8, 4,
          8,12,12,12,12,16, 8,16,  8, 4,12, 0,12,12, 8,16,
          8,12,12, 0,12,16, 8,16,  8,16,12, 0,12, 0, 8,16,
         12,12, 8, 0, 0,16, 8,16, 16, 4,16, 0, 0, 0, 8,16,
         12,12, 8, 4, 0,16, 8,16, 12, 8,16, 4, 0, 0, 8,16 
        };
        byte[] CyclesCB =
        {
          2, 6, 4, 4, 2, 2, 4, 4, 10, 4, 4, 4, 2, 2, 4, 4,
          2, 6, 4, 4, 2, 2, 4, 4,  4, 4, 4, 4, 2, 2, 4, 4,
          4, 6, 4, 4, 2, 2, 4, 2,  4, 4, 4, 4, 2, 2, 4, 2,
          4, 6, 4, 4, 6, 6, 6, 2,  4, 4, 4, 4, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          4, 4, 4, 4, 4, 4, 2, 4,  2, 2, 2, 2, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          2, 2, 2, 2, 2, 2, 4, 2,  2, 2, 2, 2, 2, 2, 4, 2,
          4, 6, 6, 6, 6, 8, 4, 8,  4, 2, 6, 0, 6, 6, 4, 8,
          4, 6, 6, 0, 6, 8, 4, 8,  4, 8, 6, 0, 6, 0, 4, 8,
          6, 6, 4, 0, 0, 8, 4, 8,  8, 2, 8, 0, 0, 0, 4, 8,
          6, 6, 4, 2, 0, 8, 4, 8,  6, 4, 8, 2, 0, 0, 4, 8 
        };
        enum Codes
        {
            NOP, LD_BC_WORD, LD_xBC_A, INC_BC, INC_B, DEC_B, LD_B_BYTE, RLCA,
            EX_AF_AF, ADD_HL_BC, LD_A_xBC, DEC_BC, INC_C, DEC_C, LD_C_BYTE, RRCA,
            DJNZ, LD_DE_WORD, LD_xDE_A, INC_DE, INC_D, DEC_D, LD_D_BYTE, RLA,
            JR, ADD_HL_DE, LD_A_xDE, DEC_DE, INC_E, DEC_E, LD_E_BYTE, RRA,
            JR_NZ, LD_HL_WORD, LD_xWORD_HL, INC_HL, INC_H, DEC_H, LD_H_BYTE, DAA,
            JR_Z, ADD_HL_HL, LD_HL_xWORD, DEC_HL, INC_L, DEC_L, LD_L_BYTE, CPL,
            JR_NC, LD_SP_WORD, LD_xWORD_A, INC_SP, INC_xHL, DEC_xHL, LD_xHL_BYTE, SCF,
            JR_C, ADD_HL_SP, LD_A_xWORD, DEC_SP, INC_A, DEC_A, LD_A_BYTE, CCF,
            LD_B_B, LD_B_C, LD_B_D, LD_B_E, LD_B_H, LD_B_L, LD_B_xHL, LD_B_A,
            LD_C_B, LD_C_C, LD_C_D, LD_C_E, LD_C_H, LD_C_L, LD_C_xHL, LD_C_A,
            LD_D_B, LD_D_C, LD_D_D, LD_D_E, LD_D_H, LD_D_L, LD_D_xHL, LD_D_A,
            LD_E_B, LD_E_C, LD_E_D, LD_E_E, LD_E_H, LD_E_L, LD_E_xHL, LD_E_A,
            LD_H_B, LD_H_C, LD_H_D, LD_H_E, LD_H_H, LD_H_L, LD_H_xHL, LD_H_A,
            LD_L_B, LD_L_C, LD_L_D, LD_L_E, LD_L_H, LD_L_L, LD_L_xHL, LD_L_A,
            LD_xHL_B, LD_xHL_C, LD_xHL_D, LD_xHL_E, LD_xHL_H, LD_xHL_L, HALT, LD_xHL_A,
            LD_A_B, LD_A_C, LD_A_D, LD_A_E, LD_A_H, LD_A_L, LD_A_xHL, LD_A_A,
            ADD_B, ADD_C, ADD_D, ADD_E, ADD_H, ADD_L, ADD_xHL, ADD_A,
            ADC_B, ADC_C, ADC_D, ADC_E, ADC_H, ADC_L, ADC_xHL, ADC_A,
            SUB_B, SUB_C, SUB_D, SUB_E, SUB_H, SUB_L, SUB_xHL, SUB_A,
            SBC_B, SBC_C, SBC_D, SBC_E, SBC_H, SBC_L, SBC_xHL, SBC_A,
            AND_B, AND_C, AND_D, AND_E, AND_H, AND_L, AND_xHL, AND_A,
            XOR_B, XOR_C, XOR_D, XOR_E, XOR_H, XOR_L, XOR_xHL, XOR_A,
            OR_B, OR_C, OR_D, OR_E, OR_H, OR_L, OR_xHL, OR_A,
            CP_B, CP_C, CP_D, CP_E, CP_H, CP_L, CP_xHL, CP_A,
            RET_NZ, POP_BC, JP_NZ, JP, CALL_NZ, PUSH_BC, ADD_BYTE, RST00,
            RET_Z, RET, JP_Z, PFX_CB, CALL_Z, CALL, ADC_BYTE, RST08,
            RET_NC, POP_DE, JP_NC, OUTA, CALL_NC, PUSH_DE, SUB_BYTE, RST10,
            RET_C, EXX, JP_C, INA, CALL_C, PFX_DD, SBC_BYTE, RST18,
            RET_PO, POP_HL, JP_PO, EX_HL_xSP, CALL_PO, PUSH_HL, AND_BYTE, RST20,
            RET_PE, LD_PC_HL, JP_PE, EX_DE_HL, CALL_PE, PFX_ED, XOR_BYTE, RST28,
            RET_P, POP_AF, JP_P, DI, CALL_P, PUSH_AF, OR_BYTE, RST30,
            RET_M, LD_SP_HL, JP_M, EI, CALL_M, PFX_FD, CP_BYTE, RST38
        };
        enum CodesCB
        {
            RLC_B, RLC_C, RLC_D, RLC_E, RLC_H, RLC_L, RLC_xHL, RLC_A,
            RRC_B, RRC_C, RRC_D, RRC_E, RRC_H, RRC_L, RRC_xHL, RRC_A,
            RL_B, RL_C, RL_D, RL_E, RL_H, RL_L, RL_xHL, RL_A,
            RR_B, RR_C, RR_D, RR_E, RR_H, RR_L, RR_xHL, RR_A,
            SLA_B, SLA_C, SLA_D, SLA_E, SLA_H, SLA_L, SLA_xHL, SLA_A,
            SRA_B, SRA_C, SRA_D, SRA_E, SRA_H, SRA_L, SRA_xHL, SRA_A,
            SWAP_B,SWAP_C,SWAP_D,SWAP_E,SWAP_H,SWAP_L,SWAP_xHL,SWAP_A,
            SRL_B, SRL_C, SRL_D, SRL_E, SRL_H, SRL_L, SRL_xHL, SRL_A,
            BIT0_B, BIT0_C, BIT0_D, BIT0_E, BIT0_H, BIT0_L, BIT0_xHL, BIT0_A,
            BIT1_B, BIT1_C, BIT1_D, BIT1_E, BIT1_H, BIT1_L, BIT1_xHL, BIT1_A,
            BIT2_B, BIT2_C, BIT2_D, BIT2_E, BIT2_H, BIT2_L, BIT2_xHL, BIT2_A,
            BIT3_B, BIT3_C, BIT3_D, BIT3_E, BIT3_H, BIT3_L, BIT3_xHL, BIT3_A,
            BIT4_B, BIT4_C, BIT4_D, BIT4_E, BIT4_H, BIT4_L, BIT4_xHL, BIT4_A,
            BIT5_B, BIT5_C, BIT5_D, BIT5_E, BIT5_H, BIT5_L, BIT5_xHL, BIT5_A,
            BIT6_B, BIT6_C, BIT6_D, BIT6_E, BIT6_H, BIT6_L, BIT6_xHL, BIT6_A,
            BIT7_B, BIT7_C, BIT7_D, BIT7_E, BIT7_H, BIT7_L, BIT7_xHL, BIT7_A,
            RES0_B, RES0_C, RES0_D, RES0_E, RES0_H, RES0_L, RES0_xHL, RES0_A,
            RES1_B, RES1_C, RES1_D, RES1_E, RES1_H, RES1_L, RES1_xHL, RES1_A,
            RES2_B, RES2_C, RES2_D, RES2_E, RES2_H, RES2_L, RES2_xHL, RES2_A,
            RES3_B, RES3_C, RES3_D, RES3_E, RES3_H, RES3_L, RES3_xHL, RES3_A,
            RES4_B, RES4_C, RES4_D, RES4_E, RES4_H, RES4_L, RES4_xHL, RES4_A,
            RES5_B, RES5_C, RES5_D, RES5_E, RES5_H, RES5_L, RES5_xHL, RES5_A,
            RES6_B, RES6_C, RES6_D, RES6_E, RES6_H, RES6_L, RES6_xHL, RES6_A,
            RES7_B, RES7_C, RES7_D, RES7_E, RES7_H, RES7_L, RES7_xHL, RES7_A,
            SET0_B, SET0_C, SET0_D, SET0_E, SET0_H, SET0_L, SET0_xHL, SET0_A,
            SET1_B, SET1_C, SET1_D, SET1_E, SET1_H, SET1_L, SET1_xHL, SET1_A,
            SET2_B, SET2_C, SET2_D, SET2_E, SET2_H, SET2_L, SET2_xHL, SET2_A,
            SET3_B, SET3_C, SET3_D, SET3_E, SET3_H, SET3_L, SET3_xHL, SET3_A,
            SET4_B, SET4_C, SET4_D, SET4_E, SET4_H, SET4_L, SET4_xHL, SET4_A,
            SET5_B, SET5_C, SET5_D, SET5_E, SET5_H, SET5_L, SET5_xHL, SET5_A,
            SET6_B, SET6_C, SET6_D, SET6_E, SET6_H, SET6_L, SET6_xHL, SET6_A,
            SET7_B, SET7_C, SET7_D, SET7_E, SET7_H, SET7_L, SET7_xHL, SET7_A
        };
        Dictionary<byte, string> Instructions;
        Dictionary<byte, string> InstructionsCB;
        ArrayList Lines;
//        public byte[] RAM;
        public Word AF, BC, DE, HL, IX, IY;
        public Word ResultReg;
        public byte Reg;
        public ushort PC, SP;
        public ushort AF1, BC1, DE1, HL1;
        public byte IME;
        
        public delegate byte ReadRAMHandler (int adress);
        public delegate void WriteRAMHandler(int adress, byte value);
        ReadRAMHandler m_readhandler;
        WriteRAMHandler m_writehandler;
        public long CycleCount;
        
        byte IFF, I;              
        byte R;
        
        bool InterruptsEnabled;
        bool TablesInitialized=false;
        byte[] PTable;
        byte[] ZSTable;
        byte[] ZSPTable;
        public static byte JOYPAD_INTERRUPT = 0x10;
        public static byte SERIAL_INTERRUPT = 0x08;
        public static byte TIMER_INTERRUPT = 0x04;
        public static byte LCD_STAT_INTERRUPT = 0x02;
        public static byte VBLANK_INTERRUPT = 0x01;      
#if GAMEBOY
        /* Bits in Z80 F register:    */
        public static byte Z_FLAG = 0x80;       /* 1: Result is zero          */
        public static byte N_FLAG = 0x40;       /* 1: Subtraction occured     */
        public static byte H_FLAG = 0x20;       /* 1: Halfcarry/Halfborrow    */
        public static byte C_FLAG = 0x10;       /* 1: Carry/Borrow occured    */
      
#else
        /* Bits in Z80 F register:    */
        public static byte S_FLAG = 0x80;       /* 1: Result negative         */
        public static byte Z_FLAG = 0x40;       /* 1: Result is zero          */
        public static byte H_FLAG = 0x10;       /* 1: Halfcarry/Halfborrow    */
        public static byte P_FLAG = 0x04;       /* 1: Result is even          */
        public static byte V_FLAG = 0x04;       /* 1: Overflow occured        */
        public static byte N_FLAG = 0x02;       /* 1: Subtraction occured     */
        public static byte C_FLAG = 0x01;       /* 1: Carry/Borrow occured    */       
#endif
        public class InvalidOpCodeException : System.Exception
        {
        };

        public Z80Cpu( ReadRAMHandler readhandler, WriteRAMHandler writehandler, byte[] memory)
        {
            m_readhandler = readhandler;
            m_writehandler = writehandler;
            InterruptsEnabled = true;
            Instructions = new Dictionary<byte, string>();
            InstructionsCB = new Dictionary<byte, string>();
#if GAMEBOY
            ReadOpCodeStrings("gbopcodes.txt", Instructions);
            ReadOpCodeStrings("cbopcodes.txt", InstructionsCB);
#else
            ReadOpCodeStrings("opcodes.txt");
#endif
            Lines = new ArrayList();            

//            RAM = memory;           
            Reset();
        }
        
        public void Reset()
        {
            PC = 0;
            SP = 0xf000;
            AF.w = 0x01b0;
            BC.w = 0x0013;
            DE.w = 0x00d8;
            HL.w = 0x014D;
            IX.w = 0;
            IY.w = 0;
            IFF = 0;
            IME = 0;
            CycleCount = 0;
        }
        public byte ReadRAM(int adress)
        {
            return m_readhandler(adress);
            //if (adress == 0xff40)
            //    return 0x80;
            //else
            //    return RAM[adress];
        }
        public void WriteRAM(int adress, byte value)
        {
            m_writehandler(adress, value);
//            RAM[adress]=value;
        }
        public unsafe void WriteRAM(int adress, ushort value)
        {
            ushort* shrtpntr = &value;

            byte* bytes = (byte*)shrtpntr;
            WriteRAM(adress, bytes[0]);
            WriteRAM(adress + 1, bytes[1]);

        }
       
        public unsafe int Run(int cycles)
        {
            int nr_cycles_run=0;
            int current_cycles_run = 0;
            while (nr_cycles_run<cycles)
            {
                byte currentop = ReadRAM(PC);
                PC++;
                
                switch (currentop)
                {
                    case 0xCB:
                        currentop = ReadRAM(PC);
                        PC++;
                        ExecuteCB(currentop);
                        current_cycles_run = CyclesCB[currentop];
                        break;
                    case 0xDD:
                        currentop = ReadRAM(PC);
                        PC++;
                        break;
                    case 0xED:
                        currentop = ReadRAM(PC);
                        PC++;
                        break;
                    case 0xFD:
                        currentop = ReadRAM(PC);
                        PC++;
                        break;
                    default:
                        Execute(currentop);
                        current_cycles_run = Cycles[currentop];
                        break;
                }
                nr_cycles_run += current_cycles_run;
                CycleCount += current_cycles_run;
            }
            return nr_cycles_run;
        }
        unsafe public void Execute(byte currentop)
        {
            ushort jumpadress = 0;
            ushort adress = 0;            
            sbyte boffset = 0;
            byte bvalue = 0;            
            byte* bytes;            
            Codes code = (Codes)currentop;
            switch (code)
            {
                case Codes.NOP:
                    break;
                #region JP Instructions
                case Codes.JP:
                    jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    PC = jumpadress;
                    break;
                case Codes.JR:
                    boffset = (sbyte)(ReadRAM(PC));
                    PC = (ushort)((short)(PC + 1) + boffset);
                    break;                               
                case Codes.JP_NZ:
                    if ((AF.l & Z_FLAG) == 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC = jumpadress;
                    }
                    else
                        PC+=2;
                    break;
                case Codes.JP_Z:
                    if ((AF.l & Z_FLAG) != 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC = jumpadress;
                    }
                    else
                        PC += 2;
                    break;
                case Codes.JP_NC:
                    if ((AF.l & C_FLAG) == 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC = jumpadress;
                    }
                    else
                        PC += 2;
                    break;
                case Codes.JP_C:
                    if ((AF.l & C_FLAG) != 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC = jumpadress;
                    }
                    else
                        PC += 2;
                    break;
                case Codes.JR_NZ:
                    if ((AF.l & Z_FLAG) == 0)
                    {
                        boffset = (sbyte)(ReadRAM(PC));
                        PC = (ushort)((short)(PC + 1) + boffset);
                    }
                    else
                        PC++;
                    break;
                case Codes.JR_Z:
                    if ((AF.l & Z_FLAG) != 0)
                    {
                        boffset = (sbyte)(ReadRAM(PC));
                        PC = (ushort)((short)(PC + 1) + boffset);
                    }
                    else
                        PC++;
                    break;
                case Codes.JR_C:
                    if ((AF.l & C_FLAG) != 0)
                    {
                        boffset = (sbyte)(ReadRAM(PC));
                        PC = (ushort)((short)(PC + 1) + boffset);
                    }
                    else
                        PC++;
                    break;
                case Codes.JR_NC:
                    if ((AF.l & C_FLAG) == 0)
                    {
                        boffset = (sbyte)(ReadRAM(PC));
                        PC = (ushort)((short)(PC + 1) + boffset);
                    }
                    else
                        PC++;
                    break;
                case Codes.JP_M:
                    adress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    AF.h = ReadRAM(adress);
                    PC += 2;
                    break;
                case Codes.JP_PE:
#if GAMEBOY
                    adress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    WriteRAM(adress, AF.l);
                    WriteRAM(adress, AF.h);
                    PC += 2;
                    break;
#else
                        //fix PE test
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC = jumpadress;
#endif
                case Codes.JP_PO:
#if GAMEBOY
                    adress = (ushort)((ushort)(0xff << 8) + BC.l);
                    WriteRAM(adress, AF.h);                    
#else
                        //fix PE test
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC = jumpadress;
#endif
                    break;
                #endregion
                #region LD Instructions
                case Codes.LD_SP_WORD:
                    SP = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    PC += 2;
                    break;
                case Codes.LD_BC_WORD:
                    BC.w = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    PC += 2;
                    break;
                case Codes.LD_DE_WORD:
                    DE.w = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    PC += 2;
                    break;
                case Codes.LD_HL_WORD:
                    HL.w = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    PC += 2;
                    break;
                case Codes.LD_xWORD_HL:
                    WriteRAM(HL.w, AF.h);
                    HL.w++;
                    break;
                case Codes.LD_HL_xWORD:
                    AF.h = ReadRAM(HL.w);
                    HL.w++;
                    break;
                case Codes.LD_xWORD_A:
                    WriteRAM(HL.w, AF.h);
                    HL.w--;
                    break;
                case Codes.LD_A_xWORD:
                    AF.h=ReadRAM(HL.w);
                    HL.w--;
                    break;

                
                #region LD_x_BYTE
                case Codes.LD_A_BYTE:
                    bvalue = (ReadRAM(PC));
                    AF.h = bvalue;
                    PC++;
                    break;
                case Codes.LD_B_BYTE:
                    bvalue = (ReadRAM(PC));
                    BC.h = bvalue;
                    PC++;
                    break;
                case Codes.LD_C_BYTE:
                    bvalue = (ReadRAM(PC));
                    BC.l = bvalue;
                    PC++;
                    break;
                case Codes.LD_D_BYTE:
                    bvalue = (ReadRAM(PC));
                    DE.h = bvalue;
                    PC++;
                    break;
                case Codes.LD_E_BYTE:
                    bvalue = (ReadRAM(PC));
                    DE.l = bvalue;
                    PC++;
                    break;
                case Codes.LD_H_BYTE:
                    bvalue = (ReadRAM(PC));
                    HL.h = bvalue;
                    PC++;
                    break;
                case Codes.LD_L_BYTE:
                    bvalue = (ReadRAM(PC));
                    HL.l = bvalue;
                    PC++;
                    break;
                #endregion        
                #region LD_x_xXX
                case Codes.LD_A_xHL:
                    bvalue = ReadRAM(HL.w);
                    AF.h = bvalue;
                    break;
                case Codes.LD_A_xBC:
                    bvalue = ReadRAM(BC.w);
                    AF.h = bvalue;
                    break;
                case Codes.LD_A_xDE:
                    bvalue = ReadRAM(DE.w);
                    AF.h = bvalue;
                    break;
                case Codes.LD_B_xHL:
                    bvalue = ReadRAM(HL.w);
                    BC.h = bvalue;
                    break;
                case Codes.LD_C_xHL:
                    bvalue = ReadRAM(HL.w);
                    BC.l = bvalue;
                    break;
                case Codes.LD_D_xHL:
                    bvalue = ReadRAM(HL.w);
                    DE.h = bvalue;
                    break;
                case Codes.LD_E_xHL:
                    bvalue = ReadRAM(HL.w);
                    DE.l = bvalue;
                    break;                
                case Codes.LD_H_xHL:
                    bvalue = ReadRAM(HL.w);
                    HL.h = bvalue;
                    break;
                case Codes.LD_L_xHL:
                    bvalue = ReadRAM(HL.w);
                    HL.l = bvalue;
                    break;
                #endregion
                #region LD_A_x
                case Codes.LD_A_A:
                    AF.h = AF.h;
                    break;
                case Codes.LD_A_B:
                    AF.h = BC.h;
                    break;
                case Codes.LD_A_C:
                    AF.h = BC.l;
                    break;
                case Codes.LD_A_D:
                    AF.h = DE.h;
                    break;
                case Codes.LD_A_E:
                    AF.h = DE.l;
                    break;
                case Codes.LD_A_H:
                    AF.h = HL.h;
                    break;
                case Codes.LD_A_L:
                    AF.h = HL.l;
                    break;
                #endregion
                #region LD_B_x
                case Codes.LD_B_A:
                    BC.h = AF.h;
                    break;
                case Codes.LD_B_B:
                    BC.h = BC.h;
                    break;
                case Codes.LD_B_C:
                    BC.h = BC.l;
                    break;
                case Codes.LD_B_D:
                    BC.h = DE.h;
                    break;
                case Codes.LD_B_E:
                    BC.h = DE.l;
                    break;
                case Codes.LD_B_H:
                    BC.h = HL.h;
                    break;
                case Codes.LD_B_L:
                    BC.h = HL.l;
                    break;
                #endregion
                #region LD_C_x
                case Codes.LD_C_A:
                    BC.l = AF.h;
                    break;
                case Codes.LD_C_B:
                    BC.l = BC.h;
                    break;
                case Codes.LD_C_C:
                    BC.l = BC.l;
                    break;
                case Codes.LD_C_D:
                    BC.l = DE.h;
                    break;
                case Codes.LD_C_E:
                    BC.l = DE.l;
                    break;
                case Codes.LD_C_H:
                    BC.l = HL.h;
                    break;
                case Codes.LD_C_L:
                    BC.l = HL.l;
                    break;
                    #endregion
                #region LD_D_x
                case Codes.LD_D_A:
                    DE.h = AF.h;
                    break;
                case Codes.LD_D_B:
                    DE.h = BC.h;
                    break;
                case Codes.LD_D_C:
                    DE.h = BC.l;
                    break;
                case Codes.LD_D_D:
                    DE.h = DE.h;
                    break;
                case Codes.LD_D_E:
                    DE.h = DE.l;
                    break;
                case Codes.LD_D_H:
                    DE.h = HL.h;
                    break;
                case Codes.LD_D_L:
                    DE.h = HL.l;
                    break;
                    #endregion
                #region LD_E_x
                case Codes.LD_E_A:
                    DE.l = AF.h;
                    break;
                case Codes.LD_E_B:
                    DE.l = BC.h;
                    break;
                case Codes.LD_E_C:
                    DE.l = BC.l;
                    break;
                case Codes.LD_E_D:
                    DE.l = DE.h;
                    break;
                case Codes.LD_E_E:
                    DE.l = DE.l;
                    break;
                case Codes.LD_E_H:
                    DE.l = HL.h;
                    break;
                case Codes.LD_E_L:
                    DE.l = HL.l;
                    break;
                    #endregion
                #region LD_H_x
                case Codes.LD_H_A:
                    HL.h = AF.h;
                    break;
                case Codes.LD_H_B:
                    HL.h = BC.h;
                    break;
                case Codes.LD_H_C:
                    HL.h = BC.l;
                    break;
                case Codes.LD_H_D:
                    HL.h = DE.h;
                    break;
                case Codes.LD_H_E:
                    HL.h = DE.l;
                    break;
                case Codes.LD_H_H:
                    HL.h = HL.h;
                    break;
                case Codes.LD_H_L:
                    HL.h = HL.l;
                    break;
                #endregion
                #region LD_L_x
                case Codes.LD_L_A:
                    HL.l = AF.h;
                    break;
                case Codes.LD_L_B:
                    HL.l = BC.h;
                    break;
                case Codes.LD_L_C:
                    HL.l = BC.l;
                    break;
                case Codes.LD_L_D:
                    HL.l = DE.h;
                    break;
                case Codes.LD_L_E:
                    HL.l = DE.l;
                    break;
                case Codes.LD_L_H:
                    HL.l = HL.h;
                    break;
                case Codes.LD_L_L:
                    HL.l = HL.l;
                    break;
                    #endregion
                case Codes.LD_PC_HL:
                    PC = HL.w;
                    break;
                #region LD_xXX_X
                case Codes.LD_xHL_A:
                    WriteRAM(HL.w, AF.h);
                    break;
                case Codes.LD_xHL_B:
                    WriteRAM(HL.w, BC.h);
                    break;
                case Codes.LD_xHL_C:
                    WriteRAM(HL.w, BC.l);
                    break;
                case Codes.LD_xHL_D:
                    WriteRAM(HL.w, DE.h);
                    break;
                case Codes.LD_xHL_E:
                    WriteRAM(HL.w, DE.l);
                    break;
                case Codes.LD_xHL_H:
                    WriteRAM(HL.w, HL.h);
                    break;
                case Codes.LD_xHL_L:
                    WriteRAM(HL.w, HL.l);
                    break;
                case Codes.LD_xHL_BYTE:
                    WriteRAM(HL.w, ReadRAM(PC));
                    PC++;
                    break;
                case Codes.LD_xBC_A:
                    WriteRAM(BC.w, AF.h);
                    break;
                case Codes.LD_xDE_A:
                    WriteRAM(DE.w, AF.h);
                    break;
                #endregion
                #endregion
                #region Arithmetic Instructions
                #region Subtract Instructions
                case Codes.SUB_A:
                    SUB(AF.h);                        
                    break;
                case Codes.SUB_B:
                    SUB(BC.h);                    
                    break;
                case Codes.SUB_C:
                    SUB(BC.l);                   
                    break;
                case Codes.SUB_D:
                    SUB(DE.h);                    
                    break;
                case Codes.SUB_E:
                    SUB(DE.l);                    
                    break;
                case Codes.SUB_H:
                    SUB(HL.h);                    
                    break;
                case Codes.SUB_L:
                    SUB(HL.l);                    
                    break;
                case Codes.SUB_xHL:
                    SUB(ReadRAM(HL.w));
                    break;
                case Codes.SUB_BYTE:
                    SUB(ReadRAM(PC));
                    PC++;
                    break;                
                #endregion
                #region SBC
                case Codes.SBC_A:
                    SBC_X(AF.h);                    
                    break;
                case Codes.SBC_B:
                    SBC_X(BC.h);                    
                    break;
                case Codes.SBC_C:
                    SBC_X(BC.l);                    
                    break;
                case Codes.SBC_D:
                    SBC_X(DE.h);
                    break;
                case Codes.SBC_E:
                    SBC_X(DE.l);
                    break;
                case Codes.SBC_H:
                    SBC_X(HL.h);
                    break;
                case Codes.SBC_L:
                    SBC_X(HL.l);
                    break;
                case Codes.SBC_xHL:
                    SBC_X(ReadRAM(HL.w));
                    break;
                case Codes.SBC_BYTE:
                    SBC_X(ReadRAM(PC));
                    PC++;
                    break;

                #endregion
                #region Addition Instructions
                case Codes.ADD_A:
                    ADD_X(AF.h);
                    break;
                case Codes.ADD_B:
                    ADD_X(BC.h);                    
                    break;
                case Codes.ADD_C:
                    ADD_X(BC.l);
                    break;
                case Codes.ADD_D:
                    ADD_X(DE.h);
                    break;
                case Codes.ADD_E:
                    ADD_X(DE.l);
                    break;
                case Codes.ADD_H:
                    ADD_X(HL.h);
                    break;
                case Codes.ADD_L:
                    ADD_X(HL.l);
                    break;
                case Codes.ADD_BYTE:
                    ADD_X(ReadRAM(PC));
                    PC++;
                    break;
                case Codes.ADD_xHL:
                    ADD_X(ReadRAM(HL.w));
                    break;
                 //HL instructions
                case Codes.ADD_HL_SP:
                    ResultReg.w = (ushort)(HL.w + SP);                    
                    SetFlags((byte)(((ResultReg.w) == 0) ? 1 : 0),
                         0,
                         (byte)((((HL.w & 0x0FFF) + (SP & 0x0FFF)) > 0x0FFF) ? 1 : 0),
                         (byte)(((HL.w + SP) > 0xFFFF) ? 1 : 0));
                    HL.w = ResultReg.w;
                    break;
                case Codes.ADD_HL_BC:
                    ResultReg.w = (ushort)(HL.w + BC.w);
                    SetFlags((byte)(((ResultReg.w) == 0) ? 1 : 0),
                         0,
                         (byte)((((HL.w & 0x0FFF) + (BC.w & 0x0FFF)) > 0x0FFF) ? 1 : 0),
                         (byte)(((HL.w + BC.w) > 0xFFFF) ? 1 : 0));
                    HL.w = ResultReg.w;
                    break;
                case Codes.ADD_HL_DE:
                    ResultReg.w = (ushort)(HL.w + DE.w);
                    SetFlags((byte)(((ResultReg.w) == 0) ? 1 : 0),
                         0,
                         (byte)((((HL.w & 0x0FFF) + (DE.w & 0x0FFF)) > 0x0FFF) ? 1 : 0),
                         (byte)(((HL.w + DE.w) > 0xFFFF) ? 1 : 0));
                    HL.w = ResultReg.w;
                    break;
                case Codes.ADD_HL_HL:
                    ResultReg.w = (ushort)(HL.w + HL.w);
                    SetFlags((byte)(((ResultReg.w) == 0) ? 1 : 0),
                         0,
                         (byte)((((HL.w & 0x0FFF) + (HL.w & 0x0FFF)) > 0x0FFF) ? 1 : 0),
                         (byte)(((HL.w + HL.w) > 0xFFFF) ? 1 : 0));
                    HL.w = ResultReg.w;
                    break;
                              
                #endregion
                #region ADC
                case Codes.ADC_A:
                    ADC_X(AF.h);
                    break;
                case Codes.ADC_B:
                    ADC_X(BC.h);
                    break;
                case Codes.ADC_C:
                    ADC_X(BC.l);
                    break;
                case Codes.ADC_D:
                    ADC_X(DE.h);
                    break;
                case Codes.ADC_E:
                    ADC_X(DE.l);
                    break;
                case Codes.ADC_H:
                    ADC_X(HL.h);
                    break;
                case Codes.ADC_L:
                    ADC_X(HL.l);
                    break;
                case Codes.ADC_xHL:
                    ADC_X(ReadRAM(HL.w));
                    break;
                case Codes.ADC_BYTE:
                    ADC_X(ReadRAM(PC));
                    PC++;
                    break;

                    #endregion
                #region INC
                case Codes.INC_A:
                    INC_X(ref AF.h);                    
                    break;
                case Codes.INC_B:
                    INC_X(ref BC.h);
                    break;
                case Codes.INC_C:
                    INC_X(ref BC.l);
                    break;
                case Codes.INC_D:
                    INC_X(ref DE.h);
                    break;
                case Codes.INC_E:
                    INC_X(ref DE.l);
                    break;
                case Codes.INC_H:
                    INC_X(ref HL.h);
                    break;
                case Codes.INC_L:
                    INC_X(ref HL.l);
                    break;
                case Codes.INC_xHL:
                    ResultReg.w = (ushort)(ReadRAM(HL.w) + 1);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                         0,
                         (byte)((((ReadRAM(HL.w) & 0x0F) + 1) > 0x0F) ? 1 : 0));
                    WriteRAM(HL.w, ResultReg.l);
                    break;
                case Codes.INC_BC:
                    ResultReg.w = (ushort)(BC.w + 1);                   
                    BC.w++;
                    break;
                case Codes.INC_DE:
                    ResultReg.w = (ushort)(DE.w + 1);                    
                    DE.w++;
                    break;
                case Codes.INC_HL:
                    ResultReg.w = (ushort)(HL.w + 1);                   
                    HL.w++;
                    break;
                case Codes.INC_SP:
                    ResultReg.w = (ushort)(SP + 1);                   
                    SP++;
                    break;
                #endregion
                #region DEC
                case Codes.DEC_A:
                    DEC_X(ref AF.h);                    
                    break;
                case Codes.DEC_B:
                    DEC_X(ref BC.h);                    
                    break;
                case Codes.DEC_C:
                    DEC_X(ref BC.l);
                    break;
                case Codes.DEC_D:
                    DEC_X(ref DE.h);
                    break;
                case Codes.DEC_E:
                    DEC_X(ref DE.l);
                    break;
                case Codes.DEC_H:
                    DEC_X(ref HL.h);
                    break;
                case Codes.DEC_L:
                    DEC_X(ref HL.l);
                    break;
                case Codes.DEC_xHL:
                    ResultReg.w = (ushort)(ReadRAM(HL.w) - 1);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                         1,
                         (byte)((!((ReadRAM(HL.w) & 0x0F) > 0)) ? 1 : 0));
                    WriteRAM(HL.w, ResultReg.l);
                    break;
                case Codes.DEC_BC:                    
                    BC.w--;
                    break;
                case Codes.DEC_DE:
                    DE.w--;
                    break;
                case Codes.DEC_HL:
                    HL.w--;
                    break;
                case Codes.DEC_SP:
                    SP--;
                    break;
                #endregion
                case Codes.EXX:
                    
#if GAMEBOY         
                    //RETI
                    PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                    SP += 2;
                    IME = 1;
                    break;
#else
                    break;
#endif

                #endregion
                #region Bitmanipulation instructions
                #region AND
                case Codes.AND_A:
                    ResultReg.w = (ushort)(AF.h & AF.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_B:
                    ResultReg.w = (ushort)(AF.h & BC.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_C:
                    ResultReg.w = (ushort)(AF.h & BC.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_D:
                    ResultReg.w = (ushort)(AF.h & DE.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_E:
                    ResultReg.w = (ushort)(AF.h & DE.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_H:
                    ResultReg.w = (ushort)(AF.h & HL.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_L:
                    ResultReg.w = (ushort)(AF.h & HL.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.AND_BYTE:
                    ResultReg.w = (ushort)(AF.h & ReadRAM(PC));
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             1,
                             0);
                    AF.h = ResultReg.l;
                    PC++;
                    break;
                #endregion
                #region OR
                case Codes.OR_A:
                    ResultReg.w = (ushort)(AF.h | AF.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_B:
                    ResultReg.w = (ushort)(AF.h | BC.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_C:
                    ResultReg.w = (ushort)(AF.h | BC.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_D:
                    ResultReg.w = (ushort)(AF.h | DE.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_E:
                    ResultReg.w = (ushort)(AF.h | DE.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_H:
                    ResultReg.w = (ushort)(AF.h & HL.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_L:
                    ResultReg.w = (ushort)(AF.h | HL.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.OR_BYTE:
                    ResultReg.w = (ushort)(AF.h | ReadRAM(PC));
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    PC++;
                    break;
                case Codes.OR_xHL:
                    ResultReg.w = (ushort)(AF.h | ReadRAM(HL.w));
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                #endregion
                #region XOR
                case Codes.XOR_A:
                    ResultReg.w = (ushort)(AF.h ^ AF.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_B:
                    ResultReg.w = (ushort)(AF.h ^ BC.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_C:
                    ResultReg.w = (ushort)(AF.h ^ BC.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_D:
                    ResultReg.w = (ushort)(AF.h ^ DE.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_E:
                    ResultReg.w = (ushort)(AF.h ^ DE.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_H:
                    ResultReg.w = (ushort)(AF.h ^ HL.h);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_L:
                    ResultReg.w = (ushort)(AF.h ^ HL.l);
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    break;
                case Codes.XOR_BYTE:
                    ResultReg.w = (ushort)(AF.h ^ ReadRAM(PC));
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             0);
                    AF.h = ResultReg.l;
                    PC++;
                    break;
                #endregion
                case Codes.CPL:
                    ResultReg.w = (ushort)~AF.h;
                    AF.l = (byte)((AF.l  & Z_FLAG) | ((1 << 6) & N_FLAG) | ((1 << 5) & H_FLAG) | (AF.l & C_FLAG));
                    AF.h = ResultReg.l;
                    break;
                case Codes.RLCA:
                    ResultReg.w = (ushort)((AF.h << 1)|(AF.h >> 7));
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             (byte)(((AF.h & 0x80)>0) ? 1 : 0));
                    AF.h = ResultReg.l;
                    break;
                case Codes.RRCA:
                    ResultReg.w = (ushort)((AF.h >> 1) | (AF.h << 7));
                    SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                             0,
                             0,
                             (byte)(((AF.h & 0x01)>0) ? 1 : 0));
                    AF.h = ResultReg.l;
                    break;
                case Codes.RLA:
                    {
                        int carry = GetFlag(C_FLAG) ? 1 : 0;
                        ResultReg.w = (ushort)((AF.h << 1) | carry);
                        SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                                 0,
                                 0,
                                 (byte)(((AF.h & 0x80) > 0) ? 1 : 0));
                        AF.h = ResultReg.l;
                    }
                    break;
                case Codes.RRA:
                    {
                        int carry = GetFlag(C_FLAG) ? 1 : 0;
                        ResultReg.w = (ushort)((AF.h >> 1) | carry);
                        SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                                 0,
                                 0,
                                 (byte)(((AF.h & 0x80) > 0) ? 1 : 0));
                        AF.h = ResultReg.l;
                    }
                    break;
                #endregion
                #region Function Instructions
                case Codes.CALL:
                    jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                    PC += 2;
                   
                    fixed (ushort* shrtpntr = &PC)
                    {
                        SP -= 2;
                        bytes = (byte*)shrtpntr;
                        WriteRAM(SP, bytes[0]);
                        WriteRAM(SP + 1, bytes[1]);        
                        PC = jumpadress;
                    }
                    break;
                case Codes.CALL_NC:
                    //test for not carry
                    if ((AF.l & C_FLAG) == 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC += 2;

                        fixed (ushort* shrtpntr = &PC)
                        {
                            SP -= 2;
                            bytes = (byte*)shrtpntr;
                            WriteRAM(SP, bytes[0]);
                            WriteRAM(SP + 1, bytes[1]);
                            PC = jumpadress;
                        }
                    }
                    break;
                case Codes.CALL_C:
                    //test for not carry
                    if ((AF.l & C_FLAG) != 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC += 2;

                        fixed (ushort* shrtpntr = &PC)
                        {
                            SP -= 2;
                            bytes = (byte*)shrtpntr;
                            WriteRAM(SP, bytes[0]);
                            WriteRAM(SP + 1, bytes[1]);
                            PC = jumpadress;
                        }
                    }
                    break;
                case Codes.CALL_NZ:
                    //test for not carry
                    if ((AF.l & Z_FLAG) == 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC += 2;

                        fixed (ushort* shrtpntr = &PC)
                        {
                            SP -= 2;
                            bytes = (byte*)shrtpntr;
                            WriteRAM(SP, bytes[0]);
                            WriteRAM(SP + 1, bytes[1]);
                            PC = jumpadress;
                        }
                    }
                    break;
                case Codes.CALL_Z:
                    //test for not carry
                    if ((AF.l & Z_FLAG) != 0)
                    {
                        jumpadress = (ushort)(ReadRAM(PC) + (ReadRAM(PC + 1) << 8));
                        PC += 2;

                        fixed (ushort* shrtpntr = &PC)
                        {
                            SP -= 2;
                            bytes = (byte*)shrtpntr;
                            WriteRAM(SP, bytes[0]);
                            WriteRAM(SP + 1, bytes[1]);
                            PC = jumpadress;
                        }
                    }
                    break;
                case Codes.RST00:
                    RST(0x00);
                    break;
                case Codes.RST08:
                    RST(0x08);
                    break;
                case Codes.RST10:
                    RST(0x10);
                    break;
                case Codes.RST18:
                    RST(0x18);
                    break;
                case Codes.RST20:
                    RST(0x20);
                    break;
                case Codes.RST28:
                    RST(0x28);
                    break;
                case Codes.RST30:
                    RST(0x30);
                    break;
                case Codes.RST38:
                    RST(0x38);
                    break;

                case Codes.RET:                    
                    PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                    SP += 2;
                    break;
                case Codes.RET_M:
#if GAMEBOY
                    boffset = (sbyte)(ReadRAM(PC));    
                    adress = (ushort)((ushort)(SP) + boffset);
                    HL.w = adress;
                    PC += 1;
                    SetFlags(0, 0, 0, (byte)(((SP + boffset) > 0xffff) ? 1 : 0));
#else 
                        //fix PO test
                        SP += 2;
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));                        
#endif
                    break;
                case Codes.RET_PO:
#if GAMEBOY
                    adress = (ushort)((ushort)(0xff << 8) + (ushort)(ReadRAM(PC)));
                    WriteRAM(adress, AF.h);
                    PC += 1;
#else 
                        //fix PO test
                        SP += 2;
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));                        
#endif
                    break;
                case Codes.RET_PE:
#if GAMEBOY                    
                    //ADD SP, constant
                    boffset = (sbyte)(ReadRAM(PC));                                        
                    SP = (ushort)((short)SP + boffset);                                       
                    SetFlags(0, 0, 0, (byte)(((SP + boffset) > 0xffff) ? 1 : 0));

                    PC += 1;
#else 
                        //fix PO test
                        SP += 2;
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));                        
#endif
                    break;
                case Codes.RET_P:
#if GAMEBOY
                    adress = (ushort)((0xFF << 8)+ReadRAM(PC));
                    AF.h = ReadRAM(adress);
                    PC += 1;
#else 
                        //fix P test
                        SP += 2;
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));                        
#endif

                    break;
                case Codes.RET_NC:
                    //test for not carry
                    if ((AF.l & C_FLAG) == 0)
                    {                        
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                        SP += 2;
                    }
                    break;
                case Codes.RET_C:
                    //test for carry
                    if ((AF.l & C_FLAG) != 0)
                    {                        
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                        SP += 2;
                    }
                    break;
                case Codes.RET_NZ:
                    //test for not zero
                    if ((AF.l & Z_FLAG) == 0)
                    {                        
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                        SP += 2;
                    }
                    break;
                case Codes.RET_Z:
                    //test for zero
                    if ((AF.l & Z_FLAG) != 0)
                    {                        
                        PC = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                        SP += 2;
                    }
                    break;
                #endregion
                #region Interrupt Instructions
                case Codes.DI:
                    IME = 0;
                    break;
                case Codes.EI:
                    IME = 1;
                    break;

                #endregion               
                #region CP
                case Codes.CP_BYTE:
                    CP_X(ReadRAM(PC));
                    PC++;
                    break;
                case Codes.CP_A:
                    CP_X(AF.h);
                    break;
                case Codes.CP_B:
                    CP_X(BC.h);
                    break;
                case Codes.CP_C:
                    CP_X(BC.l);
                    break;
                case Codes.CP_D:
                    CP_X(DE.h);
                    break;
                case Codes.CP_E:
                    CP_X(DE.l);
                    break;
                case Codes.CP_H:
                    CP_X(HL.h);
                    break;
                case Codes.CP_L:
                    CP_X(HL.l);
                    break;
                case Codes.CP_xHL:
                    CP_X(ReadRAM(HL.w));
                    break;
                #endregion
                #region PUSH_POP
                case Codes.PUSH_HL:
                    SP -= 2;
                    WriteRAM(SP, HL.w);                    
                    break;
                case Codes.PUSH_BC:
                    SP -= 2;
                    WriteRAM(SP, BC.w);                    
                    break;
                case Codes.PUSH_DE:
                    SP -= 2;
                    WriteRAM(SP, DE.w);                    
                    break;
                case Codes.PUSH_AF:
                    SP -= 2;
                    WriteRAM(SP, AF.w);                    
                    break;
                case Codes.POP_AF:
                    AF.w = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                    SP += 2;
                    break;
                case Codes.POP_BC:
                    BC.w = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                    SP += 2;
                    break;
                case Codes.POP_DE:
                    DE.w = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                    SP += 2;
                    break;
                case Codes.POP_HL:
                    HL.w = (ushort)(ReadRAM(SP) + (ReadRAM(SP + 1) << 8));
                    SP += 2;
                    break;
                #endregion
                case Codes.SCF:
                    AF.l = (byte)(((AF.l) & Z_FLAG) | ((0 << 6) & N_FLAG) | ((0 << 5) & H_FLAG) | ((1 << 4) & C_FLAG));
                    break;
                case Codes.CCF:
                    AF.l = (byte)(((AF.l) & Z_FLAG) | ((0 << 6) & N_FLAG) | ((0 << 5) & H_FLAG) | (((AF.l & C_FLAG)>0?0:1) << 4) &C_FLAG );
                    break;
                
                case Codes.DJNZ:
                    break;
                case Codes.DAA:
                {
                    if ((AF.l & N_FLAG) == 0) 
                    {
                        if (((AF.h & 0x0F) > 9) || ((AF.l & H_FLAG) > 0)) 
					    {
                            AF.h += 6;
                            AF.l |= (byte)((AF.h & 0xF0) == 0 ? C_FLAG | C_FLAG : H_FLAG); 
                        }
                        if (((AF.h & 0xF0) > 0x90) || ((AF.l & C_FLAG) > 0)) 
						{
                            AF.h += 0x60;
                            AF.l |= C_FLAG; 
			            } 
	                }
					else 
					{
                        if (((AF.h & 0x0F) > 9) || ((AF.l & H_FLAG) > 0)) 
			            {
                            AF.h -= 6;
                            AF.l |= (byte)((AF.h & 0xF0) == 0xF0 ? C_FLAG | H_FLAG : H_FLAG); 
			            }
                        if (((AF.h & 0xF0) > 0x90) || ((AF.l & C_FLAG) > 0)) 
			            {
                            AF.h -= 0x60;
                            AF.l |= C_FLAG; 
			            } 
                    }
                    AF.l = (byte)(AF.h == 0 ? AF.l | Z_FLAG : AF.l & (0xFF - Z_FLAG)); 
                }                    
                    break;
                case Codes.EX_AF_AF:
                    WriteRAM(ReadRAM(PC), SP);
                    PC++;
                    break;
                case Codes.HALT:
                    break;
                default:                    
                    throw new InvalidOpCodeException();                    
            }
        }

        unsafe public void ExecuteCB(byte currentop)
        {
            CodesCB code = (CodesCB)currentop;
            switch (code)
            {
                #region SLA
                case CodesCB.SLA_A:
                    AF.h=SLA_X(AF.h);
                    break;
                case CodesCB.SLA_B:
                    BC.h = SLA_X(BC.h);                    
                    break;
                case CodesCB.SLA_C:
                    BC.l = SLA_X(BC.l);
                    break;
                case CodesCB.SLA_D:
                    DE.h = SLA_X(DE.h);                    
                    break;
                case CodesCB.SLA_E:
                    DE.l = SLA_X(DE.l);
                    break;
                case CodesCB.SLA_H:
                    HL.h = SLA_X(HL.h);
                    break;
                case CodesCB.SLA_L:
                    HL.l = SLA_X(HL.l);
                    break;
                case CodesCB.SLA_xHL:
                    WriteRAM(HL.w,SLA_X(ReadRAM(HL.w)));                    
                    break;
                #endregion
                #region SRA
                case CodesCB.SRA_A:
                    AF.h = SRA_X(AF.h);
                    break;
                case CodesCB.SRA_B:
                    BC.h = SRA_X(BC.h);
                    break;
                case CodesCB.SRA_C:
                    BC.l = SRA_X(BC.l);
                    break;
                case CodesCB.SRA_D:
                    DE.h = SRA_X(DE.h);
                    break;
                case CodesCB.SRA_E:
                    DE.l = SRA_X(DE.l);
                    break;
                case CodesCB.SRA_H:
                    HL.h = SRA_X(HL.h);
                    break;
                case CodesCB.SRA_L:
                    HL.l = SRA_X(HL.l);
                    break;
                case CodesCB.SRA_xHL:
                    WriteRAM(HL.w, SRA_X(ReadRAM(HL.w)));
                    break;
                #endregion
                #region SRL
                case CodesCB.SRL_A:
                    AF.h=SRL_X(AF.h);
                    break;
                case CodesCB.SRL_B:
                    BC.h = SRL_X(BC.h);                    
                    break;
                case CodesCB.SRL_C:
                    BC.l = SRL_X(BC.l);                    
                    break;
                case CodesCB.SRL_D:
                    DE.h = SRL_X(DE.h);
                    break;
                case CodesCB.SRL_E:
                    DE.l = SRL_X(DE.l);
                    break;
                case CodesCB.SRL_H:
                    HL.h = SRL_X(HL.h);
                    break;
                case CodesCB.SRL_L:
                    HL.l = SRL_X(HL.l);
                    break;
                case CodesCB.SRL_xHL:
                    WriteRAM(HL.w,SRL_X(ReadRAM(HL.w)));
                    
                    break;
                    #endregion
                #region RLC
                case CodesCB.RLC_A:
                    AF.h=RLC_X(AF.h);                    
                    break;
                case CodesCB.RLC_B:
                    BC.h=RLC_X(BC.h);                    
                    break;
                case CodesCB.RLC_C:
                    BC.l=RLC_X(BC.l);
                    break;
                case CodesCB.RLC_D:
                    DE.h=RLC_X(DE.h);
                    break;
                case CodesCB.RLC_E:
                    DE.l=RLC_X(DE.l);
                    break;                
                case CodesCB.RLC_H:
                    HL.h=RLC_X(HL.h);
                    break;
                case CodesCB.RLC_L:
                    HL.l=RLC_X(HL.l);
                    break;
                case CodesCB.RLC_xHL:
                    WriteRAM(HL.w, RLC_X(ReadRAM(HL.w)));
                    break;
                #endregion
                #region RR
                case CodesCB.RR_A:
                    AF.h=RR_X(AF.h);                    
                    break;
                case CodesCB.RR_B:
                    BC.h=RR_X(BC.h);                    
                    break;
                case CodesCB.RR_C:
                    BC.l=RR_X(BC.l);                    
                    break;
                case CodesCB.RR_D:
                    DE.h=RR_X(DE.h);                    
                    break;
                case CodesCB.RR_E:
                    DE.l=RR_X(DE.l);
                    break;
                case CodesCB.RR_H:
                    HL.h=RR_X(HL.h);
                    break;
                case CodesCB.RR_L:
                    HL.l=RR_X(HL.l);
                    break;
                case CodesCB.RR_xHL:
                    WriteRAM(HL.w, RR_X(ReadRAM(HL.w)));
                    break;
                #endregion
                #region RL
                case CodesCB.RL_A:
                    AF.h=RL_X(AF.h);                    
                    break;
                case CodesCB.RL_B:
                    BC.h=RL_X(BC.h);                    
                    break;
                case CodesCB.RL_C:
                    BC.l=RL_X(BC.l);                    
                    break;
                case CodesCB.RL_D:
                    DE.h=RL_X(DE.h);
                    break;
                case CodesCB.RL_E:
                    DE.l=RL_X(DE.l);
                    break;
                case CodesCB.RL_H:
                    HL.h=RL_X(HL.h);
                    break;
                case CodesCB.RL_L:
                    HL.l=RL_X(HL.l);                    
                    break;
                case CodesCB.RL_xHL:
                    WriteRAM(HL.w, RL_X(ReadRAM(HL.w)));                    
                    break;
                #endregion
                #region SWAP
                case CodesCB.SWAP_A:
                    SWAP_X(ref AF.h);                   
                    break;
                case CodesCB.SWAP_B:
                    SWAP_X(ref BC.h);
                    break;
                case CodesCB.SWAP_C:
                    SWAP_X(ref BC.l);
                    break;
                case CodesCB.SWAP_D:
                    SWAP_X(ref DE.h);
                    break;
                case CodesCB.SWAP_E:
                    SWAP_X(ref DE.l);
                    break;
                case CodesCB.SWAP_H:
                    SWAP_X(ref HL.h);
                    break;
                case CodesCB.SWAP_L:
                    SWAP_X(ref HL.l);
                    break;
                case CodesCB.SWAP_xHL:
                    ResultReg.w = (ushort)((ReadRAM(HL.w) >> 4) | (ReadRAM(HL.w) << 4));
                    SetFlags((byte)((ResultReg.l == 0) ? 1 : 0),
                        0,
                        0,
                        0);
                    WriteRAM(HL.w, ResultReg.l);
                    break;
                #endregion
                #region RES0_X
                case CodesCB.RES0_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 0));           
                    break;
                case CodesCB.RES0_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 0));
                    break;
                case CodesCB.RES0_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 0));
                    break;
                case CodesCB.RES0_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 0));
                    break;
                case CodesCB.RES0_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 0));
                    break;
                case CodesCB.RES0_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 0));
                    break;
                case CodesCB.RES0_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 0));
                    break;
                case CodesCB.RES0_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 0)));
                    break;
                #endregion                
                #region RES1_X
                case CodesCB.RES1_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 1));
                    break;
                case CodesCB.RES1_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 1));
                    break;
                case CodesCB.RES1_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 1));
                    break;
                case CodesCB.RES1_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 1));
                    break;
                case CodesCB.RES1_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 1));
                    break;
                case CodesCB.RES1_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 1));
                    break;
                case CodesCB.RES1_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 1));
                    break;
                case CodesCB.RES1_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 1)));
                    break;
                #endregion
                #region RES2_X
                case CodesCB.RES2_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 2));
                    break;
                case CodesCB.RES2_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 2));
                    break;
                case CodesCB.RES2_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 2));
                    break;
                case CodesCB.RES2_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 2));
                    break;
                case CodesCB.RES2_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 2));
                    break;
                case CodesCB.RES2_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 2));
                    break;
                case CodesCB.RES2_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 2));
                    break;
                case CodesCB.RES2_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 2)));
                    break;
                    #endregion
                #region RES3_X
                case CodesCB.RES3_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 3));
                    break;
                case CodesCB.RES3_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 3));
                    break;
                case CodesCB.RES3_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 3));
                    break;
                case CodesCB.RES3_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 3));
                    break;
                case CodesCB.RES3_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 3));
                    break;
                case CodesCB.RES3_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 3));
                    break;
                case CodesCB.RES3_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 3));
                    break;
                case CodesCB.RES3_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 3)));
                    break;
                    #endregion
                #region RES4_X
                case CodesCB.RES4_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 4));
                    break;
                case CodesCB.RES4_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 4));
                    break;
                case CodesCB.RES4_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 4));
                    break;
                case CodesCB.RES4_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 4));
                    break;
                case CodesCB.RES4_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 4));
                    break;
                case CodesCB.RES4_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 4));
                    break;
                case CodesCB.RES4_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 4));
                    break;
                case CodesCB.RES4_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 4)));
                    break;
                    #endregion
                #region RES5_X
                case CodesCB.RES5_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 5));
                    break;
                case CodesCB.RES5_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 5));
                    break;
                case CodesCB.RES5_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 5));
                    break;
                case CodesCB.RES5_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 5));
                    break;
                case CodesCB.RES5_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 5));
                    break;
                case CodesCB.RES5_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 5));
                    break;
                case CodesCB.RES5_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 5));
                    break;
                case CodesCB.RES5_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 5)));
                    break;
                    #endregion
                #region RES6_X
                case CodesCB.RES6_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 6));
                    break;
                case CodesCB.RES6_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 6));
                    break;
                case CodesCB.RES6_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 6));
                    break;
                case CodesCB.RES6_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 6));
                    break;
                case CodesCB.RES6_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 6));
                    break;
                case CodesCB.RES6_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 6));
                    break;
                case CodesCB.RES6_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 6));
                    break;
                case CodesCB.RES6_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 6)));
                    break;
                    #endregion
                #region RES7_X
                case CodesCB.RES7_A:
                    AF.h = (byte)((int)AF.h & ~(1 << 7));
                    break;
                case CodesCB.RES7_B:
                    BC.h = (byte)((int)BC.h & ~(1 << 7));
                    break;
                case CodesCB.RES7_C:
                    BC.l = (byte)((int)BC.l & ~(1 << 7));
                    break;
                case CodesCB.RES7_D:
                    DE.h = (byte)((int)DE.h & ~(1 << 7));
                    break;
                case CodesCB.RES7_E:
                    DE.l = (byte)((int)DE.l & ~(1 << 7));
                    break;
                case CodesCB.RES7_H:
                    HL.h = (byte)((int)HL.h & ~(1 << 7));
                    break;
                case CodesCB.RES7_L:
                    HL.l = (byte)((int)HL.l & ~(1 << 7));
                    break;
                case CodesCB.RES7_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) & ~(1 << 7)));
                    break;
                    #endregion
                #region SET0_X
                case CodesCB.SET0_A:
                    AF.h = (byte)((int)AF.h |(1 << 0));
                    break;
                case CodesCB.SET0_B:
                    BC.h = (byte)((int)BC.h |(1 << 0));
                    break;
                case CodesCB.SET0_C:
                    BC.l = (byte)((int)BC.l |(1 << 0));
                    break;
                case CodesCB.SET0_D:
                    DE.h = (byte)((int)DE.h |(1 << 0));
                    break;
                case CodesCB.SET0_E:
                    DE.l = (byte)((int)DE.l |(1 << 0));
                    break;
                case CodesCB.SET0_H:
                    HL.h = (byte)((int)HL.h |(1 << 0));
                    break;
                case CodesCB.SET0_L:
                    HL.l = (byte)((int)HL.l |(1 << 0));
                    break;
                case CodesCB.SET0_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 0)));
                    break;
                                    #endregion
                #region SET1_X
                case CodesCB.SET1_A:
                    AF.h = (byte)((int)AF.h |(1 << 1));
                    break;
                case CodesCB.SET1_B:
                    BC.h = (byte)((int)BC.h |(1 << 1));
                    break;
                case CodesCB.SET1_C:
                    BC.l = (byte)((int)BC.l |(1 << 1));
                    break;
                case CodesCB.SET1_D:
                    DE.h = (byte)((int)DE.h |(1 << 1));
                    break;
                case CodesCB.SET1_E:
                    DE.l = (byte)((int)DE.l |(1 << 1));
                    break;
                case CodesCB.SET1_H:
                    HL.h = (byte)((int)HL.h |(1 << 1));
                    break;
                case CodesCB.SET1_L:
                    HL.l = (byte)((int)HL.l |(1 << 1));
                    break;
                case CodesCB.SET1_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 1)));
                    break;
                #endregion
                #region SET2_X
                case CodesCB.SET2_A:
                    AF.h = (byte)((int)AF.h |(1 << 2));
                    break;
                case CodesCB.SET2_B:
                    BC.h = (byte)((int)BC.h |(1 << 2));
                    break;
                case CodesCB.SET2_C:
                    BC.l = (byte)((int)BC.l |(1 << 2));
                    break;
                case CodesCB.SET2_D:
                    DE.h = (byte)((int)DE.h |(1 << 2));
                    break;
                case CodesCB.SET2_E:
                    DE.l = (byte)((int)DE.l |(1 << 2));
                    break;
                case CodesCB.SET2_H:
                    HL.h = (byte)((int)HL.h |(1 << 2));
                    break;
                case CodesCB.SET2_L:
                    HL.l = (byte)((int)HL.l |(1 << 2));
                    break;
                case CodesCB.SET2_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 2)));
                    break;
                #endregion
                #region SET3_X
                case CodesCB.SET3_A:
                    AF.h = (byte)((int)AF.h |(1 << 3));
                    break;
                case CodesCB.SET3_B:
                    BC.h = (byte)((int)BC.h |(1 << 3));
                    break;
                case CodesCB.SET3_C:
                    BC.l = (byte)((int)BC.l |(1 << 3));
                    break;
                case CodesCB.SET3_D:
                    DE.h = (byte)((int)DE.h |(1 << 3));
                    break;
                case CodesCB.SET3_E:
                    DE.l = (byte)((int)DE.l |(1 << 3));
                    break;
                case CodesCB.SET3_H:
                    HL.h = (byte)((int)HL.h |(1 << 3));
                    break;
                case CodesCB.SET3_L:
                    HL.l = (byte)((int)HL.l |(1 << 3));
                    break;
                case CodesCB.SET3_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 3)));
                    break;
                #endregion
                #region SET4_X
                case CodesCB.SET4_A:
                    AF.h = (byte)((int)AF.h |(1 << 4));
                    break;
                case CodesCB.SET4_B:
                    BC.h = (byte)((int)BC.h |(1 << 4));
                    break;
                case CodesCB.SET4_C:
                    BC.l = (byte)((int)BC.l |(1 << 4));
                    break;
                case CodesCB.SET4_D:
                    DE.h = (byte)((int)DE.h |(1 << 4));
                    break;
                case CodesCB.SET4_E:
                    DE.l = (byte)((int)DE.l |(1 << 4));
                    break;
                case CodesCB.SET4_H:
                    HL.h = (byte)((int)HL.h |(1 << 4));
                    break;
                case CodesCB.SET4_L:
                    HL.l = (byte)((int)HL.l |(1 << 4));
                    break;
                case CodesCB.SET4_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 4)));
                    break;
                #endregion
                #region SET5_X
                case CodesCB.SET5_A:
                    AF.h = (byte)((int)AF.h |(1 << 5));
                    break;
                case CodesCB.SET5_B:
                    BC.h = (byte)((int)BC.h |(1 << 5));
                    break;
                case CodesCB.SET5_C:
                    BC.l = (byte)((int)BC.l |(1 << 5));
                    break;
                case CodesCB.SET5_D:
                    DE.h = (byte)((int)DE.h |(1 << 5));
                    break;
                case CodesCB.SET5_E:
                    DE.l = (byte)((int)DE.l |(1 << 5));
                    break;
                case CodesCB.SET5_H:
                    HL.h = (byte)((int)HL.h |(1 << 5));
                    break;
                case CodesCB.SET5_L:
                    HL.l = (byte)((int)HL.l |(1 << 5));
                    break;
                case CodesCB.SET5_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 5)));
                    break;
                #endregion
                #region SET6_X
                case CodesCB.SET6_A:
                    AF.h = (byte)((int)AF.h |(1 << 6));
                    break;
                case CodesCB.SET6_B:
                    BC.h = (byte)((int)BC.h |(1 << 6));
                    break;
                case CodesCB.SET6_C:
                    BC.l = (byte)((int)BC.l |(1 << 6));
                    break;
                case CodesCB.SET6_D:
                    DE.h = (byte)((int)DE.h |(1 << 6));
                    break;
                case CodesCB.SET6_E:
                    DE.l = (byte)((int)DE.l |(1 << 6));
                    break;
                case CodesCB.SET6_H:
                    HL.h = (byte)((int)HL.h |(1 << 6));
                    break;
                case CodesCB.SET6_L:
                    HL.l = (byte)((int)HL.l |(1 << 6));
                    break;
                case CodesCB.SET6_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 6)));
                    break;
                #endregion
                #region SET7_X
                case CodesCB.SET7_A:
                    AF.h = (byte)((int)AF.h |(1 << 7));
                    break;
                case CodesCB.SET7_B:
                    BC.h = (byte)((int)BC.h |(1 << 7));
                    break;
                case CodesCB.SET7_C:
                    BC.l = (byte)((int)BC.l |(1 << 7));
                    break;
                case CodesCB.SET7_D:
                    DE.h = (byte)((int)DE.h |(1 << 7));
                    break;
                case CodesCB.SET7_E:
                    DE.l = (byte)((int)DE.l |(1 << 7));
                    break;
                case CodesCB.SET7_H:
                    HL.h = (byte)((int)HL.h |(1 << 7));
                    break;
                case CodesCB.SET7_L:
                    HL.l = (byte)((int)HL.l |(1 << 7));
                    break;
                case CodesCB.SET7_xHL:
                    WriteRAM(HL.w, (byte)((int)ReadRAM(HL.w) |(1 << 7)));
                    break;
                #endregion
                #region BITx_A
                case CodesCB.BIT0_A:
                    BIT(AF.h, 0);
                    break;
                case CodesCB.BIT1_A:
                    BIT(AF.h, 1);
                    break;
                case CodesCB.BIT2_A:
                    BIT(AF.h, 2);
                    break;
                case CodesCB.BIT3_A:
                    BIT(AF.h, 3);
                    break;
                case CodesCB.BIT4_A:
                    BIT(AF.h, 4);
                    break;
                case CodesCB.BIT5_A:
                    BIT(AF.h, 5);
                    break;
                case CodesCB.BIT6_A:
                    BIT(AF.h, 6);
                    break;
                case CodesCB.BIT7_A:
                    BIT(AF.h, 7);
                    break;                
                #endregion
                #region BITx_B
                case CodesCB.BIT0_B:
                    BIT(BC.h, 0);
                    break;
                case CodesCB.BIT1_B:
                    BIT(BC.h, 1);
                    break;
                case CodesCB.BIT2_B:
                    BIT(BC.h, 2);
                    break;
                case CodesCB.BIT3_B:
                    BIT(BC.h, 3);
                    break;
                case CodesCB.BIT4_B:
                    BIT(BC.h, 4);
                    break;
                case CodesCB.BIT5_B:
                    BIT(BC.h, 5);
                    break;
                case CodesCB.BIT6_B:
                    BIT(BC.h, 6);
                    break;
                case CodesCB.BIT7_B:
                    BIT(BC.h, 7);
                    break;
                    #endregion
                #region BITx_C
                case CodesCB.BIT0_C:
                    BIT(BC.l, 0);
                    break;
                case CodesCB.BIT1_C:
                    BIT(BC.l, 1);
                    break;
                case CodesCB.BIT2_C:
                    BIT(BC.l, 2);
                    break;
                case CodesCB.BIT3_C:
                    BIT(BC.l, 3);
                    break;
                case CodesCB.BIT4_C:
                    BIT(BC.l, 4);
                    break;
                case CodesCB.BIT5_C:
                    BIT(BC.l, 5);
                    break;
                case CodesCB.BIT6_C:
                    BIT(BC.l, 6);
                    break;
                case CodesCB.BIT7_C:
                    BIT(BC.l, 7);
                    break;
                #endregion
                #region BITx_D
                case CodesCB.BIT0_D:
                    BIT(DE.h, 0);
                    break;
                case CodesCB.BIT1_D:
                    BIT(DE.h, 1);
                    break;
                case CodesCB.BIT2_D:
                    BIT(DE.h, 2);
                    break;
                case CodesCB.BIT3_D:
                    BIT(DE.h, 3);
                    break;
                case CodesCB.BIT4_D:
                    BIT(DE.h, 4);
                    break;
                case CodesCB.BIT5_D:
                    BIT(DE.h, 5);
                    break;
                case CodesCB.BIT6_D:
                    BIT(DE.h, 6);
                    break;
                case CodesCB.BIT7_D:
                    BIT(DE.h, 7);
                    break;
                                    #endregion
                #region BITx_E
                case CodesCB.BIT0_E:
                    BIT(DE.l, 0);
                    break;
                case CodesCB.BIT1_E:
                    BIT(DE.l, 1);
                    break;
                case CodesCB.BIT2_E:
                    BIT(DE.l, 2);
                    break;
                case CodesCB.BIT3_E:
                    BIT(DE.l, 3);
                    break;
                case CodesCB.BIT4_E:
                    BIT(DE.l, 4);
                    break;
                case CodesCB.BIT5_E:
                    BIT(DE.l, 5);
                    break;
                case CodesCB.BIT6_E:
                    BIT(DE.l, 6);
                    break;
                case CodesCB.BIT7_E:
                    BIT(DE.l, 7);
                    break;
                #endregion
                #region BITx_H
                case CodesCB.BIT0_H:
                    BIT(HL.h, 0);
                    break;
                case CodesCB.BIT1_H:
                    BIT(HL.h, 1);
                    break;
                case CodesCB.BIT2_H:
                    BIT(HL.h, 2);
                    break;
                case CodesCB.BIT3_H:
                    BIT(HL.h, 3);
                    break;
                case CodesCB.BIT4_H:
                    BIT(HL.h, 4);
                    break;
                case CodesCB.BIT5_H:
                    BIT(HL.h, 5);
                    break;
                case CodesCB.BIT6_H:
                    BIT(HL.h, 6);
                    break;
                case CodesCB.BIT7_H:
                    BIT(HL.h, 7);
                    break;
                #endregion
                #region BITx_L
                case CodesCB.BIT0_L:
                    BIT(HL.l, 0);
                    break;
                case CodesCB.BIT1_L:
                    BIT(HL.l, 1);
                    break;
                case CodesCB.BIT2_L:
                    BIT(HL.l, 2);
                    break;
                case CodesCB.BIT3_L:
                    BIT(HL.l, 3);
                    break;
                case CodesCB.BIT4_L:
                    BIT(HL.l, 4);
                    break;
                case CodesCB.BIT5_L:
                    BIT(HL.l, 5);
                    break;
                case CodesCB.BIT6_L:
                    BIT(HL.l, 6);
                    break;
                case CodesCB.BIT7_L:
                    BIT(HL.l, 7);
                    break;
                #endregion
                #region BITx_xHL
                case CodesCB.BIT0_xHL:
                    BIT(ReadRAM(HL.w), 0);
                    break;
                case CodesCB.BIT1_xHL:
                    BIT(ReadRAM(HL.w), 1);
                    break;
                case CodesCB.BIT2_xHL:
                    BIT(ReadRAM(HL.w), 2);
                    break;
                case CodesCB.BIT3_xHL:
                    BIT(ReadRAM(HL.w), 3);
                    break;
                case CodesCB.BIT4_xHL:
                    BIT(ReadRAM(HL.w), 4);
                    break;
                case CodesCB.BIT5_xHL:
                    BIT(ReadRAM(HL.w), 5);
                    break;
                case CodesCB.BIT6_xHL:
                    BIT(ReadRAM(HL.w), 6);
                    break;
                case CodesCB.BIT7_xHL:
                    BIT(ReadRAM(HL.w), 7);
                    break;
                    #endregion
                default:                    
                    throw new InvalidOpCodeException();                    
            }
        }
        void CP_X(byte inreg)
        {
            ResultReg.l = inreg;
            SetFlags((byte)((AF.h == ResultReg.l) ? 1 : 0),
                1,
                (byte)(((AF.h & 0x0F) < (ResultReg.l & 0x0F)) ? 1 : 0),
                (byte)((AF.h < ResultReg.l) ? 1 : 0));
        }
        byte SLA_X(byte inreg)
        {
            ResultReg.w = (ushort)((inreg << 1));
            SetFlags((byte)((ResultReg.l == 0) ? 1 : 0),
                0,
                0,
                (byte)((inreg & 0x80) > 0 ? 1 : 0));
            return ResultReg.l;
        }
        byte SRA_X(byte inreg)
        {
            ResultReg.w = (ushort)((inreg >> 1));
            SetFlags((byte)((ResultReg.l == 0) ? 1 : 0),
                0,
                0,
                (byte)((inreg & 0x80) > 0 ? 1 : 0));
            return ResultReg.l;
        }
        byte SRL_X(byte inreg)
        {
            ResultReg.w = (ushort)((inreg >> 1));
            SetFlags((byte)((ResultReg.l == 0) ? 1 : 0),
                0,
                0,
                (byte)((inreg & 0x01) > 0 ? 1 : 0));
            return ResultReg.l;
        }
        byte RL_X(byte inreg)
        {
            int carry = GetFlag(C_FLAG) ? 1 : 0;
            ResultReg.w = (ushort)((inreg << 1) | carry);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                     0,
                     0,
                     (byte)(((inreg & 0x80)>0) ? 1 : 0));
            return ResultReg.l;
        }
        byte RR_X(byte inreg)
        {
            int carry = GetFlag(C_FLAG) ? 0x80 : 0;
            ResultReg.w = (ushort)((inreg << 1) | carry);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                     0,
                     0,
                     (byte)(((inreg & 0x01) > 0) ? 1 : 0));
            return ResultReg.l;
        }
        byte RLC_X(byte inreg)
        {
            ResultReg.w = (ushort)((inreg << 1) | (inreg >> 7));
            SetFlags((byte)((ResultReg.l == 0) ? 1 : 0),
                0,
                0,
                (byte)((inreg & 0x80) > 0 ? 1 : 0));
            return ResultReg.l;
        }
        void SBC_X(byte src)
        {
            int carry = GetFlag(C_FLAG) ? 1 : 0;
            ResultReg.w = (ushort)(AF.h - src - carry);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                     1,
                     (byte)(((AF.h & 0x0F) < ((src - carry) & 0x0F)) ? 1 : 0),
                     (byte)((((short)ResultReg.w) < 0) ? 1 : 0));
            AF.h = ResultReg.l;
        }
        void SUB(byte src)
        {
            ResultReg.w = (ushort)(AF.h - src);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                      1,
                      (byte)(((AF.h & 0x0F) < (src & 0x0F)) ? 1 : 0),
                      (byte)((((short)ResultReg.w) < 0) ? 1 : 0));
            AF.h = ResultReg.l;
        }
        void ADC_X(byte src)
        {
            int carry = GetFlag(C_FLAG) ? 1 : 0;
            ResultReg.w = (ushort)(AF.h + src + carry);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                     0,
                     (byte)((((AF.h & 0x0F) + ((src & 0x0F)+ carry ))>0x0F) ? 1 : 0),
                     (byte)((((ushort)ResultReg.w) > 0xFF) ? 1 : 0));
            AF.h = ResultReg.l;
        }
        void ADD_X(byte src)
        {
            ResultReg.w = (ushort)(AF.h + src);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                     0,
                     (byte)((((AF.h & 0x0F) + (src & 0x0F)) > 0x0F) ? 1 : 0),
                     (byte)((ResultReg.w > 0xFF) ? 1 : 0));
            AF.h = ResultReg.l;
        }
        void INC_X(ref byte src)
        {
            ResultReg.w = (ushort)(src + 1);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                 0,
                 (byte)((((src & 0x0F) + 1) > 0x0F) ? 1 : 0));
            src++;
        }
        void DEC_X(ref byte src)
        {
            ResultReg.w = (ushort)(src - 1);
            SetFlags((byte)(((ResultReg.l) == 0) ? 1 : 0),
                 1,
                 (byte)((!((src & 0x0F) > 0)) ? 1 : 0));
            src--;
        }
        void BIT(byte inbyte, int bit)
        {
            AF.l = (byte)((((((inbyte & (1 << bit)) == 0) ? 1 : 0) << 7) & Z_FLAG) | ((0 << 6) & N_FLAG) | ((1 << 5) & H_FLAG) | ((AF.l) & C_FLAG));
        }
        void SWAP_X(ref byte src)
        {
            ResultReg.w = (ushort)((src >> 4) | (src << 4));
            SetFlags((byte)((ResultReg.l == 0) ? 1 : 0),
                0,
                0,
                0);
            src = ResultReg.l;
        }
        unsafe void RST(ushort adress)
        {
            byte* bytes;
            fixed (ushort* shrtpntr = &PC)
            {
                SP -= 2;
                bytes = (byte*)shrtpntr;
                WriteRAM(SP, bytes[0]);
                WriteRAM(SP + 1, bytes[1]);
                PC = adress;
            }
        }
        unsafe public void Interrupt(ushort adress)        
        {
            if (IME == 0)
                return;
            IME = 0;
            byte* bytes;
            fixed (ushort* shrtpntr = &PC)
            {
                SP -= 2;
                bytes = (byte*)shrtpntr;
                WriteRAM(SP, bytes[0]);
                WriteRAM(SP + 1, bytes[1]);                
                PC = adress;
            }
        }
        public void SetFlags(byte Z, byte N, byte H, byte C)
        {
            AF.l = (byte)(((Z << 7) & Z_FLAG) | ((N << 6) & N_FLAG) | ((H << 5) & H_FLAG) | ((C << 4) & C_FLAG));
        }
        public void SetFlags(byte Z, byte N, byte H)
        {
            //AF.l = (byte)(((Z << 7) & Z_FLAG) | ((N << 6) & N_FLAG) | ((H << 5) & H_FLAG) | (((AF.l & C_FLAG) << 4) & C_FLAG));
            AF.l = (byte)(((Z << 7) & Z_FLAG) | ((N << 6) & N_FLAG) | ((H << 5) & H_FLAG) | (AF.l & C_FLAG) );
        }
        public bool InterruptEnabled(byte which)
        {
            byte bits=ReadRAM(0xffff);
            if ((bits & which) == 1 && IME == 1)
                return true;
            else
                return false;
        }
        
        public bool GetFlag(byte which)
        {
            return ((AF.l & which) > 0);
        }
        public void ReadCart(string filename)
        {

            FileStream fs = File.Open(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(0, SeekOrigin.End);
            long filesize = fs.Position;
            fs.Seek(0, SeekOrigin.Begin);
            byte[] cartbytes = new byte[filesize];
            cartbytes = br.ReadBytes((int)filesize);
            br.Close();
            fs.Close();
            byte[] boot_rom=
		    {
			    0x31,0xFE,0xFF,0xAF,0x21,0xFF,0x9F,0x32,0xCB,0x7C,0x20,0xFB,0x21,0x26,0xFF,0x0E,
			    0x11,0x3E,0x80,0x32,0xE2,0x0C,0x3E,0xF3,0xE2,0x32,0x3E,0x77,0x77,0x3E,0xFC,0xE0,
			    0x47,0x11,0x04,0x01,0x21,0x10,0x80,0x1A,0xCD,0x95,0x00,0xCD,0x96,0x00,0x13,0x7B,
			    0xFE,0x34,0x20,0xF3,0x11,0xD8,0x00,0x06,0x08,0x1A,0x13,0x22,0x23,0x05,0x20,0xF9,
			    0x3E,0x19,0xEA,0x10,0x99,0x21,0x2F,0x99,0x0E,0x0C,0x3D,0x28,0x08,0x32,0x0D,0x20,
			    0xF9,0x2E,0x0F,0x18,0xF3,0x67,0x3E,0x64,0x57,0xE0,0x42,0x3E,0x91,0xE0,0x40,0x04,
			    0x1E,0x02,0x0E,0x0C,0xF0,0x44,0xFE,0x90,0x20,0xFA,0x0D,0x20,0xF7,0x1D,0x20,0xF2,
			    0x0E,0x13,0x24,0x7C,0x1E,0x83,0xFE,0x62,0x28,0x06,0x1E,0xC1,0xFE,0x64,0x20,0x06,
			    0x7B,0xE2,0x0C,0x3E,0x87,0xE2,0xF0,0x42,0x90,0xE0,0x42,0x15,0x20,0xD2,0x05,0x20,
			    0x4F,0x16,0x20,0x18,0xCB,0x4F,0x06,0x04,0xC5,0xCB,0x11,0x17,0xC1,0xCB,0x11,0x17,
			    0x05,0x20,0xF5,0x22,0x23,0x22,0x23,0xC9,0xCE,0xED,0x66,0x66,0xCC,0x0D,0x00,0x0B,
			    0x03,0x73,0x00,0x83,0x00,0x0C,0x00,0x0D,0x00,0x08,0x11,0x1F,0x88,0x89,0x00,0x0E,
			    0xDC,0xCC,0x6E,0xE6,0xDD,0xDD,0xD9,0x99,0xBB,0xBB,0x67,0x63,0x6E,0x0E,0xEC,0xCC,
			    0xDD,0xDC,0x99,0x9F,0xBB,0xB9,0x33,0x3E,0x3C,0x42,0xB9,0xA5,0xB9,0xA5,0x42,0x3C,
			    0x21,0x04,0x01,0x11,0xA8,0x00,0x1A,0x13,0xBE,0x20,0xFE,0x23,0x7D,0xFE,0x34,0x20,
			    0xF5,0x06,0x19,0x78,0x86,0x23,0x05,0x20,0xFB,0x86,0x20,0xFE,0x3E,0x01,0xE0,0x50
		    };
            

            //lets read the cart into RAM
           // for (int i = 0; i < boot_rom.Length; i++)
             //   WriteRAM(i, boot_rom[i]);
            for (int i = 0x00; i < cartbytes.Length; i++)
            {
                try
                {
                    WriteRAM(i, cartbytes[i]);
                }
                catch (Gameboy.InvalidWriteAccessException)
                {
                    //we know that we should not write to the ROM, but we do so anyways
                }
            }
            Disassemble();

        }
        public void ReadOpCodeStrings(string filename, Dictionary<byte, string> instructions)
        {
            FileStream fs = File.Open(filename, FileMode.Open);
            TextReader tr = new StreamReader(fs);
            fs.Seek(0, SeekOrigin.End);
            long filesize = fs.Position;
            fs.Seek(0, SeekOrigin.Begin);
            string opcodes = tr.ReadToEnd();
            int string_position = 0;
            int counter = 0;
            for (; string_position<opcodes.Length;)
            {
                string_position = opcodes.IndexOf('\"', string_position)+1;
                int next_snuff=opcodes.IndexOf('\"', string_position);
                string opcode = opcodes.Substring(string_position, next_snuff-string_position);
                instructions.Add((byte)counter, opcode);
                string_position = opcodes.IndexOf('\"', string_position) + 1;
                counter++;
            }
            tr.Close();
            fs.Close();
        }
        public void Disassemble()
        {
            byte[] buffer = new byte[0xffff];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i]=ReadRAM(i);

            for (int idx = 0; idx < buffer.Length; )
            {
                Lines.Add(DisassembleSingleOp(buffer, ref idx));
            }
        }
        public ArrayList GetDisassembly()
        {
            return Lines;
        }
        public Z80DisassembledLine DisassembleSingleOp(byte[] buffer, ref int idx)
        {
            Z80DisassembledLine line = new Z80DisassembledLine();
            line.OPCodeBytes = new byte[4];
            line.PC = idx;
            line.nr_bytes = 0;
            if (buffer[idx] == 0xCB)
            {
                line.OPCodeBytes[line.nr_bytes] = buffer[idx];
                line.nr_bytes++;
                idx++;
                line.OPCodeBytes[line.nr_bytes] = buffer[idx];
                line.nr_bytes++;
                line.OPCode = InstructionsCB[buffer[idx]];
                idx++;
                
            }
            else
            {
                line.OPCodeBytes[line.nr_bytes] = buffer[idx];
                line.nr_bytes++;
                line.OPCode = Instructions[buffer[idx]];
                idx++;
                
            }
            
            
            if (line.OPCode.IndexOf('*') != -1)
            {
                line.OPCode = line.OPCode.Replace("*", buffer[idx].ToString("X"));
                line.OPCodeBytes[line.nr_bytes] = buffer[idx];
                line.nr_bytes++;
                idx++;
//                line.Arguments = buffer[idx].ToString();
            }
            else if (line.OPCode.IndexOf('@') != -1)
            {
                int sign_bit = (buffer[idx] & 0x80);
                byte jump_byte=0;
                if (sign_bit != 0)
                {
                    jump_byte = (byte)(256 - (int)(buffer[idx] & 0x7f));
                    int destination_pc = idx + jump_byte + 1;
                    line.OPCode = line.OPCode.Replace("@", "-" + jump_byte.ToString("X4")) +" (" + destination_pc.ToString("X4") + "h)";
                }
                else
                {
                    jump_byte = (byte)(buffer[idx] & 0x7f);
                    int destination_pc = idx + jump_byte + 1;
                    line.OPCode = line.OPCode.Replace("@", "+" + jump_byte.ToString("X4")) + " (" + destination_pc.ToString("X4") + "h)";
                }
                
                line.OPCodeBytes[1] = buffer[idx];
                line.nr_bytes++;
                
                idx++;
            }
            else if (line.OPCode.IndexOf('#') != -1)
            {
                int jump_pos = buffer[idx] + 256 * buffer[idx + 1];
                line.OPCodeBytes[line.nr_bytes] = buffer[idx];
                line.OPCodeBytes[line.nr_bytes+1] = buffer[idx + 1];
                line.nr_bytes += 2;
                line.OPCode = line.OPCode.Replace("#", jump_pos.ToString("X"));
                idx += 2;
            }
            return line;
        }

    }
}
