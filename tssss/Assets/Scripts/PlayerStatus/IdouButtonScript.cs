using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdouButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject SkilButtonScript;

    [SerializeField]
    private Text ButtonText;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(IdouButton);
    }

    private void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1 && NewBehaviourScript.Instance.map.FocusingUnit != null)
        {
            if (NewBehaviourScript.Instance.map.SerchSecondFocusing() != null)
            {
                if (NewBehaviourScript.Instance.map.FocusingUnit.team == Teams.Player1)
                {
                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    gameObject.GetComponent<CanvasGroup>().interactable = true;
                    ButtonText.text = "移動";
                }

            }
            else if (NewBehaviourScript.Instance.map.FocusingUnit.BforeCell != null　&& !NewBehaviourScript.Instance.map.FocusingUnit.Atacked)
            {
                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    gameObject.GetComponent<CanvasGroup>().interactable = true;
                    ButtonText.text = "元に戻る";               

            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.GetComponent<CanvasGroup>().interactable = false;
                ButtonText.text = "移動";
            }
        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            ButtonText.text = "移動";
        }
    }

    private void IdouButton()
    {
        if (ButtonText.text == "移動"){ 
        Main_Stage map = NewBehaviourScript.Instance.map;
            if (map.FocusingUnit != null && map.SerchSecondFocusing() != null && map.FocusingUnit.team == Teams.Player1 && !map.FocusingUnit.IsMoved)
            {
                map.FocusingUnit.BforeCell = map.GetCells(map.FocusingUnit.X, map.FocusingUnit.Y); ;
                map.MoveTo(map.FocusingUnit, map.SerchSecondFocusing());
                map.FocusingUnit.IsMoved = true;
            }
            map.ClearHighlight();
            SkilButtonScript.GetComponent<SkilButtonScript>().CloseSkilRan();
        }
        else if(ButtonText.text == "元に戻る")
        {
            Main_Stage map = NewBehaviourScript.Instance.map;
            //map.MoveTo(map.FocusingUnit, map.FocusingUnit.BforeCell);
            map.FocusingUnit.X = map.FocusingUnit.BforeCell.X;
            map.FocusingUnit.Y = map.FocusingUnit.BforeCell.Y;
            map.FocusingUnit.transform.position = map.FocusingUnit.BforeCell.transform.position;


            map.FocusingUnit.IsMoved = false;
            map.FocusingUnit.BforeCell = null;
            map.ClearHighlight();
            ButtonText.text = "移動";
            SkilButtonScript.GetComponent<SkilButtonScript>().CloseSkilRan();
            Debug.Log(NewBehaviourScript.Instance.map.FocusingUnit.IsMoved);
        }
    }

}
