using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;
using System;
using UnityEngine.UI;

public class AutoFileLoader : MonoBehaviour
{
    // list containing al wav files
    public List<string> fileNames;
    public List<List<List<string>>> entryList2 = new List<List<List<string>>>();

    // input
    public UnityEngine.UI.InputField inputFieldBPM;
    public UnityEngine.UI.InputField inputFieldBeats;
    public int layerAmount;

    // transitioning
    public List<int> transitionValues = new List<int>();

    // text output
    public string textForOutput;

    // Parameterss
    public List<int> parameterX = new List<int>();

    // Folder location
    public UnityEngine.UI.InputField inputField3;
    public Button loadButton;
    public string folderLocation = "../BounceLocation/";

    GameObject SpawnerObject;

    private void Start()
    {
        loadButton.onClick.AddListener(LoadButtonPressed);
    }

    void LoadButtonPressed()
    {
        folderLocation = inputField3.text;
        print("new path set to: " + folderLocation);
    }

    public void ReadFiles()
    {
        DirectoryInfo dir = new DirectoryInfo("../BounceLocation");
        FileInfo[] info = dir.GetFiles("*.wav*");

        int oldValue = 0;
        foreach (FileInfo f in info)
        {
            fileNames.Add(f.Name);
            // get layerAmount
            if (int.Parse(f.Name[0].ToString()) > oldValue)
            {
                layerAmount = int.Parse(f.Name[0].ToString());
            }
        }

        // initialise entrylist
        for (int i = 0; i < layerAmount; i++)
        {
            entryList2.Add(new List<List<string>>());
            transitionValues.Add(0);
            parameterX.Add(0);
        }

        // parse list
        print("loaded files: ");
        textForOutput = textForOutput + "loaded files: ";
        for (int i = 0; i < fileNames.Count; i++)
        {
            print(fileNames[i]);
            textForOutput = textForOutput + "\n" + fileNames[i];

            // get transition possibilities for loop from filename
            string possibleTransitions = "";
            string tempName = fileNames[i].Remove(fileNames[i].Length - 4, 4);
            while (true)
            {
                int number;
                bool result = Int32.TryParse(tempName[tempName.Length - 1].ToString(), out number);
                if (result)
                {
                    possibleTransitions += number.ToString();
                    tempName = tempName.Remove(tempName.Length - 1, 1);
                }
                else
                {
                    break;
                }
            }

            // pass name and transition possibilities per track to entry list
            entryList2[int.Parse(fileNames[i][0].ToString()) - 1].Add(new List<string> { fileNames[i], possibleTransitions });
        }
    }

    public float calculateTime()
    {
        float beatsPerMinute = float.Parse(inputFieldBPM.text);
        float beatAmount = float.Parse(inputFieldBeats.text);

        return (60 / beatsPerMinute) * beatAmount;
    }
}