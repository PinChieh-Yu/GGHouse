using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    private bool isInstalled;
    private Animator animator;
    public Sprite faucet;

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
            GetComponent<SpriteRenderer>().sprite = faucet;
            GetComponent<ObjectInfo>().Properties.Remove(ObjectProperty.Container);
            isInstalled = true;
        }
    }
}
