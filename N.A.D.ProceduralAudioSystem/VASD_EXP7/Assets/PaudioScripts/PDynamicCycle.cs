using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDynamicCycle
{
    public List<int> dynamicLayersCurrent; // huidige layer waar mee bezig (list int meegegeven layers)
    public List<int> dynamicLayers; // welke layers uiteindelijk aan moeten zijn
    public int currentTick = 8;
    public int lengthInTicks = 8;

    public bool first = true;

    // TODO: add a specific order or influence the order based on game data

    public PDynamicCycle(List<int> dynamicLayersCurrent, List<int> dynamicLayers, int currentTick, int lengthInTicks, bool first = true)
    {
        this.dynamicLayersCurrent = dynamicLayersCurrent;
        this.dynamicLayers = dynamicLayers;
        this.currentTick = currentTick;
        this.lengthInTicks = lengthInTicks;
        this.first = first;
    }
}


