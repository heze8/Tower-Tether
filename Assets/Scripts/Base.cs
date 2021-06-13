using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public healthBar hp;
    public int myHp;
    public void Start()
    {
        myHp = GameManager.Instance.baseHp;
        hp.SetMaxHealth(myHp);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemyBlob = other.GetComponent<EnemyBlob>();
        if (enemyBlob && enemyBlob.notAttackingBase)
        {
            enemyBlob.SetAttackingBase(this);
          
        }
    }

}
