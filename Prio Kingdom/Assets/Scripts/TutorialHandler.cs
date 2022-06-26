using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{

    SourceCounter sc;

    public List<GameObject> tutoHints;
    void Start()
    {
        sc = GetComponent<SourceCounter>();
    }

    public void TutoStarts()
    {
        if (sc.tutoCount != 11)
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
        yield return new WaitForSeconds(2f);
        if (sc.tutoCount != 11)
        {
            tutoHints[sc.tutoCount].SetActive(true);
        }
    }
}
