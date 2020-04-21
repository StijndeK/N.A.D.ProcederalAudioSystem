using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;

public class ProceduralAudio : MonoBehaviour
{
    // TODO: INREADME: niet unityobjectcomponent based systeem
    // TODO: create scale size variable

    List<List<string>> entryList = new List<List<string>>(); // layer, tracks, filename
    List<List<string>> entryListOS = new List<List<string>>(); // layer, tracks, filename

    List<List<int>> rythms = new List<List<int>>(); // layer, rythm

    List<List<int>> melodies = new List<List<int>>(); // layer, melody

    List<int> currentTicks = new List<int>();

    public static List<int> currentScale = new List<int>();

    public static List<List<int>> chords = new List<List<int>>(); // chord, chordnotes

    // ---------------------------------
    // VARIABLES TO SET BEFORE RUNNING | (some of) these variables in the future should be linked to game parameters

    // amount of options per layer to choose from
    List<int> amountOfSoundOptions = new List<int> {12, 12, 11, 1};// layer, amount of options

    // amount of beats in a measure per layer
    List<int> beatsPerMeasures = new List<int> { 8, 4, 4, 4 };// layer, amount of options

    // length of a beat in ticks(4th notes)
    List<int> beatLengths = new List<int> { 1, 1, 4, 1 };// layer, amount of options

    // layer types (0=melody, 1=countermelody, 2=percussion, 3=chordslayer1, 4=chordslayer2, 5=chordslayer3 ...)
    public static List<int> layerTypes = new List<int> { 0, 1, 3, 2 }; // layer, type

    // average note density of rythms: 10 = playing every tick, 5 = 50% chance to play every tick
    List<int> noteDensities = new List<int> { 8, 4, 10, 10 };// layer, amount of options

    List<bool> layerOn = new List<bool> {false, false, false, false };

    // set to what scale is used
    List<string> scaleInNotes = new List<string> {"c", "c#", "d", "d#", "e", "f", "f#","g", "g#", "a", "a#", "b" };

    // total number of looping layers and oneshots
    private int amountOfLayers = 4;
    private int amountOfSoundEffects = 1;

    private string folderLocationLoops = "../ProceduralBounceLocation/";
    private string folderLocationOneShots = "../ProceduralBounceLocationOS/";

    private int bpm = 120;

    public static int chordAmount = 4;
    public static int chordLayerAmount = 3;
    // ---------------------------------

    void Start()
    {
        // read looping audio
        entryList = PAutoFileLoader.ReadFiles(folderLocationLoops, amountOfLayers);

        // read oneshots
        entryListOS = PAutoFileLoader.ReadFiles(folderLocationOneShots, amountOfSoundEffects);

        PClock.Init(bpm);

        PAudioPlayer.Start(); // setup fmod API

        GenerateAudioData();
    }

    void Update()
    {
        // receive ticks
        PClock.Update();

        if (PClock.nextTick)
        {
            print("tick");
            print(PAudioPlayer.currentFq);

            // for every layer
            for (int layer = 0; layer < amountOfLayers; layer++)
            {
                // convert tick number into measure
                currentTicks[layer] = currentTicks[layer] % rythms[layer].Count;

                // check if layer is active
                if(layerOn[layer])
                {
                    // check rythm
                    if (rythms[layer][currentTicks[layer]] == 1)
                    {
                        // play audio
                        PAudioPlayer.PlayFile(folderLocationLoops, (string)entryList[layer][melodies[layer][currentTicks[layer]]]);
                    }
                }

                // next tick
                currentTicks[layer] += 1;
            }
        }

        // generate new data
        if (Input.GetKeyDown(KeyCode.Space))
            GenerateAudioData();

        // toggle layers
        if (Input.GetKeyDown(KeyCode.Alpha1))
            layerOn[0] = !layerOn[0];
        if (Input.GetKeyDown(KeyCode.Alpha2))
            layerOn[1] = !layerOn[1];
        if (Input.GetKeyDown(KeyCode.Alpha3))
            layerOn[2] = !layerOn[2];
        if (Input.GetKeyDown(KeyCode.Alpha4))
            layerOn[3] = !layerOn[3];

        // call oneshots
        if (Input.GetKeyDown(KeyCode.A))
        {
            string soundToPlay = (string)entryListOS[0][Random.Range(0, entryListOS[0].Count)];
            POneshots.playOneShot(folderLocationOneShots, soundToPlay, true);
        }

    }

    void GenerateAudioData()
    {
        // set scale
        currentScale = PTonal.setScale(Random.Range(0, 2));

        // TODO: currently there is only one layer per type. In the future this has to be a modular amount
        for (int type = 0; type < layerTypes.Count; type++)
        {
            if (rythms.Count < type + 1) // check if layer is initialised
            {
                rythms.Add(PRythm.GenerateRythm(beatsPerMeasures[type], beatLengths[type], noteDensities[type]));
                melodies.Add(PTonal.GenerateTonalIntervals(rythms[type], amountOfSoundOptions[type], layerTypes[type]));
                currentTicks.Add(0);
            }
            else
            {
                rythms[type] = PRythm.GenerateRythm(beatsPerMeasures[type], beatLengths[type], noteDensities[type]);
                melodies[type] = PTonal.GenerateTonalIntervals(rythms[type], amountOfSoundOptions[type], layerTypes[type]);
                currentTicks[type] = 0;
            }

            print("layer " + type.ToString());

            string rythmOutput = "";
            for (int i = 0; i < rythms[type].Count; i++) rythmOutput += rythms[type][i].ToString();
            print(rythmOutput);

            string melodyOutput = "";
            for (int i = 0; i < rythms[type].Count; i++) melodyOutput += melodies[type][i].ToString();
            print(melodyOutput);
        }

        for (int chord = 0; chord < chords.Count; chord++) print("chord: " + scaleInNotes[chords[chord][0]] + " major");
    }
}
