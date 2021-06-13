using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIScript : MonoBehaviour
{
    public GameObject primaryUI;
    public GameObject[] otherUIs;
    
    private void OnEnable()
    {
       primaryUI.SetActive(true);
       foreach (GameObject o in otherUIs)
       {
           o.SetActive(false);
       }
    }
}
