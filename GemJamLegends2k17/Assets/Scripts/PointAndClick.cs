using UnityEngine;
using System.Collections;

public class PointAndClick : MonoBehaviour {
/*
    // variable to hold item
    private GameObject heldObj;

    private Vector3 heldObjOrigin;

    // varibale to hold hexagon script
    private Hexagon hexScript;

    // variable for determining whether lens is additive or subtractive
    // true = additive; false = subtractive
    private bool add = true;

	// Use this for initialization
	void Start () {
        hexScript = GetComponent<Hexagon>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Jump"))
        {
            if(add)
            {
                add = false;
                GameObject.FindGameObjectWithTag("subtractive").GetComponent<Renderer>().material = Resources.Load("Materials/Minus-Active", typeof(Material)) as Material;
                GameObject.FindGameObjectWithTag("additive").GetComponent<Renderer>().material = Resources.Load("Materials/Plus", typeof(Material)) as Material;
            }
            else
            {
                add = true;
                GameObject.FindGameObjectWithTag("additive").GetComponent<Renderer>().material = Resources.Load("Materials/Plus-Active", typeof(Material)) as Material;
                GameObject.FindGameObjectWithTag("subtractive").GetComponent<Renderer>().material = Resources.Load("Materials/Minus", typeof(Material)) as Material;
            }
        }

        // get mouse position and check for collisions
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // something has been detected
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            //Debug.Log("true");

            
            //if (hit.transform != null)
            //{
                // left-click
                if (Input.GetMouseButtonDown(0))
                {
                
                //restart button hit, restarts level
                if(hit.collider.gameObject.tag == "restart")
                {
                    Application.LoadLevel(Application.loadedLevel);
                   // int scene = SceneManager.GetActiveScene().buildIndex;
                    //SceneManager.LoadScene(scene, LoadSceneMode.Single);
//                    SceneManager.LoadScene()
                }
                // clicked additive lens, additive mode activated
                if(!add && hit.collider.gameObject.tag == "additive")
                {
                    add = true;
                    hit.collider.gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Plus-Active", typeof(Material)) as Material;
                    GameObject.FindGameObjectWithTag("subtractive").GetComponent<Renderer>().material = Resources.Load("Materials/Minus", typeof(Material)) as Material;
                }
                // clicked subtractive lens, subtractive mode activated
                if(add && hit.collider.gameObject.tag == "subtractive")
                {
                    add = false;
                    hit.collider.gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Minus-Active", typeof(Material)) as Material;
                    GameObject.FindGameObjectWithTag("additive").GetComponent<Renderer>().material = Resources.Load("Materials/Plus", typeof(Material)) as Material;
                }
                // if nothing is being held, hold this object and make sure the inventory tag, save original location
                if (heldObj == null && hit.collider.gameObject.tag == "inventoryHex") {
                    heldObjOrigin = hit.collider.gameObject.transform.position;
                    heldObj = hit.collider.gameObject;
                }

                // if something is being held already, check to see if gameObject is intersecting
                if(heldObj != null)
                {
                    if (Physics.Raycast(heldObj.transform.position, new Vector3(0, 0, 1.0f), out hit, 100.0f))
                    {
                        //Debug.Log("intersect!");
                        //Hit a valid location of place to put
                        if (hit.collider.gameObject.tag == "Hex")
                        {
                            // get siblings
                            Hexagon[] sibs = hit.collider.gameObject.GetComponent<Hexagon>().siblings;

                            // change material
                            hit.collider.gameObject.GetComponent<Renderer>().material = heldObj.GetComponent<Renderer>().material;

                            // make inactive and mix the colors if necessary
                            hit.collider.gameObject.tag = "inactiveHex";
                            bool anyChange = false;
                            bool changeMade = false;
                            foreach (Hexagon hex in sibs)
                            {
                                hex.mixColor(heldObj.gameObject.GetComponent<Hexagon>(), out changeMade, add);
                                if (changeMade)
                                    anyChange = true;
                            }
                            if (anyChange)
                            {
                                hit.collider.gameObject.GetComponent<Hexagon>().type = 'a';
                                hit.collider.gameObject.GetComponent<Hexagon>().GetComponent<Renderer>().material = Resources.Load("Materials/grey", typeof(Material)) as Material;
                            }
                            else
                            {
                                hit.collider.gameObject.GetComponent<Hexagon>().type = heldObj.GetComponent<Hexagon>().type;
                            }

                            // done with held object
                            Destroy(heldObj);
                            heldObj = null;
                        }
                        // hit an invalid or restart location, places heldObj back
                        if (heldObj != null && (hit.collider.gameObject.tag == "restart" || hit.collider.gameObject.tag == "inactiveHex"))
                        {
                        
                            // reset heldObj to original location and rotation
                            heldObj.transform.position = heldObjOrigin;
                            heldObj.transform.rotation = Quaternion.Euler(0, 180, 0);
                            
                            // done with held object, BUT DON'T DESTROY BECAUSE WE PUT IT BACK
                            heldObj = null;
                        }
                    }

                    // final test, nothing was actually hit, so drop the held object
                    if (heldObj != null && heldObj.transform.position != heldObjOrigin)
                    {
                        // reset heldObj to original location and rotation
                        heldObj.transform.position = heldObjOrigin;
                        heldObj.transform.rotation = Quaternion.Euler(0, 180, 0);

                        // done with held object, BUT DON'T DESTROY BECAUSE WE PUT IT BACK
                        heldObj = null;
                    }
                }

                //Debug.Log("CLICKED");
                }
            //}
        }

        // update held object to move with mouse
        if (heldObj != null)
        {
            heldObj.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10.0f);
            //Debug.Log(heldObj.transform.position);

            // slowly rotate the held object to represent being held
            heldObj.transform.Rotate(Vector3.back * Time.deltaTime * 30.0f);
            

        }


        
	
	}
    */
}
