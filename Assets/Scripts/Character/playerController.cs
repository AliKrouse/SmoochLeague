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

    public float groundedSpeed, airSpeed, movementSpeed;
    private string direction = "right";
    public int dashForce, jumpPower;
    public int jumpNumber, dashNumber;
    public int maxJump, maxDash;
    public float dashTime;
    private Vector2 dashDirection;
    
    private bool canKiss = true;
    public float kissCoolTime;
    public GameObject blownKiss;
    public int numberOfKisses;
    public int maxKisses;
    public float rechargeTime;
    private Coroutine recharger;

    public bool isGrounded, isDashing;

    private Vector2 lastVelocity;

    private GameObject target;
    private targetController tController;

    public float stunTime;
    public int blownKissesRecieved;
    public Coroutine stunPlayer;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(PLAYERNUMBER);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        target = transform.GetChild(0).gameObject;
        tController = target.GetComponent<targetController>();

        numberOfKisses = maxKisses;
	}
	
	void Update ()
    {
        if (!isDashing)
        {
            if (isGrounded)
            {
                anim.SetBool("isGrounded", true);
                movementSpeed = groundedSpeed;
            }
            else
            {
                anim.SetBool("isGrounded", false);

                if (direction == "right" && p.GetAxis("Move") < -float.Epsilon)
                {
                    movementSpeed = airSpeed;
                    direction = "left";
                }
                if (direction == "left" && p.GetAxis("Move") > float.Epsilon)
                {
                    movementSpeed = airSpeed;
                    direction = "right";
                }
            }

            if (Mathf.Abs(p.GetAxis("Move")) > float.Epsilon)
            {
                anim.SetBool("isMoving", true);
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
            else
                anim.SetBool("isMoving", false);

            if (rb.velocity.y < 0)
                rb.gravityScale = 2;

            if (p.GetButtonDown("BlowKiss") && canKiss && numberOfKisses > 0)
            {
                GameObject k = Instantiate(blownKiss, target.transform.position, Quaternion.identity);
                numberOfKisses--;
                k.GetComponent<blownKiss>().direction = target.transform.position - transform.position;
                StartCoroutine(cooldownKiss());
                if (recharger == null)
                    recharger = StartCoroutine(rechargeKisses());
            }
        }

        if (jumpNumber > 0)
        {
            if (p.GetButtonDown("Jump"))
            {
                movementSpeed = groundedSpeed;
                Jump();
            }
        }

        if (dashNumber > 0)
        {
            if (p.GetButtonDown("Dash"))
            {
                dashDirection = target.transform.position - transform.position;
                Dash();
            }
        }
	}

    void Jump()
    {
        anim.SetTrigger("jump");
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * jumpPower);
        jumpNumber--;
    }

    void Dash()
    {
        isDashing = true;
        rb.gravityScale = 1;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
        lastVelocity = rb.velocity;

        dashNumber--;

        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        rb.AddForce(dashDirection * dashForce);

        StartCoroutine(returnGravity());
    }

    IEnumerator returnGravity()
    {
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = 1;
        rb.velocity = lastVelocity;
        isDashing = false;
    }

    IEnumerator cooldownKiss()
    {
        canKiss = false;
        yield return new WaitForSeconds(kissCoolTime);
        canKiss = true;
    }

    IEnumerator rechargeKisses()
    {
        while (numberOfKisses < maxKisses)
        {
            yield return new WaitForSeconds(rechargeTime);
            numberOfKisses++;
        }
        Debug.Log("recharge finished");
        recharger = null;
    }

    public IEnumerator stun()
    {
        sr.color = Color.red;
        p.controllers.maps.SetAllMapsEnabled(false);
        while (blownKissesRecieved > 0)
        {
            yield return new WaitForSeconds(stunTime);
            blownKissesRecieved--;
        }
        p.controllers.maps.SetAllMapsEnabled(true);
        stunPlayer = null;
        sr.color = Color.white;
    }
}
