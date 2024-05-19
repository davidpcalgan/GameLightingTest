using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper
{
    float startTime;
    float duration;
    float startValue;
    float endValue;

    public Lerper (float StartValue, float EndValue, float Duration)
    {
        startValue = StartValue;
        endValue = EndValue;
        duration = Duration;
        startTime = Time.time;
    }

    public float value ()
    {
        float t = (Time.time - startTime) / duration;
        t = (t > 1 ? 1 : t);
        return t * endValue + (1 - t) * startValue;
    }
}