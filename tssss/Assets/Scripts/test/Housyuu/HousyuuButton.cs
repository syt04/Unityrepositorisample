using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HousyuuButton : MonoBehaviour
{


    [SerializeField]
   private Housyuu housyuu;
    [SerializeField]
    private Text undernametext;
    [SerializeField]
    private Text underdescriptiontext;
    [SerializeField]
    private CanvasGroup canvasGroup;

    private CanvasPanel canvasPanel = new CanvasPanel();

    private void Start()
    {
        canvasPanel.Criate(canvasGroup);

    }

    public void ChengeHousyuu()
    {
        HousyuuManager.Instance.OpenObject(HousyuuManager.Instance.CanvasPanel);
        GanareteUnitRan.Instance.Housyuu = housyuu;
        HousyuuManager.Instance.OpenObject(canvasPanel);

    }

    public void PointerEnter()
    {
        switch (housyuu)
        {
            case Housyuu.NewtStage:
                undernametext.text = "次のステージ";
                underdescriptiontext.text = "次のステージを選びます";
                break;
            case Housyuu.Shop:
                undernametext.text = "お店";
                underdescriptiontext.text = "アイテムや食事を取れます。";
                break;

            case Housyuu.Kunren:
                undernametext.text = "ユニット訓練";
                underdescriptiontext.text = "ユニット1体の才能を一段階開花させます。";
                break;
            case Housyuu.Nakama:
                undernametext.text = "ユニット募集";
                underdescriptiontext.text = "ユニット1体新たに仲間にします。";
                break;
            case Housyuu.Item:
                undernametext.text = "アイテム採取";
                underdescriptiontext.text = "アイテムやお金を集めます";
                break;
            case Housyuu.Rest:
                undernametext.text = "休憩";
                underdescriptiontext.text = "ユニット全員のHPとSPを回復させます";
                break;
            case Housyuu.Result:
                undernametext.text = "戦況報告";
                underdescriptiontext.text = "前回の戦闘の記録を見ます";
                break;

        }





    }

}
