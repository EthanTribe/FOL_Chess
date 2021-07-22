using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    /*public GameObject popup;


    void Start()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        for (float t = 0; t < 8f; t += Time.deltaTime)
        {
            yield return null;
        }
        Popdown();
    }

    public void Popup()
    {
        popup.SetActive(true);
        //StartCoroutine("Timer");
    }

    public void Popdown()
    {
        popup.SetActive(false);
    }*/

    public void RecapTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}