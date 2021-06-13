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
        StartCoroutine(CoroutineUpdate(GameManager.Instance.baseAPRate));
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
    public IEnumerator CoroutineUpdate(float time)
    {
        GameManager.Instance.actionPoints.points++;
        yield return new WaitForSeconds(time );
        StartCoroutine(CoroutineUpdate(time));
    }
    
    
}