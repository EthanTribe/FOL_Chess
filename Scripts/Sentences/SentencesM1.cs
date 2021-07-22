using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesM1 : Sentences
{
    public PiecePositions pp;

    private GameObject[] pieces;
    private string[] positions;
    private List<List<string>> obstructions;

    [SerializeField] private bool satisfied;
    Tuple<string, int, GameObject[]> feedback; // (feedback string, sentence number in question, list of pieces involved)


    void Start()
    {
        pieces = pp.getPieces();
        positions = pp.getPositions();
        obstructions = pp.getObstructions();

        satisfied = false;

        feedback = new Tuple<string, int, GameObject[]>(null, 0, null);
    }


    void Update()
    {
        positions = pp.getPositions();
        obstructions = pp.getObstructions();

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


    // ∀ x rook(x) ⇒ ∃ y adjacent(x,y) ∧ rook(y)
    private bool sentence1()
    {
        bool aPieceIsAdjacent = false;

        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].ToString().Substring(0, 4) == "Rook")
            {
                int[] xCoord = tileToCoord(positions[i]);

                for (int j = 0; j < pieces.Length; j++)
                {                    
                    int[] yCoord = tileToCoord(positions[j]);

                    if (adjacent(xCoord, yCoord))
                    {
                        aPieceIsAdjacent = true;

                        if (pieces[j].ToString().Substring(0, 4) == "Rook")
                        {
                            goto nextX;
                        }
                    }
                }

                GameObject[] feedbackPieces = { pieces[i] };
                if (!aPieceIsAdjacent)
                {
                    feedback = Tuple.Create<string, int, GameObject[]>("No pieces are adjacent to this rook.", 1, feedbackPieces);
                }
                else
                {
                    feedback = Tuple.Create<string, int, GameObject[]>("No pieces adjacent to this rook are rooks.", 1, feedbackPieces);
                }
                return false;           
            }
        nextX:
            aPieceIsAdjacent = false;
            continue;
        }
        return true;
    }


    // ∃ y pawn(y) ∧ onRow(Arthur) = onRow(y)
    private bool sentence2()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].ToString().Substring(0, 4) == "King")
            {
                int[] arthurCoord = tileToCoord(positions[i]);

                for (int j = 0; j < pieces.Length; j++)
                {  
                    if (pieces[j].ToString().Substring(0, 4) == "Pawn")
                    {
                        int[] yCoord = tileToCoord(positions[j]);

                        if (arthurCoord[0] == yCoord[0])
                        {
                            return true;
                        }
                    }
                }
            }
        }
        
        feedback = Tuple.Create<string, int, GameObject[]>("No piece is on the same row as Arthur.", 2, null);
        return false;
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
