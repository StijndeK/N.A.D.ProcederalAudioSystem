using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// base cycle data
public class PBaseCycle
{
    public bool terminalOutput;

    public PBaseCycle(bool terminalOutput = false)
    {
        this.terminalOutput = terminalOutput;
    }
}

// time cycles
public class PCycleTimer
{
    public PCycle cycle;
    public PDynamicCycle dynamicCycle;
    public int currentTick;
    public int lengthInTicks;

    public PCycleTimer(PCycle cycle = null, PDynamicCycle dynamicCycle = null, int currentTick = 7, int lengthInTicks = 8)
    {
        this.cycle = cycle;
        this.dynamicCycle = dynamicCycle;
        this.currentTick = currentTick;
        this.lengthInTicks = lengthInTicks;
    }
}

// normal cycle, has adaptable parameters
public class PCycle : PBaseCycle
{
    public List<PParameter> parametersToAdapt;

    public PCycle(List<PParameter> parametersToAdapt, bool terminalOutput = false) : base(terminalOutput)
    {
        this.parametersToAdapt = parametersToAdapt;
        this.terminalOutput = terminalOutput;
    }
}

// dyanmic cycle, only adapts on/off off layers, so doesnt have adaptable parameters
// TODO: allow for more then 1 dynamic cycle
public class PDynamicCycle : PBaseCycle
{
    public List<int> dynamicLayersCurrent; // huidige layer waar mee bezig (list int meegegeven layers)
    public List<int> dynamicLayersToAdapt; // welke layers uiteindelijk aan moeten zijn

    public bool first = true;

    public PDynamicCycle(List<int> dynamicLayersToAdapt, bool terminalOutput = false, bool first = true) : base(terminalOutput)
    {
        this.dynamicLayersToAdapt = dynamicLayersToAdapt;
        this.first = first;
        this.dynamicLayersCurrent = new List<int>();
    }
}


