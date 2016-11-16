using UnityEngine;
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

    // Use this for initialization
    void Start () {
        message1 = "The Player ";
        message2 = " Pressed: ";
        trigDown = false;
        stickInUse = false;
        xPos = 0;
        yPos = 0;
        forceEnd = true;
        currPlayer = game.p1;
        board.selectedSpace = board.spaces[xPos*9 + yPos];
        board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;
    }
	
	// Update is called once per frame
	void Update () {
        forceEnd = true;

        //check if its player 1's turn
        if (board.activePlayer == 1)
        {
            currPlayer = game.p1;
            //check if Player 1 has no more moves
            for (int i = 0; i < NUM_UNITS; i++)
            {
                if (game.p1.units[i].GetComponent<Unit>().IsMoved == false)
                {
                    forceEnd = false;
                }
            }

            //runs for if it is player 1's turn to handle their actions
            if (turnStarted == true)
            {
                turnStarted = false;
                game.p1.units[0].GetComponent<Unit>().Selected = true;
            }
            
            player1Turn();
        }

        if (board.activePlayer == 2)
        {
            currPlayer = game.p2;
            //check if Player 2 has no more moves
            for (int i = 0; i < NUM_UNITS; i++)
            {
                if (game.p2.units[i].GetComponent<Unit>().IsMoved == false)
                {
                    forceEnd = false;
                }
            }

            //runs for if it is player 1's turn to handle their actions
            if (turnStarted == true)
            {
                turnStarted = false;
                game.p2.units[0].GetComponent<Unit>().Selected = true;
            }
            player2Turn();
        }

        if (forceEnd)
        {
            endTurn();
        }
    }

    //Handles input for Player 1's turn
    void player1Turn()
    {
        //Xbox then PlayStation
        //Actual Buttons
        //If you press A or X
        if (Input.GetButtonDown("SelectP1"))
        {
            for (int i = 0; i < NUM_UNITS; i++)
            {
                if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
                {
                    currPlayer.units[i].GetComponent<Unit>().Move(board.selectedSpace);
                }
            }
            Debug.Log(message1 + "1" + message2 + "A");
        }

        //If you press B or Circle
        if (Input.GetButtonDown("BackP1"))
        {
            Debug.Log(message1 + "1" + message2 + "B");
        }

        //If you press Y or Triangle
        if (Input.GetButtonDown("EndP1"))
        {
            Debug.Log(message1 + "1" + message2 + "Y");
            endTurn();
        }

        //If you press Left Bumper or L1
        if (Input.GetButtonDown("Prev UnitP1"))
        {
            getPrev();
            Debug.Log(message1 + "1" + message2 + "Left Bumper");
        }

        //If you press Right Bumper or R1
        if (Input.GetButtonDown("Next UnitP1"))
        {
            getNext();
            Debug.Log(message1 + "1" + message2 + "Right Bumper");
        }

        //If you press Back or Whatever PS calls Back
        if (Input.GetButtonDown("Game InfoP1"))
        {
            Debug.Log(message1 + "1" + message2 + "Back");
        }

        //If you press Start or Whatever PS calls Start
        if (Input.GetButtonDown("PauseP1"))
        {
            Debug.Log(message1 + "1" + message2 + "Start");
        }

        //Axises Inputs (Sticks, DPad, Triggers)
        //If you press Left or Right
        if (Input.GetAxis("Left/RightP1") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "1" + message2 + "Left/Right");
            stickInUse = true;
            Debug.Log("X Value: " + Input.GetAxis("Left/RightP1"));
            moveSelectedSpace((int)Input.GetAxis("Left/RightP1"), 0);
        }

        //If you press Up or Down
        if (Input.GetAxis("Up/DownP1") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "1" + message2 + "Up/Down");
            stickInUse = true;
            Debug.Log("Y Value: " + Input.GetAxis("Up/DownP1"));
            moveSelectedSpace(0, (int)Input.GetAxis("Up/DownP1"));
        }

        //If you press Left/Right Trigger or L2/R2
        if (Input.GetAxis("Char InfoP1") != 0 && trigDown == false)
        {
            Debug.Log(message1 + "1: " + "One or more Triggers down");
            trigDown = true;
        }

        //check for axises reset
        //Left/Right and Left/Right
        if (Input.GetAxis("Left/RightP1") == 0 && Input.GetAxis("Up/DownP1") == 0 && stickInUse == true)
        {
            Debug.Log(message1 + "1: " + "The stick is ready for new input");
            stickInUse = false;
        }

        //Triggers
        if (Input.GetAxis("Char InfoP1") == 0 && trigDown == true)
        {
            Debug.Log(message1 + "1" + "Triggers ready for new input");
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
            for (int i = 0; i < NUM_UNITS; i++)
            {
                if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
                {
                    currPlayer.units[i].GetComponent<Unit>().Move(board.selectedSpace);
                }
            }
            Debug.Log(message1 + "2" + message2 + "A");
        }

        //If you press B or Circle
        if (Input.GetButtonDown("BackP2"))
        {
            Debug.Log(message1 + "2" + message2 + "B");
        }

        //If you press Y or Triangle
        if (Input.GetButtonDown("EndP2"))
        {
            Debug.Log(message1 + "2" + message2 + "Y");
            endTurn();
        }

        //If you press Left Bumper or L1
        if (Input.GetButtonDown("Prev UnitP2"))
        {
            getPrev();
            Debug.Log(message1 + "2" + message2 + "Left Bumper");
        }

        //If you press Right Bumper or R1
        if (Input.GetButtonDown("Next UnitP2"))
        {
            getNext();
            Debug.Log(message1 + "2" + message2 + "Right Bumper");
        }

        //If you press Back or Whatever PS calls Back
        if (Input.GetButtonDown("Game InfoP2"))
        {
            Debug.Log(message1 + "2" + message2 + "Back");
        }

        //If you press Start or Whatever PS calls Start
        if (Input.GetButtonDown("PauseP2"))
        {
            Debug.Log(message1 + "2" + message2 + "Start");
        }

        //Axises Inputs (Sticks, DPad, Triggers)
        //If you press Left or Right
        if (Input.GetAxis("Left/RightP2") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "2" + message2 + "Left/Right");
            stickInUse = true;
            Debug.Log("X Value: " + Input.GetAxis("Left/RightP2"));
            moveSelectedSpace((int)Input.GetAxis("Left/RightP2"), 0);
        }

        //If you press Up or Down
        if (Input.GetAxis("Up/DownP2") != 0 && stickInUse == false)
        {
            Debug.Log(message1 + "2" + message2 + "Up/Down");
            stickInUse = true;
            Debug.Log("Y Value: " + Input.GetAxis("Up/DownP2"));
            moveSelectedSpace(0, (int)Input.GetAxis("Up/DownP2"));
        }

        //If you press Left/Right Trigger or L2/R2
        if (Input.GetAxis("Char InfoP2") != 0 && trigDown == false)
        {
            Debug.Log(message1 + "2" + message2 + "One or more Triggers down");
            trigDown = true;
        }

        //check for axises reset
        //Left/Right and Left/Right
        if (Input.GetAxis("Left/RightP2") == 0 && Input.GetAxis("Up/DownP2") == 0 && stickInUse == true)
        {
            Debug.Log(message1 + "2: " + "The stick is ready for new input");
            stickInUse = false;
        }

        //Triggers
        if (Input.GetAxis("Char InfoP2") == 0 && trigDown == true)
        {
            Debug.Log(message1 + "2: " + "Triggers ready for new input");
            trigDown = false;
        }
    }

    void getNext()
    {
        for (int i = 0; i < NUM_UNITS; i++)
        {
            if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
            {
                currPlayer.units[i].GetComponent<Unit>().Selected = false;
                if(i == 5)
                {
                    currPlayer.units[0].GetComponent<Unit>().Selected = true;
                    moveSelectedSpace(currPlayer.units[0].GetComponent<Unit>().XPos - xPos, currPlayer.units[0].GetComponent<Unit>().YPos - yPos);
                    break;
                }
                currPlayer.units[i+1].GetComponent<Unit>().Selected = true;
                moveSelectedSpace(currPlayer.units[i + 1].GetComponent<Unit>().XPos - xPos, currPlayer.units[i + 1].GetComponent<Unit>().YPos - yPos);
                Debug.Log(currPlayer.units[i + 1].GetComponent<Unit>().Selected);
                break;
            }
        }
    }

    void getPrev()
    {
        for (int i = 0; i < NUM_UNITS; i++)
        {
            if (currPlayer.units[i].GetComponent<Unit>().Selected == true)
            {
                currPlayer.units[i].GetComponent<Unit>().Selected = false;
                if (i == 0)
                {
                    currPlayer.units[5].GetComponent<Unit>().Selected = true;
                    moveSelectedSpace(currPlayer.units[5].GetComponent<Unit>().XPos - xPos, currPlayer.units[5].GetComponent<Unit>().YPos - yPos);
                    break;
                }
                currPlayer.units[i - 1].GetComponent<Unit>().Selected = true;
                moveSelectedSpace(currPlayer.units[i - 1].GetComponent<Unit>().XPos - xPos, currPlayer.units[i - 1].GetComponent<Unit>().YPos - yPos);
                Debug.Log(currPlayer.units[i - 1].GetComponent<Unit>().Selected);
                break;
            }
        }
    }

    void moveSelectedSpace(int xChange, int yChange)
    {
        board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("sand_2", typeof(Material)) as Material;
        xPos += xChange;
        yPos += yChange;

        if (xPos > 8)
        {
            xPos = 8;
        }
        if (xPos < 0)
        {
            xPos = 0;
        }
        if (yPos > 8)
        {
            yPos = 8;
        }
        if (yPos < 0)
        {
            yPos = 0;
        }

        board.selectedSpace = board.spaces[xPos*9 + yPos];
        board.selectedSpace.GetComponent<Renderer>().material = Resources.Load("Green", typeof(Material)) as Material;
        Debug.Log(xPos + ", " + yPos);
    }

    void endTurn()
    {
        Debug.Log("endTurn() called");
        game.finishTurn();
        turnStarted = true;
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
