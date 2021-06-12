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
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(new Vector3());
        var scale = (int) Math.Pow(2, level -1 );
        transform.localScale = Vector3.one * scale;
        hp *= scale;
        combined = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private static EnemyBlob SpawnBlob(Vector3 transformPosition, int level)
    {
        var blob = Instantiate(EnemySpawningSystem.Instance.gameObject, transformPosition, Quaternion.identity, parent: EnemySpawningSystem.Instance.transform);
        return blob.GetComponent<EnemyBlob>();
    }

    public void OnMyTriggerEnter(Collider other)
    {
        var enemyBlob = other.gameObject.GetComponentInParent<EnemyBlob>();
        Debug.Log(other.gameObject);
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
}
