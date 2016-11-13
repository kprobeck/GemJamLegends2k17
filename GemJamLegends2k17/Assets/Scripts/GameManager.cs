using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject board;
    public GameObject[] boardChildren;
    //   public GameObject restartButton;
    bool endGame;
    public bool unlockLock = false;
    public int currentPlayer;
    public PlayerScript p1;
    public PlayerScript p2;

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool finishTurn()
    {
        if (currentPlayer == 1)
        {
            p1.isTurn = false;
            p2.isTurn = true;
            ++currentPlayer;
            return true;
        }
        if (currentPlayer == 2)
        {
            p1.isTurn = false;
            p2.isTurn = true;
            --currentPlayer;
            return true;
        }
        return false;
    }
}
