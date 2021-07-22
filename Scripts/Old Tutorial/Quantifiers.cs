using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Quantifiers : MonoBehaviour
{
    public Text[] bools;

    public PiecePositions pp;

    private GameObject[] pieces;
    private string[] positions;

    private bool A;
    private bool B;


    void Start()
    {
        pieces = pp.getPieces();
        positions = pp.getPositions();
    }


    void Update()
    {
        positions = pp.getPositions();

        A = positions[1][1] == '3'; // Arthur on 3
        B = positions[0][1] == '3'; // Corfe on 3

        if (A && B)
        {
            bools[0].text = "true";
            bools[1].text = "true";
        }
        else if (A && !B)
        {
            bools[0].text = "true";
            bools[1].text = "false";
        }
        else if (!A && B)
        {
            bools[0].text = "true";
            bools[1].text = "false";
        }
        else
        {
            bools[0].text = "false";
            bools[1].text = "false";
        }

    }
}
