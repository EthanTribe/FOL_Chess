using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial5Tooltips : MonoBehaviour
{
    public GameObject tooltip;
    public Text tipText;


    void Start()
    {
        tooltip.SetActive(false);
    }

    public void tip1()
    {
        if (tipText.text == "This sentence simply means Arthur is on row 3." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence simply means Arthur is on row 3.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(530f, -55f);
        }        
    }

    public void tip2()
    {
        if (tipText.text == "This sentence means either Arthur is on row 3 or Corfe is or both are." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means either Arthur is on row 3 or Corfe is or both are.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(530f, -120f);
        }
    }

    public void tip3()
    {
        if (tipText.text == "This sentence means Arthur is on row 3 and Corfe is also on row 3." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means Arthur is on row 3 and Corfe is also on row 3.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(530f, -185f);
        }
    }

    public void tip4()
    {
        if (tipText.text == "This sentence means Arthur is not on row 3." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means Arthur is not on row 3.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(530f, -250f);
        }
    }

    public void tip5()
    {
        if (tipText.text == "This sentence means if Arthur is on row 3 then Corfe must also be on row 3. Notice that if Arthur is not on row 3 then it does not matter where Corfe is for the sentence to be true." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means if Arthur is on row 3 then Corfe must also be on row 3. Notice that if Arthur is not on row 3 then it does not matter where Corfe is for the sentence to be true.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(530f, -315f);
        }
    }

    public void tip6()
    {
        if (tipText.text == "This sentence means that whenever Arthur is on row 3 so is Corfe and vice versa. Notice that A ⇔ B means A ⇒ B and B ⇒ A." && tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
        else
        {
            tooltip.SetActive(true);
            tipText.text = "This sentence means that whenever Arthur is on row 3 so is Corfe and vice versa. Notice that A ⇔ B means A ⇒ B and B ⇒ A.";
            tooltip.GetComponent<RectTransform>().anchoredPosition = new Vector2(530f, -380f);
        }
    }
}
