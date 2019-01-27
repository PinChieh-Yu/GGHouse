using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, ISwitch
{
    private Animator animator;
    bool isFinish;

    void Start()
    {
        isFinish = false;
        animator = transform.Find("Gas").GetComponent<Animator>();
    }

    public void Switch()
    {
        if (!isFinish)
        {
            animator.SetTrigger("Switch");
            isFinish = true;
            GameManager.instance.FinishTask();
        }
    }
}
