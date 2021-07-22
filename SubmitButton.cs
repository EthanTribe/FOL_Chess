using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Text;

public class SubmitButton : MonoBehaviour
{
    public SolutionChecker solutionChecker;
    public Sentences sentences;

    private bool correct;

    Tuple<string, int, GameObject[]> feedback;
    public Text feedbackText;

    public GameObject sentenceHighlighter;
    public GameObject pieceHighlighter;

    public Canvas canvas;
    public GameObject panel;

    public AudioSource correctSound;
    public AudioSource incorrectSound;


    public void NextLevel (string currentLevel)
    {
        correct = solutionChecker.getCorrect() && sentences.getSatisfied();

        resetFeedback();

        if (correct)
        {
            feedbackText.text = "Correct!";
            correctSound.Play();
            StartCoroutine("ChangeScene");

            CompletedData.Instance.SetComplete(currentLevel[0], Convert.ToInt32(Char.GetNumericValue(currentLevel[1])), true);            
        }
        else
        {
            incorrectSound.Play();

            if (!solutionChecker.getCorrect())
            {
                Debug.Log("The chess pieces can attack each other, or are not on the board!");
                feedback = solutionChecker.getFeedback();
            }
            else
            {
                Debug.Log("The sentences are not satisfied!");
                feedback = sentences.getFeedback();                
            }

            feedbackText.text = feedback.Item1;
                       
            // Highlight sentence
            if (feedback.Item2 == 1)
            {
                sentenceHighlighter.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270.6f, 480f);                
            }
            else if(feedback.Item2 == 2)
            {
                sentenceHighlighter.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270.6f, 380f);
            }
            else if (feedback.Item2 == 3)
            {
                sentenceHighlighter.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270.6f, 280f);
            }

            if (feedback.Item2 > 0)
            {
                sentenceHighlighter.SetActive(true);
            }


            // Highlight piece(s)
            List<GameObject> pieceHighlights = new List<GameObject>();

            foreach (GameObject piece in feedback.Item3)
            {
                GameObject pieceHighlightInstance = GameObject.Instantiate(pieceHighlighter);
                pieceHighlightInstance.transform.SetParent(canvas.transform, false);
                pieceHighlights.Add(pieceHighlightInstance);
            }

            for (int i = 0; i < pieceHighlights.Count; i++)
            {
                pieceHighlights[i].GetComponent<RectTransform>().anchoredPosition = feedback.Item3[i].GetComponent<RectTransform>().anchoredPosition * panel.GetComponent<RectTransform>().localScale;
            }


            //StartCoroutine("Timer");
        }        
    }

    IEnumerator Timer()
    {
        for (float t = 0; t < 8f; t += Time.deltaTime)
        {
            yield return null;
        }
        feedbackText.text = "...";
        sentenceHighlighter.SetActive(false);
    }

    public void resetFeedback()
    {
        feedbackText.text = "...";
        sentenceHighlighter.SetActive(false);
        foreach (GameObject highlighter in GameObject.FindGameObjectsWithTag("Highlight"))
        {
            Destroy(highlighter);
        }
    }

    IEnumerator ChangeScene()
    {
        for (float t = 0; t < 1.4f; t += Time.deltaTime)
        {
            yield return null;
        }

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) // if last level go back to the level select menu otherwise go to the next level
        {
            SceneManager.LoadScene("Level Select");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
