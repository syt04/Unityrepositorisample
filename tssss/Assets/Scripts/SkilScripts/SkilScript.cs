using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class SkilScript : MonoBehaviour
{

    [SerializeField]
    public Text SkilNameText;

    private Direction direction = Direction.N;
    public Direction Direction
    {
        get { return direction; }
        set { direction = value; }
    }


    [SerializeField]
    private Skil skil;
    public Skil Skil
    {
        get { return skil; }
        set
        {
            skil = value;
            // = Resources.Load<Sprite>(value.SpriteHighlighted);
            // = Resources.Load<Sprite>(value.SpriteNeutral);
        }
    }


    public void Use()
    {

            NewBehaviourScript.Instance.map.FocusingUnit.Unit.Sp -= SkilManager.Instance.UseSkil.Skil.UseSp;
            foreach (Main_tile SkilAttacktile in NewBehaviourScript.Instance.map.cells.Where(x => x.IsSkilAttackable == true))
            {
                if(SkilAttacktile.Unit != null)
                SkilManager.Instance.UsedUnits.Add(SkilAttacktile.Unit);
            }
            StartCoroutine("SkilCoroutine");
            NewBehaviourScript.Instance.map.ClearHighlight();

   
    }

    public void OnClick()
    {
        SkilManager.Instance.UseSkil.Skil = skil;
        skil.SkilAble();
        SkilButtonScript.Instance.ShowSkil(skil);
        
    }


    public string GetTooltip()
    {
        return skil.GetTooltip();
    }

    public float CalculateDamageSkil(UnitScript unit)
    {
        return unit.Strength * skil.Strengethho + unit.Intellect*skil.Intellectho +skil.KoteiDame;

    }


    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnPointerEnter()
    {
        if (skil != null)
        {
            skil.SkilAble();
        }
    }


    IEnumerator SkilCoroutine()
    {
        GameState.Instance.noclick.blocksRaycasts = true;
        foreach (UnitObject unit in SkilManager.Instance.UsedUnits)
        {
            Vector3 vector = new Vector3(unit.transform.position.x, unit.transform.position.y);
            var obj = Instantiate(skil.Efect, vector, Quaternion.identity);
        }
        yield return new WaitForSeconds(1.0f);
        skil.Use();


        if (NewBehaviourScript.Instance.map.FocusingUnit != null)
        {
            NewBehaviourScript.Instance.map.FocusingUnit.IsMoved = true;
            NewBehaviourScript.Instance.map.FocusingUnit.Atacked = true;
        }
            SkilManager.Instance.UsedUnits.Clear();
        GameState.Instance.noclick.blocksRaycasts = false;
    }

}
