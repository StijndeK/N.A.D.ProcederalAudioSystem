using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POneshots
{
    // call oneshot sounds
    public static void playOneShot(string folderLocation, string fileName, float ducking = 0)
    {
        // TODO: add ducking functionality
        PAudioPlayer.PlayFile(folderLocation, fileName);
    }
}
