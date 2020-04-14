using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyState : MonoBehaviour
{

    private bool myturn;
    public bool Myturn
    {
        get { return myturn; }
        set { myturn = value; }

    }

    private bool myturnEnd;
    public bool MyturnEnd
    {
        get { return myturnEnd; }
        set { myturnEnd = value; }

    }

    private int speed;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private int enegy;
    public int Enegy
    {
        get { return enegy; }
        set { enegy = value; }
    }

    public int reEnegy;

    public void ReEnegy()
    {
        
            enegy = reEnegy;
    }

    public GameObject gameobject;
    public Sprite image;
    public Slider _slider;

    public void Start()
    {

        gameobject = gameobject.transform.parent.gameObject;
      

        //GameState.Instance.SetState(this);
        myturnEnd = false;
        myturn = false;

    }

}


