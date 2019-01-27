using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sauce : MonoBehaviour
{
    private bool isInstalled;
    private Animator animator;
    //private Animator anim_Water;
    public Sprite completeSauce;

    // Start is called before the first frame update
    void Start()
    {
        isInstalled = false;
        GetComponent<Container>().OnPutIn += OnPutIn;
        animator = GetComponent<Animator>();
    }

    private void OnPutIn(ObjectInfo objectInfo)
    {
        if (!isInstalled)
        {
            GetComponent<SpriteRenderer>().sprite = completeSauce;
            GetComponent<ObjectInfo>().Properties.Remove(ObjectProperty.Container);
            isInstalled = true;

            GameManager.instance.FinishTask();
        }
    }
}
