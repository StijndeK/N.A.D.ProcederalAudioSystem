using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PAutoFileLoader
{
    // read files for looping sounds 
    public static List<List<string>> ReadFiles(string directoryLocation, int layerAmount, bool oneShotsLayers = false)
    {
        // TODO: track reading isnt really necessary anymore. could also be moved to end of name to allow for easier filenaming from daws

        DirectoryInfo dir = new DirectoryInfo(directoryLocation);
        FileInfo[] info = dir.GetFiles("*.wav*");

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
            // pass name in layer per track to entry list
            entryList[int.Parse(fileNames[i][0].ToString()) - 1].Add(fileNames[i]);

            // initialise sounds
            PAudioPlayer.InitSound(int.Parse(fileNames[i][0].ToString()) - 1, fileNames[i], directoryLocation, oneShotsLayers);
        }

        return entryList;
    }
}
