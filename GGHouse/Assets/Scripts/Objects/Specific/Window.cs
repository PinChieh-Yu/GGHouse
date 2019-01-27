using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour, ISwitch
{
    private Animator anim;

    private bool isOpen;

    void Start()
    {
        isOpen = false;
        anim = GetComponent<Animator>();
    }

    public void Switch()
    {
        if (!isOpen)
        {
            isOpen = true;
            anim.SetTrigger("Switch");
            GameManager.instance.FinishTask();
        }
        else
        {
            isOpen = false;
            anim.SetTrigger("Switch");
        }
    }
}
