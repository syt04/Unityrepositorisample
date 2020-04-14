using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaverVeiw : MonoBehaviour
{

    public GameObject Caver;

    private GameObject hardCaver;
    private GameObject harfCaver;


    // Start is called before the first frame update
    void Start()
    {
        hardCaver = Caver.transform.Find("HardCaver").gameObject;
        harfCaver = Caver.transform.Find("HarfCaver").gameObject;

       // Debug.Log(hardCaver);

    }

    // Update is called once per frame
    void Update()
    {

        
    }



    /*

      private void OnTriggerEnter2D(Collider2D other)
      {
        if(other.tag == "Player")
        {
            ShowHardCaver();



        }
      }

    private void OnTriggerExit2D(Collider2D other)
    {

        HideCaver();
    }


    */

    public void ShowHardCaver()
    {
        if (transform.parent.GetComponent<Objectscript>().Mycaver == "HardCaver")
        {
            hardCaver.SetActive(true);
        }
        else
        {
            harfCaver.SetActive(true);
        }
    }

    public void HideCaver()
    {
        hardCaver.SetActive(false);
        harfCaver.SetActive(false);
    }


}
