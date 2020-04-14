using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    private UnitScript unit = new UnitScript();
    public UnitScript Unit
    {
        get { return unit; }
        set { unit = value; }
    }
    [SerializeField]
    protected Main_Stage map;

    public Teams team;

    private bool isMoved = false;
    public bool IsMoved
    {
        get { return isMoved; }
        set
        {
            isMoved = value;
            if (isMoved && IsFocusing)
            {
                OnClick();
            }
        }
    }

    private Main_tile bforeCell;
    public Main_tile BforeCell
    {
        get
        {
            return bforeCell;
        }
        set { bforeCell = value; }
    }

    private int x;
    public int X
    {
        get { return x; }
        set { x = value; }
    }
    private int y;
    public int Y
    {
        get { return y; }
        set { y = value; }
    }

    private bool destory = false;
    public bool Destory
    {
        get { return destory; }
        set { destory = value; }
    }

    private bool atacked = false;
    public bool Atacked
    {
        set
        {
            bforeCell = null;
            atacked = value;
        }
        get { return atacked; }
    }
    private bool isFocusing = false;
    public bool IsFocusing { get { return isFocusing; } set { isFocusing = value; } }

    public Main_tile Cell
    {
        get
        {
            return map.GetCells(x, y);
        }
    }

    private void Start()
    {

        if (Resources.Load("Animation/" + Unit.AnimationController) != null)
        {
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animation/" + Unit.AnimationController));
        }

    }

    void Update()
    {
        if (team == Teams.Player1)
            Unitsousa();
    }




    public void Unitsousa()
    {
        if (IsMoved && Atacked)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void OnClick()
    {
        if (!Destory)
        {
            if (SkilManager.Instance.UseSpell != null && Cell.IsSpellable)
            {
                SkilManager.Instance.UseSpell.SkilAttacktile(Cell);
                return;
            }


            if (Cell.IsSecondSkilable)
            {
                NewBehaviourScript.Instance.map.AttackSkilFocusingcells(X,Y);
                return;
            }


            else if (Cell.IsSkilAttackable)
            {
                if (SkilManager.Instance.UseSpell == null)
                    Cell.IsSecondSkilable = true;
                return;
            }


            // 攻撃対象の選択中であれば攻撃アクション実行
            if (NewBehaviourScript.Instance.map.GetCells(X,Y).IsAttackable && !NewBehaviourScript.Instance.map.FocusingUnit.Atacked)
            {
                //map.AttackTo(map.FocusingUnit, this);
                StartCoroutine(BattleManager.Instance.AttackTo(NewBehaviourScript.Instance.map.FocusingUnit, this));
                NewBehaviourScript.Instance.map.FocusingUnit.Atacked = true;
                NewBehaviourScript.Instance.map.FocusingUnit.IsMoved = true;
                return;
            }
            Isfocusing();
        }
    }


    public void Isfocusing()
    {
        SkilManager.Instance.UseSkil.Skil = null;
        // 自分以外のユニットが選択状態であれば、そのユニットの選択を解除
        if (null != NewBehaviourScript.Instance.map.FocusingUnit && this != NewBehaviourScript.Instance.map.FocusingUnit)
        {
            NewBehaviourScript.Instance.map.FocusingUnit.IsFocusing = false;
            NewBehaviourScript.Instance.map.ClearHighlight();
        }
        IsFocusing = !IsFocusing;
        //map.FocusingUnit = this;
        if (IsFocusing)
        {
            NewBehaviourScript.Instance.map.FocusingUnit = this;


            NewBehaviourScript.Instance.map.HighlightMovableCells(X,Y, Unit.MoveAmount);
            NewBehaviourScript.Instance.map.FirstFocusingcells(this);
            //  map.HighlightAttackableCells(this);
        }
        else
        {
            NewBehaviourScript.Instance.map.ClearHighlight();
        }
    }
}
