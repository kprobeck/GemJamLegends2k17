using UnityEngine;
using System.Collections;

public class GemScript : MonoBehaviour {

    // Script for handeling Gem gameObject

    // variables for the gem
    public int xPos, yPos;
    public bool isHeld;
    public Unit holder;
    public Space spawnPoint;
    public Space pos;
    public Space[] caps;
    public bool gameGem = true; 

	// Use this for initialization
	void Start () {
        //spawnPoint = GameObject.FindGameObjectWithTag("GemSpawn");
        xPos = (int)spawnPoint.GetComponent<Space>().transform.position.x;
        yPos = (int)spawnPoint.GetComponent<Space>().transform.position.y;
    }

    // Update is called once per frame
    void Update() {
         
        // test, grabbing the correct spawnPoint. won't be grabbing transform position, but "grid coordinates" eventually
        //xPos = (int)spawnPoint.transform.position.x;
        //yPos = (int)spawnPoint.transform.position.y;

        // update the Gem's position dependant on the holder's position

        if(pos.GetComponent<Space>().getOccupier != null && gameGem)
        {
            //then there is an occupier
            holder = pos.GetComponent<Space>().getOccupier.GetComponent<Unit>();
            holder.GemHeld = true;
            isHeld = true;
        }



        if (isHeld) {
            xPos = holder.GetComponent<Unit>().XPos;
            this.transform.position = holder.transform.position;
            yPos = holder.GetComponent<Unit>().YPos;
            pos = holder.CurrSpace;
        }
        else
        {
            holder = null;
        }
        capTest();
    }

    void capTest()
    {
        if (holder != null)
        {
            for (int i = 0; i < caps.Length; i++)
            {
                if (caps[i] == pos && !caps[i].gemCapped)
                {
                    Debug.Log("test Cap");

                    //Point getter is you
                    holder.GemHeld = false;
                    holder.gem = null;
                    holder = null;
                    xPos = spawnPoint.getX;
                    yPos = spawnPoint.getY;
                    this.transform.position = spawnPoint.transform.position;
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
                    pos.gemCapped = true;
                    pos = spawnPoint;
                    isHeld = false;
                    GameObject lGem = Instantiate(Resources.Load("gem") as GameObject, new Vector3(1, 1, 1), new Quaternion(0, 0, 0, 1)) as GameObject;
                    lGem.tag = "cappedGem";
                    lGem.transform.position = caps[i].transform.position + new Vector3(0,0,-1);
                    lGem.transform.localScale = new Vector3(0.5f, 0.6f, 0.6f);
                    lGem.GetComponent<GemScript>().enabled = false;
                }
            }
        }
    }


}
