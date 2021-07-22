using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesH4 : Sentences
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


    // ∀ x onWhite(x) ⇔ queen(x)
    private bool sentence1()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            int[] coordX = tileToCoord(positions[i]);

            if (onColour(coordX) == "white")                
            {
                if (pieces[i].ToString().Substring(0, 5) != "Queen")
                {
                    GameObject[] feedbackPieces = { pieces[i] };
                    feedback = Tuple.Create<string, int, GameObject[]>("This piece is on a white tile but is not a queen.", 1, feedbackPieces);
                    return false;
                }
            }
            else
            {
                if (pieces[i].ToString().Substring(0, 5) == "Queen")
                {
                    GameObject[] feedbackPieces = { pieces[i] };
                    feedback = Tuple.Create<string, int, GameObject[]>("This queen is not on a white tile.", 1, feedbackPieces);
                    return false;
                }
            }
        }

        return true;
    }


    // ∀ x pawn(x) ⇔ ∃ y adjacent(x,y) ∧ rook(y)
    private bool sentence2()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            int[] xCoord = tileToCoord(positions[i]);

            if (pieces[i].ToString().Substring(0, 4) == "Pawn")
            {               
                for (int j = 0; j < pieces.Length; j++)
                {
                    int[] yCoord = tileToCoord(positions[j]);

                    if (adjacent(xCoord, yCoord) && pieces[j].ToString().Substring(0, 4) == "Rook")
                    {
                        goto nextX;
                    }
                }

                GameObject[] feedbackPieces = { pieces[i] };
                feedback = Tuple.Create<string, int, GameObject[]>("This pawn has no adjacent rooks.", 2, feedbackPieces);
                return false;
            }
            else
            {
                for (int j = 0; j < pieces.Length; j++)
                {
                    int[] yCoord = tileToCoord(positions[j]);

                    if (adjacent(xCoord, yCoord) && pieces[j].ToString().Substring(0, 4) == "Rook")
                    {
                        GameObject[] feedbackPieces = { pieces[i] };
                        feedback = Tuple.Create<string, int, GameObject[]>("This piece is adjacent to a rook but is not a pawn.", 2, feedbackPieces);
                        return false;
                    }
                }
            }
        nextX:
            continue;
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
