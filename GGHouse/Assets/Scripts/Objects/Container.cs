using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectInfo))]
public class Container : MonoBehaviour
{
    public List<ObjectName> requestObjects;
    public bool ShowWhenContain;

    [HideInInspector]
    public List<bool> requestFulFill;

    public event Action<ObjectInfo> OnPutIn;

    void Awake()
    {
        requestFulFill = new List<bool>();
    }

    void Start()
    {
        for (int i = 0; i < requestObjects.Count; i++)
        {
            requestFulFill.Add(false);
        }
    }

    public bool PutIn(int objId)
    {
        ObjectInfo objInfo = GameManager.instance.GetObjectInfo(objId);
        Debug.Log("Try to putin "+ objInfo.name);
        for (int id = 0; id < requestObjects.Count; id++)
        {
            if (requestObjects[id] == objInfo.Name && !requestFulFill[id])
            {
                Debug.Log(name + " Contain " + objInfo.Name.ToString());
                requestFulFill[id] = true;

                if (ShowWhenContain)
                {
                    objInfo.LayerNumber = GetComponent<ObjectInfo>().LayerNumber + 1;
                }
                else
                {
                    objInfo.LayerNumber = GetComponent<ObjectInfo>().LayerNumber - 1;
                }

                OnPutIn?.Invoke(objInfo);
                return true;
            }
        }
        return false;
    }
}
