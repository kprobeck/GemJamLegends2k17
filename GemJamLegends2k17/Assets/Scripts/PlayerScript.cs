using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    public Faction faction;
    public GameObject[] units = new GameObject[6];
    public int tears;
    public bool isTurn;
    public int playerNum;
    public GameManager gameMan;
    public boardManager boardMan;
    private bool prevTurn;
    public int x1, x2, x3, x4, x5, x6;
    public int y1, y2, y3, y4, y5, y6;
    //it u1(boardMan.spaces[x1, y1], faction, Type.Runner);
//    Unit u1 = new Unit();
    // Use this for initialization
    void Start()
    {
        //populate units array with runners, brutes, and specials of faction type
        //3 runners, 2 brutes, 1 special
        //boardManager.Spaces[0,0]
        // Unit u1 = GameObject.Instantiate(
        //u1.CreateUnit(boardMan.spaces[x1, y1], faction, Type.Runner);
        //    GameObject g = new GameObject();
        //    g.AddComponent<Unit>();
        // Unit u1 = g;

        //GameObject u1 = new GameObject();
        GameObject brute = Resources.Load("brutePref") as GameObject;
        GameObject runner = Resources.Load("runPref") as GameObject;
        GameObject special = Resources.Load("specPref") as GameObject;

        GameObject u1 = Instantiate(runner, new Vector3(1, 1, 1), new Quaternion(0, 0,0, 1)) as GameObject;
     //   u1.AddComponent<Unit>();
        u1.GetComponent<Unit>().CreateUnit(boardMan.spaces[x1*9 + y1],faction,Type.Runner, playerNum);
        boardMan.spaces[x1 * 9 + y1].occupier = u1.GetComponent<Unit>();
        boardMan.spaces[x1 * 9 + y1].isOccupied = true;

        GameObject u2 = Instantiate(runner, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1)) as GameObject;
      //  u2.AddComponent<Unit>();
        u2.GetComponent<Unit>().CreateUnit(boardMan.spaces[x2*9 +  y2], faction, Type.Runner, playerNum);
        boardMan.spaces[x2 * 9 + y2].occupier = u2.GetComponent<Unit>();
        boardMan.spaces[x2 * 9 + y2].isOccupied = true;

        GameObject u3 = Instantiate(runner, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1)) as GameObject;
//        u3.AddComponent<Unit>();
        u3.GetComponent<Unit>().CreateUnit(boardMan.spaces[x3*9+ y3], faction, Type.Runner, playerNum);
        boardMan.spaces[x3 * 9 + y3].occupier = u3.GetComponent<Unit>();
        boardMan.spaces[x3 * 9 + y3].isOccupied = true;

        GameObject u4 = Instantiate(brute, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1)) as GameObject;
 //       u4.AddComponent<Unit>();
        u4.GetComponent<Unit>().CreateUnit(boardMan.spaces[x4*9+y4], faction, Type.Brute, playerNum);
        boardMan.spaces[x4 * 9 + y4].occupier = u4.GetComponent<Unit>();
        boardMan.spaces[x4 * 9 + y4].isOccupied = true;

        GameObject u5 = Instantiate(brute, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1)) as GameObject;
  //      u5.AddComponent<Unit>();
        u5.GetComponent<Unit>().CreateUnit(boardMan.spaces[x5*9+ y5], faction, Type.Brute, playerNum);
        boardMan.spaces[x5 * 9 + y5].occupier = u5.GetComponent<Unit>();
        boardMan.spaces[x5 * 9 + y5].isOccupied = true;

        GameObject u6 = Instantiate(special, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1)) as GameObject;
    //    u6.AddComponent<Unit>();
        u6.GetComponent<Unit>().CreateUnit(boardMan.spaces[x6*9+ y6], faction, Type.Special, playerNum);
        boardMan.spaces[x6 * 9 + y6].occupier = u6.GetComponent<Unit>();
        boardMan.spaces[x6 * 9 + y6].isOccupied = true;

        units[0] = u1;
        units[1] = u2;
        units[2] = u3;
        units[3] = u4;
        units[4] = u5;
        units[5] = u6;
        //units[0] = u1;//new Unit(boardMan.spaces[x1,y1], faction, Type.Runner);
        //Debug.Log(units[0].GetComponent<Unit>().XPos + ", " + units[0].GetComponent<Unit>().YPos);
        tears = units.Length;
        prevTurn = false;
        //Debug.Log(units.Length);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(units.Length);
        if (gameMan.currentPlayer == playerNum)
        {
            isTurn = true;
        }
        else
        {
            isTurn = false;
        }

        if (isTurn == true && isTurn != prevTurn)
        {
            for (int i = 0; i < units.Length; i++)
            {
                if(units[i].GetComponent<Unit>().IsKOed)
                {
                    units[i].GetComponent<Unit>().WakeUp();
                }
            }

            //check for active units
            //if none are left, call endTurn()
            int counter = 0;
            for (int i = 0; i < units.Length; i++)
            {
                if (units[i].GetComponent<Unit>().IsKOed == false)
                {
                    break;
                }
                counter++;
            }
            if(counter == 6)
            {
                endTurn();
            }
        }
        prevTurn = isTurn;
    }

    void endTurn()
    {
        isTurn = false;
    }
}