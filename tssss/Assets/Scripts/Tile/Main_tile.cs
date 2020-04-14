using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Main_tile : MonoBehaviour
{

    //public Text myText;

    [SerializeField]
    Main_Stage map;
    [SerializeField]
    GameObject highlight;
    [SerializeField]
    int cost;
    [SerializeField]
    Color movableColor;
    [SerializeField]
    Color attackableColor;
    [SerializeField]
    Color focusableColor;
    [SerializeField]
    Color secondfocusableColor;

    [SerializeField]
    Color skilColor;
    [SerializeField]
    Color secondskilColor;
    [SerializeField]
    Color skilattackableColor;
    [SerializeField]
    Color spellColor;
    [SerializeField]
    Color spellAttackColor;
    [SerializeField]
    Color attackableHaniColor;


    [SerializeField]
    Color ButtobasimovableColor;

    public bool canmove;
    public bool canAtack;

    /// <summary>
    /// 地形によるダメージ軽減率
    /// </summary>
    [SerializeField]
    float reduceRate;
    public float ReduceRate
    {
        get { return reduceRate; }
    }


    public GameObject tile;
    public int x,y;


    public void SetCost(int cost)
    {
        this.cost = cost;

    }
    
    public int Cost
    {
        get { return cost; }
        set { cost = value; }
    }

    public int X
    {
        get { return x; }
    }

    public int Y
    {
        get { return y; }
    }

    void Start()
    {
        // GetComponent<Button>().onClick.AddListener(OnClick);
        highlight = transform.Find("hilight").gameObject;
        oncell = cost;
    }

    /// <summary>
    /// 座標をセットします
    /// </summary>
    /// <param name="x">The x coordinate.</param>
    /// <param name="y">The y coordinate.</param>
    public void SetCoordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// 移動可能なマスかどうか
    /// </summary>
    /// <value><c>true</c> if this instance is movable; otherwise, <c>false</c>.</value>
    public bool IsMovable
    {
        set
        {
            // highlight = transform.Find("hilight").gameObject;
            if (canmove)
                highlight.GetComponent<SpriteRenderer>().color = movableColor;
            else
                highlight.GetComponent<SpriteRenderer>().color = Color.clear;
                highlight.gameObject.SetActive(value);

        }   //SetActiveはアクティブを変更できる
        get
        {

                return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == movableColor&&canmove
                ;
            
      
        }   //activeSelf はオブジェクトのアクティブかどうかを調べらる


    }


    public void OnClick()
    {
            if (IsMovable)
            {
            if (GameState.Instance.currentTeam == map.FocusingUnit.team)
            {
                if (map.FocusingUnit.team == Teams.Player2)//!map.FocusingUnit.IsMoved||
                {
                    map.MoveTo(map.FocusingUnit, this);

                }


                if (map.FocusingUnit != null )
                {
                    map.SecondFocusingcells(x, y);
                }
            }
            }

            if(SkilManager.Instance.UseSkil.Skil != null)
        {
            if (IsSpellable)
            {
                map.SpellAttackFocusingcells(x, y);
                SkilManager.Instance.UseSpell.SkilAttacktile(this);
            }
            else if(IsSkilAttackable)
            {
                SkilManager.Instance.UseSkil.Skil.SkilAble();
            }
        }


    }
    public bool IsAttackable
    {
        set
        {           
                highlight.GetComponent<SpriteRenderer>().color = attackableColor;
                highlight.gameObject.SetActive(value);                  
        }
        get {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == attackableColor && canAtack;          
             
        }
    }

    public bool IsAttackHaniable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = attackableHaniColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == attackableHaniColor;

        }
    }

    public bool IsSkilAttackable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = skilattackableColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == skilattackableColor;

        }
    }

    public bool IsSpellAttackable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = spellAttackColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == spellAttackColor;

        }
    }
    public bool IsSpellable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = spellColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == spellColor;

        }
    }

    public bool AttackButton
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = attackableColor;            
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && canAtack;

        }
    }


    public bool IsFirstFocusable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = focusableColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == focusableColor;
        }
    }


    public bool IsSkilable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = skilColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == skilColor;
        }
    }

    public bool IsButtobasimovable
    {
        set
        {
            highlight.GetComponent<SpriteRenderer>().color = ButtobasimovableColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == ButtobasimovableColor;
        }
    }


    public bool IsSecondSkilable
    {
        set
        {
            if(SkilManager.Instance.UseSkil.Skil != null)
                if(!SkilManager.Instance.UseSkil.Skil.NeedDirection)
            highlight.GetComponent<SpriteRenderer>().color = secondskilColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == secondskilColor;
        }
    }

    public bool IsSecondFocusable
    {
        set
        {
            if (canmove)
            highlight.GetComponent<SpriteRenderer>().color = secondfocusableColor;
            highlight.gameObject.SetActive(value);
        }
        get
        {
            return highlight.gameObject.activeSelf && highlight.GetComponent<SpriteRenderer>().color == secondfocusableColor;
        }
    }

    public UnitObject Unit
    {
        get { return map.GetUnit(x, y); }
    }

    public int oncell;
    public bool onunit;

    

    public void Update()
    {
        if (Unit != null)
        {

            canmove = false;
        }
        else
        {
            canmove = true;

        }
    }
}