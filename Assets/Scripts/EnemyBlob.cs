using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBlob : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public int hp;
    [HideInInspector]
    public int startingHp;
    public int level;
    public healthBar healthBar;
    private Rigidbody rb;

    private bool combined = true;

    public int dmg = 1;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.SetDestination(new Vector3());
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateUpAxis = true;
        rb = GetComponent<Rigidbody>();
        var scale = (int) Math.Pow(2, level -1 );

        EnemySpawningSystem.Instance.blobsSpawned.Add(this);
        transform.localScale = Vector3.one * scale;
        hp *= scale;
        dmg *= scale;
        startingHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(navMeshAgent.nextPosition);
        if (hp < 0)
        {
            Destroy(gameObject);
            if (level > 1)
            {
                SpawnBlob(transform.position, level - 1).combined = false;
                SpawnBlob(transform.position, level - 1).combined = false;

            }
    
        }

        if (hp < startingHp)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.SetMaxHealth(startingHp);
            healthBar.SetHealth(hp);
        }
    }

    private void OnDestroy()
    {
        if (EnemySpawningSystem.Instance)
        {
            EnemySpawningSystem.Instance.blobsSpawned.Remove(this);
        }
    }

    public static EnemyBlob SpawnBlob(Vector3 transformPosition, int level)
    {
        var blob = Instantiate(EnemySpawningSystem.Instance.enemyPrefab, transformPosition, Quaternion.identity, parent: EnemySpawningSystem.Instance.transform);
        var enemyBlob = blob.GetComponent<EnemyBlob>();
        enemyBlob.level = level;
        enemyBlob.combined = false;

        return enemyBlob;
    }


    public void OnTriggerEnter(Collider other)
    {

        var enemyBlob = other.gameObject.GetComponentInParent<EnemyBlob>();
        Debug.Log(combined);
        if (enemyBlob && !combined)
        {
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
