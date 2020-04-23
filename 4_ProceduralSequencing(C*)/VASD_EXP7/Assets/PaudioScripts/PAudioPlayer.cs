using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class PAudioPlayer
{
    // FMOD API
    static FMOD.System corSystem;
    static ChannelGroup channelgroup;

    public static void Start()
    {
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);
        corSystem.createChannelGroup("master", out channelgroup);
    }

    public static void InitSound(int currentLayer, string fileName, string folderLocation)
    {
        Sound tempSound;

        corSystem.createSound(folderLocation + fileName, MODE.DEFAULT, out tempSound);

        ProceduralAudio.layers[currentLayer].sounds.Add(tempSound);
    }

    public static void PlayFile(string folderLocation, string fileName, int layer, int soundIndex, bool reverse = false)
    {
        Channel channel;

        Sound sound = ProceduralAudio.layers[layer].sounds[soundIndex];

        //if (reverse)
        //{
        //    channel.getFrequency(out currentFq);
        //    channel.setFrequency(0 - currentFq);
        //}

        corSystem.playSound(sound, channelgroup, false, out channel);
    }

    public static void StopFile()
    {
        
    }
}
