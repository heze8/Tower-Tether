using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ActionPoints : Singleton<ActionPoints>
{
    [FormerlySerializedAs("_points")] public int points = 0;
    public TextMeshProUGUI pointsText;

    

    private void Update()
    {
        pointsText.text = this.points.ToString();
    }
}
