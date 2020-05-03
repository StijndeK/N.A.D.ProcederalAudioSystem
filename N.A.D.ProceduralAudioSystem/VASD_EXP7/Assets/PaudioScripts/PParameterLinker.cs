using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PParameterLinker : MonoBehaviour
{
    public static List<PCycle> testCycles = new List<PCycle>();
    public static List<PCycleTimer> cycleTimers = new List<PCycleTimer>();
    public static PDynamicCycle dynamicCycle;
    public static List<PAdaptionMoment> adaptionMoments = new List<PAdaptionMoment>();

    public enum AdaptableParametersCycle { chordsAndScale, layerdata, rythmAndMelody, melody, bpm, onOff, dynamicCycleLayerAmount };

    public static void Start()
    {
        // create cycles and parameters
        testCycles.Add(new PCycle(new List<PParameter>()));

        testCycles[0].parametersToAdapt.Add(new PParameter(AdaptableParametersCycle.chordsAndScale));
        testCycles[0].parametersToAdapt.Add(new PParameter(AdaptableParametersCycle.layerdata, MockGameData(), new List<int> {1})); 
        testCycles[0].parametersToAdapt.Add(new PParameter(AdaptableParametersCycle.rythmAndMelody, null, new List<int> { 0, 1, 2, 3, 4, 5 })); 
        testCycles[0].parametersToAdapt.Add(new PParameter(AdaptableParametersCycle.bpm, MockGameData())); 
        testCycles[0].parametersToAdapt.Add(new PParameter(AdaptableParametersCycle.dynamicCycleLayerAmount, MockGameData()));

        testCycles.Add(new PCycle(new List<PParameter> { new PParameter(AdaptableParametersCycle.rythmAndMelody, null, new List<int> { 1 })}));

        // init dynamic cycle
        dynamicCycle = new PDynamicCycle(new List<int> { 0, 1, 2, 3, 4, 5 }, true);

        // set a timer on the second cycle and the dynamic cycle
        cycleTimers.Add(new PCycleTimer(testCycles[1], null));
        cycleTimers.Add(new PCycleTimer(null, dynamicCycle));

        // create adaptionmoments
        adaptionMoments.Add(new PAdaptionMoment(testCycles));

        // call dataa
        PAudioDataSystem.CallCycle(testCycles[0]);
    }

    public static void Update()
    {
        ControlerInput();
    }

    // function to test parameter linking, when no game is active to receive data from
    static float MockGameData()
    {
        return Random.Range(0.0f, 1.0f);
    }

    static void ControlerInput()
    {
        // generate new data
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // set new gamedata
            // TODO: automate this
            testCycles[0].parametersToAdapt[1].value = MockGameData();
            testCycles[0].parametersToAdapt[3].value = MockGameData();
            testCycles[0].parametersToAdapt[4].value = MockGameData();

            // call cycles
            foreach (PAdaptionMoment adaptMoment in adaptionMoments)
            {
                foreach(PCycle cycle in adaptMoment.cycles)
                {
                    PAudioDataSystem.CallCycle(cycle);
                }
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