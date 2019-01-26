using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    [HideInInspector]
    public int Id;
    public ObjectName Name;
    public bool IsInteractable;
    [HideInInspector]
    public Transform Transform;
    public List<ObjectProperty> Properties;

    void Awake()
    {
        Transform = transform;
        IsInteractable = true;
    }

    void Start()
    {
        SetHintActive(false);
    }

    public void SetHintActive(bool active)
    {
        transform.Find("hint").gameObject.SetActive(active);
    }
}
