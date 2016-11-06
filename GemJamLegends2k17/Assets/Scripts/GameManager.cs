using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
