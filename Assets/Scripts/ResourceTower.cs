using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu]
public class ResourceTower : Tower
{
    public float resourceFreq;
    public GameObject resourceEffect;
    public int resourcePerFreq;
    public override void Start()
    {
        GridManager.Instance.StartCoroutine(CoroutineUpdate(resourceFreq));
    }
    public override IEnumerator CoroutineUpdate(float time)
    {
        Debug.Log("reousurce");

        yield return new WaitForSeconds(time);
        GenerateResource(); 
        
        GridManager.Instance.StartCoroutine(CoroutineUpdate(resourceFreq));
    }

    private void GenerateResource()
    {
        var effect = Instantiate(resourceEffect, pos, Quaternion.identity);
        GameManager.Instance.actionPoints.points += resourcePerFreq;
        Destroy(effect, resourceFreq - .5f);
    }
}