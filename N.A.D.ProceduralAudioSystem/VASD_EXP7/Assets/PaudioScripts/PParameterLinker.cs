using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PParameterLinker : MonoBehaviour
{
    static List<int> changingLayers = new List<int>();
    static List<int> changingDynamicLayers = new List<int>();

    static List<PParameter> parameters = new List<PParameter>();
    static List<PAdaptionMoment> adaptionMoments = new List<PAdaptionMoment>();

    public enum AdaptableData { bpm, layerdata, dynamicCycleLayers };

    public static List<PCycle> currentCycles;
    public static List<PDynamicCycle> currentDynamicCycles;

    public static void Start()
    {
        // create and set cycles
        for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++) changingLayers.Add(layer);
        changingDynamicLayers = changingLayers;

        PAudioDataSystem.cycles.Add(new PCycle(new List<PAudioDataSystem.AdaptableParameter> { PAudioDataSystem.AdaptableParameter.rythmAndMelody}, changingLayers, true));

        PAudioDataSystem.GenerateMacroAudioData();
        PAudioDataSystem.GenerateCycleAudioData(PAudioDataSystem.cycles[0]);
        PAudioDataSystem.AudioDataTerminalOutput();

        PAudioDataSystem.timedCycles.Add(new PTimedCycle(new List<PAudioDataSystem.AdaptableParameter> { PAudioDataSystem.AdaptableParameter.rythmAndMelody }, new List<int> { 1 }, false, 0, 8)); // so here every 8 ticks the countermelody changes rythm and melody

        PAudioDataSystem.dynamicCycles.Add(new PDynamicCycle(new List<int>(), changingDynamicLayers, 7, 8));

        // create parameters
        parameters.Add(new PParameter(AdaptableData.bpm, Random.Range(0.0f, 1.0f)));
        parameters.Add(new PParameter(AdaptableData.layerdata, Random.Range(0.0f, 1.0f)));
        parameters.Add(new PParameter(AdaptableData.dynamicCycleLayers, Random.Range(0.0f, 1.0f)));

        // create adaption moments
        adaptionMoments.Add(new PAdaptionMoment(true, PAudioDataSystem.cycles, true, PAudioDataSystem.dynamicCycles));
    }

    public static void Update()
    {
        ControlerInput();
    }

    // function to test parameter linking, when no game is active to receive data from
    static void MockGameData(PParameter parameter)
    {
        parameter.value = Random.Range(0.0f, 1.0f);
    }

    static void CallAllParameters(List<PCycle> cycles, List<PDynamicCycle> dynamicCycles)
    {
        currentCycles = cycles;
        currentDynamicCycles = dynamicCycles;

        foreach (PParameter currentParam in parameters)
        {
            // get game data
            MockGameData(currentParam);

            // adapt audio data to game data
            CallParameter(currentParam);
        }
    }

    static void CallParameter(PParameter parameter)
    {
        switch (parameter.dataToAdapt)
        {
            case (AdaptableData.bpm):
                // set bpm and pas it through
                PClock.SetNewTime(SetNewBPM(0.2f, parameter.value));

                break;

            case (AdaptableData.layerdata):
                foreach (PCycle cycle in currentCycles)
                {
                    // set data
                    SetNewLayerData(cycle.layers[0], parameter.value); // TODO: using first layer in list for now

                    // create new cycle with data
                    PAudioDataSystem.GenerateCycleAudioData(cycle);
                }

                break;

            case (AdaptableData.dynamicCycleLayers): // amount of layers to change by a dynamic cycle. TODO: This should be split in groups to prevent 2 dynamiccycles from changing the same layer
                for (int currentCycle = 0; currentCycle < currentDynamicCycles.Count; currentCycle++)
                {
                    // set data
                    SetNewDYnamicCycleLayers(parameter.value);

                    // create new cycle with data
                    if (currentCycle < currentDynamicCycles.Count)
                    {
                        PAudioDataSystem.dynamicCycles[currentCycle] = new PDynamicCycle(new List<int>(), changingDynamicLayers, 7, 8);
                    }
                    else
                    {
                        PAudioDataSystem.dynamicCycles.Add(new PDynamicCycle(new List<int>(), changingDynamicLayers, 7, 8));
                    }
                }

                break;

            default:
                break;
        }
    }

    static float SetNewBPM(float range, float input)
    {
        float bpm = PClock.currentBPM * ((input * range) + (1.0f - (range / 2)));
        print("bpm: " + bpm.ToString());
        return bpm;
    }

    static void SetNewLayerData(int layer, float input)
    {
        // TODO: dynamically set which variables should adapt instead of adapting all
        // TODO: use game data to have more influence on how they adapt instead of just choosing between 2 options
        ProceduralAudio.layers[layer].beatsPerMeasure = (input > 0.5f) ? 8 : 6;
        ProceduralAudio.layers[layer].beatLength = (input > 0.5f) ? 1 : 2;
        ProceduralAudio.layers[layer].noteDensity = (input > 0.5f) ? 8 : 4;
    }

    static void SetNewDYnamicCycleLayers(float input)
    {
        changingDynamicLayers = new List<int>();

        for (int layer = 0; layer < ProceduralAudio.amountOfLayers; layer++)
        {
            if (input > 0.25f)
            {
                changingDynamicLayers.Add(layer);
            }
        }
    }

    static void ControlerInput()
    {
        // generate new data
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (PAdaptionMoment adaptMoment in adaptionMoments)
            {
                CallAllParameters(adaptMoment.cycle, adaptMoment.dynamicCycles);
            }
        }

        // toggle first 5 layers with keyinput for testing
        var layers = ProceduralAudio.layers;
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

        // call oneshots for testing
        if (Input.GetKeyDown(KeyCode.A))
        {
            POneshots.playOneShot(0, Random.Range(0, ProceduralAudio.entryListOS[0].Count));
        }
    }
}