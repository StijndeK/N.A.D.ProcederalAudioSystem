using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using FMOD;
using FMODUnity;


public class FMODTEST : MonoBehaviour
{
    public List<string> fileNames; // list containing al wav files

    public FMOD.System corSystem;
    public ChannelGroup channelgroup;
    public Channel channel;

    void Start()
    {
        // get acces to fmod api
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);

        // create channel group
        corSystem.createChannelGroup("master", out channelgroup);

        ReadFiles();
        for (int i = 0; i < fileNames.Count; i++)
        {
            print(fileNames[i]);
        }

        // handle user input

        // initialise game loop
        playTrack(fileNames[0]);
        playTrack(fileNames[3]);
    }

    void Update()
    {
        
    }

    public void ReadFiles()
    {
        // get all files and put them into a list
        DirectoryInfo dir = new DirectoryInfo("../BounceLocation");
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            fileNames.Add(f.Name);
        }

        // put them into a entry list and divide by layer
        //entryList.append([name, name[4], possibleTransitions, name[6]])
    }

    public void playTrack(string filename)
    {
        Sound sound;
        corSystem.createSound("../BounceLocation/" + filename, MODE.DEFAULT, out sound);
        corSystem.playSound(sound, channelgroup, false, out channel);

        print("playing: " + filename);
    }

    private int calculateTime()
    {
        // get user input on bpm and amount of beats
        return 0;
    }
}