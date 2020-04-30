using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// timed cycle automatically cycles based on time instead of external input
public class PTimedCycle : PCycle
{
    public int currentTick = 0;
    public int lengthInTicks = 8;


    public PTimedCycle(List<PAudioDataSystem.AdaptableParameter> parameters, List<int> layers, bool changeChordsAndScale, int currentTick, int lengthInTicks) : base(parameters, layers, changeChordsAndScale)
    {
        this.currentTick = currentTick;
        this.lengthInTicks = lengthInTicks;
    }
}
