using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SourceCounter : MonoBehaviour
{
    public int foodCount;
    public int woodCount;
    public int ironCount;
    public int copperCount;
    public int goldCount;

    public TextMeshProUGUI foodElem;
    public TextMeshProUGUI woodElem;
    public TextMeshProUGUI ironElem;
    public TextMeshProUGUI copperElem;
    public TextMeshProUGUI goldElem;

    public void AddFood(int count)
    {
        foodCount += count;
        foodElem.text = foodCount.ToString();
    }

    public void AddWood(int count)
    {
        woodCount += count;
        woodElem.text = woodCount.ToString();
    }

    public void AddIron(int count)
    {
        ironCount += count;
        ironElem.text = ironCount.ToString();
    }

    public void AddCopper(int count)
    {
        copperCount += count;
        copperElem.text = copperCount.ToString();
    }

    public void AddGold(int count)
    {
        goldCount += count;
        goldElem.text = goldCount.ToString();
    }
}
