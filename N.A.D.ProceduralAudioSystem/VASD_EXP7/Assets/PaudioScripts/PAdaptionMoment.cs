using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAdaptionMoment
{
    public bool macro;
    public List<PCycle> cycle;
    public bool terminalOutput;
    public List<PDynamicCycle> dynamicCycles;

    public PAdaptionMoment(bool macro, List<PCycle> cycle, bool terminalOutput, List<PDynamicCycle> dynamicCycles)
    {
        this.macro = macro;
        this.cycle = cycle;
        this.terminalOutput = terminalOutput;
        this.dynamicCycles = dynamicCycles;
    }
}