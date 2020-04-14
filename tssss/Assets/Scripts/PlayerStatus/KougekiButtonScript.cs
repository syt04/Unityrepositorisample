using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KougekiButtonScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(KougekiButton);
    }

    private void Update()
    {
        if (GameState.Instance.currentTeam == Teams.Player1 && NewBehaviourScript.Instance.map.FocusingUnit != null && NewBehaviourScript.Instance.map.FocusingUnit.team == Teams.Player1)
        {
            if (!NewBehaviourScript.Instance.map.FocusingUnit.Atacked)
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

    private void KougekiButton()
    {
        Main_Stage map = NewBehaviourScript.Instance.map;
        foreach(Main_tile cell in map.cells.Where(x => x.IsMovable == true||x.IsFirstFocusable == true || x.IsSecondFocusable == true))
        {
            cell.IsMovable = false;
            cell.IsFirstFocusable = false;
            cell.IsSecondFocusable = false;
        }

        if (map.cells.Any(x => x.IsAttackHaniable == true) || map.cells.Any(x => x.IsAttackable == true))
        {
            map.ClearHighlight();
        }
        else
        {
            AttackRenge(NewBehaviourScript.Instance.map.FocusingUnit);
        }
    }

    public void AttackRenge(UnitObject unit)
    {
        Main_Stage map = NewBehaviourScript.Instance.map;
        var startCell = map.cells.First(c => c.X == unit.X && c.Y == unit.Y);

        foreach (var cell in map.GetCellsByDistance(startCell, unit.Unit.AttackRangeMin, unit.Unit.AttackRangeMax))
        {
            if (null != cell.Unit && cell.Unit.team != GameState.Instance.currentTeam && unit.team != cell.Unit.team)
            {
                
                cell.IsAttackable = true;
                Debug.Log("attackable");
            }
            else
            {

                cell.IsAttackHaniable = true;
                Debug.Log("attackHaniable");
            }
        }

    }



}
