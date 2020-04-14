using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilDirectionButton : MonoBehaviour
{

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SkilDirection);
    }

    private void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1)
        {
            if (SkilManager.Instance.UseSkil.Skil != null)
            {
                if (SkilManager.Instance.UseSkil.Skil.NeedDirection)
                {
                    gameObject.GetComponent<CanvasGroup>().alpha = 1;
                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    gameObject.GetComponent<CanvasGroup>().interactable = true;
                }
                else
                {
                    gameObject.GetComponent<CanvasGroup>().alpha = 0;
                    gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    gameObject.GetComponent<CanvasGroup>().interactable = false;
                }
            }
            else
            {
                gameObject.GetComponent<CanvasGroup>().alpha = 0;
                gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                gameObject.GetComponent<CanvasGroup>().interactable = false;
            }
        }
    }




    private void SkilDirection()
    {

        if (SkilManager.Instance.UseSkil.Skil.NeedDirection)
        {
            
            switch (SkilManager.Instance.UseSkil.Direction)
            {
                case Direction.N:
                    SkilManager.Instance.UseSkil.Direction = Direction.W;
                    break;
                case Direction.W:
                    SkilManager.Instance.UseSkil.Direction = Direction.S;
                    break;
                case Direction.S:
                    SkilManager.Instance.UseSkil.Direction = Direction.E;
                    break;
                case Direction.E:
                    SkilManager.Instance.UseSkil.Direction = Direction.N;
                    break;                                      
            }
            SkilManager.Instance.UseSkil.Skil.SkilAble();
            Debug.Log(SkilManager.Instance.UseSkil.Direction);
        }

      
    }
}
