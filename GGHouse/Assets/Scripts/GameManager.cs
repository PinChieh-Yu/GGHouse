using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private GameObject junior, senior;
    private ObjectInfo[] objectList;

    void Start()
    {
        junior = GameObject.Find("Junior");
        senior = GameObject.Find("Senior");
        objectList = FindObjectsOfType<ObjectInfo>();
        for (int i = 0; i < objectList.Length; i++)
        {
            objectList[i].Id = i;
        }
    }

    public Transform GetCharacterTransform(CharacterIdentity identity)
    {
        if (identity == CharacterIdentity.Junior)
        {
            return junior.transform;
        }
        else if (identity == CharacterIdentity.Senior)
        {
            return senior.transform;
        }
        else
        {
            return null;
        }
    }

    public ObjectInfo GetObjectInfo(int objId)
    {
        return objectList[objId];
    }

    public void TimeUp()
    {
        junior.GetComponent<PlayerMover>().enabled = false;
        senior.GetComponent<PlayerMover>().enabled = false;
    }
}
