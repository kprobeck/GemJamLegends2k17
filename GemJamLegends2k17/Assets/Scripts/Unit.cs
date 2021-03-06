﻿using UnityEngine;
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

    protected int hp, maxKO, dex, move, startingMove, xPos, yPos, koturn;
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
                startingMove = 3;
                break;
            case Type.Brute:
                hp = 6;
                maxKO = 2;
                dex = 1;
                startingMove = 2;
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
                startingMove = 2;
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

    public int StartingMove
    {
        get
        {
            return startingMove;
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

    //Finds adjacent enemies and deals damage to them
    public void Attack()
    {
        Space[] spaces = board.adjacentUnits(this.currSpace);
        for(int i = 0; i < spaces.Length; i++)
        {
            if(spaces[i].isOccupied)
            {
                spaces[i].getOccupier.GetComponent<Unit>().TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int dam)
    {
        hp -= dam;
        if (hp <= 0)
        {
            isKOed = true;
            if(gemHeld)
            {
                this.gemHeld = false;
                this.currSpace.hasGem = false;
                Space land = board.RandomAdjacentSpace(currSpace);
                land.hasGem = true;
                if(land.isOccupied)
                {
                    land.getOccupier.GetComponent<Unit>().GemHeld = true;
                }
            }
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
    //Returns true if gem leaves this unit's possession, false if it doesn't
    public bool Pass(Unit other)
    {
        if (other.Fact != this.fact)
        {
            this.gemHeld = false;
            this.currSpace.hasGem = false;
            other.TakeDamage(2);
            Space land = board.RandomAdjacentSpace(other.CurrSpace);
            land.hasGem = true;
            if(land.isOccupied)
            {
                land.getOccupier.GetComponent<Unit>().GemHeld = true;
            }
        }
        else
        {
            if (other.AbilityUsed)
                return false;
            this.gemHeld = false;
            this.currSpace.hasGem = false;
            if (other.XPos == this.xPos + 1 || other.XPos == this.xPos - 1 || other.YPos == this.yPos + 1 || other.YPos == this.yPos - 1)
            {
                other.GemHeld = true;
                other.CurrSpace.hasGem = true;
            }
            else
            {
                other.AbilityUsed = true;
                int succRate = Random.Range(0, (other.Dex + 7));
                if (succRate >= 5)
                {
                    other.GemHeld = true;
                    other.CurrSpace.hasGem = true;
                }
                else
                {
                    Space land = board.RandomAdjacentSpace(other.CurrSpace);
                    land.hasGem = true;
                    if (land.isOccupied)
                    {
                        land.getOccupier.GetComponent<Unit>().GemHeld = true;
                    }
                }
            }
        }
        return true;
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

    //Does Ability specific to units of each Faction
    public void Ability(Space selectedSpace)
    {
        if (abilityUsed)
            return;
        switch(this.uType)
        {
            case Type.Runner:
                RunnerAbility(selectedSpace);
                break;
            case Type.Brute:
                BruteAbility(selectedSpace);
                break;
            case Type.Special:
                SpecialAbility(selectedSpace);
                break;
        }
    }

    private void RunnerAbility(Space selectedSpace)
    {
        switch(this.fact)
        {
            case Faction.FunGuys:
                if(ActionPossible(selectedSpace, 2) && selectedSpace.isOccupied)
                {
                    Unit other = selectedSpace.getOccupier.GetComponent<Unit>();
                    if(other.Fact != Faction.FunGuys)
                    {
                        if(other.Movement == other.StartingMove)
                        {
                            other.Movement--;
                            this.abilityUsed = true;
                        }
                    }
                }
                break;
            case Faction.SnowMen:
                if(ActionPossible(selectedSpace, 2) && selectedSpace.isOccupied)
                {
                    Unit other = selectedSpace.getOccupier.GetComponent<Unit>();
                    if(!other.IsKOed)
                    {
                        other.TakeDamage(1);
                        this.abilityUsed = true;
                    }
                }
                break;
        }
    }

    private void BruteAbility(Space selectedSpace)
    {
        switch(this.fact)
        {
            case Faction.FunGuys:
                int spaceX = selectedSpace.getX;
                int spaceY = selectedSpace.getY;
                if(spaceX != xPos && spaceY == yPos)
                {
                    if (spaceX > xPos)
                    {
                        for (int i = xPos; i < 9; i++)
                        {
                            if (board.spaces[i, yPos].isOccupied)
                            {
                                board.spaces[i, yPos].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                    }
                    else
                    {
                        for (int i = xPos; i > 0; i--)
                        {
                            if (board.spaces[i, yPos].isOccupied)
                            {
                                board.spaces[i, yPos].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                    }
                    abilityUsed = true;
                }
                else if(spaceX == xPos && spaceY != yPos)
                {
                    if(spaceY > yPos)
                    {
                        for (int i = yPos; i < 9; i++)
                        {
                            if (board.spaces[xPos, i].isOccupied)
                            {
                                board.spaces[xPos, i].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                    }
                    else
                    {
                        for (int i = yPos; i > 0; i--)
                        {
                            if (board.spaces[xPos, i].isOccupied)
                            {
                                board.spaces[xPos, i].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                    }
                    abilityUsed = true;
                }
                break;
            case Faction.SnowMen:
                break;
        }
    }

    private void SpecialAbility(Space selectedSpace)
    {
        switch(this.fact)
        {
            case Faction.FunGuys:
                Space[] spaces = board.adjacentAndDiagUnits(selectedSpace);
                for(int i = 0; i < spaces.Length; i++)
                {
                    if(spaces[i].isOccupied)
                    {
                        Unit en = spaces[i].getOccupier.GetComponent<Unit>();
                        en.TakeDamage(en.HP);
                    }
                }
                break;
            case Faction.SnowMen:
                if(ActionPossible(selectedSpace, 2))
                {
                    int spaceX = selectedSpace.getX;
                    int spaceY = selectedSpace.getY;
                    //Not on diagonal, x has changed
                    if(xPos != spaceX && yPos == spaceY)
                    {
                        //Direction is right
                        if(xPos < spaceX)
                        {
                            //Damages units up to two spaces away to the right
                            for(int i = xPos; i < xPos + 2 || i < 9; i++)
                            {
                                if (board.spaces[i, yPos].isOccupied)
                                    board.spaces[i, yPos].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                        else //Direction is left
                        {
                            //Damages units up to two spaces away
                            for (int i = xPos; i > xPos - 2 || i > 0; i--)
                            {
                                if (board.spaces[i, yPos].isOccupied)
                                    board.spaces[i, yPos].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                        Move(selectedSpace);
                        abilityUsed = true;
                    }
                    else if(xPos == spaceX && yPos != spaceY) //Not on diagonal, y has changed
                    {
                        //Direction is down
                        if (yPos < spaceY)
                        {
                            //Damages units up to two spaces away to the right
                            for (int i = yPos; i < yPos + 2 || i < 9; i++)
                            {
                                if (board.spaces[xPos, i].isOccupied)
                                    board.spaces[xPos, i].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                        else //Direction is up
                        {
                            //Damages units up to two spaces away
                            for (int i = yPos; i > yPos - 2 || i > 0; i--)
                            {
                                if (board.spaces[xPos, i].isOccupied)
                                    board.spaces[xPos, i].getOccupier.GetComponent<Unit>().TakeDamage(1);
                            }
                        }
                        Move(selectedSpace);
                        abilityUsed = true;
                    }
                }
                break;
        }
    }

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