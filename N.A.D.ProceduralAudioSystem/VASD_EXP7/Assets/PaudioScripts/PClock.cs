using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PClock
{
    public static float loopTime;
    public static float currentBPM;
    public static bool nextTick;

    public static void Init(float bpm)
    {
        currentBPM = calculateTime(bpm, 1);
        setNewTickTime();

    }

    public static void SetNewTime(float bpm)
    {
        currentBPM = bpm;
        setNewTickTime();
    }

    private static void setNewTickTime()
    {
        loopTime = Time.time + currentBPM;
        nextTick = false;
    }

    public static void Update()
    {
        if (nextTick)
        {
            setNewTickTime();
        }

        nextTick |= Time.time > loopTime;
    }

    public static float calculateTime(float bpm, float ticks)
    {
        return (60 / bpm) * ticks;
    }
}
