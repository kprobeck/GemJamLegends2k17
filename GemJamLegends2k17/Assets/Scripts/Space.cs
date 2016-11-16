using UnityEngine;
using System.Collections;

public class Space : MonoBehaviour {

    public int x;
    public int y;
    public bool p1Spawn;
    public bool p2Spawn;
    public bool active;
    public bool hasGem;
    public bool gemSpot;
    public bool gemCapped;
    public int getX
    {
        get
        {
            return x;
        }
    }
    public int getY
    {
        get
        {
            return y;
        }
    }
    private bool occupied;
    public bool isOccupied
    {
        get
        {
            return occupied;
        }
        set
        {
            occupied = value;
        }
    }
    private GameObject occupier;

    public GameObject getOccupier
    {
        get
        {
            return occupier;
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

    public GameObject occupation()
    {
        return occupier;
    }

}
