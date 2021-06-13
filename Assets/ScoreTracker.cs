using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreTracker
{
    private static float timeElapsed = 0;

    public static void setTime(float time)
    {
        timeElapsed = time;
    }

    public static float getTime()
    {
        return timeElapsed;
    }
}
