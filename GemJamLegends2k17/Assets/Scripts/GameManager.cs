using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public GameObject board;
    public GameObject[] boardChildren;
    //   public GameObject restartButton;
    bool endGame;
    bool abilityPhase;
    public bool unlockLock = false;
    public int currentPlayer;
    public PlayerScript p1;
    public PlayerScript p2;
    public GemScript gem;
    public GameObject p1Win;
    public GameObject p2Win;

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
        // player 1 wins
        if (gem.GetComponent<GemScript>().caps[1].GetComponent<Space>().gemCapped && gem.GetComponent<GemScript>().caps[3].GetComponent<Space>().gemCapped)
        {
            p1Win.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Player 1 WINS!");
        }

        // player 2 wins
        if (gem.GetComponent<GemScript>().caps[0].GetComponent<Space>().gemCapped && gem.GetComponent<GemScript>().caps[2].GetComponent<Space>().gemCapped)
        {
            p2Win.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Player 2 WINS!");
        }
    }

    public bool finishTurn()
    {
        abilityPhase = !abilityPhase;
        if (currentPlayer == 1)
        {
            p1.isTurn = false;
            p2.isTurn = true;
            ++currentPlayer;
            foreach (GameObject g in p1.GetComponent<PlayerScript>().units)//int i = 0; i < p1.GetComponent<PlayerScript>().units.Length; i++)
            {
                if (!g.GetComponent<Unit>().IsKOed)
                {
                    g.GetComponent<Unit>().IsMoved = false;
                    g.GetComponent<Unit>().AbilityUsed = false;
                }
                g.GetComponent<Unit>().Selected = false;
            }
            return true;
        }
        if (currentPlayer == 2)
        {
            p1.isTurn = true;
            p2.isTurn = false;
            --currentPlayer;

            foreach (GameObject g in p1.GetComponent<PlayerScript>().units)//int i = 0; i < p1.GetComponent<PlayerScript>().units.Length; i++)
            {
                if (!g.GetComponent<Unit>().IsKOed)
                {
                    g.GetComponent<Unit>().Attack();
                }
            }

            foreach (GameObject g in p2.GetComponent<PlayerScript>().units)//int i = 0; i < p1.GetComponent<PlayerScript>().units.Length; i++)
            {
                if (!g.GetComponent<Unit>().IsKOed)
                {
                    g.GetComponent<Unit>().Attack();
                    g.GetComponent<Unit>().IsMoved = false;
                    g.GetComponent<Unit>().AbilityUsed = false;
                }

                g.GetComponent<Unit>().Selected = false;
            }
            return true;
        }
        return false;
    }
}
