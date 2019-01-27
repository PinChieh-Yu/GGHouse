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

    private SpriteRenderer renderer;
    public int LayerNumber
    {
        get
        {
            return renderer.sortingOrder;
        }
        set
        {
            renderer.sortingOrder = value;
        }
    }

    void Awake()
    {
        Transform = transform;
        IsInteractable = true;
    }

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        SetHintActive(false);
    }

    public void SetHintActive(bool active)
    {
        transform.Find("hint").gameObject.SetActive(active);
    }
}
