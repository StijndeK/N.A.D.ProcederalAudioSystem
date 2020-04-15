using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMelody
{
    private static List<int> currentScale = new List<int> { 0, 2, 4, 5, 7, 9, 11};

    public static List<int> GenerateMelody(List<int> rythm, int frequencyRange) // custom amount of ticks allows for polyrythm
    {
        if (frequencyRange > 7) frequencyRange = 7; // max one octave per layer

        var layerMelody = new List<int>();

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            // if a note needs to be played
            if (rythm[tick] == 1)
            {
                //layerMelody.Add(Random.Range(0, frequencyRange));
                layerMelody.Add(currentScale[Random.Range(0, frequencyRange)]);
            }

            else
            {
                layerMelody.Add(0);
            }
        }

        return layerMelody;
    }
}
