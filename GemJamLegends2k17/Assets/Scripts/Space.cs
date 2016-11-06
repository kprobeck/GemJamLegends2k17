using UnityEngine;
using System.Collections;

public class Space : MonoBehaviour {

    private int x;
    private int y;
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
