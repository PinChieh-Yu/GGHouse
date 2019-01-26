using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public KeyCode UpKey, DownKey, LeftKey, RightKey;
    [SerializeField]
    private float speed;

    private Transform view;

    private BoxCollider2D viewCollider;

    private PlayerStatus status;

    private Vector2 direction;
    private bool isMoving;

    private CollideObject collide;
    private Animator animator;
    private bool isLeft = true;

    void Start()
    {
        collide = GetComponent<CollideObject>();
        viewCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        view = transform.GetChild(0);
        status = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (CheckPlayerCanMove())
        {
            Move();
        }
        animator.SetBool("isMoving", isMoving);
    }

    private bool CheckPlayerCanMove()
    {
        return !status.IsAnchored && !(status.isHolding && !GameManager.instance.GetObjectInfo(status.holdingObjectId).GetComponent<Portable>().IsReadyToMove);
    }

    void Move()
    {
        direction = Vector2.zero;
        isMoving = false;
        if (Input.GetKey(UpKey))
        {
            isMoving = true;
            direction += Vector2.up;
        }
        if (Input.GetKey(DownKey))
        {
            isMoving = true;
            direction += Vector2.down;
        }
        if (Input.GetKey(LeftKey))
        {
            isMoving = true;
            if (!isLeft)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isLeft = true;
            }
            direction += Vector2.left;
        }
        if (Input.GetKey(RightKey))
        {
            isMoving = true;
            if (isLeft)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                isLeft = false;
            }
            direction += Vector2.right;
        }

        if (isMoving)
        {
            collide.Move(direction * speed);
            Rotate();
        }
    }

    void Rotate()
    {
        if (direction.y > 0)
        {
            viewCollider.offset = new Vector2(0f, -1.8f);
            viewCollider.size = new Vector2(1.4f, 0.7f);
        }

        if (direction.x != 0)
        {
            viewCollider.offset = new Vector2(-1f, -2f);
            viewCollider.size = new Vector2(1.5f, 0.7f);
        }

        if (direction.y < 0)
        {
            viewCollider.offset = new Vector2(0f, -2.5f);
            viewCollider.size = new Vector2(1.4f, 0.7f);
        }
        //view.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, direction));
    }
}
