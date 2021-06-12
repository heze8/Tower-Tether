using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlob : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public int hp;

    public int level;

    private bool combined = true;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.SetDestination(new Vector3());
        navMeshAgent.updatePosition = false;
        // navMeshAgent.updateUpAxis = true;
        var scale = (int) Math.Pow(2, level -1 );
        transform.localScale = Vector3.one * scale;
        hp *= scale;
        combined = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = navMeshAgent.nextPosition;
    }
    
    private static EnemyBlob SpawnBlob(Vector3 transformPosition, int level)
    {
        var blob = Instantiate(EnemySpawningSystem.Instance.enemyPrefab, transformPosition, Quaternion.identity, parent: EnemySpawningSystem.Instance.transform);
        var enemyBlob = blob.GetComponent<EnemyBlob>();
        enemyBlob.level = level;
        return enemyBlob;
    }


    public void OnTriggerEnter(Collider other)
    {
        var enemyBlob = other.gameObject.GetComponentInParent<EnemyBlob>();

        if (enemyBlob && !combined)
        {
            Debug.Log(enemyBlob);

            if (enemyBlob.level == level)
            {
                enemyBlob.combined = true;
                combined = true;
                Destroy(gameObject);
                Destroy(enemyBlob.gameObject);
                SpawnBlob((enemyBlob.transform.position + transform.position) / 2, level + 1);

            }
               
        }
    }

    public void OnMyTriggerEnter(Collider other)
    {
        
    }
}
