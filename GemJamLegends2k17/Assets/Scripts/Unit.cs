using UnityEngine;
using System.Collections;

public enum Faction
{
    FunGuys,
    SnowMen
}

public class Unit : MonoBehaviour {

    protected int hp, maxKO, dex, move, xPos, yPos;
    protected bool isMoved, abilityUsed, gemHeld;
    private Faction fact;

    //Needed Parameter for enum
    public Faction Fact
    {
        get
        {
            return fact;
        }
        set
        {
            fact = value;
        }
    }

    #region Methods

    //Moves the unit
    public void Move()
    {
        
    }

    //Takes an adjacent enemy Unit as parameter and deals damage to them
    public void Attack(Unit enemy)
    {

    }

    //Checks if space is moveable to
    //Takes x and y of the grid spot being attempted to move to
    //Returns true if within movement range, false if not within movement range
    public bool MovePossible(int x, int y)
    {
        return false;
    }

    //Passes gem to teammate
    //Takes teammate as parameter other
    //Returns true if pass successful, false if unsuccessful
    public bool Pass(Unit other)
    {
        return false;
    }

    //Method to be overloaded by children classes
    virtual public void Ability() { }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #endregion
}
