using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [HideInInspector]
    public bool isHolding;
    [HideInInspector]
    public int holdingObjectId;

    private CollideObject collide;

    private Anchor anchor;
    public bool IsAnchored { get { return anchor.isAnchored; } }

    void Awake()
    {
        isHolding = false;
    }

    void Start()
    {
        collide = GetComponent<CollideObject>();
        anchor = GetComponent<Anchor>();
    }

    public void HoldObject(int objId)
    {
        isHolding = true;
        holdingObjectId = objId;
        collide.ignoreObjectId = objId;
    }

    public void ReleaseObject()
    {
        isHolding = false;
        collide.ignoreObjectId = -1;
    }

    public void SetAnchor(Transform transform)
    {
        anchor.SetAnchor(transform);
    }

    public void ResetAnchor()
    {
        anchor.ResetAnchor();
    }
}
