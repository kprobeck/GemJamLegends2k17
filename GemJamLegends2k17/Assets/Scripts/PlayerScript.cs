using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public Faction faction;
    public Unit[] units;
    public bool isTurn;
    public Space[] capPoints;
    public int playerNum;
	// Use this for initialization
	void Start () {
	    //populate units array with runners, brutes, and specials of faction type
	}
	
	// Update is called once per frame
	void Update () {

        do
        {
            //check for active units
            //if none are left, call endTurn()
            //for(int i = 0; i < units.size(); i++)
            controllingUnits();

            //checks for turn skipping
            passTurn();

        } while (isTurn);
	}

    void controllingUnits()
    {
        //check for active units
        //pressing directions on joystick or d-pad searches through units array
        //if (left)
        //{ int i = 0, units[i - 1] }

        //if(right)
        //{ int i = 0, units[i+1] }

        //once player selects unit (hits interact button), call unitActions()
        unitActions();
    }

    void unitActions()
    {
        //check for button input
        //button corresponds to either move, attack, use abiity, or Pass(gem)
        //pressing back button stops selection
    }

    void passTurn()
    {
        //timer holds gametime
        //incremented every frame
        //if (button) is held for x time, call endTurn()
    }

    void endTurn()
    {
        isTurn = false;
    }
}
