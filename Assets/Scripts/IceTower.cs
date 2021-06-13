using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class IceTower : Tower
{
    public float slowTime;
    public override void Attack(EnemyBlob blob)
    {
        GameManager.Instance.StartCoroutine(SlowSpeed(blob)); 
        var effect =Instantiate(attackEffect, position: pos, Quaternion.identity);
        effect.GetComponent<LineRenderer>().SetPositions(new Vector3[]{pos, blob.transform.position});
        blob.hp -= dmg;
    }

    IEnumerator SlowSpeed(EnemyBlob blob)
    {
        blob.GetComponentInChildren<NavMeshAgent>().speed /= 2;
        yield return new WaitForSeconds(slowTime);
        if (blob)
        {
            blob.GetComponentInChildren<NavMeshAgent>().speed *= 2;
        }
    }
    
}