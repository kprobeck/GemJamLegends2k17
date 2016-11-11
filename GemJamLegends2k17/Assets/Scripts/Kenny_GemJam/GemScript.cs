using UnityEngine;
using System.Collections;

public class GemScript : MonoBehaviour {

    // Script for handeling Gem gameObject

    // variables for the gem
    public int xPos, yPos;
    bool isHeld;
    Unit holder;
    public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
        spawnPoint = GameObject.FindGameObjectWithTag("GemSpawn");    
    }

    // Update is called once per frame
    void Update() {
         
        // test, grabbing the correct spawnPoint. won't be grabbing transform position, but "grid coordiantes" eventually
        xPos = (int)spawnPoint.transform.position.x;
        yPos = (int)spawnPoint.transform.position.y;

        // update the Gem's position dependant on the holder's position
        if (holder != null) {
        //xPos = holder.xPos;
        //yPos = holder.yPos;
           }
    }


}
