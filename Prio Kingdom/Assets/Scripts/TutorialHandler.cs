using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{

    SourceCounter sc;
    public RaidHandler rh;

    public List<GameObject> tutoHints;

    public void TutoStarts()
    {
        sc = GetComponent<SourceCounter>();
        if (sc.tutoCount != 20)
        {
            tutoHints[sc.tutoCount].SetActive(true);
        }
    }

    public void TutoDone()
    {
        StartCoroutine(SwapTuto());
    }

    IEnumerator SwapTuto()
    {
        tutoHints[sc.tutoCount++].SetActive(false);
        yield return new WaitForSeconds(1f);
        if (sc.tutoCount != 20)
        {
            tutoHints[sc.tutoCount].SetActive(true);
        }
    }

    public void Tuto18()
    {
        tutoHints[18].SetActive(false);
        rh.RaidStarted(1);
    }
}
