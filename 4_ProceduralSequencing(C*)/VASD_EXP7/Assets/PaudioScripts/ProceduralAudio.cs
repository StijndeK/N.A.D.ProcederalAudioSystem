using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;

public class ProceduralAudio : MonoBehaviour
{
    // TODO: per layer instellen hoevaak het kan ticken. zodat je ook langere noten kan hebben etc
    // TODO: INREADME: niet unityobjectcomponent based systeem

    List<List<string>> entryList = new List<List<string>>(); // layer, tracks, filename
    List<List<int>> rythms = new List<List<int>>(); // layer, rythm
    List<List<int>> melodies = new List<List<int>>(); // layer, melody
    List<int> currentTicks = new List<int>();

    // ---------------------------------
    // VARIABLES TO SET BEFORE RUNNING
    // these variables in the future should be linked to game parameters

    // amount of options per layer to choose from
    List<int> amountOfSoundOptions = new List<int> {12, 12, 11, 1};// layer, amount of options

    // amount of beats in a measure per layer
    List<int> beatsPerMeasures = new List<int> { 3, 4, 1, 4 };// layer, amount of options

    // length of a beat in ticks(4th notes)
    List<int> beatLengths = new List<int> { 1, 1, 4, 1 };// layer, amount of options

    // average note density of rythms: 10 = playing every tick, 5 = 50% chance to play every tick
    List<int> noteDensities = new List<int> { 8, 4, 10, 10 };// layer, amount of options

    // total number of vertical audio layers
    private int amountOfLayers = 4;

    private string folderLocation = "../ProceduralBounceLocation/";

    private int bpm = 100;
    // ---------------------------------

    void Start()
    {
        entryList = PAutoFileLoader.ReadFiles(folderLocation, amountOfLayers);

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
                print(currentTicks[layer]);

                // check rythm
                if (rythms[layer][currentTicks[layer]] == 1)
                {
                    // play audio
                    PAudioPlayer.PlayFile(folderLocation, (string)entryList[layer][melodies[layer][currentTicks[layer]]]);
                }

                // next tick
                currentTicks[layer] += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateAudioData();
        }
    }

    void GenerateAudioData()
    {
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

            print("layer " + layer.ToString());
            print("rythm: ");
            for (int i = 0; i < rythms[layer].Count; i++) print(rythms[layer][i]);
            print("melody: ");
            for (int i = 0; i < rythms[layer].Count; i++) print(melodies[layer][i]);
        }
    }
}
