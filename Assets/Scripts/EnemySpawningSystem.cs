using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemySpawningSystem : Singleton<EnemySpawningSystem>
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineUpdate(0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoroutineUpdate(float time)
    {
        Vector3 enemyPos = Random.insideUnitCircle * GameManager.Instance.mapSize.x;
        yield return new WaitForSeconds(time);
        Instantiate(enemyPrefab, position: enemyPos, Quaternion.identity, transform);
        StartCoroutine(CoroutineUpdate(Random.value * 10f));
    }
}