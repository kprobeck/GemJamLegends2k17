using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public GameObject board;
    public GameObject[] boardChildren;
 //   public GameObject restartButton;
    bool endGame;
    public bool unlockLock = false;

    //made getter for private endGame attribute
    //used in LockScript to determine if animation should be played
    public bool GetEndGame
    {
        get
        {
            return endGame;
        }
    }
	// Use this for initialization
	void Start () {
        endGame = false;
        boardChildren = board.GetComponent<boardManager>().children;
        
        foreach(GameObject go in boardChildren)
        {
            if (go.GetComponent<Hexagon>().getColor() == "black")
            {
                go.GetComponent<MeshRenderer>().enabled = false;
            }

            if (go.GetComponent<Hexagon>().type == 'e' || go.GetComponent<Hexagon>().type == 'a')
                go.tag = "Hex";
            else
                go.tag = "inactiveHex";
        }
    }
	
	// Update is called once per frame
	void Update () {
        endGame = true;
        //Check to see if everything is ready to end level
        foreach(GameObject child in boardChildren)
        {
            if (child.GetComponent<Hexagon>().type == 'e' || child.GetComponent<Hexagon>().type == 'a')
                child.tag = "Hex";
            else
                child.tag = "inactiveHex";
            Hexagon hex = child.GetComponent<Hexagon>();
            if (hex.type != 'x' && hex.type != 'a')
            {
                endGame = false;
            }
            if(hex.type == 'a')
            {
                child.tag = "Hex";
            }
        }
        //end level, you won! Next level
        if (endGame)
        {
            unlockLock = true;
            Debug.Log("You won");
        }
	}
}
