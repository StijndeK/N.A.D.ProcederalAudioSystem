using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class POSLayer
{
    public int soundOptionsAmount;     // amount of options per layer to choose from

    public List<Sound> sounds = new List<Sound>();

    public POSLayer(int soundOptionsAmount)
    {
        this.soundOptionsAmount = soundOptionsAmount;
    }
}
