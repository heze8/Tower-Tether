using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : DestroyableObject 
{

    public override void Start()
    {
        myHp = GameManager.Instance.baseHp;
        hp.SetMaxHealth(myHp);
        StartCoroutine(CoroutineUpdate(GameManager.Instance.baseAPRate));
    }
    public override void Update()
    {
        if (myHp <= 0)
        {
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }

    }
    public IEnumerator CoroutineUpdate(float time)
    {
        GameManager.Instance.actionPoints.points++;
        yield return new WaitForSeconds(time );
        StartCoroutine(CoroutineUpdate(time));
    }
}