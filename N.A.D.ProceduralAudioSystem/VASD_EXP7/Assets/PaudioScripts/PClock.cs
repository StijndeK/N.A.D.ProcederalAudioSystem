using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PClock
{
    public static float loopTime;
    public static float currentBPM;
    public static bool nextTick;

    public static void Init(int bpm)
    {
        currentBPM = calculateTime(bpm, 1);
        loopTime = Time.time + currentBPM;
        nextTick = Time.time < loopTime;
    }

    public static void Update()
    {
        if (nextTick)
        {
            loopTime = Time.time + currentBPM;
            nextTick = false;
        }

        nextTick |= Time.time > loopTime;
    }

    public static float calculateTime(float bpm, float ticks)
    {
        return (60 / bpm) * ticks;
    }
}
