using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A : MonoBehaviour
{
    public Slider HPSlider;
   public int hp;
   public int hpMax ;
    public int at;

     void Start()
    {
        hp = hpMax;
        //HPSlider.maxValue = hpMax;
        HPSlider.maxValue = hpMax;
        HPSlider.value = hpMax;
        at = 10;
    }
    public void OnDamage(int _damage)
    {
        hp -= _damage;
        //Debug.Log("Ondamege");
        if (hp<=0)
        {
            hp = 0;

        }
        HPSlider.value = hp;
    }
}
