using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesE7 : Sentences
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


    // onRow(Boudica) = 3 ⇔ onBlack(<i>Boudica</i>)
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
                feedback = Tuple.Create<string, int, GameObject[]>("Boudica is on row 3 and is not on a black tile. The parts either side of ⇔ are not equal.", 1, feedbackPieces);
                return false;
            }
        }
        else if (onColour(coord) == "black")
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Boudica is not on row 3 and is on a black tile. The parts either side of ⇔ are not equal.", 1, feedbackPieces);
            return false;
        }
        else
        {
            return true;
        }
    }


    // ¬adjacent(<i>Boudica</i>,<i>Nero</i>) ⇔ onRow(<i>Boudica</i>) = 2
    private bool sentence2()
    {
        int[] coordB = tileToCoord(positions[0]);
        int[] coordN = tileToCoord(positions[1]);

        if (!adjacent(coordB, coordN))
        {
            if (positions[0][1] == '2')
            {
                return true;
            }
            else
            {
                GameObject[] feedbackPieces = { pieces[0] };
                feedback = Tuple.Create<string, int, GameObject[]>("Boudica and Nero are not adjacent but Boudica is not on row 2. The parts either side of ⇔ are not equal.", 2, feedbackPieces);
                return false;
            }
        }
        else
        {
            if (positions[0][1] == '2')
            {
                GameObject[] feedbackPieces = { pieces[0] };
                feedback = Tuple.Create<string, int, GameObject[]>("Boudica and Nero are not not adjacent but Boudica is on row 2. The parts either side of ⇔ are not equal.", 2, feedbackPieces);
                return false;
            }
            else
            {
                return true;
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
