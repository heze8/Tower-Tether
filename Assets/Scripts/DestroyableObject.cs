using System.Collections;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    
    public healthBar hp;
    public int myHp;
    private int startingHp;
    public virtual void Start()
    {
        startingHp = myHp;
    }
    
    public void SetHP(int hp)
    {
        myHp = hp;
        this.hp.SetMaxHealth(myHp);
    }
    
    public virtual void Update()
    {
        if (myHp <= 0)
        {
            Destroy(gameObject);
        }
        if (myHp < startingHp)
        {
            hp.gameObject.SetActive(true);
            hp.SetMaxHealth(startingHp);
            hp.SetHealth(myHp);
        }
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