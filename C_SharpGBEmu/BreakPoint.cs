namespace C_SharpGBEmu
{
    public class BreakPoint
    {
        public BreakPoint(int aadress) { adress = aadress; enabled = true; }
        public int adress;
        public bool enabled;
    }
}