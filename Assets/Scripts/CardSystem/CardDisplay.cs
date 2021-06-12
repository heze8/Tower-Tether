using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public TowerCard towerCard;

    public Text nameText;
    public Text descriptionText;
    public Text cost;
    void Start()
    {
        nameText.text = towerCard.name;
        descriptionText.text = towerCard.description;
        cost.text = towerCard.cost.ToString();
    }
}
