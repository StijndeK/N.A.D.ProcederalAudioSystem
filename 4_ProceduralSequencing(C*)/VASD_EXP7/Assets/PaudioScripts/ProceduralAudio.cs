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

    int amountOfLayers = 1;

    // TODO: make dynamic to allow for polyrythm
    int beatsPerMeasure = 4;
    int currentBeat;

    public string folderLocation = "../ProceduralBounceLocation/";

    void Start()
    {
        entryList = PAutoFileLoader.ReadFiles(folderLocation, amountOfLayers);

        PClock.Start();

        PAudioPlayer.Start();

        for (int layer = 0; layer < amountOfLayers; layer++)
        {
            rythms.Add(PRythm.GenerateRythm(beatsPerMeasure));
        }
    }

    void Update()
    {
        // receive ticks
        PClock.Update();

        if (PClock.nextTick)
        {
            print("tick");

            if (currentBeat > 3)
            {
                currentBeat = 0;
            }

            // for every layer
            for (int layer = 0; layer < amountOfLayers; layer++)
            {
                // check rythm
                if (rythms[layer][currentBeat] == 1)
                {
                    // play audio
                    PAudioPlayer.PlayFile(folderLocation, (string)entryList[0][Random.Range(0, 12)]);
                }
            }

            currentBeat += 1;
        }
    }
}
