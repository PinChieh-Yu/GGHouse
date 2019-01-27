using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Container))]
public class Closet : MonoBehaviour, ISwitch
{
    private Animator animator;
    private ObjectInfo objInfo;

    private GameObject[] clothes = new GameObject[3];

    private bool isOpen;

    void Start()
    {
        isOpen = false;
        objInfo = GetComponent<ObjectInfo>();
        animator = GetComponent<Animator>();
        clothes[0] = transform.Find("TShirt").gameObject;
        clothes[1] = transform.Find("Skirt").gameObject;
        clothes[2] = transform.Find("Pants").gameObject;
        for (int i = 0; i < 3; i++) clothes[i].SetActive(false);
        GetComponent<Container>().OnPutIn += OnClothesPutIn;
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

        if (clothes[0].activeSelf && clothes[1].activeSelf && clothes[2].activeSelf && !isOpen)
        {
            GameManager.instance.FinishTask();
        }
    }

    private void OnClothesPutIn (ObjectInfo objInfo)
    {
        Debug.Log("Put in " + objInfo.Name);
        if (objInfo.Name == ObjectName.TShirt)
        {
            clothes[0].SetActive(true);
        }
        else if (objInfo.Name == ObjectName.Skirt)
        {
            clothes[1].SetActive(true);
        }
        else if (objInfo.Name == ObjectName.Pants)
        {
            clothes[2].SetActive(true);
        }
    }
}
