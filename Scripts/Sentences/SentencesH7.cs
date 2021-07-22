using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesH7 : Sentences
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


    // ∀ x rook(x) ⇔ onColumn(x) = C ∨ onRow(x) = 4
    private bool sentence1()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i][0] == 'C')
            {
                GameObject[] feedbackPieces = { pieces[i] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is on column C but is not a rook.", 1, feedbackPieces);
                return false;
            }
            else if (positions[i][1] == '4')
            {
                GameObject[] feedbackPieces = { pieces[i] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is on row 4 but is not a rook.", 1, feedbackPieces);
                return false;
            }
        }

        return true;
    }


    // ∀ x ∃ y pawn(y) ∧ ¬adjacent(x,y)
    private bool sentence2()
    {
        int[] pCoord = tileToCoord(positions[4]);

        for (int i = 0; i < pieces.Length; i++)
        {
            int[] xCoord = tileToCoord(positions[0]);

            if (adjacent(xCoord,pCoord))
            {
                GameObject[] feedbackPieces = { pieces[i] };
                feedback = Tuple.Create<string, int, GameObject[]>("For this piece there does not exist a non-adjacent pawn.", 2, feedbackPieces);
                return false;
            }
        }

        return true;
    }


    // ∃ x bishop(x) ∧ ∀ y ¬bishop(y) ∨ ¬adjacent(x,y)
    private bool sentence3()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].ToString().Substring(0, 6) == "Bishop")
            {
                int[] xCoord = tileToCoord(positions[i]);

                for (int j = 0; j < pieces.Length; j++)
                {
                    int[] yCoord = tileToCoord(positions[j]);

                    if (pieces[j].ToString().Substring(0, 6) == "Bishop" && adjacent(xCoord, yCoord))
                    {
                        goto nextX;                        
                    }
                }

                return true;
            }
        nextX:
            continue;
        }

        feedback = Tuple.Create<string, int, GameObject[]>("There are no bishops that are adjacent to no bishops.", 3, null);
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
