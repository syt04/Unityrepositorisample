using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class SkilData : ScriptableObject
{
    public List<Skil> SkilList = new List<Skil>();


    public List<Skil> GetSkillist()
    {
        return SkilList;
    }

}


