using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : DestroyableObject 
{

    public override void Start()
    {
        myHp = GameManager.Instance.baseHp;
        hp.SetMaxHealth(myHp);
        StartCoroutine(CoroutineUpdate(GameManager.Instance.baseAPRate));
    }
    
    public IEnumerator CoroutineUpdate(float time)
    {
        GameManager.Instance.actionPoints.points++;
        yield return new WaitForSeconds(time );
        StartCoroutine(CoroutineUpdate(time));
    }
}