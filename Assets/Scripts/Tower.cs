using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class Tower : Tile, IObstacle
{
    public bool connected;
    public Vector2 pos;
    public float attackInterval;
    public int level;

    public GameObject attackEffect;
    // Tower properties
    public int dmg;
    public int health;
    public string description;
    public string cardName;
    public int cost;
    public int range;
    public virtual void Start()
    {
        GridManager.Instance.StartCoroutine(CoroutineUpdate(0));
    }
    
    [HideInInspector]
    public virtual IEnumerator CoroutineUpdate(float time)
    {
        yield return new WaitForSeconds(time);

        EnemyBlob blob = EnemySpawningSystem.Instance.GetNearestEnemy(pos);
        if (blob)
        {
            if(((Vector2)blob.transform.position - pos).magnitude < range)
                Attack(blob);
        }
        GridManager.Instance.StartCoroutine(CoroutineUpdate(attackInterval));
    }

    public virtual void Attack(EnemyBlob blob)
    {
        var effect =Instantiate(attackEffect, position: pos, Quaternion.identity);
        effect.GetComponent<LineRenderer>().SetPositions(new Vector3[]{pos + new Vector2(0.5f, 0.5f), blob.transform.position});
        blob.hp -= dmg;
    }

    
}