using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSystem : MonoBehaviour
{
    public GameObject CardPrefab;
    public GameObject Hand;

    public List<Tower> deck;
    public List<Tower> hand;

    public void onClick()
    {
        DrawCardFromDeck();
       
    }

    private void DrawCardFromDeck()
    {
        GameObject playerCard = Instantiate(CardPrefab, Hand.transform, false);
        playerCard.GetComponent<CardDisplay>();
    }
}
