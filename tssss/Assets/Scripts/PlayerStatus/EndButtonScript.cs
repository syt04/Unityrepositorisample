using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SkilButtonScript;

   
   private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Endbutton);   
    }


    private void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1) { 

            if (NewBehaviourScript.Instance.map.FocusingUnit != null)
        {
            if (!NewBehaviourScript.Instance.map.FocusingUnit.Atacked || !NewBehaviourScript.Instance.map.FocusingUnit.IsMoved)
            {
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                gameObject.GetComponent<CanvasGroup>().interactable = true;
            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.GetComponent<CanvasGroup>().interactable = false;
            }
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
        }
    }
    }

    public void Endbutton()
    {
        NewBehaviourScript.Instance.map.FocusingUnit.IsMoved = true;
        NewBehaviourScript.Instance.map.FocusingUnit.Atacked = true;
        NewBehaviourScript.Instance.map.ClearHighlight();
        NewBehaviourScript.Instance.map.FocusingUnit = null;
        StatusManager.Instance.ComandRan.GetComponent<CanvasGroup>().alpha = 0;
        SkilButtonScript.GetComponent<SkilButtonScript>().CloseSkilRan();
    }
}
