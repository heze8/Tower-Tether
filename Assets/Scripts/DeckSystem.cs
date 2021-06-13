using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckSystem : MonoBehaviour
{
    public GameObject CardPrefab;
    public GameObject Hand;

    public List<Tower> availableTowers;
    public List<Vector2Int> defineDeck;
    [HideInInspector]
    public List<Tower> deck;
    [HideInInspector]
    public List<Tower> hand;

    public void onClick()
    {
        DrawCardFromDeck();
       
    }

    public void Start()
    {
        deck = new List<Tower>();
        hand = new List<Tower>();
        foreach (var towerCount in defineDeck)
        {
            for (int i = 0; i < towerCount.y; i++)
            {
                deck.Add(Instantiate(availableTowers[towerCount.x]));
            }
        }
    }

    private void DrawCardFromDeck()
    {
        GameObject playerCard = Instantiate(CardPrefab, Hand.transform, false);
        var cardDisplay = playerCard.GetComponent<CardDisplay>();
        var card = Random.Range(0, deck.Count);
        cardDisplay.tower = deck[card];
        hand.Add( deck[card]);
        deck.RemoveAt(card);
    }
}
