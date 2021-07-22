using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesT4 : Sentences
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


    // onBlack(Ethan)
    private bool sentence1()
    {
        int[] coord;
        coord = tileToCoord(positions[0]);
        if ((coord[0] + coord[1]) % 2 == 1)
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Ethan is not on a black tile", 1, feedbackPieces);
            return false;
        }
    }


    // onBlack(Philippa)
    private bool sentence2()
    {
        int[] coord;
        coord = tileToCoord(positions[2]);
        if ((coord[0] + coord[1]) % 2 == 1)
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[2] };
            feedback = Tuple.Create<string, int, GameObject[]>("Philippa is not on a black tile", 2, feedbackPieces);
            return false;
        }
    }


    // onBlack(Max)
    private bool sentence3()
    {
        int[] coord;
        coord = tileToCoord(positions[1]);
        if ((coord[0] + coord[1]) % 2 == 1)
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[1] };
            feedback = Tuple.Create<string, int, GameObject[]>("Max is not on a black tile", 3, feedbackPieces);
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
