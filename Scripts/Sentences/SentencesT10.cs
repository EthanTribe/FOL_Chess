using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesT10 : Sentences
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


    // ∃ x onColumn(x) = B
    private bool sentence1()
    {
        foreach (string pos in positions)
        {
            if (pos[0] == 'B')
            {
                return true;
            }
        }

        feedback = Tuple.Create<string, int, GameObject[]>("No pieces are on column B. ∃ means we need the statement to be true for at least on piece.", 1, null);
        return false;
    }


    // ∃ x onRow(x) = 3
    private bool sentence2()
    {
        foreach (string pos in positions)
        {
            if (pos[1] == '3')
            {
                return true;
            }
        }

        feedback = Tuple.Create<string, int, GameObject[]>("No pieces are on row 3. ∃ means we need the statement to be true for at least on piece.", 2, null);
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
