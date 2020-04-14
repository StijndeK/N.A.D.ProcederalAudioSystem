using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PAutoFileLoader
{
    // NOTE: transition possability not necessary here. maybe necessary for later for different parts of the audio

    public static List<List<string>> ReadFiles(string directoryLocation)
    {
        DirectoryInfo dir = new DirectoryInfo(directoryLocation);
        FileInfo[] info = dir.GetFiles("*.wav*");

        int layerAmount = 1;

        // list containing al wav files
        List<string> fileNames = new List<string>();

        // all layers and their info
        List<List<string>> entryList = new List<List<string>>(); // layer, tracks, filename

        foreach (FileInfo f in info)
        {
            fileNames.Add(f.Name);
        }

        // initialise entrylist
        for (int i = 0; i < layerAmount; i++)
        {
            entryList.Add(new List<string>());
        }

        // parse list per filename
        for (int i = 0; i < fileNames.Count; i++)
        {
            //print(fileNames[i]);

            // pass name and transition possibilities per track to entry list
            entryList[int.Parse(fileNames[i][0].ToString()) - 1].Add(fileNames[i]);
        }

        return entryList;
    }
}
