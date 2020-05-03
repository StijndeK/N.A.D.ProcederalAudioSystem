using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PAudioDataSystem : MonoBehaviour
{
    public static List<List<int>> chords = new List<List<int>>(); // chord, chordnotes
    public static List<int> currentScale = new List<int>();
    public static List<int> previousScale = new List<int>();


    public enum AdaptableParameter { rythmAndMelody, melody, beatsPerMeasure, beatLength, noteDensity, onOff };

    public static void CallCycle(PCycle cycle)
    {
        foreach (PParameter parameter in cycle.parametersToAdapt)
        {
            switch (parameter.dataToAdapt)
            {
                case PParameterLinker.AdaptableParametersCycle.chordsAndScale:
                    previousScale = currentScale;

                    // set scale (maj/min)
                    currentScale = PTonal.setScale(Random.Range(0, 2));

                    // generate chords
                    chords = PTonal.GenerateChords();
                    break;

                case PParameterLinker.AdaptableParametersCycle.layerdata:
                    foreach (int layer in parameter.layersToAdapt)
                    {
                        // TODO: dynamically set which variables should adapt instead of adapting all
                        // TODO: use game data to have more influence on how they adapt instead of just choosing between 2 options
                        ProceduralAudio.layers[layer].beatsPerMeasure = (parameter.value > 0.5f) ? 8 : 6;
                        ProceduralAudio.layers[layer].beatLength = (parameter.value > 0.5f) ? 1 : 2;
                        ProceduralAudio.layers[layer].noteDensity = (parameter.value > 0.5f) ? 8 : 4;
                    }
                    break;

                case PParameterLinker.AdaptableParametersCycle.rythmAndMelody:
                    // with a new rythm a new melody has to be created because the melody is created on top of a rythm
                    foreach (int layer in parameter.layersToAdapt)
                    {
                        ProceduralAudio.layers[layer].rythm = PRythm.GenerateRythm(ProceduralAudio.layers[layer]);
                        ProceduralAudio.layers[layer].melody = PTonal.GenerateTonalIntervals(ProceduralAudio.layers[layer]);
                        ProceduralAudio.layers[layer].currentTick = 0;
                    }
                    break;

                case PParameterLinker.AdaptableParametersCycle.melody:
                    foreach (int layer in parameter.layersToAdapt)
                    {
                        ProceduralAudio.layers[layer].melody = PTonal.GenerateTonalIntervals(ProceduralAudio.layers[layer]);
                    }
                    break;

                case PParameterLinker.AdaptableParametersCycle.bpm:
                    float range = 0.2f;
                    float bpm = (float)(PClock.currentBPM * ((parameter.value * range) + (1.0f - (range / 2))));
                    PClock.SetNewTime(bpm);
                    break;

                case PParameterLinker.AdaptableParametersCycle.onOff:
                    foreach (int layer in parameter.layersToAdapt)
                    {
                        ProceduralAudio.layers[layer].layerOn = !ProceduralAudio.layers[layer].layerOn;
                    }
                    break;
                case PParameterLinker.AdaptableParametersCycle.dynamicCycleLayerAmount:
                    // set what layers to adapt
                    var changingDynamicLayers = new List<int>();
                    float probability = 0.75f; // percentage minimum

                    //for (float amount )
                    for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++)
                    {
                        if (Random.Range(probability, 1.0f) > parameter.value)
                        {
                            changingDynamicLayers.Add(layer);
                        }
                    }

                    // change dynamic cycle
                    PParameterLinker.dynamicCycle = new PDynamicCycle(changingDynamicLayers, true); // TODO: for now only 1 dynamic cycle is used
                    for (int i = 0; i < PParameterLinker.cycleTimers.Count; i++)
                    {
                        //PCycleTimer cycleTimer = ;
                        if (PParameterLinker.cycleTimers[i].dynamicCycle != null)
                        {
                            PParameterLinker.cycleTimers[i] = new PCycleTimer(null, PParameterLinker.dynamicCycle);
                        }
                    }
                    break;
            }
        }

        if (cycle.terminalOutput)
            AudioDataTerminalOutput();
    }

    public static void callDynamicCycle(PDynamicCycle dynamicCycle)
    {
        // if first time running set all layers that are to be turned on, off if they arent already
        if (dynamicCycle.first)
        {
            for (int layer = 0; layer < ProceduralAudio.layers.Count; layer++)
            {
                bool isInList = dynamicCycle.dynamicLayersToAdapt.IndexOf(layer) != -1;
                if (isInList)
                {
                    ProceduralAudio.layers[layer].layerOn = false;
                    print(layer.ToString() + " turned off");
                }
                else
                {
                    ProceduralAudio.layers[layer].layerOn = true;
                    print(layer.ToString() + " turned on");
                }
            }

            dynamicCycle.first = false;
        }


        // check how many layers still need to turned on
        var differences = dynamicCycle.dynamicLayersToAdapt.Except(dynamicCycle.dynamicLayersCurrent).ToList();

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

        if (dynamicCycle.terminalOutput)
            print("dynamic cycle called");
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

            print(rythmOutput);
            print(melodyOutput);
        }

        for (int chord = 0; chord < chords.Count; chord++) print("chord: " + chords[chord][0]);
    }
}
