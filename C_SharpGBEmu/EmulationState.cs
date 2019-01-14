using System.Collections.Generic;

namespace C_SharpGBEmu
{
    public class EmulationState
    {
        public bool IsPaused { get; set; } = false;
        public long RunToCursorBreakpoint { get; set; } = -1;
        public List<BreakPoint> Breakpoints { get; set; } = new List<BreakPoint>();
    }
}