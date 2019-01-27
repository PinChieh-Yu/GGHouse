using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public KeyCode UpKey, DownKey, LeftKey, RightKey;
    [SerializeField]
    private float speed;

    private BoxCollider2D viewCollider;
    private SpriteRenderer renderer;

    private PlayerStatus status;

    private Vector2 direction;
    private bool isMoving;

    private CollideObject collide;
    private Animator animator;
    private bool isLeft = true;
    private int anim_direc;

    void Start()
    {
        collide = GetComponent<CollideObject>();
        viewCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        status = GetComponent<PlayerStatus>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (CheckPlayerCanMove())
        {
            Move();
        }
        animator.SetBool("isMoving", isMoving);
        animator.SetInteger("dir", anim_direc);
        renderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10);
    }

    private bool CheckPlayerCanMove()
    {
        return !status.IsAnchored && !(status.isHolding && !GameManager.instance.GetObjectInfo(status.holdingObjectId).GetComponent<Portable>().IsReadyToMove);
    }

    public void Move()
    {
        direction = Vector2.zero;
        isMoving = false;
        if (Input.GetKey(UpKey))
        {
            isMoving = true;
            direction += Vector2.up;
            //anim_direc = 0;//back
        }
        if (Input.GetKey(DownKey))
        {
            isMoving = true;
            direction += Vector2.down;
            //anim_direc = 1;//front
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
            //anim_direc = 2;
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
            //anim_direc = 2;
        }
        direction.Normalize();

        if (isMoving)
        {
            collide.Move(direction * speed);
            RotateView();
        }
    }

    private void RotateView()
    {
        if (direction.y > 0)
        {
            viewCollider.offset = new Vector2(0f, -1.8f);
            viewCollider.size = new Vector2(1.4f, 0.7f);
            anim_direc = 0;
        }

        if (direction.x != 0)
        {
            viewCollider.offset = new Vector2(-1f, -2f);
            viewCollider.size = new Vector2(1.5f, 0.7f);
            anim_direc = 2;
        }

        if (direction.y < 0)
        {
            viewCollider.offset = new Vector2(0f, -2.5f);
            viewCollider.size = new Vector2(1.4f, 0.7f);
            anim_direc = 1;
        }
    }
}
