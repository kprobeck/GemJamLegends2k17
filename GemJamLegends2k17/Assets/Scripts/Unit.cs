using UnityEngine;
using System.Collections;

public enum Faction
{
    FunGuys,
    SnowMen
}

public enum Type
{
    Runner,
    Brute,
    Special
}

public class Unit : MonoBehaviour
{

    protected int hp, maxKO, dex, move, xPos, yPos, koturn;
    protected bool isMoved = false, abilityUsed = false, gemHeld = false, isKOed = false, selected = false;
    protected Space currSpace;
    private Faction fact;
    private Type uType;
    private boardManager board;

    public Unit(Space starting, Faction group, Type unitType)
    {
        currSpace = starting;
        xPos = starting.getX;
        yPos = starting.getY;
        koturn = 0;
        fact = group;
        uType = unitType;
        switch (uType)
        {
            case Type.Runner:
                if (fact == Faction.FunGuys)
                {
                    hp = 4;
                    maxKO = 3;
                    dex = 1;
                }
                else
                {
                    hp = 3;
                    maxKO = 3;
                    dex = 2;
                }
                move = 3;
                break;
            case Type.Brute:
                hp = 6;
                maxKO = 2;
                dex = 1;
                move = 2;
                break;
            case Type.Special:
                if (fact == Faction.FunGuys)
                {
                    hp = 3;
                    maxKO = 2;
                    dex = 2;
                }
                else
                {
                    hp = 3;
                    maxKO = 3;
                    dex = 0;
                }
                move = 2;
                break;
        }
    }

    #region Parameters
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }

    public int MaxKO
    {
        get
        {
            return maxKO;
        }
    }

    public int Dex
    {
        get
        {
            return dex;
        }
    }

    public int Movement
    {
        get
        {
            return move;
        }
        set
        {
            move = value;
        }
    }

    public int XPos
    {
        get
        {
            return xPos;
        }
        set
        {
            xPos = value;
        }
    }

    public int YPos
    {
        get
        {
            return yPos;
        }
        set
        {
            yPos = value;
        }
    }

    public int KOturn
    {
        get
        {
            return koturn;
        }
        set
        {
            koturn = value;
        }
    }

    public bool IsMoved
    {
        get
        {
            return isMoved;
        }
        set
        {
            isMoved = value;
        }
    }

    public bool AbilityUsed
    {
        get
        {
            return abilityUsed;
        }
        set
        {
            abilityUsed = value;
        }
    }

    public bool GemHeld
    {
        get
        {
            return gemHeld;
        }
        set
        {
            gemHeld = value;
        }
    }

    public bool IsKOed
    {
        get
        {
            return isKOed;
        }
        set
        {
            isKOed = value;
        }
    }

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
        }
    }

    public Space CurrSpace
    {
        get
        {
            return currSpace;
        }
        set
        {
            currSpace = value;
        }
    }

    public Faction Fact
    {
        get
        {
            return fact;
        }
    }

    public Type UType
    {
        get
        {
            return uType;
        }
    }

    #endregion

    #region Methods

    //Moves the unit
    public void Move(Space moveSpace)
    {
        if (ActionPossible(moveSpace, move))
        {
            currSpace = moveSpace;
            xPos = moveSpace.getX;
            yPos = moveSpace.getY;
            isMoved = true;
        }
    }

    //Takes an adjacent enemy Unit as parameter and deals damage to them
    public void Attack()
    {
        /*Space spaceToCheck;
        if(xPos - 1 >= 0)
        {
            spaceToCheck = board.spaces[xPos - 1, yPos];
            if (spaceToCheck.isOccupied)
            {
                spaceToCheck.getOccupier.GetComponent<Unit>().TakeDamage();
            }
        }
        if(yPos - 1 >= 0)
        {
            spaceToCheck = board.spaces[xPos, yPos - 1];
            if (spaceToCheck.isOccupied)
            {
                spaceToCheck.getOccupier.GetComponent<Unit>().TakeDamage();
            }
        }
        if (xPos + 1 >= 0)
        {
            spaceToCheck = board.spaces[xPos + 1, yPos];
            if (spaceToCheck.isOccupied)
            {
                spaceToCheck.getOccupier.GetComponent<Unit>().TakeDamage();
            }
        }
        if (yPos + 1 >= 0)
        {
            spaceToCheck = board.spaces[xPos, yPos + 1];
            if (spaceToCheck.isOccupied)
            {
                spaceToCheck.getOccupier.GetComponent<Unit>().TakeDamage();
            }
        }*/
    }

    public void TakeDamage(int dam)
    {
        hp -= dam;
        if (hp == 0)
        {
            isKOed = true;
        }
    }

    //Replacement for MovePossible that allows to test various other actions
    //(Passing the gem and unit abilities, for example)
    //Checks the Manhattan distance between current location and given space against the passed in max distance
    //Returns false if out of 
    public bool ActionPossible(Space actSpace, int dist)
    {
        if ((Mathf.Abs(xPos - actSpace.getX) + (Mathf.Abs(yPos - actSpace.getY))) > dist)
            return false;
        return true;
    }

    //Passes gem to teammate
    //Takes teammate as parameter other
    //Returns true if pass successful, false if unsuccessful
    public bool Pass(Unit other)
    {
        if (other.Fact != this.fact)
        {
            other.TakeDamage(2);
            //Gem falls on random space
            //Waiting on RandomAdjacentSpace method in boardManager
        }
        else
        {
            if (other.AbilityUsed)
                return false;
            if (other.XPos == this.xPos + 1 || other.XPos == this.xPos - 1 || other.YPos == this.yPos + 1 || other.YPos == this.yPos - 1)
                return true;
            else
            {
                other.AbilityUsed = true;
                int succRate = Random.Range(0, (other.Dex + 7));
                if (succRate >= 5)
                    return true;
                //Gem falls on random space next to receiver
            }
        }
        return false;
    }

    public bool WakeUp()
    {
        if (koturn == maxKO)
        {
            koturn = 0;
            isKOed = false;
            return true;
        }
        else
        {
            int wakeChance = Random.Range(1, 7);
            if (wakeChance >= 5)
            {
                koturn = 0;
                isKOed = false;
                return true;
            }
            else
            {
                koturn++;
                return false;
            }
        }
    }

    //Method to be overloaded by children classes
    virtual public void Ability() { }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion
}