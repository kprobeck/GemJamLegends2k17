using UnityEngine;
using System.Collections;

public class Hexagon : MonoBehaviour {

    public char type;
    public int pX;
    public int pY;
    public Hexagon[] siblings;
    public bool activated;

    public Hexagon()
    {
        type = 'x';
        pX = 0;
        pY = 0;
        activated = false;
    }

    public Hexagon(char t, int x, int y)
    {
        type = t;
        pX = x;
        pY = y;
        activated = false;
    }

    public string getColor()
    {
        switch (type)
        {
            case 'r':
                return "red";
            case 'g':
                return "green";
            case 'b':
                return "blue";
            case 'y':
                return "yellow";
            case 'o':
                return "orange";
            case 'p':
                return "purple";
            case 'e':
                return "white";
            case 'x':
                return "black";
            case 'a':
                return "grey";
            default:
                return "";
        }
    }

    public void mixColor(Hexagon h, out bool changeMade, bool add)
    {
        char c = h.type;
        changeMade = false;
        Material mat = this.GetComponent<Renderer>().material;
        if (c == type)
        {
            type = 'a';
            activated = true;
            changeMade = true;
            mat = Resources.Load("Materials/grey", typeof(Material)) as Material;
        }
        else
        {
            if (add)
            {
                if (c == 'r')
                {
                    if (type == 'b')
                    {
                        type = 'p';
                        changeMade = true;
                        mat = Resources.Load("Materials/purple", typeof(Material)) as Material;
                    }
                    // empty
                    else if (type == 'e')
                    {
                        type = 'r';
                        changeMade = true;
                        mat = Resources.Load("Materials/red", typeof(Material)) as Material;
                    }
                    else if (type == 'y')
                    {
                        type = 'o';
                        changeMade = true;
                        mat = Resources.Load("Materials/orange", typeof(Material)) as Material;
                    }
                }
                else if (c == 'b')
                {
                    //primary color mixing
                    if (type == 'r')
                    {
                        type = 'p';
                        changeMade = true;
                        mat = Resources.Load("Materials/purple", typeof(Material)) as Material;
                    }
                    // empty
                    else if (type == 'e')
                    {
                        type = 'b';
                        changeMade = true;
                        mat = Resources.Load("Materials/blue", typeof(Material)) as Material;
                    }
                    else if (type == 'y')
                    {
                        type = 'g';
                        changeMade = true;
                        mat = Resources.Load("Materials/green", typeof(Material)) as Material;
                    }
                }
                else if (c == 'g')
                {
                    if (type == 'e')
                    {
                        type = 'g';
                        changeMade = true;
                        mat = Resources.Load("Materials/green", typeof(Material)) as Material;
                    }
                }
                else if (c == 'o')
                {
                    if (type == 'e')
                    {
                        type = 'o';
                        changeMade = true;
                        mat = Resources.Load("Materials/orange", typeof(Material)) as Material;
                    }
                }
                else if (c == 'y')
                {
                    if (type == 'r')
                    {
                        type = 'o';
                        changeMade = true;
                        mat = Resources.Load("Materials/orange", typeof(Material)) as Material;
                    }
                    else if (type == 'b')
                    {
                        type = 'g';
                        changeMade = true;
                        mat = Resources.Load("Materials/green", typeof(Material)) as Material;
                    }
                    // empty
                    else if (type == 'e')
                    {
                        type = 'y';
                        changeMade = true;
                        mat = Resources.Load("Materials/yellow", typeof(Material)) as Material;
                    }
                }
                else if (c == 'p')
                {
                    if (type == 'e')
                    {
                        type = 'p';
                        changeMade = true;
                        mat = Resources.Load("Materials/purple", typeof(Material)) as Material;
                    }
                }
            }
            else
            {
                if (c == 'r')
                {
                    if (type == 'p')
                    {
                        type = 'b';
                        changeMade = true;
                        mat = Resources.Load("Materials/blue", typeof(Material)) as Material;
                    }
                    else if(type == 'o')
                    {
                        type = 'y';
                        changeMade = true;
                        mat = Resources.Load("Materials/yellow", typeof(Material)) as Material;
                    }
                }
                else if (c == 'b')
                {
                    if (type == 'p')
                    {
                        type = 'r';
                        activated = true;
                        changeMade = true;
                        mat = Resources.Load("Materials/red", typeof(Material)) as Material;
                    }
                    else if (type == 'g')
                    {
                        type = 'y';
                        changeMade = true;
                        mat = Resources.Load("Materials/yellow", typeof(Material)) as Material;
                    }
                }
                else if (c == 'g')
                {
                    if (type == 'b')
                    {
                        type = 'y';
                        changeMade = true;
                        mat = Resources.Load("Materials/yellow", typeof(Material)) as Material;
                    }
                    else if(type == 'y')
                    {
                        type = 'b';
                        changeMade = true;
                        mat = Resources.Load("Materials/blue", typeof(Material)) as Material;
                    }
                }
                else if (c == 'o')
                {
                    if (type == 'y')
                    {
                        type = 'r';
                        changeMade = true;
                        mat = Resources.Load("Materials/red", typeof(Material)) as Material;
                    }
                    else if(type == 'r')
                    {
                        type = 'y';
                        changeMade = true;
                        mat = Resources.Load("Materials/yellow", typeof(Material)) as Material;
                    }
                }
                else if (c == 'y')
                {
                    if (type == 'o')
                    {
                        type = 'r';
                        changeMade = true;
                        mat = Resources.Load("Materials/red", typeof(Material)) as Material;
                    }
                    else if (type == 'g')
                    {
                        type = 'b';
                        changeMade = true;
                        mat = Resources.Load("Materials/blue", typeof(Material)) as Material;
                    }
                }
                else if (c == 'p')
                {
                    if (type == 'r')
                    {
                        type = 'b';
                        changeMade = true;
                        mat = Resources.Load("Materials/blue", typeof(Material)) as Material;
                    }
                    else if(type == 'b')
                    {
                        type = 'r';
                        changeMade = true;
                        mat = Resources.Load("Materials/red", typeof(Material)) as Material;
                    }
                }
            }
        }
        this.GetComponent<Renderer>().material = mat;
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

