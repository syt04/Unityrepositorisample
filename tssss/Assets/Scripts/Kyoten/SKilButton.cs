using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SKilButton : MonoBehaviour
{
    [SerializeField]
    private Button SkilButton;


    [SerializeField]
    private Text SkilNameText;

    [SerializeField]
    private Skil skil;
    public Skil Skil
    {
        get { return skil; }
        set { skil = value; }
    }

    private void Start()
    {
        SkilButton.onClick.AddListener(UnderInfoSkilShow);
        SkilNameText.text = skil.SkilName;
    }

    public void UnderInfoSkilShow()
    {
        KyotenManager.Instance.UnderInfomationNameText.text = skil.SkilName;
        KyotenManager.Instance.UnderInfomationText.text = skil.Description;
    }
}
