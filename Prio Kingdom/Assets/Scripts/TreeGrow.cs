using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrow : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        for (float scale = 0.1f; scale <= 1; scale+=0.05f)
        {
            transform.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForSeconds(5f); // 5f for 90 sec
        }
        GetComponent<Resource>().enabled = true;
    }
}
