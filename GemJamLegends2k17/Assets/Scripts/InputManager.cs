using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private string message;
    private bool trigDown;
    private bool stickInUse;
    private int xPos;
    private int yPos;
    private bool selected;
    private boardManager board;
    private Unit[] usedUnits;
    private const int NUM_UNITS = 6;

    // Use this for initialization
    void Start () {
        message = "The Player Pressed: ";
        trigDown = false;
        stickInUse = false;
        xPos = 0;
        yPos = 0;
        selected = false;
}
	
	// Update is called once per frame
	void Update () {
        //Xbox then PlayStation
        //Actual Buttons
        //If you press A or X
        if (Input.GetButtonDown("Select"))
        {
            Debug.Log(message + "A");
        }

        //If you press B or Circle
        if (Input.GetButtonDown("Back"))
        {
            Debug.Log(message + "B");
        }

        //If you press Y or Triangle
        if (Input.GetButtonDown("End"))
        {
            Debug.Log(message + "Y");
        }

        //If you press Left Bumper or L1
        if (Input.GetButtonDown("Prev Unit"))
        {
            Debug.Log(message + "Left Bumper");
        }

        //If you press Right Bumper or R1
        if (Input.GetButtonDown("Next Unit"))
        {
            Debug.Log(message + "Right Bumper");
        }

        //If you press Back or Whatever PS calls Back
        if (Input.GetButtonDown("Game Info"))
        {
            Debug.Log(message + "Back");
        }

        //If you press Start or Whatever PS calls Start
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log(message + "Start");
        }

        //Axises Inputs (Sticks, DPad, Triggers)
        //If you press Left or Right
        if (Input.GetAxis("Left/Right") != 0 && stickInUse == false)
        {
            Debug.Log(message + "Left/Right");
            stickInUse = true;
            Debug.Log("X Value: " + Input.GetAxis("Left/Right"));
        }

        //If you press Up or Down
        if (Input.GetAxis("Up/Down") != 0 && stickInUse == false)
        {
            Debug.Log(message + "Up/Down");
            stickInUse = true;
            Debug.Log("Y Value: " + Input.GetAxis("Up/Down"));
        }
        
        //If you press Left/Right Trigger or L2/R2
        if (Input.GetAxis("Char Info") != 0 && trigDown == false)
        {
            Debug.Log(message + "One or more Triggers down");
            trigDown = true;
        }
        
        //check for axises reset
        //Left/Right and Left/Right
        if (Input.GetAxis("Left/Right") == 0 && Input.GetAxis("Up/Down") == 0 && stickInUse == true)
        {
            Debug.Log("The stick is ready for new input");
            stickInUse = false;
        }

        //Triggers
        if (Input.GetAxis("Char Info") == 0 && trigDown == true)
        {
            Debug.Log(message + "Triggers ready for new input");
            trigDown = false;
        }
    }

    void getNext()
    {

    }

    void getPrev()
    {

    }

    void endTurn()
    {
        usedUnits = new Unit[NUM_UNITS];
    }
}
