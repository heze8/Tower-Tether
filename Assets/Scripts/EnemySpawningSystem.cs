using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Build.Content;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawningSystem : Singleton<EnemySpawningSystem>
{
    public GameObject enemyPrefab;
    public float difficulty = 1;
    public HashSet<EnemyBlob> blobsSpawned;
    // Start is called before the first frame update
    void Start()
    {
        blobsSpawned = new HashSet<EnemyBlob>();
        StartCoroutine(CoroutineUpdate(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoroutineUpdate(float time)
    {
        var insideUnitCircle = Random.insideUnitCircle;
        insideUnitCircle *= (1 / insideUnitCircle.magnitude);
        Vector3 enemyPos =  insideUnitCircle* GameManager.Instance.mapSize.x ;
        Debug.Log(enemyPos);
        yield return new WaitForSeconds(time/ difficulty);
        EnemyBlob.SpawnBlob( enemyPos, Mathf.RoundToInt(difficulty));
        StartCoroutine(CoroutineUpdate(Random.value * 10f));
    }

    public EnemyBlob GetNearestEnemy(Vector2 pos)
    {
        EnemyBlob closest = null;
        float min = math.INFINITY;
        foreach (var blob in blobsSpawned)
        {
            var f = ((Vector2) blob.transform.position - pos).sqrMagnitude;
            if (f < min)
            {
                min = f;
                closest = blob;
            }
        }

        return closest;
    }
}