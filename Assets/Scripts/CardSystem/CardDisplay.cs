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
    void Start()
    {
        nameText.text = tower.cardName;
        descriptionText.text = tower.description;
        cost.text = tower.cost.ToString();
    }
}
