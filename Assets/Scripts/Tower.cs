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

    public void Start()
    {
        GridManager.Instance.StartCoroutine(CoroutineUpdate(0));
    }
    
    IEnumerator CoroutineUpdate(float time)
    {
        yield return new WaitForSeconds(time);

        EnemyBlob blob = EnemySpawningSystem.Instance.GetNearestEnemy(pos);
        if (blob)
        {
            Attack(blob);
        }
        GridManager.Instance.StartCoroutine(CoroutineUpdate(attackInterval));
    }

    private void Attack(EnemyBlob blob)
    {
        var effect =Instantiate(attackEffect, position: pos, Quaternion.identity);
        effect.GetComponent<LineRenderer>().SetPositions(new Vector3[]{pos, blob.transform.position});
        blob.hp -= dmg;
    }
}