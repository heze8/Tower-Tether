using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemySpawningSystem : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
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

public class GameManager : Singleton<GameManager>
{
    public Vector2 mapSize = new Vector2(20, 20);
}
