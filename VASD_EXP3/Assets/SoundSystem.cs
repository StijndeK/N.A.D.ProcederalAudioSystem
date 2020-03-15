using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;
using System;
using System.Linq;

public class SoundSystem : MonoBehaviour
{
    // list containing al wav files
    public List<string> fileNames;
    public List<List<string>> entryList = new List<List<string>>();
    // FMOD core API
    public FMOD.System corSystem;
    public ChannelGroup channelgroup;
    public Channel channel;
    // playing bool
    private bool playing;
    // string input
    private float bpm;
    public UnityEngine.UI.InputField inputField;
    public UnityEngine.UI.InputField inputField2;

    bool startPlaying;
    float loopTime;

    void Start()
    {
        // acces FMOD
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);

        // create channel group
        corSystem.createChannelGroup("master", out channelgroup);

        // load all files
        ReadFiles();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            startPlaying = true;
        }
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
        int layerAmount = 0;

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
            entryList.Add(new List<string>());
        }

        // parse list
        print("loaded files: ");
        for (int i = 0; i < fileNames.Count; i++)
        {
            entryList[int.Parse(fileNames[i][0].ToString()) - 1].Add(fileNames[i]);
            print(fileNames[i]);
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
        // Get UI information (dit hooeft niet in update)
        bpm = calculateTime(float.Parse(inputField.text), float.Parse(inputField2.text));

        // check if needs to stop
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("paused");
            // TODO: actually stop the audio 
        }

        if (playing == false)
        {
            // TODO: implement transition decision system
            loopTime = Time.time + bpm;
            playTrack(entryList[0][0]);
            playTrack(entryList[1][0]);
            playing = true;
        }
        if (Time.time >= loopTime)
        {
            playing = false;
        }
    }
}