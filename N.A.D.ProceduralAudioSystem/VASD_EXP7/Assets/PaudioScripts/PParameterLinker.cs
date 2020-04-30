using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PParameterLinker : MonoBehaviour
{
    // This is where to put the receivers and change listeners of game paramters
    // and set what cycles they need to adapt
    // or test with controler input.

    public static void Start()
    {
        // create cycles
        List<int> layers = new List<int>();
        for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++) layers.Add(layer);

        PAudioDataSystem.cycles.Add(new PCycle(new List<PAudioDataSystem.AdaptableParameter> { PAudioDataSystem.AdaptableParameter.rythmAndMelody}, layers, true));

        PAudioDataSystem.GenerateMacroAudioData();
        PAudioDataSystem.GenerateCycleAudioData(PAudioDataSystem.cycles[0]);
        PAudioDataSystem.AudioDataTerminalOutput();

        PAudioDataSystem.timedCycles.Add(new PTimedCycle(new List<PAudioDataSystem.AdaptableParameter> { PAudioDataSystem.AdaptableParameter.rythmAndMelody}, new List<int> {1}, false, 0, 8)); // so here every 8 ticks the countermelody changes rythm and melody

        PAudioDataSystem.dynamicCycles.Add(new PDynamicCycle(new List<int>(), layers, 7, 8));

        // link cycles to parameters here ..
    }

    public static void Update()
    {
        ControlerInput();
    }

    static void NewLinkedParameter(PCycle cycle, float input)
    {
        // if input changed -> call cycle
    }

    static void ControlerInput()
    {
        // generate new data
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PAudioDataSystem.GenerateMacroAudioData();
            PAudioDataSystem.GenerateCycleAudioData(PAudioDataSystem.cycles[0]);
            PAudioDataSystem.AudioDataTerminalOutput();
        }

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
            POneshots.playOneShot(0, Random.Range(0, ProceduralAudio.entryListOS[0].Count));
        }
    }
}
