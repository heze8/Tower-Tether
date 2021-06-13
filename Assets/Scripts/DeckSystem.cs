using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DeckSystem : Singleton<DeckSystem>
{
    public GameObject CardPrefab;
    public GameObject Hand;
    public Text deckSize;
    public List<Tower> availableTowers;
    public List<Vector2Int> defineDeck;
    [HideInInspector]
    public List<Tower> deck;
    [HideInInspector]
    public List<CardDisplay> hand;

    public Tower currentTower;
    private GameObject cardDisplayObj;
    public void onClick()
    {
        DrawCardFromDeck();
    }

    public void Update()
    {
        deckSize.text = deck.Count.ToString();
        
    }

    public void Start()
    {
        deck = new List<Tower>();
        hand = new List<CardDisplay>();
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
        if (deck.Count == 0) return;

        if (GameManager.Instance.actionPoints.points <= 0)
        {
            return;
        }
        else
        {
            GameManager.Instance.actionPoints.points -= 1;
        }

        GameObject playerCard = Instantiate(CardPrefab, Hand.transform, false);
        var cardDisplay = playerCard.GetComponent<CardDisplay>();
        var card = Random.Range(0, deck.Count);
        cardDisplay.tower = deck[card];
        hand.Add( cardDisplay);
        deck.RemoveAt(card);
    }

    public void UseCard(CardDisplay cardDisplay)
    {
        if (currentTower) return;
        currentTower = cardDisplay.tower;
        hand.Remove(cardDisplay);
        deck.Add(Instantiate(currentTower));
        cardDisplayObj = cardDisplay.gameObject;
        cardDisplay.FollowMousePosition();
    }

    public void PlacedTower()
    {
        currentTower = null;
        Destroy(cardDisplayObj);
        cardDisplayObj = null;

    }
}
