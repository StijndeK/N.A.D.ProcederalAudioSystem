using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAudioDataSystem : MonoBehaviour
{
    public static List<List<int>> chords = new List<List<int>>(); // chord, chordnotes
    public static List<int> currentScale = new List<int>();


    public static void GenerateAudioData()
    {
        // TODO: check what needs to be changed and if so if values need to be passed

        // set scale (maj/min)
        currentScale = PTonal.setScale(Random.Range(0, 2));

        // generate chords
        chords = PTonal.GenerateChords();

        for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++)
        {
            ProceduralAudio.layers[layer].currentTick = 0;
            ProceduralAudio.layers[layer].rythm = PRythm.GenerateRythm(ProceduralAudio.layers[layer].beatsPerMeasure, ProceduralAudio.layers[layer].beatLength, ProceduralAudio.layers[layer].noteDensity); ;
            ProceduralAudio.layers[layer].melody = PTonal.GenerateTonalIntervals(ProceduralAudio.layers[layer].rythm, ProceduralAudio.layers[layer].soundOptionsAmount, ProceduralAudio.layers[layer].layerType);
        }

        AudioDataInfoOutput();
    }

    static void AudioDataInfoOutput()
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
