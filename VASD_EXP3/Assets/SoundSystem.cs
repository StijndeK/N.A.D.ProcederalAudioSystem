using System.Collections.Generic;
using System.IO;
using UnityEngine;
using FMOD;

public class SoundSystem : MonoBehaviour
{
    public List<string> fileNames; // list containing al wav files

    public FMOD.System corSystem;
    public ChannelGroup channelgroup;
    public Channel channel;

    void Start()
    {
        // acced FMOD
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);

        // create channel group
        corSystem.createChannelGroup("master", out channelgroup);

        // load all files
        ReadFiles();
        print("Loaded files: ");
        for (int i = 0; i < fileNames.Count; i++)
        {
            print(fileNames[i]);
        }

        // TODO: handle user input
        int bpm = calculateTime();

        // TODO: start the game loop
        gameLoop();
    }

    void Update()
    {
        // TODO: run gameplay loop, statemachine
    }

    public void ReadFiles()
    {
        DirectoryInfo dir = new DirectoryInfo("../BounceLocation");
        FileInfo[] info = dir.GetFiles("*.wav*");
        foreach (FileInfo f in info)
        {
            fileNames.Add(f.Name);
            // TODO: put into a entry list and divide by layer
            //entryList.append([name, name[4], possibleTransitions, name[6]])
        }
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

    private void gameLoop()
    {
        playTrack(fileNames[0]);
        playTrack(fileNames[3]);
        
        // check for pause or stop
        // For every layer:
        //  play current files
        //  decide what next files can be played
        //  wait for the horizontal loops to finish and run again
    }
}