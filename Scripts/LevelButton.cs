using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;


[DisallowMultipleComponent]
public class LevelButton : MonoBehaviour
{
    public Image[] buttonBackground;
    public TMP_Text[] levelNumber;

    private List<List<bool>> completed;
    private int[] lengths;


    void Start()
    {
        lengths = new int[4] { 4, 7, 7, 7 };

        completed = CompletedData.Instance.completed;

        for (int stage = 0; stage < 4; stage++)
        {
            for (int lvl = 0; lvl < completed[stage].Count; lvl++)
            {
                int i = lvl;
                if (stage > 0)
                {
                    for (int l = 0; l < stage; l++)
                    {
                        i = i + lengths[l];
                    }
                }
                //Debug.Log(completed[stage][lvl].ToString());
                if (completed[stage][lvl])
                {
                    complete(i);
                }
                else
                {
                    uncomplete(i);
                }
            }
        }

        // Print completed
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            foreach (bool b in completed[i])
            {
                sb.Append(b);
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }


    private void complete(int i)
    {
        //Debug.Log(i);        
        buttonBackground[i].color = new Color32(172, 114, 48, 255);
        levelNumber[i].color = new Color32(236, 179, 112, 255);
    }


    private void uncomplete(int i)
    {
        buttonBackground[i].color = new Color32(236, 179, 112, 255); 
        levelNumber[i].color = new Color32(172, 114, 48, 255);
    }
}
