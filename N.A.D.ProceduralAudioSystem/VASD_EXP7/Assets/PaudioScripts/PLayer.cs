using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

// base class for loop and oneshot layers
public class PLayer
{
    public int soundOptionsAmount;     // amount of options per layer to choose from

    public List<Sound> sounds = new List<Sound>();

    public PLayer(int soundOptionsAmount)
    {
        this.soundOptionsAmount = soundOptionsAmount;
    }
}
