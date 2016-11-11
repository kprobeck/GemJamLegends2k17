using UnityEngine;
using System.Collections;

public class boardManager : MonoBehaviour {

    public GameObject[] children;
    public Space[,] spaces;
    public int activePlayer;
    public bool isPaused;
    public bool bothCapped;
	// Use this for initialization
	void Start () {
        spaces = new Space[9,9];
        isPaused = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public GameObject[] occupiedSpaces()
    {
        // GameObject [] ret;
        ArrayList ret = new ArrayList();
        int counter = 0;
        GameObject [] returnArray;
        for(int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (spaces[i, j].tag == "Occupied")
                {
                    counter++;
                    ret.Add(spaces[i, j]);
                }
            }
        }
        returnArray = new GameObject [counter];
        for(int i = 0; i < ret.Capacity - 1; i++)
        {
            Space s = ret[i] as Space;
            returnArray[i] = s.getOccupier;
        }

        return returnArray;
    }

    public GameObject[] koedUnits()
    {
        GameObject [] toParse = occupiedSpaces();
        ArrayList parser = new ArrayList();
        for(int i = 0; i < toParse.Length; i++)
        {
            
            ///cannot finish without units
        }
        GameObject[] parsedList = new GameObject[0];
        return parsedList;
    }

    public Space[] adjacentUnits(Space center)
    {
        Space[] adjacentSpots;
        ArrayList parser = new ArrayList();
        int counter = 0;
        int x = center.x;
        int y = center.y;

        if (x - 1 > 0)
        {
            parser.Add(spaces[x - 1, y]);
            counter++;
        }
        if(y - 1 > 0)
        {
            parser.Add(spaces[x, y - 1]);
            counter++;
        }
        if (x + 1 > 8)
        {
            parser.Add(spaces[x + 1, y]);
            counter++;
        }
        if (y + 1 > 8)
        {
            parser.Add(spaces[x, y + 1]);
            counter++;
        }

        adjacentSpots = new Space[counter];
        for (int i = 0; i < parser.Capacity - 1; i++)
        {
            Space s = parser[i] as Space;
            adjacentSpots[i] = s;
        }

        return adjacentSpots;
    }
    

}
