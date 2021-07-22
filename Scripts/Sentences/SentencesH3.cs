using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesH3 : Sentences
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


    // ∀ x rook(x) ∨ king(x) ⇒ onWhite(x)
    private bool sentence1()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            int[] coordX = tileToCoord(positions[i]);

            if (pieces[i].ToString().Substring(0, 4) == "Rook" || pieces[i].ToString().Substring(0, 4) == "King")
            {
                if (onColour(coordX) != "white")
                {
                    GameObject[] feedbackPieces = { pieces[i] };
                    feedback = Tuple.Create<string, int, GameObject[]>("This piece is a rook or a king but is not on a white tile.", 1, feedbackPieces);
                    return false;
                }
            }
        }

        return true;
    }


    // ∀ x knight(x) ⇒ ∀ y ¬adjacent(x,y) ∨ ¬knight(y)
    private bool sentence2()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            int[] coordX = tileToCoord(positions[i]);

            if (pieces[i].ToString().Substring(0, 6) == "Knight")
            {
                for (int j = 0; j < positions.Length; j++)
                {
                    int[] coordY = tileToCoord(positions[j]);

                    if (adjacent(coordX,coordY) && pieces[j].ToString().Substring(0, 6) == "Knight")
                    {
                        GameObject[] feedbackPieces = { pieces[i] };
                        feedback = Tuple.Create<string, int, GameObject[]>("This knight is adjacent to a piece that is a knight.", 2, feedbackPieces);
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
