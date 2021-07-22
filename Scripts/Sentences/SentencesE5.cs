using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesE5 : Sentences
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


    // ¬onBlack(Boudica)
    private bool sentence2()
    {
        int[] coord = tileToCoord(positions[0]);

        if (onColour(coord) != "black")
        {
            return true;
        }
        else
        {
            GameObject[] feedbackPieces = { pieces[0] };
            feedback = Tuple.Create<string, int, GameObject[]>("Boudica is not not on a black tile", 2, feedbackPieces);
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
