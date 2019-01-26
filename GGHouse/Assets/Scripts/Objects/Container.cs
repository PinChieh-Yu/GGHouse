using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectInfo))]
public class Container : MonoBehaviour
{
    public List<ObjectName> requestObjects;

    private List<int> containingObjects;

    void Awake()
    {
        containingObjects = new List<int>();
    }

    public bool PutIn(int objId)
    {
        if (!containingObjects.Contains(objId) && objId != GetComponent<ObjectInfo>().Id)
        {
            Debug.Log(name + " Contain " + GameManager.instance.GetObjectInfo(objId).name);
            containingObjects.Add(objId);
            GameManager.instance.GetObjectInfo(objId).IsInteractable = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int TakeOut()
    {
        if (containingObjects.Count > 0)
        {
            int tmp = containingObjects[0];
            containingObjects.RemoveAt(0);
            return tmp;
        }
        else
        {
            return -1;
        }
    }
}
