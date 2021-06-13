using System;
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
    

}