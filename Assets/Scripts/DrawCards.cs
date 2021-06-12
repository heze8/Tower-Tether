using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject Card1;
    public GameObject Hand;
    public void onClick()
    {
        GameObject playerCard = Instantiate(Card1, new Vector3(0, 0, 0), Quaternion.identity);
        playerCard.transform.SetParent(Hand.transform, false);
    }
}
