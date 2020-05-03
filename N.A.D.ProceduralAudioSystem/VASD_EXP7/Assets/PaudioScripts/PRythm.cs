using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRythm
{
    public static List<int> GenerateRythm(PLoopLayer layer) // custom amount of ticks allows for polyrythm
    {
        var layerRythm = new List<int>();

        // amount of ticks is amount of notes to be played and tick
        for (int i = 0; i < layer.beatsPerMeasure; i++)
        {
            // use probability to decide if hit needs to be added
            layerRythm.Add((Random.Range(0, 10) + 1 <= layer.noteDensity) ? 1 : 0);

            // add 0s (pause) to set ticklength
            for (int tick = 1; tick < layer.beatLength; tick++) layerRythm.Add(0);
        }

        return layerRythm;
    }
}
