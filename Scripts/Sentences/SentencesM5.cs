using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesM5 : Sentences
{
    public PiecePositions pp;

    private GameObject[] pieces;
    private string[] positions;

    [SerializeField] private bool satisfied;
    Tuple<string, int, GameObject[]> feedback; // (feedback string, sentence number in question, list of pieces involved)


    void Start()
    {
        pieces = pp.getPieces();
        positions = pp.getPositions();

        satisfied = false;

        feedback = new Tuple<string, int, GameObject[]>(null, 0, null);
    }


    void Update()
    {
        positions = pp.getPositions();

        foreach (string pos in positions)
        {
            if (pos == "none")
            {
                satisfied = false;
                return;
            }
        }

        satisfied = sentence2();
    }


    // ∀ x ∀ y onRow(x) = onRow(y) ∧ x ≠ y ⇒ ¬bishop(x)
    private bool sentence2()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            for (int j = 0; j < positions.Length; j++)
            {
                if (positions[i][1] == positions[j][1] && i != j)
                {
                    if (pieces[i].ToString().Substring(0, 6) == "Bishop")
                    {
                        GameObject[] feedbackPieces = { pieces[i] };
                        feedback = Tuple.Create<string, int, GameObject[]>("This piece is on the same row as another piece but is a bishop.", 2, feedbackPieces);
                        return false;
                    }
                }
            }
        }

        return true;
    }


    public override bool getSatisfied()
    {        
        return satisfied;
    }

    public override Tuple<string, int, GameObject[]> getFeedback()
    {
        return feedback;
    }
}
