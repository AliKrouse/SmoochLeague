using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class playerController : MonoBehaviour
{
    public int PLAYERNUMBER;
    private Player p;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    public float movementSpeed;
    public int jumpPower;
    public int jumpNumber;
    public int maxJump;
    
    private bool canKiss = true;
    public float kissCoolTime;

    public bool isGrounded;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(PLAYERNUMBER);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        if (isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        if (Mathf.Abs(p.GetAxis("Move")) > float.Epsilon)
        {
            anim.SetBool("isMoving", true);
            Move();
        }
        else
        {
            anim.SetBool("isMoving", false);
            if (isGrounded)
                rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (rb.velocity.y < 0)
            rb.gravityScale = 2;

        if (jumpNumber > 0)
        {
            if (p.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
	}

    void Move()
    {
        if (p.GetAxis("Move") > float.Epsilon)
        {
            sr.flipX = false;
        }
        if (p.GetAxis("Move") < -float.Epsilon)
        {
            sr.flipX = true;
        }

        rb.velocity = new Vector2(p.GetAxis("Move") * movementSpeed, rb.velocity.y);
    }

    void Jump()
    {
        anim.SetTrigger("jump");
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower);
        jumpNumber--;
    }

    IEnumerator cooldownKiss()
    {
        canKiss = false;
        yield return new WaitForSeconds(kissCoolTime);
        canKiss = true;
    }
}
