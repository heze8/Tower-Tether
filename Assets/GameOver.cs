using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI timeSurvivedText;
    private void Start()
    {
        float time = ScoreTracker.getTime();
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        string text = minutes.ToString() + "m " + seconds.ToString() + "s";
        timeSurvivedText.SetText("You survived for: \n" + text);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
