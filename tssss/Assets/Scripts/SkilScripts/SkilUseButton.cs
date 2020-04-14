using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkilUseButton : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Use);
    }


    private void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1 &&SkilManager.Instance.UseSkil.Skil != null)
        {

            if (NewBehaviourScript.Instance.map.cells.Any(x => x.IsSkilAttackable == true))
            {
                    gameObject.GetComponent<CanvasGroup>().alpha = 1;
                    gameObject.GetComponent<CanvasGroup>().interactable = true;
                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                
            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().alpha = 0;
                gameObject.GetComponent<CanvasGroup>().interactable = false;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }

            if(NewBehaviourScript.Instance.map.cells.Any(x => x.IsSpellAttackable== true))
            {
                gameObject.GetComponent<CanvasGroup>().alpha = 1;
                gameObject.GetComponent<CanvasGroup>().interactable = true;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }

        }
        else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            gameObject.GetComponent<CanvasGroup>().interactable = false;
            gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    private void Use()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        SkilManager.Instance.UseSkil.Use();
    }
}
