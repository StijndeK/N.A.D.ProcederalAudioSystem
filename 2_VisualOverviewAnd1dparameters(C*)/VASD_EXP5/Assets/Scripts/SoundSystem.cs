using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;
using System;
using UnityEngine.UI;

// initalising lists moet nu hier gebeuren of moet het halen uit de file loader

public class SoundSystem : MonoBehaviour
{
    // ---------------------------
    // gameobjects
    GameObject FileLoaderObject;
    GameObject SpawnerObject;
    GameObject SequencerObject;

    // ---------------------------
    // list containing al wav files
    public List<string> fileNames;
    public List<List<List<string>>> entryList2 = new List<List<List<string>>>();
    // FMOD API
    public FMOD.System corSystem;
    public ChannelGroup channelgroup;
    public Channel channel;
    // playing/stopping check
    private bool playing;
    private bool stopping;
    // string input
    private float bpm;
    public int layerAmount;
    // time
    bool startPlaying;
    float loopTime;
    // transitioning
    List<int> transitionValues = new List<int>();
    // Button
    public Button playButton;
    public Button pauseButton;
    bool firstPress = true;
    // boxtext
    int boxText;
    // text output
    public string textForOutput;

    /* TODO:
        * highlight when box is being played
        * visualise how the data is loaded and parsed
    */

    void Start()
    {
        SequencerObject = GameObject.FindGameObjectWithTag("sequencer");

        // read files and receive lists
        FileLoaderObject = GameObject.FindGameObjectWithTag("fileloader");
        var autoFileLoaderComponent = FileLoaderObject.GetComponent<AutoFileLoader>();

        autoFileLoaderComponent.ReadFiles();
        layerAmount = autoFileLoaderComponent.layerAmount;
        transitionValues = autoFileLoaderComponent.transitionValues;
        entryList2 = autoFileLoaderComponent.entryList2;

        // FMOD
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);
        corSystem.createChannelGroup("master", out channelgroup);         // create channel group

        // UI listeners
        playButton.onClick.AddListener(playButtonPressed);
        pauseButton.onClick.AddListener(pauseButtonPressed);

        // spawn boxes
        SpawnerObject = GameObject.FindGameObjectWithTag("spawncomponent");
        SpawnerObject.GetComponent<SpawnerSystem>().SpawnBoxes();
    }

    void Update()
    {
        if (startPlaying == true)
        {
            gameLoop();
        }
    }

    public void playButtonPressed()
    {
        if (firstPress)         // start game on first
        {
            bpm = FileLoaderObject.GetComponent<AutoFileLoader>().calculateTime();
            startPlaying = true;
            firstPress = false;
        }
        else
        {
            print("play");
            stopping = !stopping;
        }
    }

    private void pauseButtonPressed()
    {
        print("pause (on next horizontal cycle)");
        stopping = !stopping;
    }

    // set text on layer box
    public int setBoxText()
    {
        boxText++;
        return boxText;
    }

    public void playTrack(string filename)
    {
        Sound sound;
        corSystem.createSound("../BounceLocation/" + filename, MODE.DEFAULT, out sound);
        corSystem.playSound(sound, channelgroup, false, out channel);

        print("playing: " + filename);
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

                // play track if parameter is checked
                if (SequencerObject.GetComponent<SequencerSystem>().parameterX[i] == (int)SequencerObject.GetComponent<SequencerSystem>().parameterSettingSlider.value)
                {
                    playTrack(entryList2[i][transitionValues[i]][0]);
                }
            }
            playing = true;
        }

        // check if looptime has passed and new track needs to be played/paused
        playing &= Time.time < loopTime;
    }
}