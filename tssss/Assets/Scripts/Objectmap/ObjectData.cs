using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectData : ScriptableObject
{


   
    

        //ListステータスのList
        public List<Objectscript> ObjectList = new List<Objectscript>();



        public List<Objectscript> GetObjectList()
        {
            return ObjectList;
        }

    

}
