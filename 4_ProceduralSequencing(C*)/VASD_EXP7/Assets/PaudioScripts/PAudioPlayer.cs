using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class PAudioPlayer
{
    // FMOD API
    static FMOD.System corSystem;
    static ChannelGroup channelgroup;
    static Channel channel;

    public static void Start()
    {
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);
        corSystem.createChannelGroup("master", out channelgroup);
    }

    public static void PlayFile(string folderLocation, string fileName)
    {
        Sound sound;
        corSystem.createSound(folderLocation + fileName, MODE.DEFAULT, out sound);
        corSystem.playSound(sound, channelgroup, false, out channel);
    }
}
