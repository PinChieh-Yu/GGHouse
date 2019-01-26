using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollideObject : MonoBehaviour {

    [SerializeField]
    private float shellWidth;
    [SerializeField]
    private int rayCount;
    [SerializeField]
    private LayerMask collisionLayerMask;

    private BoxCollider2D bodyCollider;

    [HideInInspector]
    public int ignoreObjectId;

    void Awake()
    {
        bodyCollider = GetComponent<BoxCollider2D>();
        ignoreObjectId = -1;
    }

    public void Move(Vector2 movement)
    {
        VerticalMovementAdapt(ref movement);
        HorizontalMovementAdapt(ref movement);

        transform.Translate(movement);
    }

    private void HorizontalMovementAdapt(ref Vector2 movement)
    {
        Bounds colliderBounds = bodyCollider.bounds;
        float anchorX = movement.x > 0 ? (colliderBounds.max.x - shellWidth) : (colliderBounds.min.x + shellWidth);
        float bottomY = colliderBounds.min.y + shellWidth;
        float spaceLenght = 2f * (colliderBounds.extents.y - shellWidth) / (rayCount - 1);

        float raycastLength = Mathf.Abs(movement.x) + shellWidth;
        Vector2 raycastDirection = movement.x > 0 ? Vector2.right : Vector2.left;
        Vector2 raycastPoint = new Vector2(anchorX, bottomY - spaceLenght);

        for (int i = 0; i < rayCount; i++)
        {
            raycastPoint.y += spaceLenght;
            RaycastHit2D hit = Physics2D.Raycast(raycastPoint, raycastDirection, raycastLength, collisionLayerMask);
            if (hit)
            {
                if (hit.collider.GetComponent<ObjectInfo>() && hit.collider.GetComponent<ObjectInfo>().Id == ignoreObjectId) continue;
                movement.x = (hit.distance - shellWidth) * Mathf.Sign(movement.x);
            }
        }
    }

    private void VerticalMovementAdapt(ref Vector2 movement)
    {
        Bounds colliderBounds = bodyCollider.bounds;
        float anchorY = movement.y > 0 ? (colliderBounds.max.y - shellWidth) : (colliderBounds.min.y + shellWidth);
        float leftX = colliderBounds.min.x + shellWidth;
        float spaceLenght = 2f * (colliderBounds.extents.x - shellWidth) / (rayCount - 1);

        float raycastLength = Mathf.Abs(movement.y) + shellWidth;
        Vector2 raycastDirection = movement.y > 0 ? Vector2.up : Vector2.down;
        Vector2 raycastPoint = new Vector2(leftX - spaceLenght, anchorY);

        for (int i = 0; i < rayCount; i++)
        {
            raycastPoint.x += spaceLenght;
            RaycastHit2D hit = Physics2D.Raycast(raycastPoint, raycastDirection, raycastLength, collisionLayerMask);
            if (hit)
            {
                if (hit.collider.GetComponent<ObjectInfo>() && hit.collider.GetComponent<ObjectInfo>().Id == ignoreObjectId) continue;
                movement.y = (hit.distance - shellWidth) * Mathf.Sign(movement.y);
            }
        }
    }
}
