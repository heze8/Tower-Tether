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
    public bool notAttackingBase;

    private NavMeshPath _navMeshPath;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshPath = new NavMeshPath();

        notAttackingBase = true;
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        navMeshAgent.SetDestination(new Vector3());
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateUpAxis = true;
        rb = GetComponent<Rigidbody>();
        var scale = (int) Math.Pow(2, level -1 );
        CheckRoute();
        EnemySpawningSystem.Instance.blobsSpawned.Add(this);
        transform.localScale = Vector3.one * scale;
        hp *= scale;
        dmg *= scale;
        startingHp = hp;
    }

    public void CheckRoute()
    {
        navMeshAgent.CalculatePath(Vector3.zero, _navMeshPath);
        navMeshAgent.SetPath(_navMeshPath);
        for (int i = 0; i < _navMeshPath.corners.Length - 1; i++)
            Debug.DrawLine(_navMeshPath.corners[i], _navMeshPath.corners[i + 1], Color.red);
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

    public static EnemyBlob SpawnBlob(Vector3 transformPosition, int level, int hp = 10)
    {
        var blob = Instantiate(EnemySpawningSystem.Instance.enemyPrefab, transformPosition, Quaternion.identity, parent: EnemySpawningSystem.Instance.transform);
        var enemyBlob = blob.GetComponent<EnemyBlob>();
        enemyBlob.level = level;
        enemyBlob.combined = false;
        enemyBlob.hp = hp;
        return enemyBlob;
    }


    public void OnTriggerEnter(Collider other)
    {

        var enemyBlob = other.gameObject.GetComponentInParent<EnemyBlob>();
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
    

    public void SetAttackingBase(DestroyableObject destroyableObject)
    {
        notAttackingBase = false;
        navMeshAgent.speed = 0;
        StartCoroutine(CoroutineUpdate(destroyableObject, 0));
    }
    IEnumerator CoroutineUpdate(DestroyableObject destroyableObject, float time)
    {
         destroyableObject.myHp -= dmg;
         destroyableObject.hp.SetHealth(destroyableObject.myHp);
         if (!destroyableObject)
         {
             navMeshAgent.speed = 3.5f;
         }
        yield return new WaitForSeconds(time);
        StartCoroutine(CoroutineUpdate(destroyableObject, GameManager.Instance.blobAttackRate));
    }
}
