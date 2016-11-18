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

        if(pos.GetComponent<Space>().getOccupier != null)
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
                if (caps[i] == pos)
                {
                    Debug.Log("test Cap");

                    //Point getter is you
                    holder.GemHeld = false;
                    holder = null;
                    xPos = spawnPoint.getX;
                    yPos = spawnPoint.getY;
                    this.transform.position = spawnPoint.transform.position;
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -1);
                    isHeld = false;
                }
            }
        }
    }


}
