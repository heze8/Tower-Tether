using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeElapsed;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        ScoreTracker.setTime(timeElapsed);
        DisplayTime();
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        string text = minutes.ToString() + "m " + seconds.ToString() + "s";
        
        timeText.SetText(text);
    }
}
