using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHarmony
{
    // Receive a (amount of) melody layer(s) and add a layer on top or bottom etc
    // account for not every melody not being played with rythm
    // maybe do this per note instead of line. kan beide uitproberen
    // acount for polyrythm

    //private static List<int> currentScale = new List<int> { 0, 2, 4, 5, 7, 9, 11 }; // TODO: different scaler class

    public static List<int> GenerateHarmony(List<int> melody)
    {
        var layerMelody = new List<int>();

        return layerMelody;
    }


    // take one melody layer and create a layer to acompany it
    public static List<int> GenerateOppositeMelodyLine(List<int> rythm, List<int> melody)
    {
        var layerMelody = new List<int>();

        // TODO: recognise empty notes from input melody instead of treating them as a zero

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            if (rythm[tick] == 1)
            {
                // add a note 2 steps below input melody
                layerMelody.Add(melody[tick] - 2 % 7);
            }
        }

        return layerMelody;
    }

    public static List<int> setScale(int majMin)
    {
        // TODO: other modes support
        // TODO: INREADME: right now only an octave per sound can be added, which makes it hard for scales when the base note can't be in the bass

        List<int> intervals = new List<int>();

        if (majMin == 1)
        {
            intervals = new List<int> { 0, 2, 4, 5, 7, 9, 11 };
        }
        else
        {
            intervals = new List<int> { 0, 2, 3, 5, 7, 8, 10 };
        }

        return intervals;
    }

    public static List<List<int>> GenerateChords(int amountOfChords = 4, int tickLength = 4, int chordLayers = 3)
    {
        // TODO: add rythm (ticks)
        // TODO: react to melody or have melody react to chords

        var chords = new List<List<int>>();

        int currentChordBase = Random.Range(0, 7);

        for (int chord = 0; chord < amountOfChords; chord++) // for every chord
        {
            chords.Add(new List<int>());

            for (int note = 0; note < chordLayers; note++) // for every chord note
            {
                chords[chord].Add(ProceduralAudio.currentScale[(currentChordBase + (note * 2)) % 7]); // times 2 to take third steps
            }

            currentChordBase += Random.Range(-2, 2) % 7; // calculate new chord
        }

        return chords; // chord, notes (1, 3, 5)
    }

    // TODO: funtion for thirds modulation or other types of modulation
}
