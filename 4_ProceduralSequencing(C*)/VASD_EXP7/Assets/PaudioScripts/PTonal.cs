using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTonal
{
    public static List<int> melody = new List<int>();
    public static List<int> chordsRythm = new List<int>();

    // TODO: chords moeten een eigen ritme losstaand vanuit procedural audio class

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

    public static List<List<int>> GenerateChords(int amountOfChords = 4, int chordLayers = 2)
    {
        // TODO: react to melody
        // TODO: only take third steps
        // TODO: creates index errors

        var chords = new List<List<int>>();

        int currentChordBase = Random.Range(0, 7);

        for (int chord = 0; chord < amountOfChords; chord++) // for every chord
        {
            chords.Add(new List<int>());

            for (int note = 0; note < chordLayers; note++) // for every chord note
            {
                chords[chord].Add(ProceduralAudio.currentScale[(currentChordBase + (note * 2)) % 7]); // times 2 to take third steps
            }

            currentChordBase = Mathf.Abs((currentChordBase + Random.Range(-2, 2)) % 7);
        }

        return chords; // chord, notes (1, 3, 5)
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
                layerMelody.Add(ProceduralAudio.currentScale[Random.Range(0, frequencyRange)]);
            }

            else
            {
                // TODO: add nlull instead of -1 to represent empty value (int?)
                layerMelody.Add(-1);
            }
        }

        return layerMelody;
    }

    public static List<int> GenerateMelodyOnChord(List<int> rythm, int frequencyRange) // custom amount of ticks allows for polyrythm
    {
        if (frequencyRange > 7) frequencyRange = 7; // max one octave per layer

        var layerMelody = new List<int>();

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            // if a note needs to be played
            if (rythm[tick] == 1)
            {
                layerMelody.Add(Random.Range(0, frequencyRange));
            }

            else
            {
                // TODO: add nlull instead of -1 to represent empty value (int?)
                layerMelody.Add(-1);
            }
        }

        return layerMelody;
    }

    // take one melody layer and create a layer to acompany it
    public static List<int> GenerateCounterMelody(List<int> melodyRythm, List<int> melody)
    {
        // TODO: recognise empty notes from input melody instead of treating them as a zero
        // TODO: find/design a good algorythm for counter melodys

        var output = new List<int>();

        for (int tick = 0; tick < melodyRythm.Count; tick++)
        {
            if (melodyRythm[tick] == 1)
            {
                // add a note 2 steps below input melody
                output.Add(melody[tick] - 2 % 7);
            }
        }

        return output;
    }


    public static List<int> LinkChordLine(List<int> rythm, int chordLayer)
    {
        // TODO: so in the future rythm should be received from the global chord rythm value, to allow for more chord layers to be played by different looplayers

        var output = new List<int>();

        int currentChord = 0;

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            if (rythm[tick] == 1)
            {
                output.Add(ProceduralAudio.chords[currentChord][chordLayer]);

                // move to next chord, loop through amount of chords
                currentChord += 1 % ProceduralAudio.chordAmount;
            }
            else
            {
                // TODO: add nlull instead of -1 to represent empty value (int?)
                output.Add(-1);
            }
        }

        return output;
    }

    public static List<int> GeneratePercussionPart(List<int> rythm, int frequencyRange) // custom amount of ticks allows for polyrythm
    {
        if (frequencyRange > 7) frequencyRange = 7; // max one octave per layer

        var layerMelody = new List<int>();

        for (int tick = 0; tick < rythm.Count; tick++)
        {
            // if a note needs to be played
            if (rythm[tick] == 1)
            {
                layerMelody.Add(ProceduralAudio.currentScale[Random.Range(0, frequencyRange)]);
            }

            else
            {
                // TODO: add nlull instead of -1 to represent empty value (int?)
                layerMelody.Add(-1);
            }
        }

        return layerMelody;
    }

    public static List<int> GenerateTonalIntervals(List<int> rythm, int frequencyRange, int layerType)
    {
        var output = new List<int>();

        for (int type = 0; type < ProceduralAudio.amountOfLayers + 1; type++)
        {
            if (type == layerType)
            {
                switch (layerType)
                {
                    case 0: // melody
                        output = GenerateMelody(rythm, frequencyRange);

                        // generate chords based on generated melody
                        ProceduralAudio.chords = GenerateChords();

                        // set melody layer for other layers to react to
                        melody = output;

                        break;
                    case 1: // countermelody
                        output = GenerateMelody(rythm, frequencyRange);

                        break;
                    case 2: // percussion
                        for (int tick = 0; tick < rythm.Count; tick++)
                        {
                            if (rythm[tick] == 1) output.Add(0);
                            else output.Add(-1);
                        }
                        break;
                    default: // chords
                        output = LinkChordLine(rythm, layerType - 3);

                        break;
                }

                break;
            }
        }

        return output;
    }
}
