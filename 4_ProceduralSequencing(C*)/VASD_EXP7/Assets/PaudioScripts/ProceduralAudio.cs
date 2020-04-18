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


    // ---------------------------------
    // VARIABLES TO SET BEFORE RUNNING
    // (some of) these variables in the future should be linked to game parameters

    // amount of options per layer to choose from
    List<int> amountOfSoundOptions = new List<int> {12, 12, 11, 1};// layer, amount of options

    // amount of beats in a measure per layer
    List<int> beatsPerMeasures = new List<int> { 8, 4, 2, 4 };// layer, amount of options

    // length of a beat in ticks(4th notes)
    List<int> beatLengths = new List<int> { 1, 1, 4, 1 };// layer, amount of options

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

    private int bpm = 100;

    private int chordAmount = 4;
    private int chordLengthInTicks = 4;
    private int chordLayerAmount = 3;
    // ---------------------------------

    void Start()
    {
        // read looping audio
        entryList = PAutoFileLoader.ReadFiles(folderLocationLoops, amountOfLayers);

        // read oneshots
        entryListOS = PAutoFileLoader.ReadFiles(folderLocationOneShots, amountOfSoundEffects);

        PClock.Init(bpm);

        PAudioPlayer.Start();

        GenerateAudioData();
    }

    void Update()
    {
        // receive ticks
        PClock.Update();

        if (PClock.nextTick)
        {
            print("tick");

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
            POneshots.playOneShot(folderLocationOneShots, soundToPlay);
        }

    }

    void GenerateAudioData()
    {
        // set scale
        currentScale = PHarmony.setScale(Random.Range(0, 2));

        // generate chords
        var chords = PHarmony.GenerateChords(chordAmount, chordLengthInTicks, chordLayerAmount);
        for (int chord = 0; chord < chords.Count; chord++) print("chord: " + scaleInNotes[chords[chord][0]] + " major");

        // generate melody
        for (int layer = 0; layer < amountOfLayers; layer++)
        {
            // check if layer initialised
            if (rythms.Count < layer + 1)
            {
                rythms.Add(PRythm.GenerateRythm(beatsPerMeasures[layer], beatLengths[layer], noteDensities[layer]));
                melodies.Add(PMelody.GenerateMelody(rythms[layer], amountOfSoundOptions[layer]));
                currentTicks.Add(0);
            }
            else
            {
                rythms[layer] = PRythm.GenerateRythm(beatsPerMeasures[layer], beatLengths[layer], noteDensities[layer]);
                melodies[layer] = PMelody.GenerateMelody(rythms[layer], amountOfSoundOptions[layer]);
                currentTicks[layer] = 0;
            }
            // ik wil hier dus melody 3 de grondtoon van het akkoord laten spelen

            print("layer " + layer.ToString());

            string rythmOutput = "";
            for (int i = 0; i < rythms[layer].Count; i++) rythmOutput += rythms[layer][i].ToString();
            print(rythmOutput);

            string melodyOutput = "";
            for (int i = 0; i < rythms[layer].Count; i++) melodyOutput += melodies[layer][i].ToString();
            print(melodyOutput);
        }
    }
}
