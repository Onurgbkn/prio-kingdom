using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SourceCounter : MonoBehaviour
{
    public int foodCount;
    public int woodCount;
    public int ironCount;
    public int copperCount;
    public int goldCount;

    public int nextWorkerCost;
    public int worker4Mine;
    public int worker4Farm;
    public int storageBoost;
    public int treeGrowrate;
    public int foodGrowrate;

    public int powerBoost;
    public int healthBoost;
    public int raidCount;
    public int tutoCount;

    public TextMeshProUGUI foodElem;
    public TextMeshProUGUI woodElem;
    public TextMeshProUGUI ironElem;
    public TextMeshProUGUI copperElem;
    public TextMeshProUGUI goldElem;

    public TutorialHandler th;

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

    public void GetFood(int count)
    {
        foodCount -= count;
        foodElem.text = foodCount.ToString();
    }

    public void GetWood(int count)
    {
        woodCount -= count;
        woodElem.text = woodCount.ToString();
    }

    public void GetIron(int count)
    {
        ironCount -= count;
        ironElem.text = ironCount.ToString();
    }

    public void GetCopper(int count)
    {
        copperCount -= count;
        copperElem.text = copperCount.ToString();
    }

    public void GetGold(int count)
    {
        goldCount -= count;
        goldElem.text = goldCount.ToString();
    }

    public void UpdateUI()
    {
        AddFood(0);
        AddWood(0);
        AddIron(0);
        AddCopper(0);
        AddGold(0);
    }

    public void StartTuto()
    {
        th = GetComponent<TutorialHandler>();
        th.TutoStarts();
    }
}
