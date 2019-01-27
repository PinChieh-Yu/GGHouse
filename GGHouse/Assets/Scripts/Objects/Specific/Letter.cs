using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour, ISwitch
{
    public void Switch()
    {
        GameManager.instance.GetLetter();
        Destroy(gameObject);
    }
}
