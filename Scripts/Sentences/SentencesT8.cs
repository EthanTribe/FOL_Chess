using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesT8 : Sentences
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


    // onColumn(<i>Boudica</i>) = B ∨ onColumn(<i>Boudica</i>) = D
    private bool sentence1()
    {
        if (positions[0][0] == 'B' || positions[0][0] == 'D')
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Boudica is not on column B or D. We need at least one of the sides of ∨ to be true.", 1, feedbackPieces);
            return false;
        }
    }


    // onRow(Boudica) = 3 ∨ onBlack(<i>Boudica</i>)
    private bool sentence2()
    {
        int[] coord = tileToCoord(positions[0]);

        if (positions[0][1] == '3' || onColour(coord) == "black")
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Boudica is not on row 3 or on a black tile. We need at least one of the sides of ∨ to be true.", 2, feedbackPieces);
            return false;
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
