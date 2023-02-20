using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameState : MonoBehaviour
{
    int itemCount = 0;

    public TextMeshProUGUI itemCounter;

    public void addItemCount()
    {
        itemCount++;
        itemCounter.text = "Collected " + itemCount;
    }
}
