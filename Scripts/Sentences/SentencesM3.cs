using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesM3 : Sentences
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

        satisfied = sentence1() && sentence2();
    }


    // ∃ x knight(x) ∧ onRow(x) = 3
    private bool sentence1()
    {
        if (positions[0][1] == '3')
        {
            return true;            
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("There are no knights on row 3.", 1, feedbackPieces);
            return false;
        }
    }


    // ∀ x ¬adjacent(Lancelot,x)
    private bool sentence2()
    {
        int[] coordL = tileToCoord(positions[0]);

        for (int i = 1; i < 3; i++)
        {
            int[] coord = tileToCoord(positions[i]);

            if (adjacent(coordL,coord))
            {
                GameObject[] feedbackPieces = { pieces[i] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is adjacent to Lancelot.", 2, feedbackPieces);
                return false;
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
