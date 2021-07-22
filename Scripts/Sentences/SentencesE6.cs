using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesE6 : Sentences
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


    // ¬adjacent(Boudica,Nero) ⇒ onBlack(Boudica)
    private bool sentence1()
    {
        int[] coordB = tileToCoord(positions[0]);
        int[] coordN = tileToCoord(positions[1]);

        if (!adjacent(coordB, coordN))
        {
            if (onColour(coordB) == "black")
            {
                return true;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[0] };
                feedback = Tuple.Create<string, int, GameObject[]>("Boudica and Nero are not adjacent but Boudica is not on a black tile. The LHS of ⇒ does not imply the RHS.", 1, feedbackPieces);
                return false;
            }
        }
        else
        {
            return true;
        }
    }


    // onWhite(<i>Boudica</i>) ⇒ onRow(<i>Boudica</i>) = onRow(<i>Nero</i>)
    private bool sentence2()
    {
        int[] coordB = tileToCoord(positions[0]);
        int[] coordN = tileToCoord(positions[1]);

        if (onColour(coordB) == "white")
        {
            if (positions[0][1] == positions[1][1])
            {
                return true;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[0], pieces[1] };
                feedback = Tuple.Create<string, int, GameObject[]>("Boudica is on a white tile but her and Nero are not on the same row. The LHS of ⇒ does not imply the RHS.", 2, feedbackPieces);
                return false;
            }
        }
        else
        {
            return true;
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
