using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDataList", menuName = "Time Edge/DataList")]
public class DataList : ScriptableObject
{
    [System.Serializable]
    public struct DataElement 
    {
        public string key;
        public ScriptableObject value;
    }

    public DataElement[] dictionary;
    public Dictionary<string, ScriptableObject> datas = new Dictionary<string, ScriptableObject>();

    public void Awake()
    {
        foreach(var ele in dictionary)
        {
            datas.Add(ele.key, ele.value);
        }
    }
}
