using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;
using System;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{
    // list containing al wav files
    public List<string> fileNames;
    public List<List<List<string>>> entryList2 = new List<List<List<string>>>();
    // FMOD core API
    public FMOD.System corSystem;
    public ChannelGroup channelgroup;
    public Channel channel;
    // playing/stopping check
    private bool playing;
    private bool stopping;
    // string input
    private float bpm;
    public UnityEngine.UI.InputField inputField;
    public UnityEngine.UI.InputField inputField2;
    int layerAmount;
    // time
    bool startPlaying;
    float loopTime;
    // transitioning
    List<int> transitionValues = new List<int>();
    // Button
    public Button playPauseButton;
    bool firstPress = true;
    // spawner 
    GameObject spawnerObject;

    // TODO: visualise how the data is loaded and parsed

    /* TODO:
        * add text to visualisation boxes
        * make boxes moveable
        * highlight when box is being played
    */

    public void buttonPressed()
    {
        // start game on first
        if (firstPress)
        {
            bpm = calculateTime(float.Parse(inputField.text), float.Parse(inputField2.text));
            startPlaying = true;
            firstPress = false;
        }
        // pause
        else
        {
            print("pause/play (on next horizontal cycle)");
            stopping = !stopping;
        }
    }

    void Start()
    {
        // spawner object reference
        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");


        // acces FMOD
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);

        // create channel group
        corSystem.createChannelGroup("master", out channelgroup);

        // load all files
        ReadFiles();

        // UI listeners
        playPauseButton.onClick.AddListener(buttonPressed);

        // create boxes
        for (int i = 0; i < layerAmount; i++)
        {
            spawnerObject.GetComponent<spawner>().SpawnBox();
        }
    }


    void Update()
    {
        if (startPlaying == true)
        {
            gameLoop();
        }
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
        }

        // parse list
        print("loaded files: ");
        for (int i = 0; i < fileNames.Count; i++)
        {
            print(fileNames[i]);

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

    public void playTrack(string filename)
    {
        Sound sound;
        corSystem.createSound("../BounceLocation/" + filename, MODE.DEFAULT, out sound);
        corSystem.playSound(sound, channelgroup, false, out channel);

        print("playing: " + filename);
    }

    private float calculateTime(float beatsPerMinute, float beatAmount)
    {
        float duration = (60 / beatsPerMinute) * beatAmount;
        return duration;
    }

    private void gameLoop()
    {
        // play tracks
        if (playing == false && stopping == false)
        {
            loopTime = Time.time + bpm;
            for (int i = 0; i < layerAmount; i++)
            {
                string currentTransitionOptions = entryList2[i][transitionValues[i]][1];

                // get a random value as large as the amount of transition options for the file
                int value = UnityEngine.Random.Range(1, (currentTransitionOptions.Length + 1));

                // chose between the transition option by applying the random value to the string with options
                transitionValues[i] = int.Parse(currentTransitionOptions[currentTransitionOptions.Length - value].ToString()) - 1;

                // play track
                playTrack(entryList2[i][transitionValues[i]][0]);
            }
            playing = true;
        }

        // check if looptime has passed and new track needs to be played/paused
        playing &= Time.time < loopTime;
    }
}