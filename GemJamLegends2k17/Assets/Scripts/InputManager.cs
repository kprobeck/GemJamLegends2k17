using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputManager : MonoBehaviour {

    private string message1, message2;
    private bool trigDown;
    private bool stickInUse;
    private int xPos;
    private int yPos;
    public boardManager board;
    public GameManager game;
    private bool forceEnd;
    private const int NUM_UNITS = 6;
    private PlayerScript currPlayer;
    private bool turnStarted = true;
    public Screen currScreen;
    private Selection currPosition;
    public GameObject controls;
    private bool paused = false;
    private Unit selectedUnit;

    public GameObject runFGScreen;
    public GameObject bruteFGScreen;
    public GameObject specFGScreen;
    public GameObject runSPScreen;
    public GameObject bruteSPScreen;
    public GameObject specSPScreen;
    public Text HealthText;
    public Text TurnText;
    private Vector3 healthStartPos;
    private Vector3 turnStartPos;
    public enum Screen
    {
        Main,
        Game
    }

    public enum Selection
    {
        Start,
        Controls,
        Options
    }

    // Use this for initialization
    void Start () {
        message1 = "The Player ";                               //set message fragments
        message2 = ": Pressed: ";                                //for input debugging
        currPosition = Selection.Start;
        trigDown = false;                                       //makes sure that holding down the trigger doesn't cause constant calls.
        stickInUse = false;                                     //makes sure that holding down the D-Pad doesn't cause constant calls.
        xPos = 0;                                               //sets initial x position
        yPos = 0;                                               //sets initial y position
        forceEnd = true;                                        //bool to force the end of a player's turn if they have no more available actions
        currPlayer = game.p1;                                   //keeps a local reference to the active player's object
        board.selectedSpace = board.spaces[xPos*9 + yPos];      //sets the selected space's data so that it is trackable
        board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;      //sets the selected space's material to a distinct value
        selectedUnit = currPlayer.units[0].GetComponent<Unit>();
        healthStartPos = HealthText.transform.position;
        turnStartPos = TurnText.transform.position;
}
	
	// Update is called once per frame
	void Update () {
        switch (currScreen)
        {
            case Screen.Main:
                if (!paused)
                {
                    //Xbox then PlayStation
                    //Actual Buttons
                    //If you press A or X
                    if (Input.GetButtonDown("SelectP1") || Input.GetButtonDown("Jump"))
                    {
          
                        switch (currPosition)
                        {

                            case Selection.Start:
                                Application.LoadLevel("mainScene");
                                break;
                            case Selection.Controls:
                                paused = true;
                                controls.transform.Translate(new Vector3(0, 0, -3));
                                break;
                        }
                    }

                    //Axises Inputs (Sticks, DPad, Triggers)
                    //If you press Left or Right
                    if (Input.GetAxis("Up/DownP1") != 0 && stickInUse == false)
                    {
                        stickInUse = true;
                        if (Input.GetAxis("Up/DownP1") > 0)
                        {
                            switch (currPosition)
                            {
                                case Selection.Controls:
                                    currPosition = Selection.Start;
                                    game.gem.transform.position = new Vector3(-1.65f, 1.67f, -1.0f);
                                    break;
                                case Selection.Options:
                                    currPosition = Selection.Controls;
                                    game.gem.transform.position = new Vector3(-2.59f, -1.61f, -1.0f);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (currPosition)
                            {
                                case Selection.Start:
                                    currPosition = Selection.Controls;
                                    game.gem.transform.position = new Vector3(-2.59f, -1.61f, -1.0f);
                                    break;
                                case Selection.Controls:
                                    currPosition = Selection.Options;
                                    game.gem.transform.position = new Vector3(-5.16f, -4.63f, -1.0f);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    //check for axises reset
                    //Left/Right and Up/Down
                    if (Input.GetAxis("Left/RightP1") == 0 && Input.GetAxis("Up/DownP1") == 0 && stickInUse == true)
                    {
                        Debug.Log(message1 + currPlayer.playerNum + ": The stick is ready for new input");

                        //sets stickInUse to false so it is ready to accept new input
                        stickInUse = false;
                    
                    }
                }
                else
                {
                    if (Input.GetButtonDown("BackP1"))
                    {
                        controls.transform.Translate(new Vector3(0, 0, 3));
                        paused = false;
                    }
                }

                return;

            case Screen.Game:
                //reset forceEnd to true so that if the player has no more moves, their turn ends
                forceEnd = true;

                if (Input.GetButtonDown("Jump"))
                {
                    endTurn();
                }
                //check if its player 1's turn
                if (game.currentPlayer == 1)
                {
                    //set currPlayer to Game Manager's player 1
                    currPlayer = game.p1;
                    TurnText.text = "FunGuys";
                    //check if Player 1 has no more moves
                    for (int i = 0; i < NUM_UNITS; i++)
                    {
                        //if a unit hasn't moved, set forceEnd to false so that it doesn't force the end of the player's turn
                        if (currPlayer.units[i].GetComponent<Unit>().IsMoved == false)
                        {
                            forceEnd = false;
                        }
                    }

                    //runs for if it is player 1's turn to handle their actions
                    if (turnStarted == true)
                    {
                        //sets turnStarted to false so this code doesn't run again
                        turnStarted = false;

                        //set the selected unit's selected property to true, then move the selected space to the selected unit's space
                        currPlayer.units[0].GetComponent<Unit>().Selected = true;
                        selectedUnit = currPlayer.units[0].GetComponent<Unit>();
                        HealthText.text = selectedUnit.GetComponent<Unit>().hp.ToString();
                        //set the selected unit's selected property to true, then move the selected space to the selected unit's space
                        if (!currPlayer.units[0].GetComponent<Unit>().IsKOed)
                        {
                            getPossibleMovements();
                            moveSelectedSpace(currPlayer.units[0].GetComponent<Unit>().XPos - xPos, currPlayer.units[0].GetComponent<Unit>().YPos - yPos);
                        }
                        else
                        {
                            getNext(0);
                        }

                    }

                    //run the playerTurn() function to handle the input of player 1 
                    playerTurn();
                }

                //check if its player 1's turn
                if (game.currentPlayer == 2)
                {
                    //set currPlayer to Game Manager's player 2
                    currPlayer = game.p2;
                    TurnText.text = "Snow Patrol";
                    //check if Player 2 has no more moves
                    for (int i = 0; i < NUM_UNITS; i++)
                    {
                        //if a unit hasn't moved, set forceEnd to false so that it doesn't force the end of the player's turn
                        if (currPlayer.units[i].GetComponent<Unit>().IsMoved == false)
                        {
                            forceEnd = false;
                        }
                    }

                    //runs for if it is player 2's turn to handle their actions
                    if (turnStarted == true)
                    {
                        //sets turnStarted to false so this code doesn't run again
                        turnStarted = false;

                        //set the selected unit's selected property to true, then move the selected space to the selected unit's space
                        currPlayer.units[0].GetComponent<Unit>().Selected = true;
                        selectedUnit = currPlayer.units[0].GetComponent<Unit>();
                        HealthText.text = selectedUnit.GetComponent<Unit>().hp.ToString();
                        //set the selected unit's selected property to true, then move the selected space to the selected unit's space
                        if (!currPlayer.units[0].GetComponent<Unit>().IsKOed)
                        {
                            getPossibleMovements();
                            moveSelectedSpace(currPlayer.units[0].GetComponent<Unit>().XPos - xPos, currPlayer.units[0].GetComponent<Unit>().YPos - yPos);
                        }
                        else
                        {
                            getNext(0);
                        }
                    }

                    //run the playerTurn() function to handle the input of player 2
                    playerTurn();
                }

                //if the player didn't have a unit still available for action, force their turn to end
                if (forceEnd)
                {
                    endTurn();
                }

                return;
            default:
                break;
        }

        
    }

    //handles input during either player's turn
    void playerTurn()
    {
        if (!paused)
        {
            //Xbox then PlayStation
            //Actual Buttons
            //If you press A or X
            if (Input.GetButtonDown("SelectP" + currPlayer.playerNum))
            {
                //iterate through to find the selected unit, then move the unit to selected space
                for (int i = 0; i < NUM_UNITS; i++)
                {
                    if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
                    {
                        currPlayer.units[i].GetComponent<Unit>().Move(board.selectedSpace);
                    }
                }

                //call getNext() for the first time to select the next unit automatically
                getNext(0);
                getPossibleMovements();

                //change the texture of the new selectedSpace to be green
                board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;
            }

            //If you press B or Circle
            if (Input.GetButtonDown("BackP" + currPlayer.playerNum))
            {
                //No idea what the B button does as of now
                Debug.Log(message1 + currPlayer.playerNum + message2 + "B");
            }

            //If you press Y or Triangle
            if (Input.GetButtonDown("EndP" + currPlayer.playerNum))
            {
                //call endTurn() to end the current player's turn
                Debug.Log(message1 + currPlayer.playerNum + message2 + "Y");
                endTurn();
            }

            //If you press Left Bumper or L1
            if (Input.GetButtonDown("Prev UnitP" + currPlayer.playerNum))
            {
                //call getPrev for the first time, makes the selected unit the previous unit in the unit array
                getPrev(0);
                getPossibleMovements();

                //change the texture of the new selectedSpace to be green
                board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;
            }

            //If you press Right Bumper or R1
            if (Input.GetButtonDown("Next UnitP" + currPlayer.playerNum))
            {
                //call getNext for the first time, makes the selected unit the next unit in the unit array
                getNext(0);
                getPossibleMovements();

                //change the texture of the new selectedSpace to be green
                board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;
            }

            //If you press Back or Whatever PS calls Back
            if (Input.GetButtonDown("Game InfoP" + currPlayer.playerNum))
            {
                controls.GetComponent<SpriteRenderer>().enabled = true;
                paused = true;
                //will display the unit information screen later
                Debug.Log(message1 + currPlayer.playerNum + message2 + "Back");
            }

            //If you press Start or Whatever PS calls Start
            if (Input.GetButtonDown("PauseP" + currPlayer.playerNum))
            {
                //will pause the game later
                Debug.Log(message1 + currPlayer.playerNum + message2 + "Start");
            }

            //Axises Inputs (Sticks, DPad, Triggers)
            //If you press Left or Right
            if (Input.GetAxis("Left/RightP" + currPlayer.playerNum) != 0 && stickInUse == false)
            {
                Debug.Log(message1 + currPlayer.playerNum + message2 + "Left /Right");

                //set stickInUse to true so that no other input from the D-Pad can come in until the the D-Pad is released
                stickInUse = true;
                Debug.Log("X Value: " + Input.GetAxis("Left/RightP" + currPlayer.playerNum));

                //call moveSelectedSpace so that the selected space moves in the desired direction
                moveSelectedSpace((int)Input.GetAxis("Left/RightP" + currPlayer.playerNum), 0);
            }

            //If you press Up or Down
            if (Input.GetAxis("Up/DownP" + currPlayer.playerNum) != 0 && stickInUse == false)
            {
                Debug.Log(message1 + currPlayer.playerNum + message2 + "Up/Down");

                //set stickInUse to true so that no other input from the D-Pad can come in until the the D-Pad is released
                stickInUse = true;
                Debug.Log("Y Value: " + Input.GetAxis("Up/DownP" + currPlayer.playerNum));

                //call moveSelectedSpace so that the selected space moves in the desired direction
                moveSelectedSpace(0, (int)Input.GetAxis("Up/DownP" + currPlayer.playerNum));
            }

            //If you press Left/Right Trigger or L2/R2
            if (Input.GetAxis("Char InfoP" + currPlayer.playerNum) != 0 && trigDown == false)
            {
                //will display the chatacter's information
                Debug.Log(message1 + currPlayer.playerNum + ": One or more Triggers down");
                HealthText.transform.position += new Vector3(0, 1000, 0);
                TurnText.transform.position += new Vector3(0, 1000, 0);
                switch (selectedUnit.UType)
                {
                    case Type.Runner:
                        switch (selectedUnit.Fact)
                        {
                            case Faction.FunGuys:
                                runFGScreen.GetComponent<SpriteRenderer>().enabled = true;
                                break;
                            case Faction.SnowPatrol:
                                runSPScreen.GetComponent<SpriteRenderer>().enabled = true;
                                break;
                        }
                        break;
                    case Type.Brute:
                        switch (selectedUnit.Fact)
                        {
                            case Faction.FunGuys:
                                bruteFGScreen.GetComponent<SpriteRenderer>().enabled = true;
                                break;
                            case Faction.SnowPatrol:
                                bruteSPScreen.GetComponent<SpriteRenderer>().enabled = true;
                                break;
                        }
                        break;
                    case Type.Special:
                        switch (selectedUnit.Fact)
                        {
                            case Faction.FunGuys:
                                specFGScreen.GetComponent<SpriteRenderer>().enabled = true;
                                break;
                            case Faction.SnowPatrol:
                                specSPScreen.GetComponent<SpriteRenderer>().enabled = true;
                                break;
                        }
                        break;
                    default:
                        break;
                }


                //set trigDown to true so that no other input from the triggers can come in until the the trigger is released
                trigDown = true;
            }
            else if(trigDown == false)
            {
                HealthText.transform.position = healthStartPos;
                TurnText.transform.position = turnStartPos;
            }
            //check for axises reset
            //Left/Right and Up/Down
            if (Input.GetAxis("Left/RightP" + currPlayer.playerNum) == 0 && Input.GetAxis("Up/DownP" + currPlayer.playerNum) == 0 && stickInUse == true)
            {
                Debug.Log(message1 + currPlayer.playerNum + ": The stick is ready for new input");

                //sets stickInUse to false so it is ready to accept new input
                stickInUse = false;
            }

            //Triggers
            if (Input.GetAxis("Char InfoP" + currPlayer.playerNum) == 0 && trigDown == true)
            {
                Debug.Log(message1 + currPlayer.playerNum + ": Triggers ready for new input");

                hideScreens();

                //sets trigDown to false so it is ready to accept new input
                trigDown = false;
            }
        }
        else
        {
            //If you press Back or Whatever PS calls Back
            if (Input.GetButtonDown("Game InfoP" + currPlayer.playerNum))
            {
                hideScreens();
                paused = false;
                //will display the unit information screen later
                Debug.Log(message1 + currPlayer.playerNum + message2 + "Back");
            }
        }
    }

    void getNext(int numCalled)
    {
        //if this is the 7th time the method is called in a row, it has gone all the way through all the units in the array and has none that haven't been used, return
        if(numCalled == 6)
        {
            return;
        }

        //iterate through the array of units to find the selected unit
        for (int i = 0; i < NUM_UNITS; i++)
        {
            if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
            {
                //set the current selected unit to false so that we're sure only one unit is selected at a time
                currPlayer.units[i].GetComponent<Unit>().Selected = false;
                
                //if the selected unit is the last one in the array
                if(i == 5)
                {
                    //set the first unit's selected value to true
                    currPlayer.units[0].GetComponent<Unit>().Selected = true;
                    selectedUnit = currPlayer.units[0].GetComponent<Unit>();

                    //if the first unit has already been used, enter a recursive loop to find the next unit that hasn't been moved, then break out
                    if (currPlayer.units[0].GetComponent<Unit>().IsMoved  == true)
                    {
                        getNext(numCalled + 1);
                        getPossibleMovements();
                        break;
                    }

                    //move the selected space to the selected unit's location and break out
                    moveSelectedSpace(currPlayer.units[0].GetComponent<Unit>().XPos - xPos, currPlayer.units[0].GetComponent<Unit>().YPos - yPos);
                    break;
                }

                //if it's not the last, just set the next unit's selected value to true
                currPlayer.units[i + 1].GetComponent<Unit>().Selected = true;
                selectedUnit = currPlayer.units[i + 1].GetComponent<Unit>();
                HealthText.text = selectedUnit.GetComponent<Unit>().hp.ToString();
                //if the next unit has already been used, enter a recursive loop to find the next unit that hasn't been moved, then break out
                if (currPlayer.units[i + 1].GetComponent<Unit>().IsMoved == true)
                {
                    getNext(numCalled + 1);
                    getPossibleMovements();
                    break;
                }

                //move the selected space to the selected unit's location and break out
                moveSelectedSpace(currPlayer.units[i + 1].GetComponent<Unit>().XPos - xPos, currPlayer.units[i + 1].GetComponent<Unit>().YPos - yPos);
                break;
            }
        }
    }

    void getPrev(int numCalled)
    {
        //if this is the 7th time the method is called in a row, it has gone all the way through all the units in the array and has none that haven't been used, return
        if (numCalled == 6)
        {
            return;
        }

        //iterate through the units to find the selected unit
        for (int i = 0; i < NUM_UNITS; i++)
        {
            if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
            {

                //set the current selected unit to false so that we're sure only one unit is selected at a time
                currPlayer.units[i].GetComponent<Unit>().Selected = false;

                //if the selected unit was the first in the array
                if (i == 0)
                {
                    //set the last unit in the array to have a selected value of true
                    currPlayer.units[5].GetComponent<Unit>().Selected = true;
                    selectedUnit = currPlayer.units[5].GetComponent<Unit>();

                    //if the last unit has already been used, enter a recursive loop to find the previous unit that hasn't been moved, then break out
                    if (currPlayer.units[5].GetComponent<Unit>().IsMoved == true)
                    {
                        getPrev(numCalled + 1);
                        getPossibleMovements();
                        break;
                    }

                    //move the selected space to the selected unit's location and break out
                    moveSelectedSpace(currPlayer.units[5].GetComponent<Unit>().XPos - xPos, currPlayer.units[5].GetComponent<Unit>().YPos - yPos);
                    break;
                }

                //if it's not the last, just set the next unit's selected value to true
                currPlayer.units[i - 1].GetComponent<Unit>().Selected = true;
                selectedUnit = currPlayer.units[i - 1].GetComponent<Unit>();

                //if the previous unit has already been used, enter a recursive loop to find the next unit that hasn't been moved, then break out
                if (currPlayer.units[i - 1].GetComponent<Unit>().IsMoved == true)
                {
                    getPrev(numCalled + 1);
                    getPossibleMovements();
                    break;
                }

                //move the selected space to the selected unit's location and break out
                moveSelectedSpace(currPlayer.units[i - 1].GetComponent<Unit>().XPos - xPos, currPlayer.units[i - 1].GetComponent<Unit>().YPos - yPos);
                break;
            }
        }
    }

    void moveSelectedSpace(int xChange, int yChange)
    {
        //change the entire board to be their natural textures and possible movement textures
        getPossibleMovements();

        //create temp variables to hold prospective new selected space
        int xTemp = xPos + xChange;
        int yTemp = yPos + yChange;

        for(int i = 0; i < 6; i++)
        {
            if (currPlayer.units[i].GetComponent<Unit>().Selected)
            {
                if ((xTemp >= 0 && xTemp < 9) && (yTemp >= 0 && yTemp < 9))
                {
                    if (currPlayer.units[i].GetComponent<Unit>().ActionPossible(board.spaces[xTemp * 9 + yTemp], currPlayer.units[i].GetComponent<Unit>().Movement))
                    {
                        xPos = xTemp;
                        yPos = yTemp;

                        //actually set the selectSpace value to the space at the xPos and yPos
                        board.selectedSpace = board.spaces[xPos * 9 + yPos];
                    }
                }
                break;
            }
        }
        //change the texture of the new selectedSpace to be green
        board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;
        Debug.Log(xPos + ", " + yPos);
    }

    void getPossibleMovements()
    {
        //declare a variable to store a Unit value temporarily
        Unit temp;

        //iterate through to find the selected unit, then move the unit to selected space
        for (int i = 0; i < NUM_UNITS; i++)
        {
            //set the unit to be the temp variable
            temp = currPlayer.units[i].GetComponent<Unit>();

            //if temp is the selected space
            if (temp.Selected == true)
            {
                //go through all the spaces on the board
                foreach (Space space in board.spaces)
                {
                    //check if the space is available to be moved to
                    if (temp.ActionPossible(space, temp.Movement))
                    {
                        //change the material of the available spaces to yellow
                        space.GetComponent<Renderer>().material = Resources.Load("Yellow", typeof(Material)) as Material;
                    }
                    else
                    {
                        //change the material of the available spaces to the neutral texture
                        space.GetComponent<Renderer>().material = Resources.Load("sand_2", typeof(Material)) as Material;
                    }
                }
            }
        }
    }

    void hideScreens()
    {
        runFGScreen.GetComponent<SpriteRenderer>().enabled = false;
        bruteFGScreen.GetComponent<SpriteRenderer>().enabled = false;
        specFGScreen.GetComponent<SpriteRenderer>().enabled = false;
        runSPScreen.GetComponent<SpriteRenderer>().enabled = false;
        bruteSPScreen.GetComponent<SpriteRenderer>().enabled = false;
        specSPScreen.GetComponent<SpriteRenderer>().enabled = false;
        controls.GetComponent<SpriteRenderer>().enabled = false;
    }

    void endTurn()
    {
        Debug.Log("endTurn() called");

        //call game.finishTurn() to handle all the end of turn operations that InputManager doesn't control
        game.finishTurn();

        //reset turnStarted so that the start of turn code is run for the next player
        turnStarted = true;

        //Switch the active player to the alternate player
        if (board.activePlayer == 1)
        {
            board.activePlayer = 2;
        }
        else
        {
            board.activePlayer = 1;
        }
    }
}

//Old Code No Longer Used
/*
    //Handles input for Player 1's turn
    void player1Turn()
    {
        //Xbox then PlayStation
        //Actual Buttons
        //If you press A or X
        if (Input.GetButtonDown("SelectP1"))
        {
            //iterate through to find the selected unit, then move the unit to selected space
            for (int i = 0; i < NUM_UNITS; i++)
            {
                if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
                {
                    currPlayer.units[i].GetComponent<Unit>().Move(board.selectedSpace);
                }
            }

            //call getNext() for the first time to select the next unit automatically
            getNext(0);
            getPossibleMovements();
            Debug.Log(message1 + "1" + message2 + "A");
        }

        //If you press B or Circle
        if (Input.GetButtonDown("BackP1"))
        {
            //No idea what the B button does as of now
            Debug.Log(message1 + "1" + message2 + "B");
        }

        //If you press Y or Triangle
        if (Input.GetButtonDown("EndP1"))
        {
            //call endTurn() to end the current player's turn
            Debug.Log(message1 + "1" + message2 + "Y");
            endTurn();
        }

        //If you press Left Bumper or L1
        if (Input.GetButtonDown("Prev UnitP1"))
        {
            //call getPrev for the first time, makes the selected unit the previous unit in the unit array
            getPrev(0);
            getPossibleMovements();
            Debug.Log(message1 + "1" + message2 + "Left Bumper");
        }

        //If you press Right Bumper or R1
        if (Input.GetButtonDown("Next UnitP1"))
        {
            //call getNext for the first time, makes the selected unit the next unit in the unit array
            getNext(0);
            getPossibleMovements();
            Debug.Log(message1 + "1" + message2 + "Right Bumper");
        }

        //If you press Back or Whatever PS calls Back
        if (Input.GetButtonDown("Game InfoP1"))
        {
            //will display the unit information screen later
            Debug.Log(message1 + "1" + message2 + "Back");
        }

        //If you press Start or Whatever PS calls Start
        if (Input.GetButtonDown("PauseP1"))
        {
            //will pause the game later
            Debug.Log(message1 + "1" + message2 + "Start");
        }

        //Axises Inputs (Sticks, DPad, Triggers)
        //If you press Left or Right
        if (Input.GetAxis("Left/RightP1") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "1" + message2 + "Left/Right");

            //set stickInUse to true so that no other input from the D-Pad can come in until the the D-Pad is released
            stickInUse = true;
            Debug.Log("X Value: " + Input.GetAxis("Left/RightP1"));

            //call moveSelectedSpace so that the selected space moves in the desired direction
            moveSelectedSpace((int)Input.GetAxis("Left/RightP1"), 0);
        }

        //If you press Up or Down
        if (Input.GetAxis("Up/DownP1") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "1" + message2 + "Up/Down");

            //set stickInUse to true so that no other input from the D-Pad can come in until the the D-Pad is released
            stickInUse = true;
            Debug.Log("Y Value: " + Input.GetAxis("Up/DownP1"));

            //call moveSelectedSpace so that the selected space moves in the desired direction
            moveSelectedSpace(0, (int)Input.GetAxis("Up/DownP1"));
        }

        //If you press Left/Right Trigger or L2/R2
        if (Input.GetAxis("Char InfoP1") != 0 && trigDown == false)
        {
            //will display the chatacter's information
            Debug.Log(message1 + "1: " + "One or more Triggers down");

            //set trigDown to true so that no other input from the triggers can come in until the the trigger is released
            trigDown = true;
        }

        //check for axises reset
        //Left/Right and Left/Right
        if (Input.GetAxis("Left/RightP1") == 0 && Input.GetAxis("Up/DownP1") == 0 && stickInUse == true)
        {
            Debug.Log(message1 + "1: " + "The stick is ready for new input");

            //sets stickInUse to false so it is ready to accept new input
            stickInUse = false;
        }

        //Triggers
        if (Input.GetAxis("Char InfoP1") == 0 && trigDown == true)
        {
            Debug.Log(message1 + "1" + "Triggers ready for new input");

            //sets trigDown to false so it is ready to accept new input
            trigDown = false;
        }
    }

    //handles input during Player 2's turn
    void player2Turn()
    {
        //Xbox then PlayStation
        //Actual Buttons
        //If you press A or X
        if (Input.GetButtonDown("SelectP2"))
        {
            //iterate through to find the selected unit, then move the unit to selected space
            for (int i = 0; i < NUM_UNITS; i++)
            {
                if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
                {
                    currPlayer.units[i].GetComponent<Unit>().Move(board.selectedSpace);
                }
            }

            //call getNext() for the first time to select the next unit automatically
            getNext(0);
            getPossibleMovements();
            Debug.Log(message1 + "2" + message2 + "A");
        }

        //If you press B or Circle
        if (Input.GetButtonDown("BackP2"))
        {
            //No idea what the B button does as of now
            Debug.Log(message1 + "2" + message2 + "B");
        }

        //If you press Y or Triangle
        if (Input.GetButtonDown("EndP2"))
        {
            //call endTurn() to end the current player's turn
            Debug.Log(message1 + "2" + message2 + "Y");
            endTurn();
        }

        //If you press Left Bumper or L1
        if (Input.GetButtonDown("Prev UnitP2"))
        {
            //call getPrev for the first time, makes the selected unit the previous unit in the unit array
            getPrev(0);
            getPossibleMovements();
            Debug.Log(message1 + "2" + message2 + "Left Bumper");
        }

        //If you press Right Bumper or R1
        if (Input.GetButtonDown("Next UnitP2"))
        {
            //call getNext for the first time, makes the selected unit the next unit in the unit array
            getNext(0);
            getPossibleMovements();
            Debug.Log(message1 + "2" + message2 + "Right Bumper");
        }

        //If you press Back or Whatever PS calls Back
        if (Input.GetButtonDown("Game InfoP2"))
        {
            //will display the unit information screen later
            Debug.Log(message1 + "2" + message2 + "Back");
        }

        //If you press Start or Whatever PS calls Start
        if (Input.GetButtonDown("PauseP2"))
        {
            //will pause the game later
            Debug.Log(message1 + "2" + message2 + "Start");
        }

        //Axises Inputs (Sticks, DPad, Triggers)
        //If you press Left or Right
        if (Input.GetAxis("Left/RightP2") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "2" + message2 + "Left/Right");

            //set stickInUse to true so that no other input from the D-Pad can come in until the the D-Pad is released
            stickInUse = true;
            Debug.Log("X Value: " + Input.GetAxis("Left/RightP2"));

            //call moveSelectedSpace so that the selected space moves in the desired direction
            moveSelectedSpace((int)Input.GetAxis("Left/RightP2"), 0);
        }

        //If you press Up or Down
        if (Input.GetAxis("Up/DownP2") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "2" + message2 + "Up/Down");

            //set stickInUse to true so that no other input from the D-Pad can come in until the the D-Pad is released
            stickInUse = true;
            Debug.Log("Y Value: " + Input.GetAxis("Up/DownP2"));

            //call moveSelectedSpace so that the selected space moves in the desired direction
            moveSelectedSpace(0, (int)Input.GetAxis("Up/DownP2"));
        }

        //If you press Left/Right Trigger or L2/R2
        if (Input.GetAxis("Char InfoP2") != 0 && trigDown == false)
        {
            //will display the chatacter's information
            Debug.Log(message1 + "2: " + "One or more Triggers down");

            //set trigDown to true so that no other input from the triggers can come in until the the trigger is released
            trigDown = true;
        }

        //check for axises reset
        //Left/Right and Left/Right
        if (Input.GetAxis("Left/RightP2") == 0 && Input.GetAxis("Up/DownP2") == 0 && stickInUse == true)
        {
            Debug.Log(message1 + "2: " + "The stick is ready for new input");

            //sets stickInUse to false so it is ready to accept new input
            stickInUse = false;
        }

        //Triggers
        if (Input.GetAxis("Char InfoP2") == 0 && trigDown == true)
        {
            Debug.Log(message1 + "2" + "Triggers ready for new input");

            //sets trigDown to false so it is ready to accept new input
            trigDown = false;
        }
    }
 */
