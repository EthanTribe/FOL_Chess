using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using System.Text;

public class SubmitButtonT2 : MonoBehaviour
{
    public SolutionCheckerT2 solutionChecker;
    public Sentences sentences;

    private bool correct;

    Tuple<string, int, GameObject[]> feedback;
    public TMP_Text feedbackText;

    public GameObject sentenceHighlighter;
    public GameObject pieceHighlighter;

    public Canvas canvas;
    public GameObject panel;

    public AudioSource correctSound;
    public AudioSource incorrectSound;


    public void NextLevel ()
    {
        correct = solutionChecker.getCorrect() && sentences.getSatisfied();

        resetFeedback();

        if (correct)
        {
            feedbackText.text = "Correct!";
            correctSound.Play();
            StartCoroutine("ChangeScene");

            CompletedData.Instance.SetComplete(sentences.ToString()[21], Convert.ToInt32(Char.GetNumericValue(sentences.ToString()[22])), true);
            // Print completed
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                foreach (bool b in CompletedData.Instance.completed[i])
                {
                    sb.Append(b);
                }
                sb.AppendLine();
            }
            Debug.Log(sb.ToString());
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
                sentenceHighlighter.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270.6f, 456.7f);                
            }
            else if(feedback.Item2 == 2)
            {
                sentenceHighlighter.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270.6f, 353f);
            }
            else if (feedback.Item2 == 3)
            {
                sentenceHighlighter.GetComponent<RectTransform>().anchoredPosition = new Vector2(-270.6f, 246f);
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
