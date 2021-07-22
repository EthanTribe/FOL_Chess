using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Variables : MonoBehaviour
{
    [SerializeField] private int x; // 0 is Corfe and 1 is Arthur
    [SerializeField] private int y;

    public Text[] predicates;
    public Text[] functions;

    public PiecePositions pp;

    private GameObject[] pieces;
    private string[] positions;


    void Start()
    {
        pieces = pp.getPieces();
        positions = pp.getPositions();
    }


    void Update()
    {
        positions = pp.getPositions();

        updatePredicate4();

        if (positions[x] == "none")
        {
            functions[0].text = "onRow(x) = null";
            functions[1].text = "onColumn(x) = null";
            functions[2].text = "onColour(x) = null";
        }
        else
        {
            functions[0].text = "onRow(x) = " + positions[x][0].ToString();
            functions[1].text = "onColumn(x) = " + positions[x][1].ToString();

            if ((positions[x][0] + positions[x][1]) % 2 == 0)
            {
                functions[2].text = "onColour(x) = black";
            }
            else
            {
                functions[2].text = "onColour(x) = white";
            }
        }        
    }

    public void changeX(int i)
    {
        x = i;

        if (x == 0)
        {
            predicates[1].text = "rook(x) = true";
            predicates[2].text = "king(x) = false";
        }
        else
        {
            predicates[1].text = "rook(x) = false";
            predicates[2].text = "king(x) = true";
        }        
    }
    

    public void changeY(int i)
    {
        y = i;

        updatePredicate4();
    }


    private void updatePredicate4()
    {
        if (positions[x] == "none" || positions[y] == "none")
        {
            predicates[3].text = "adjacent(x,y) = null";
        }
        else if (adjacent(tileToCoord(positions[x]), tileToCoord(positions[y])))
        {
            predicates[3].text = "adjacent(x,y) = true";
        }
        else
        {
            predicates[3].text = "adjacent(x,y) = false";
        }
    }


    private bool adjacent(int[] x, int[] y)
    {
        if (x[0] == y[0] && x[1] == y[1])
        {
            return false;
        }
        else
        {
            return Math.Abs(x[0] - y[0]) <= 1 && Math.Abs(x[1] - y[1]) <= 1;
        }
    }


    public int[] tileToCoord(string s)
    {
        int[] result = new int[2];
        if (s == "none")
        {
            result[0] = -100;
            result[1] = -100;
            return result;
        }
        result[0] = 3 - (s[1] - 49);
        result[1] = s[0] - 65;
        return result;
    }
}
