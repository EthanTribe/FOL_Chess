using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesH5 : Sentences
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


    // ∃ x knight(x) ∧ onBlack(x)
    private bool sentence1()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            int[] xCoord = tileToCoord(positions[i]);

            if (pieces[i].ToString().Substring(0, 6) == "Knight" && onColour(xCoord) == "black")                
            {
                return true;
            }
        }

        feedback = Tuple.Create<string, int, GameObject[]>("There are no knights on a white tile.", 1, null);
        return false;
    }


    // ∃ x knight(x) ∧ onWhite(x) ∧ ∀ y ¬adjacent(x,y)
    private bool sentence2()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            int[] xCoord = tileToCoord(positions[i]);

            if (pieces[i].ToString().Substring(0, 6) == "Knight" && onColour(xCoord) == "white")
            {
                for (int j = 0; j < pieces.Length; j++)
                {
                    int[] yCoord = tileToCoord(positions[j]);

                    if (adjacent(xCoord, yCoord))
                    {
                        goto nextX;
                    }
                }
                return true;
            }
        nextX:
            continue;
        }

        feedback = Tuple.Create<string, int, GameObject[]>("There are no knights on a white tile that are adjacent to nothing.", 2, null);
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
