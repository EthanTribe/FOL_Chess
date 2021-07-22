using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesH6 : Sentences
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

        satisfied = sentence1() && sentence2() && sentence3();
    }


    // ∀ x onColumn(x) = A ⇔ knight(x)
    private bool sentence1()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i][0] == 'A')
            {
                if (pieces[i].ToString().Substring(0, 6) != "Knight")
                {
                    GameObject[] feedbackPieces = { pieces[i] };
                    feedback = Tuple.Create<string, int, GameObject[]>("This piece is on column A but is not a knight.", 1, feedbackPieces);
                    return false;
                }
            }
            else
            {
                if (pieces[i].ToString().Substring(0, 6) == "Knight")
                {
                    GameObject[] feedbackPieces = { pieces[i] };
                    feedback = Tuple.Create<string, int, GameObject[]>("This piece is a knight but is not on column A.", 1, feedbackPieces);
                    return false;
                }
            }
        }

        return true;
    }


    // ∀ x ∀ y ¬(onColumn(x) = onColumn(y)) ∨ x = y
    private bool sentence2()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            for (int j = 0; j < pieces.Length; j++)
            {
                if (positions[i][0] == positions[j][0] && i != j)
                {
                    GameObject[] feedbackPieces = { pieces[i], pieces[j] };
                    feedback = Tuple.Create<string, int, GameObject[]>("These distinct pieces are on the same column.", 2, feedbackPieces);
                    return false;
                }
            }
        }

        return true;
    }


    // ∀ x king(x) ⇒ onBlack(x) ⇔ onBlack(Gawain)
    private bool sentence3()
    {
        int[] kCoord = tileToCoord(positions[0]);
        int[] gCoord = tileToCoord(positions[1]);

        if (onColour(kCoord) == onColour(gCoord))
        {
            return true;
        }
        else
        {
            if (onColour(kCoord) == "black")
            {
                GameObject[] feedbackPieces = { pieces[0] };
                feedback = Tuple.Create<string, int, GameObject[]>("This king is on a black tile but Gawain is not.", 3, feedbackPieces);
                return false;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[0] };
                feedback = Tuple.Create<string, int, GameObject[]>("This king is not on a black tile but Gawain is.", 3, feedbackPieces);
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
