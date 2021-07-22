using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class PiecePositions : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private int xDimension;
    [SerializeField] private int yDimension;
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject board;

    private Vector2 boardPos;
    private GameObject[,] tiles2d; // Not needed?
    private string[] positions;

    private List<List<string>> obstructions;


    void Start()
    {
        boardPos = board.GetComponent<RectTransform>().anchoredPosition;

        positions = new string[pieces.Length];        
        tiles2d = new GameObject[yDimension, xDimension];

        // Make the tiles into a 2D array
        int counter = 0;
        for (int j = 0; j < xDimension; j++)
        {
            for (int i = yDimension - 1; i >= 0; i--)
            {
                tiles2d[i, j] = tiles[counter];
                counter += 1;
            }
        }

        // Print tiles2d
        /*StringBuilder sb = new StringBuilder();
        for (int i = 0; i < yDimension; i++)
        {
            for (int j = 0; j < xDimension; j++)
            {
                sb.Append(tiles2d[i, j].ToString());
                sb.Append(' ');
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());*/

        obstructions = new List<List<string>>();
        foreach (GameObject p in pieces)
        {
            obstructions.Add(new List<string> { });
        }
    }


    void Update()
    {
        // For each piece see if it is on any tiles if so set position to said tile if it is not on any tiles set the position to "none"
        for (int i = 0; i < pieces.Length; i++)
        {
            bool onBoard = false;
            Vector2 piecePos = pieces[i].GetComponent<RectTransform>().anchoredPosition - boardPos;
            foreach (GameObject tile in tiles)
            {
                Vector2 tilePos = tile.GetComponent<RectTransform>().anchoredPosition;

                if (piecePos == tilePos)
                {
                    positions[i] = tile.ToString().Substring(0, 2);
                    onBoard = true;
                }
            }
            if (!onBoard)
            {
                positions[i] = "none";
            }
        }

        // Print positions
        /*StringBuilder sb = new StringBuilder();
        for (int i = 0; i < positions.Length; i++)
        {
            sb.Append(positions[i].ToString());
        }
        Debug.Log(sb.ToString());*/

        obstructions = createObstructions(obstructions);
    }


    private List<List<string>> createObstructions(List<List<string>> obs)
    {
        // For each piece if the piece is not on the board reset its obstructions
        // if the piece hasn't moved since last update don't change obstructions
        // otherwise depending on the piece's class update what tiles the piece obstructs

        List<List<string>> newObs = new List<List<string>>(obs);

        bool dontPrint = true;

        for (int i = 0; i < pieces.Length; i++)
        {
            int[] pieceCoord = tileToCoord(positions[i]);

            if (pieceCoord[0] == -100)
            {
                newObs[i] = new List<string>();
                continue;
            }

            if (!pieces[i].transform.hasChanged)
            {
                continue;
            }
            else
            {
                pieces[i].transform.hasChanged = false;

            }

            List<string> obsI = new List<string>();            

            dontPrint = false;

            if (pieces[i].ToString().Substring(0, 4) == "Pawn")                
            {
                obsI.Add(positions[i]);
                for (int r = 0; r < yDimension; r++)
                {
                    for (int c = 0; c < xDimension; c++)
                    {
                        if (pieceCoord[0] - r == 1 && Math.Abs(pieceCoord[1] - c) == 1)
                        {
                            int[] coord = { r, c };
                            obsI.Add(coordToTile(coord));
                        }
                        tileToCoord(positions[i]);
                    }
                }
            }

            else if (pieces[i].ToString().Substring(0, 4) == "Rook")
            {
                for (int r = 0; r < yDimension; r++)
                {
                    for (int c = 0; c < xDimension; c++)
                    {
                        if (pieceCoord[0] == r || pieceCoord[1] == c)
                        {
                            int[] coord = { r, c };
                            obsI.Add(coordToTile(coord));
                        }
                        tileToCoord(positions[i]);
                    }
                }
            }

            else if (pieces[i].ToString().Substring(0,6) == "Knight")
            {
                obsI.Add(positions[i]);
                for (int r = 0; r < yDimension; r++)
                {
                    for (int c = 0; c < xDimension; c++)
                    {
                        if ((Math.Abs(pieceCoord[0] - r) == 1 && Math.Abs(pieceCoord[1] - c) == 2) || (Math.Abs(pieceCoord[0] - r) == 2 && Math.Abs(pieceCoord[1] - c) == 1))
                        {
                            int[] coord = { r, c };
                            obsI.Add(coordToTile(coord));
                        }
                        tileToCoord(positions[i]);
                    }
                }
            }

            else if (pieces[i].ToString().Substring(0, 6) == "Bishop")
            {
                for (int r = 0; r < yDimension; r++)
                {
                    for (int c = 0; c < xDimension; c++)
                    {
                        if (pieceCoord[0] - pieceCoord[1] == r - c || pieceCoord[0] + pieceCoord[1] == r + c)
                        {
                            int[] coord = { r, c };
                            obsI.Add(coordToTile(coord));
                        }
                        tileToCoord(positions[i]);
                    }
                }
            }

            else if (pieces[i].ToString().Substring(0, 5) == "Queen")
            {
                for (int r = 0; r < yDimension; r++)
                {
                    for (int c = 0; c < xDimension; c++)
                    {
                        if (pieceCoord[0] - pieceCoord[1] == r - c || pieceCoord[0] + pieceCoord[1] == r + c || pieceCoord[0] == r || pieceCoord[1] == c)
                        {
                            int[] coord = { r, c };
                            obsI.Add(coordToTile(coord));
                        }
                        tileToCoord(positions[i]);
                    }
                }
            }

            else if (pieces[i].ToString().Substring(0, 4) == "King")
            {
                for (int r = 0; r < yDimension; r++)
                {
                    for (int c = 0; c < xDimension; c++)
                    {
                        if (Math.Abs(pieceCoord[0] - r) <= 1 && Math.Abs(pieceCoord[1] - c) <= 1)
                        {
                            int[] coord = { r, c };
                            obsI.Add(coordToTile(coord));
                        }
                        tileToCoord(positions[i]);
                    }
                }
            }

            newObs[i] = obsI;
        }

        // Print obstructions
        if (!dontPrint)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pieces.Length; i++)
            {
                foreach (string ob in newObs[i])
                {
                    sb.Append(ob);
                }
                sb.AppendLine();
            }
            Debug.Log(sb.ToString());
        }

        return newObs;
    }


    // Compare two lists of lists for equality -- UNUSED
    private bool listOfListsEquality<T>(List<List<T>> l1, List<List<T>> l2)
    {
        if (l1.Count != l2.Count)
        {
            return false;
        }

        for (int i = 0; i < l1.Count; i++)
        {
            if (l1[i] != l2[i])
            {
                return false;
            }
        }
        return true;
    }


    // Converts the tile name to a coordinate in tiles2d e.g. "A3" -> [1,0]
    private int[] tileToCoord(string s)
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


    private string coordToTile(int[] coord)
    {
        string result = "";
        if (coord[0] == -100)
        {
            result = "none";
            return result;
        }
        int x = coord[1] + 65;
        int y = 49 - (coord[0] - 3);
        result += (char) x;
        result += (char) y;
        return result;
    }


    // Accessor for pieces
    public GameObject[] getPieces()
    {
        return pieces;
    }


    // Accessor for piece positions
    public string[] getPositions()
    {
        return positions;
    }


    // Accessor for obstructed tiles
    public List<List<string>> getObstructions()
    {
        return obstructions;
    }


}
