using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public abstract class Sentences : MonoBehaviour
{
    // Converts the tile name to a coordinate in tiles2d e.g. "A3" -> [1,0]
    public int[] tileToCoord(string s)
    {
        int[] result = new int[2];
        if (s == "none")
        {
            result[0] = -100;
            result[1] = -100;
            return result;
        }
        else
        {
            result[0] = 3 - (s[1] - 49);
            result[1] = s[0] - 65;
            return result;
        }        
    }


    // Returns if the coordinates are adjacent to each other or not
    public bool adjacent(int[] x, int[] y)
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


    // Returns the colour of the given coordinate
    public string onColour(int[] x)
    {
        if ( (x[0] + x[1]) % 2 == 0)
        {
            return "white";
        }
        else
        {
            return "black";
        }        
    }


    public abstract bool getSatisfied();

    public abstract Tuple<string, int, GameObject[]> getFeedback();
}
