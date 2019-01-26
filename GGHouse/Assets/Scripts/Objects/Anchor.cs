using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    private Vector3 relativePosition;
    [HideInInspector]
    public bool isAnchored;

    void Awake()
    {
        isAnchored = false;
    }

    void Update()
    {
        if (isAnchored)
        {
            transform.position = target.position + relativePosition;
        }
    }

    public void SetAnchor(Transform parentTransform)
    {
        isAnchored = true;
        target = parentTransform;
        relativePosition = transform.position - parentTransform.position;
        Debug.Log(name + " set anchor on " + parentTransform.name);
    }

    public void ResetAnchor()
    {
        isAnchored = false;
    }
}
