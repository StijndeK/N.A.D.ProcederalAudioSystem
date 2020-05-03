using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTonal
{
    // TODO: add null instead of -1 to represent empty value (int?)

    public static List<int> chordsRythm = new List<int>();

    public static List<int> setScale(int majMin)
    {
        // TODO: support other modes 

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

    public static List<List<int>> GenerateChords(int amountOfChords = 4, int chordLayers = 2, int layerSteps = 5)
    {
        var chords = new List<List<int>>();

        int currentChordBase = Random.Range(0, 7);

        for (int chord = 0; chord < amountOfChords; chord++) // for every chord
        {
            chords.Add(new List<int>());

            for (int note = 0; note < chordLayers; note++) // for every chord note
            {
                chords[chord].Add(PAudioDataSystem.currentScale[(currentChordBase + (note * layerSteps)) % 7]); // times layersteps to decide interval
            }

            currentChordBase = Mathf.Abs((currentChordBase + Random.Range(-2, 2)) % 7);
        }

        return chords; // chord, notes (1, 3, 5)
    }

    public static List<int> GenerateTonalIntervals(PLoopLayer layer)
    {
        var output = new List<int>();

        for (int type = 0; type < ProceduralAudio.amountOfLayers + 1; type++)
        {
            if (type == (int)layer.layerType)
            {
                switch ((int)layer.layerType)
                {
                    case 0: // melody
                        output = GenerateChordBasedMelody(layer.rythm, new List<int> { 0, 2, 4 });

                        break;
                    case 1: // countermelody
                        output = GenerateMelody(layer.rythm, layer.soundOptionsAmount);

                        break;
                    case 2: // percussion
                        output = GenerateMelody(layer.rythm, layer.soundOptionsAmount);

                        break;
                    case 3: // soundscape
                        output = GenerateMelody(layer.rythm, layer.soundOptionsAmount);

                        break;
                    default: // chords
                        output = LinkChordLine(layer.rythm, (int)layer.layerType - 4); // subtract 4 for other vars in enum

                        break;
                }

                break;
            }
        }

        return output;
    }

    public static List<int> GenerateMelody(List<int> rythm, int frequencyRange) // custom amount of ticks allows for polyrythm
    {
        if (frequencyRange > 7) frequencyRange = 7; // max one octave per layer

        var layerMelody = new List<int>();

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            // if a note needs to be played
            if (rythm[tick] == 1)
            {
                layerMelody.Add(PAudioDataSystem.currentScale[Random.Range(0, frequencyRange)]);
            }

            else
            {
                layerMelody.Add(-1);
            }
        }

        return layerMelody;
    }

    // returns a list of intervals that have to be put on top of the current chord in the sequencer
    public static List<int> GenerateChordBasedMelody(List<int> rythm, List<int> chordNoteOptions)
    {
        var layerMelody = new List<int>();

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            // if a note needs to be played
            if (rythm[tick] == 1)
            {
                //layerMelody.Add(PAudioDataSystem.currentScale[Random.Range(0, frequencyRange)]);
                layerMelody.Add(chordNoteOptions[Random.Range(0, chordNoteOptions.Count)]);
            }

            else
            {
                layerMelody.Add(-1);
            }
        }

        return layerMelody;
    }

    // take one melody layer and create a layer to acompany it
    public static List<int> GenerateCounterMelody(List<int> melodyRythm, List<int> melody)
    {
        var output = new List<int>();

        return output;
    }

    public static List<int> LinkChordLine(List<int> rythm, int chordLayer)
    {
        var output = new List<int>();

        int currentChord = 0;

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            if (rythm[tick] == 1)
            {
                output.Add(PAudioDataSystem.chords[currentChord][chordLayer]);
            }
            else
            {
                output.Add(-1);
            }

            // TODO: dynamic check values for other rythms
            // move to next chord whether note needs to play or not to sync with other chord lines
            if (tick % 4 == 3)
            {
                // move to next chord, loop through amount of chords
                currentChord += 1 % ProceduralAudio.chordAmount;
            }
        }

        return output;
    }

    public static List<int> GeneratePercussionPart(List<int> rythm, int frequencyRange) // custom amount of ticks allows for polyrythm
    {
        var layerMelody = new List<int>();

        return layerMelody;
    }
}
