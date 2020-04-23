using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POneshots
{
    // call oneshot sounds
    public static void playOneShot(int layer, int soundIndex, bool reverse = false, float ducking = 0)
    {
        // TODO: add ducking functionality
        PAudioPlayer.PlayFile(layer, soundIndex, reverse, true);
    }
}
