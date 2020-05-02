using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PAudioDataSystem : MonoBehaviour
{
    public static List<List<int>> chords = new List<List<int>>(); // chord, chordnotes
    public static List<int> currentScale = new List<int>();
    public static List<int> previousScale = new List<int>();

    public static List<PCycle> cycles = new List<PCycle>();
    public static List<PTimedCycle> timedCycles = new List<PTimedCycle>();
    public static List<PDynamicCycle> dynamicCycles = new List<PDynamicCycle>();

    public enum AdaptableParameter { rythmAndMelody, melody, beatsPerMeasure, beatLength, noteDensity, onOff };

    public static void GenerateCycleAudioData(PCycle cycle)
    {
        if (cycle.changeChordsAndScale) GenerateMacroAudioData();

        foreach (AdaptableParameter parameter in cycle.parameters)
        {
            foreach (int layer in cycle.layers)
            {
                switch (parameter)
                {
                    case AdaptableParameter.rythmAndMelody:
                        ProceduralAudio.print("new rythm and melody for layer: " + layer.ToString());

                        ProceduralAudio.layers[layer].rythm = PRythm.GenerateRythm(ProceduralAudio.layers[layer].beatsPerMeasure, ProceduralAudio.layers[layer].beatLength, ProceduralAudio.layers[layer].noteDensity);
                        ProceduralAudio.layers[layer].melody = PTonal.GenerateTonalIntervals(ProceduralAudio.layers[layer].rythm, ProceduralAudio.layers[layer].soundOptionsAmount, ProceduralAudio.layers[layer].layerType);

                        break;
                    case AdaptableParameter.melody:
                        ProceduralAudio.layers[layer].melody = PTonal.GenerateTonalIntervals(ProceduralAudio.layers[layer].rythm, ProceduralAudio.layers[layer].soundOptionsAmount, ProceduralAudio.layers[layer].layerType);

                        break;
                    case AdaptableParameter.beatsPerMeasure:
                        // TODO

                        break;
                    case AdaptableParameter.beatLength:
                        // TODO

                        break;
                    case AdaptableParameter.noteDensity:
                        // TODO

                        break;
                    case AdaptableParameter.onOff:
                        ProceduralAudio.layers[layer].layerOn = !ProceduralAudio.layers[layer].layerOn;

                        break;
                    default:
                        print("param fell through");

                        break;
                }

                ProceduralAudio.layers[layer].currentTick = 0;
            }
        }

        //AudioDataTerminalOutput();
    }

    public static void GenerateDynamicCycleData(PDynamicCycle dynamicCycle)
    {
        // if first time running set all layers that are to be turned on off if they arent already
        if (dynamicCycle.first)
        {
            foreach (int layer in dynamicCycle.dynamicLayers)
            {
                ProceduralAudio.layers[layer].layerOn = false;
                print(layer.ToString() + " turned off");
            }

            dynamicCycle.first = false;
        }
        // check the different variables
        var differences = dynamicCycle.dynamicLayers.Except(dynamicCycle.dynamicLayersCurrent).ToList();

        // check if still layers to turn on
        if (differences.Count() != 0)
        {
            // select a layer to turn on randomly
            var layer = differences[Random.Range(0, differences.Count())];

            print(layer.ToString() + " turned on");

            // turn layer on
            ProceduralAudio.layers[layer].layerOn = true;

            // add layer to list with layers that have been turned on
            dynamicCycle.dynamicLayersCurrent.Add(layer);
        }
        else
        {
            // delete cycle when its been completed
            dynamicCycles.Remove(dynamicCycle);
            print(dynamicCycles.Count());
        }
    }

    public static void GenerateMacroAudioData()
    {
        previousScale = currentScale;

        // set scale (maj/min)
        currentScale = PTonal.setScale(Random.Range(0, 2));

        // generate chords
        chords = PTonal.GenerateChords();
    }

    public static void AudioDataTerminalOutput()
    {
        for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++)
        {
            print("layer " + layer.ToString());

            string rythmOutput = "";
            string melodyOutput = "";

            for (int i = 0; i < ProceduralAudio.layers[layer].rythm.Count; i++)
            {
                rythmOutput += ProceduralAudio.layers[layer].rythm[i].ToString();
                melodyOutput += ProceduralAudio.layers[layer].melody[i].ToString();
            }

            ProceduralAudio.print(rythmOutput);
            ProceduralAudio.print(melodyOutput);
        }

        for (int chord = 0; chord < chords.Count; chord++) print("chord: " + chords[chord][0]);
    }
}
