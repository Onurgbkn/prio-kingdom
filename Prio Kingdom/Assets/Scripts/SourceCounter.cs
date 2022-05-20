using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceCounter : MonoBehaviour
{
    public int foodCount;

    public Text foodElem;

    public void AddFood(int count)
    {
        foodCount += count;
        foodElem.text = foodCount.ToString();
    }
}
