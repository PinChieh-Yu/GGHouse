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
    private CG with_letter, without_letter;
    private AudioSource audio;

    [SerializeField]
    private int totalWinCondition;
    [SerializeField]
    private int winConditionCount;

    private bool hasGetLetter;

    void Start()
    {
        hasGetLetter = false;
        winConditionCount = 0;
        junior = GameObject.Find("Junior");
        senior = GameObject.Find("Senior");
        audio = GetComponent<AudioSource>();
        with_letter = GameObject.Find("Canvas").transform.Find("CG_with_letter").GetComponent<CG>();
        without_letter = GameObject.Find("Canvas").transform.Find("CG_without_letter").GetComponent<CG>();
        with_letter.enabled = false;
        without_letter.enabled = false;
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

    public void FinishTask()
    {
        winConditionCount++;
        //audio.Play();
        Debug.Log("Finish Tack, Current Count:" + winConditionCount.ToString());
        if(winConditionCount == totalWinCondition)
        {
            Debug.Log("Complete!");
            if (hasGetLetter)
            {
                with_letter.enabled = true;
                with_letter.start = true;
            }
            else
            {
                without_letter.enabled = true;
                without_letter.start = true;
            }
        }
    }

    public void GetLetter()
    {
        hasGetLetter = true;
    }
}
