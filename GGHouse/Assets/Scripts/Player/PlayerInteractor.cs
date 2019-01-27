using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public KeyCode InteractionKey;
    public CharacterIdentity identity;

    private PlayerStatus status;

    private List<int> detectedObjectList;
    // Start is called before the first frame update
    void Awake()
    {
        detectedObjectList = new List<int>();
    }

    void Start()
    {
        status = transform.parent.GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(InteractionKey))
        {
            TryInteract();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<ObjectInfo>() != null && col.GetComponent<ObjectInfo>().IsInteractable)
        {
            if (!status.isHolding || col.GetComponent<ObjectInfo>().Id != status.holdingObjectId)
            {
                col.GetComponent<ObjectInfo>().SetHintActive(true);
            }
            detectedObjectList.Add(col.GetComponent<ObjectInfo>().Id);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<ObjectInfo>() != null)
        {
            col.GetComponent<ObjectInfo>().SetHintActive(false);
            detectedObjectList.Remove(col.GetComponent<ObjectInfo>().Id);
        }
    }

    private void TryInteract()
    {
        if (!status.isHolding)
        {
            foreach (int objId in detectedObjectList)
            {
                ObjectInfo info = GameManager.instance.GetObjectInfo(objId);
                Debug.Log(info.Name + ":" + info.IsInteractable.ToString());
                if (!info.IsInteractable) continue;
                if (info.Properties.Contains(ObjectProperty.Switch))
                {
                    info.GetComponent<ISwitch>().Switch();
                    return;
                }
                if (info.Properties.Contains(ObjectProperty.Portable))
                {
                    info.GetComponent<Portable>().PickUp(identity, transform);
                    status.HoldObject(objId);
                    return;
                }
            }
        }
        else
        {
            foreach (int objId in detectedObjectList)
            {
                ObjectInfo info = GameManager.instance.GetObjectInfo(objId);
                if (info.Properties.Contains(ObjectProperty.Container))
                {
                    Debug.Log("Container meet " + GameManager.instance.GetObjectInfo(status.holdingObjectId));
                    if (info.GetComponent<Container>().PutIn(status.holdingObjectId))
                    {
                        GameManager.instance.GetObjectInfo(status.holdingObjectId).IsInteractable = false;
                        GameManager.instance.GetObjectInfo(status.holdingObjectId).gameObject.SetActive(false);
                        status.ReleaseObject();
                        return;
                    }
                }
            }

            GameManager.instance.GetObjectInfo(status.holdingObjectId).GetComponent<Portable>().PutDown(identity);
            status.ReleaseObject();
            return;
        }
    }
}
