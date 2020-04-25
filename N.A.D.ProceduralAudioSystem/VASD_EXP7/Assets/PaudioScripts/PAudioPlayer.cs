using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;

public class PAudioPlayer
{
    // FMOD API
    static FMOD.System corSystem;
    static ChannelGroup channelgroup;

    // TODO: automatically recognise if oneshot of loop and load from same list/class

    public static void Start()
    {
        corSystem = FMODUnity.RuntimeManager.CoreSystem;
        uint version;
        corSystem.getVersion(out version);
        corSystem.createChannelGroup("master", out channelgroup);

        //DSP pLowPass;
        //corSystem.createDSPByType(DSP_TYPE.LOWPASS, out pLowPass);
        //channelgroup.addDSP(0, pLowPass31212  124);
        //pLowPass.setParameterFloat(0, 50.0f);
    }

    public static void Update()
    {
        corSystem.update();
    }

    public static void InitSound(int currentLayer, string fileName, string folderLocation, bool oneshot = false)
    {
        Sound tempSound;

        corSystem.createSound(folderLocation + fileName, MODE.DEFAULT, out tempSound);

        if (oneshot)
        {
            ProceduralAudio.oSLayers[currentLayer].sounds.Add(tempSound);
        }
        else
        {
            ProceduralAudio.layers[currentLayer].sounds.Add(tempSound);
        }
    }

    public static void PlayFile(int layer, int soundIndex, bool reverse = false, bool oneshot = false)
    {
        Channel channel;

        Sound sound = (oneshot) ? ProceduralAudio.oSLayers[layer].sounds[soundIndex] : ProceduralAudio.layers[layer].sounds[soundIndex];

        //if (reverse)
        //{
        //    channel.getFrequency(out currentFq);
        //    channel.setFrequency(0 - currentFq);
        //}

        corSystem.playSound(sound, channelgroup, false, out channel);
    }
}
