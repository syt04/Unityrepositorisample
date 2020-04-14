using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectscript : Main_tile
{
    private  string mycaver;
    public  string Mycaver
    {
        get { return mycaver; }

    }


    private void Start()
    {
        
        if(transform.gameObject.name == "Block")
        {
            mycaver = "HardCaver";
        }
        else
        {
            mycaver = "HarfCaver";

        }


    }

}
