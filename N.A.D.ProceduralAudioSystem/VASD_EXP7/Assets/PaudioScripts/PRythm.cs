using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRythm
{
    public static List<int> GenerateRythm(int amountOfTicks, int tickLength, int noteDensity) // custom amount of ticks allows for polyrythm
    {
        var layerRythm = new List<int>();

        // amount of ticks is amount of notes to be played and tick
        for (int i = 0; i < amountOfTicks; i++)
        {
            // use probability to decide if hit needs to be added
            layerRythm.Add((Random.Range(0, 10) + 1 <= noteDensity) ? 1 : 0);

            // add 0s (pause) to set ticklength
            for (int tick = 1; tick < tickLength; tick++) layerRythm.Add(0);
        }

        return layerRythm;
    }
}
