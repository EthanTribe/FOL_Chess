using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesT5 : Sentences
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

        satisfied = sentence2();
    }


    // adjacent(Milton,Palmerin)
    private bool sentence2()
    {
        if (adjacent(tileToCoord(positions[1]), tileToCoord(positions[2])))
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[1], pieces[2] };
            feedback = Tuple.Create<string, int, GameObject[]>("Milton and Palmerin are not next to each other.", 2, feedbackPieces);
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
