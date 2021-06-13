using System;

using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private EnemyBlob _blob;
    private void Start()
    {
        _blob = GetComponentInParent<EnemyBlob>();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        _blob.OnMyTriggerEnter(other);
    }
}
