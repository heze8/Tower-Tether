using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject levelText;
    // Tower properties
    public int dmg;
    public int health;
    public string description;
    public string cardName;
    public int cost;
    public int range;
    public void Start()
    {
        GridManager.Instance.StartCoroutine(CoroutineUpdate(0));
        levelText = Instantiate(levelText, pos, Quaternion.identity);
        levelText.GetComponentInChildren<TextMeshProUGUI>().text = level.ToString();
        
    }
    
    IEnumerator CoroutineUpdate(float time)
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

    public void LevelUp()
    {
        level++;
        dmg += dmg;
        range += 3; 
        levelText.GetComponentInChildren<TextMeshProUGUI>().text = level.ToString();
    }

    public virtual void Attack(EnemyBlob blob)
    {
        var effect =Instantiate(attackEffect, position: pos, Quaternion.identity);
        effect.GetComponent<LineRenderer>().SetPositions(new Vector3[]{pos + new Vector2(0.5f, 0.5f), blob.transform.position});
        blob.hp -= dmg;
    }

    
}