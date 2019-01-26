using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour, ISwitch
{
    private Animator animator;
    private ObjectInfo objInfo;

    private bool isOpen;

    void Start()
    {
        isOpen = false;
        objInfo = GetComponent<ObjectInfo>();
        animator = GetComponent<Animator>();
    }

    public void Switch()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.SetTrigger("Switch");
            objInfo.Properties.Add(ObjectProperty.Container);
        }
        else
        {
            isOpen = false;
            animator.SetTrigger("Switch");
            objInfo.Properties.Remove(ObjectProperty.Container);
        }
    }
}
