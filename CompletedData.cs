using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedData : MonoBehaviour
{
    public static CompletedData Instance;

    public List<List<bool>> completed;
    private Dictionary<char, int> stageDict;


    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        completed = new List<List<bool>>();

        List<bool> completedT = new List<bool>()
            { true, false, false, false };
        List<bool> completedE = new List<bool>()
            { false, false, false, false, false, false, false };
        List<bool> completedM = new List<bool>()
            { false, false, false, false, false, false, false };
        List<bool> completedH = new List<bool>()
            { false, false, false, false, false, false, false };

        completed.Add(completedT);
        completed.Add(completedE);
        completed.Add(completedM);
        completed.Add(completedH);

        stageDict = new Dictionary<char, int>()
        {
            {'T', 0}, {'E', 1}, {'M', 2}, {'H', 3}
        };
    }


    public void SetComplete(char stage, int levelNum, bool done)
    {
        completed[stageDict[stage]][levelNum - 1] = done;
    }
}
