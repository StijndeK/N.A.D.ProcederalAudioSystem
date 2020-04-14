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
    public string folderLocation = "../ProceduralBounceLocation/";

    void Start()
    {
        entryList = PAutoFileLoader.ReadFiles(folderLocation);

        PClock.Start();

        PAudioPlayer.Start();
    }

    void Update()
    {
        // receive ticks
        PClock.Update();

        if (PClock.nextTick)
        {
            print("tick");
            // play audio
            PAudioPlayer.PlayFile(folderLocation, (string)entryList[0][Random.Range(0, 11)]);
        }
    }


}
