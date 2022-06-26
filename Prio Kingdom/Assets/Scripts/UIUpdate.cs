using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpdate : MonoBehaviour
{
    public SourceCounter sc;
    public ResourceHandler rh;

    public TextMeshProUGUI worker4Mine;
    public TextMeshProUGUI worker4Farm;
    public TextMeshProUGUI cap4Storage;
    public TextMeshProUGUI foodGrowFaster;
    public TextMeshProUGUI treeGrowFaster;
    public TextMeshProUGUI powerBoost;
    public TextMeshProUGUI healthBoost;


    public void Worker4Mine()
    {
        int cost = 10 * (int)Math.Pow(2, sc.worker4Mine);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.worker4Mine += 1;
            sc.GetGold(cost);
            worker4Mine.text = (10 * Math.Pow(2, sc.worker4Mine)).ToString();
            rh.Worker4Mine();
        }
    }

    public void Worker4Farm()
    {
        int cost = 10 * (int)Math.Pow(2, sc.worker4Farm);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.worker4Farm += 1;
            sc.GetGold(cost);
            worker4Farm.text = (10 * Math.Pow(2, sc.worker4Farm)).ToString();
            rh.Worker4Farm();
        }
    }

    public void Cap4Storage()
    {
        int cost = 10 * (int)Math.Pow(2, sc.storageBoost);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.storageBoost += 1;
            sc.GetGold(cost);
            cap4Storage.text = (10 * Math.Pow(2, sc.storageBoost)).ToString();
            rh.Cap4Storage();
        }
    }

    public void FoodGrowFaster()
    {
        int cost = 10 * (int)Math.Pow(2, sc.foodGrowrate);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.foodGrowrate += 1;
            sc.GetGold(cost);
            foodGrowFaster.text = (10 * Math.Pow(2, sc.foodGrowrate)).ToString();
        }
    }

    public void TreeGrowFaster()
    {
        int cost = 10 * (int)Math.Pow(2, sc.treeGrowrate);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.treeGrowrate += 1;
            sc.GetGold(cost);
            treeGrowFaster.text = (10 * Math.Pow(2, sc.treeGrowrate)).ToString();
        }
    }

    public void Power4Slaves()
    {
        int cost = 10 * (int)Math.Pow(2, sc.powerBoost);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.powerBoost += 1;
            sc.GetGold(cost);
            powerBoost.text = (10 * Math.Pow(2, sc.powerBoost)).ToString();
            rh.Power4Slaves();
        }
    }

    public void Health4Slaves()
    {
        int cost = 10 * (int)Math.Pow(2, sc.healthBoost);
        if (sc.goldCount >= cost)
        {
            rh.GetSource("gold", cost);
            sc.healthBoost += 1;
            sc.GetGold(cost);
            healthBoost.text = (10 * Math.Pow(2, sc.healthBoost)).ToString();
            rh.Health4Slaves();
        }
    }


    public void UpdateReqUI()
    {
        worker4Mine.text = (10 * Math.Pow(2, sc.worker4Mine)).ToString();
        worker4Farm.text = (10 * Math.Pow(2, sc.worker4Farm)).ToString();
        cap4Storage.text = (10 * Math.Pow(2, sc.storageBoost)).ToString();
        foodGrowFaster.text = (10 * Math.Pow(2, sc.foodGrowrate)).ToString();
        treeGrowFaster.text = (10 * Math.Pow(2, sc.treeGrowrate)).ToString();
        powerBoost.text = (10 * Math.Pow(2, sc.powerBoost)).ToString();
        healthBoost.text = (10 * Math.Pow(2, sc.healthBoost)).ToString();
    }
}
