using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial6Tooltips : MonoBehaviour
{
    public GameObject tooltip;
    public Text tipText;


    void Start()
    {
        tooltip.SetActive(false);
    }

    public void tip1()
    {
        if (tipText.text == "This sentence means there is a piece on row 3." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means there is a piece on row 3.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(585f, -130f);
        }        
    }

    public void tip2()
    {
        if (tipText.text == "This sentence means all pieces are on row 3." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means all pieces are on row 3.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(585f, -220f);
        }
    }
}
