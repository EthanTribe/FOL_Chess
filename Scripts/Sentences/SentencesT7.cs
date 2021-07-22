using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesT7 : Sentences
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


    // onRow(Boudica) = 3 ∧ onBlack(<i>Boudica</i>)
    private bool sentence1()
    {
        int[] coord = tileToCoord(positions[0]);

        if (positions[0][1] == '3')
        {          
            if (onColour(coord) == "black")
            {
                return true;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[0] };
                feedback = Tuple.Create<string, int, GameObject[]>("Boudica is on row 3 but is not on a black tile. We need both sides of ∧ to be true.", 1, feedbackPieces);
                return false;
            }            
        }
        else if (onColour(coord) == "black")
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Boudica is on a black tile but is not on row 3. We need both sides of ∧ to be true.", 1, feedbackPieces);
            return false;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Boudica is not on row 3 and is not on a black tile either. We need both sides of ∧ to be true.", 1, feedbackPieces);
            return false;
        }
    }


    // onColumn(Nero) = A ∧ onRow(Nero) = 2 
    private bool sentence2()
    {
        if (positions[1][0] == 'A')
        {
            if (positions[1][1] == '2')
            {
                return true;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[1] };
                feedback = Tuple.Create<string, int, GameObject[]>("Nero is on column A but is not on row 2. We need both sides of ∧ to be true.", 2, feedbackPieces);
                return false;
            }
        }
        else if (positions[1][1] == '2')
        {
            GameObject[] feedbackPieces = { pieces[1] };
            feedback = Tuple.Create<string, int, GameObject[]>("Nero is on row 2 but is not on column A. We need both sides of ∧ to be true.", 2, feedbackPieces);
            return false;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[1] };
            feedback = Tuple.Create<string, int, GameObject[]>("Nero is not on column A and is not on row 2 either. We need both sides of ∧ to be true.", 2, feedbackPieces);
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
