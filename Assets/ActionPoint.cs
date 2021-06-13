using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionPoint : MonoBehaviour
{
    private int _points = 0;
    public int startingPoints;
    public TextMeshProUGUI pointsText;

    private void Start()
    {
        setActionPoints(startingPoints);
    }

    public void setActionPoints(int points)
    {
        this._points = points;
        updateText();
    }

    public void addPoint()
    {
        this._points += 1;
        updateText();
    }

    public void removePoint()
    {
        this._points -= 1;
        updateText();
    }

    private void updateText()
    {
        pointsText.text = this._points.ToString();
    }
}
