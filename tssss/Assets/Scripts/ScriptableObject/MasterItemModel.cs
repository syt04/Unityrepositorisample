using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MasterItemModel : ScriptableObject
{

    enum categori
    {
        buki,
        bougu,
        kaihukuaitemu,
        book,
        ruut,
        nagemono,
        
    };

    public new string name;
    // public Sprite thumbnail;
    [SerializeField]
    categori syurui;
    [SerializeField]
    string Info;



   
    [SerializeField]
    Sprite sprite;

    public string GetName
    {
        get
        {
            return name;
        }
    }

    public Sprite GetSprite
    {
       get{
            return sprite;
        }
    }

    public string GetInfo
    {
        get
        {
            return Info;
        }

    }

    public enum WeaponType
    {
        Sword,
        Gun,
        Other
    }


    [SerializeField]
    private int attackPower;
    [SerializeField]
    private int shotPower;
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private float weaponRange;


    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetShotPower()
    {
        return shotPower;
    }

    public WeaponType GetWeaponType()
    {
        return weaponType;
    }

    public float GetWeaponRange()
    {
        return weaponRange;
    }

    public int defense;

}