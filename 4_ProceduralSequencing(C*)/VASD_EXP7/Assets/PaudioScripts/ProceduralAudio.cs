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
    List<int> amountOfSoundOptions = new List<int> {12, 12, 11, 1};// layer, amount of options
    List<int> beatsPerMeasures = new List<int> { 4, 4, 1, 4 };// layer, amount of options


    private int amountOfLayers = 4;

    // TODO: create dynamic amount of beats per measure to allow for polyrithm in rythm melody etc 
    private int beatsPerMeasure = 4;
    private int currentTick;

    private string folderLocation = "../ProceduralBounceLocation/";

    void Start()
    {
        entryList = PAutoFileLoader.ReadFiles(folderLocation, amountOfLayers);

        PClock.Start();

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

            // reset (start new) measure
            // TODO: use % to check what polyrythm line needs to be reset when
            if (currentTick > 3)
            {
                currentTick = 0;
            }

            // for every layer
            for (int layer = 0; layer < amountOfLayers; layer++)
            {
                if (rythms[layer].Count - 1 >= currentTick) // TODO: remove this and add better polyrythm support
                {
                    // check rythm
                    if (rythms[layer][currentTick] == 1)
                    {
                        // play audio
                        PAudioPlayer.PlayFile(folderLocation, (string)entryList[layer][melodies[layer][currentTick]]);
                    }
                }
            }

            currentTick += 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateAudioData();
            currentTick = 0;
        }
    }

    void GenerateAudioData()
    {
        for (int layer = 0; layer < amountOfLayers; layer++)
        {
            // check if layer initialised
            if (rythms.Count < layer + 1)
            {
                rythms.Add(PRythm.GenerateRythm(beatsPerMeasures[layer]));
                melodies.Add(PMelody.GenerateMelody(rythms[layer], amountOfSoundOptions[layer]));
            }
            else
            {
                rythms[layer] = PRythm.GenerateRythm(beatsPerMeasures[layer]);
                melodies[layer] = PMelody.GenerateMelody(rythms[layer], amountOfSoundOptions[layer]);
            }

            print("layer " + layer.ToString());
            for (int i = 0; i < rythms[layer].Count; i++) print(rythms[layer][i]);
            for (int i = 0; i < rythms[layer].Count; i++) print(melodies[layer][i]);
        }
    }
}
