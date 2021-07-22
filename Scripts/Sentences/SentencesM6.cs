using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesM6 : Sentences
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


    // ∀ x knight(x) ⇒ ∃ y onRow(x) = onRow(y) ∧ x ≠ y
    private bool sentence2()
    {
        if (positions[1][1] == positions[2][1] )
        {
            if (positions[1][1] == positions[3][1])
            {
                return true;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[3] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is a knight by there is not another piece on the same row.", 2, feedbackPieces);
                return false;
            }
        }
        else
        {
            if (positions[1][1] == positions[3][1])
            {
                GameObject[] feedbackPieces = { pieces[2] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is a knight by there is not another piece on the same row.", 2, feedbackPieces);
                return false;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[1] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is a knight by there is not another piece on the same row.", 2, feedbackPieces);
                return false;
            }            
        }
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
