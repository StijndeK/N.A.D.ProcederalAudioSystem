using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;
using System;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{
    // gameobjects
    GameObject FileLoaderObject;
    GameObject SpawnerObject;
    GameObject SequencerObject;

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

    // Input
    public Button playButton;
    bool firstPress = true;
    public Button pauseButton;
    public Slider volumeSlider; // TODO: exponential curve with db values

    // boxtext
    int boxText;

    // text output
    public string textForOutput;

    // layer Active Check
    public List<int> layerActiveChecks = new List<int>();

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
        volumeSlider.onValueChanged.AddListener(sliderMoved);

        // spawn boxes
        SpawnerObject = GameObject.FindGameObjectWithTag("spawncomponent");
        SpawnerObject.GetComponent<SpawnerSystem>().SpawnBoxes();

        // layer active checks to see if a layer needs to be checked in the sequencer
        for (int i = 0; i < layerAmount; i++)
        {
            layerActiveChecks.Add(0);
        }
    }

    void Update()
    {
        if (startPlaying == true)
            gameLoop();
    }

    private void sliderMoved(float value)
    {
        channelgroup.setVolume(value);
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
            stopping = !stopping;
        }
        print("play");
        textForOutput = textForOutput + "\n" + "play";
    }

    private void pauseButtonPressed()
    {
        print("pause (on next horizontal cycle)");
        textForOutput = textForOutput + "\n" + "pause (on next horizontal cycle)";
        stopping = !stopping;
    }

    // set text on layer box
    public int SetBoxText()
    {
        boxText++;
        return boxText;
    }

    public void PlayTrack(string filename)
    {
        // TODO: dont create new sound every time, or delete sound after usage
        Sound sound;
        corSystem.createSound(FileLoaderObject.GetComponent<AutoFileLoader>().folderLocation + filename, MODE.DEFAULT, out sound);
        corSystem.playSound(sound, channelgroup, false, out channel);

        print("playing: " + filename);
        textForOutput = textForOutput + "\n" + "playing: " + filename;
    }

    private void gameLoop()
    {
        // play tracks
        if (playing == false && stopping == false)
        {
            loopTime = Time.time + bpm;
            for (int layer = 0; layer < layerAmount; layer++)
            {
                if (layerActiveChecks[layer] == 1)
                {
                    string currentTransitionOptions = entryList2[layer][transitionValues[layer]][1];

                    // get a random value as large as the amount of transition options for the file
                    int value = UnityEngine.Random.Range(1, (currentTransitionOptions.Length + 1));

                    // chose between the transition option by applying the random value to the string with options
                    transitionValues[layer] = int.Parse(currentTransitionOptions[currentTransitionOptions.Length - value].ToString()) - 1;

                    // play track if parameters (X & Y) are checked
                    if (SequencerObject.GetComponent<SequencerSystem>().CheckIfLayerShouldPlay(layer) == true)
                    {
                        PlayTrack(entryList2[layer][transitionValues[layer]][0]);
                    }
                }
            }
            playing = true;
        }

        // check if looptime has passed and new track needs to be played/paused
        playing &= Time.time < loopTime;
    }
}