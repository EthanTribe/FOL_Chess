using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHighlighter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        for (float t = 0; t < 8f; t += Time.deltaTime)
        {
            yield return null;
        }
        Object.Destroy(this.gameObject);
    }
}
