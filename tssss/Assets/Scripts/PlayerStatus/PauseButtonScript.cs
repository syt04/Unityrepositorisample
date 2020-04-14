using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonScript : MonoBehaviour
{
    [SerializeField]
    private GameObject PausePanel;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OpenPauseButton);
    }


    void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1)
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
    }


    public void OpenPauseButton()
    {
        if (PausePanel.GetComponent<CanvasGroup>().alpha == 0)
        {
            PausePanel.GetComponent<CanvasGroup>().alpha = 1;
            PausePanel.GetComponent<CanvasGroup>().interactable = true;
            PausePanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            PausePanel.GetComponent<CanvasGroup>().alpha = 0;
            PausePanel.GetComponent<CanvasGroup>().interactable = false;
            PausePanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
    }

    public void BackKyoten()
    {
        FadeManager.Instance.LoadScene("Kyoten",1);
    }
}
