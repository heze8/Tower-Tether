using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Tower tower;
    
    public Text nameText;
    public Text descriptionText;
    public Text cost;
    public SpriteRenderer sprite;
    private bool follow;

    void Start()
    {
        sprite.sprite = tower.sprite;
        nameText.text = tower.cardName;
        descriptionText.text = "Level: " + tower.level + "\n" + tower.description;
        cost.text = tower.cost.ToString();
    }

    public void UseCard()
    {
        if (GameManager.Instance.actionPoints.points < tower.cost) return;
        GameManager.Instance.actionPoints.points -= tower.cost;
        DeckSystem.Instance.UseCard(this);
    }

    public void Update()
    {
        if (follow)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void FollowMousePosition()
    {
        transform.SetParent(null);
        follow = true;
    }
}
