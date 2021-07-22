using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void SelectLevelByName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void SelectLevelByIndex(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
