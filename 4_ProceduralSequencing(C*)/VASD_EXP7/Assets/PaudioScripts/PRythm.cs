using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRythm
{
    public static List<int> GenerateRythm(int amountOfTicks, int tickLength) // custom amount of ticks allows for polyrythm
    {
        var layerRythm = new List<int>();

        // amount of ticks is amount of notes to be played and tick
        for (int i = 0; i < amountOfTicks; i++)
        {
            layerRythm.Add(Random.Range(0, 2));

            // add 0s to set ticklength
            for (int tick = 1; tick < tickLength; tick++) layerRythm.Add(0);
        }

        return layerRythm;
    }
}
