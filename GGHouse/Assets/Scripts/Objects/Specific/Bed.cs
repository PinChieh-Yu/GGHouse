using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject pillow, sheet;
    
    void Start()
    {
        pillow = transform.Find("Pillow").gameObject;
        sheet = transform.Find("Sheet").gameObject;
        GetComponent<Container>().OnPutIn += OnPutIn;
        pillow.SetActive(false);
        sheet.SetActive(false);
    }

    private void OnPutIn(ObjectInfo objInfo)
    {
        if (objInfo.Name == ObjectName.Pillow)
        {
            pillow.SetActive(true);
        }
        else if (objInfo.Name == ObjectName.Sheet)
        {
            sheet.SetActive(true);
        }

        if (pillow.activeSelf && sheet.activeSelf)
        {
            GetComponent<ObjectInfo>().Properties.Remove(ObjectProperty.Container);
            GameManager.instance.FinishTask();
        }
    }
}
