using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherDesk : MonoBehaviour, ISwitch
{
    private bool isOpen;
    private bool letterIsTaken;
    private ObjectInfo objInfo;
    private SpriteRenderer drawer;
    [SerializeField]
    private Sprite emptyDrawer;
    private Animator animator;

    void Start()
    {
        isOpen = true;
        objInfo = GetComponent<ObjectInfo>();
        drawer = transform.Find("Drawer").GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    public void Switch()
    {
        if (!letterIsTaken)
        {
            GameManager.instance.GetLetter();
            drawer.sprite = emptyDrawer;
            letterIsTaken = true;
            GameManager.instance.FinishTask();
        }
        else
        {
            isOpen = !isOpen;
            animator.SetTrigger("Switch");
        }
    }
}
