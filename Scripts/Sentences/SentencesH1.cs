using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using System;

public class SentencesH1 : Sentences
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


    // ∀ x ¬∃ y adjacent(x,y)
    private bool sentence2()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            int[] coordX = tileToCoord(positions[i]);

            for (int j = i; j < positions.Length; j++)
            {
                int[] coordY = tileToCoord(positions[j]);

                if (adjacent(coordX, coordY))
                {
                    GameObject[] feedbackPieces = { pieces[i] };
                    feedback = Tuple.Create<string, int, GameObject[]>("This piece has a piece that is adjacent to it.", 2, feedbackPieces);
                    return false;
                }
            }
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
