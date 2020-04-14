using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRythm
{
    public static List<int> GenerateRythm(int amountOfTicks) // custom amount of ticks allows for polyrythm
    {
        var layerRythm = new List<int>();
        for (int i = 0; i < amountOfTicks; i++)
        {
            layerRythm.Add(Random.Range(0, 2));
        }

        return layerRythm;
    }
}
