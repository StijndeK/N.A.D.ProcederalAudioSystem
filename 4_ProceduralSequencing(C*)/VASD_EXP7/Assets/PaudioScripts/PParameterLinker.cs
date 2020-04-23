using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PParameterLinker : MonoBehaviour
{
    List<int> gameParameters = new List<int>();

    public static void Update()
    {
        ControlerInput();
    }

    public static void NewCycle()
    {
        // Change data before generating new cycle here:

        // generate new cycle
        PAudioDataSystem.GenerateAudioData();
    }

    public static void SetNewRythmData(int layer, int beatsPerMeasure, int beatLength, int noteDensity)
    {
        // TODO: probability algorythm

        ProceduralAudio.layers[layer].beatsPerMeasure = beatsPerMeasure;
        ProceduralAudio.layers[layer].beatLength = beatLength;
        ProceduralAudio.layers[layer].noteDensity = noteDensity;
    }

    static void ControlerInput()
    {
        // generate new data
        if (Input.GetKeyDown(KeyCode.Space))
            NewCycle();

        var layers = ProceduralAudio.layers;

        // toggle layers
        if (Input.GetKeyDown(KeyCode.Alpha1))
            layers[0].layerOn = !layers[0].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            layers[1].layerOn = !layers[1].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            layers[2].layerOn = !layers[2].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            layers[3].layerOn = !layers[3].layerOn;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            layers[4].layerOn = !layers[4].layerOn;

        // call oneshots
        if (Input.GetKeyDown(KeyCode.A))
        {
            // TODO: oneshots now load over a normal audio layer
            POneshots.playOneShot(0, Random.Range(0, ProceduralAudio.entryListOS[0].Count));
        }
    }
}
