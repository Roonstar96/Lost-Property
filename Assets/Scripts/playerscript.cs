﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerscript : MonoBehaviour
{
    public LayerMask groundlayer;
    public bool isGrounded = false;
    public Transform groundcheck;
    public bool facingRight = true;
    public float speed;
    public float move_velocity;
    public float jump;
    public Animator animator;
    private Rigidbody2D playerbody;
    public BoxCollider2D playercollider;
    public bool isAttacking = false;
    public int lifePoints;

    // Start is called before the first frame update
    void Start()
    {
        playercollider = this.gameObject.GetComponent<BoxCollider2D>();
        playerbody = this.gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetBool("isAttacking", isAttacking);
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.2f, groundlayer);
        animator.SetFloat("Speed", Mathf.Abs(move_velocity));
        animator.SetFloat("Jump_speed", Mathf.Abs(playerbody.velocity.y));
        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isGrounded == true)
        {
            playerbody.velocity = new Vector2(move_velocity, jump);
        }
        move_velocity = 0;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            move_velocity -= speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            move_velocity += speed;
        }
        playerbody.velocity = new Vector2(move_velocity, playerbody.velocity.y);
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isAttacking = true;
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            isAttacking = false;
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

