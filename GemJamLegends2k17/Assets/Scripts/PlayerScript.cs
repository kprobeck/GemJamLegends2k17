using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    public Faction faction;
    public Unit[] units = new Unit[6];
    public bool isTurn;
    public int playerNum;
    GameManager gameMan;
    boardManager boardMan;
    private bool prevTurn;
    Unit type;

    // Use this for initialization
    void Start()
    {
        //populate units array with runners, brutes, and specials of faction type
        //3 runners, 2 brutes, 1 special
        //boardManager.Spaces[0,0]
        units[0] = new Unit(boardMan.spaces[0, 0], faction, Type.Runner);
        units[1] = new Unit(boardMan.spaces[0, 0], faction, Type.Runner);
        units[2] = new Unit(boardMan.spaces[0, 0], faction, Type.Runner);

        //units[0] = new Unit(Space(0, 0), faction, type.Type.runner); //runner 1
        //units[1] = new Unit(Space(0, 4), faction, type.Type.runner); //runner 2
        units[3] = new Unit(boardMan.spaces[0, 0], faction, Type.Brute);
        units[4] = new Unit(boardMan.spaces[0, 0], faction, Type.Brute);
        units[5] = new Unit(boardMan.spaces[0, 0], faction, Type.Special);
        prevTurn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameMan.currentPlayer == playerNum)
        {
            isTurn = true;
        }
        else
        {
            isTurn = false;
        }

        if (isTurn == true && isTurn != prevTurn)
        {
            for (int i = 0; i < units.Length; i++)
            {
                units[i].WakeUp();
            }

            //check for active units
            //if none are left, call endTurn()
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i].IsKOed == false)
                {
                    break;
                }
                endTurn();
            }
        }

        prevTurn = isTurn;
    }

    void endTurn()
    {
        isTurn = false;
    }
}