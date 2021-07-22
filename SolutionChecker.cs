using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class SolutionChecker : MonoBehaviour
{
    [SerializeField] private PiecePositions pp;

    private GameObject[] pieces;
    private string[] positions;
    private List<List<string>> obstructions;

    [SerializeField] private bool correct;
    Tuple<string, int, GameObject[]> feedback; // (feedback string, sentence number in question, list of pieces involved)


    void Start()
    {
        pieces = pp.getPieces();
        positions = pp.getPositions();
        obstructions = pp.getObstructions();

        correct = false;
    }


    // First updates the positions and obstructions
    // then if a piece is not on the board the solution is not correct, return
    // otherwise check for each piece if its position is obstructed by any other pieces
    // if none are then the solution is correct
    void Update()
    {        
        positions = pp.getPositions();
        obstructions = pp.getObstructions();

        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i] == "none")
            {
                correct = false;
                GameObject[] feedbackPieces = { pieces[i] };
                feedback = Tuple.Create<string, int, GameObject[]>("This piece is not on the board", 0, feedbackPieces);
                return;
            }
        }

        for (int a = 0; a < positions.Length; a++)
        {
            for (int b = 0; b < positions.Length; b++)
            {
                if (b == a)
                {
                    continue;
                }

                if (obstructions[b].Contains(positions[a]))
                {
                    correct = false;
                    GameObject[] feedbackPieces = { pieces[a], pieces[b] };
                    feedback = Tuple.Create<string, int, GameObject[]>("These pieces can attack each other", 0, feedbackPieces);
                    return;
                }
            }
        }
        correct = true;
    }


    public bool getCorrect()
    {
        return correct;
    }

    public Tuple<string, int, GameObject[]> getFeedback()
    {
        return feedback;
    }
}
