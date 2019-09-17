using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class playerController : MonoBehaviour
{
    public int PLAYERNUMBER;
    public Color playerColor;
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
    public playerController targetPlayer;
    private CircleCollider2D kissZone;
    public float kissDelay;
    public bool kissing;

    public bool isGrounded;

    public int characterNumber;
    public int abilityNumber;

    public int hearts;
    private GameObject display;
    private GameObject[] dHearts;

    public GameObject cupidTarget;
    public arrowParticles loveParticles;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(PLAYERNUMBER);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        kissZone = transform.GetChild(1).GetComponent<CircleCollider2D>();

        characterNumber = playerData.characterChoice[PLAYERNUMBER];

        if (abilityNumber == 0)
            gameObject.AddComponent<abilityDash>();
        if (abilityNumber == 1)
            gameObject.AddComponent<abilityBlowKiss>();
        if (abilityNumber == 2)
            gameObject.AddComponent<abilityPush>();
        if (abilityNumber == 3)
            gameObject.AddComponent<abilityArrow>();

        hearts = 3;
        display = transform.GetChild(2).gameObject;
        dHearts = new GameObject[4];
        dHearts[0] = display.transform.GetChild(0).gameObject;
        dHearts[1] = display.transform.GetChild(1).gameObject;
        dHearts[2] = display.transform.GetChild(2).gameObject;
        dHearts[3] = display.transform.GetChild(3).gameObject;

        changeHeartsDisplay();

        loveParticles = transform.GetChild(3).GetComponent<arrowParticles>();
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

        if (targetPlayer != null)
        {
            if (canKiss && targetPlayer.canKiss)
            {
                if (p.GetButtonDown("Kiss"))
                {
                    StartCoroutine(Kiss());
                }
            }
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

        if (hearts <= 0)
            Destroy(this.gameObject);
	}

    void Move()
    {
        if (p.GetAxis("Move") > float.Epsilon)
        {
            sr.flipX = false;
            kissZone.offset = new Vector2(0.35f, 0.25f);
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        if (p.GetAxis("Move") < -float.Epsilon)
        {
            sr.flipX = true;
            kissZone.offset = new Vector2(-0.35f, 0.25f);
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
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

    IEnumerator Kiss()
    {
        kissing = true;
        yield return new WaitForSeconds(kissDelay);

        if (targetPlayer.kissing)
        {
            if (hearts < 4)
                hearts++;
            changeHeartsDisplay();

            Debug.Log(name + " and " + targetPlayer.name + " made out");
        }
        else
        {
            targetPlayer.hearts--;
            targetPlayer.changeHeartsDisplay();

            Debug.Log(name + " kissed " + targetPlayer.name);
        }
        
        StartCoroutine(cooldownKiss());
    }

    IEnumerator cooldownKiss()
    {
        canKiss = false;
        yield return new WaitForSeconds(kissCoolTime);
        kissing = false;
        canKiss = true;
    }

    public void changeHeartsDisplay()
    {
        for (int i = 0; i < dHearts.Length; i++)
        {
            if (i < hearts)
                dHearts[i].SetActive(true);
            else
                dHearts[i].SetActive(false);
        }

        if (hearts == 4)
            display.transform.localPosition = new Vector2(0, 0.8f);
        else if (hearts == 3)
            display.transform.localPosition = new Vector2(0.12f, 0.8f);
        else if (hearts == 2)
            display.transform.localPosition = new Vector2(0.235f, 0.8f);
        else if (hearts == 1)
            display.transform.localPosition = new Vector2(0.35f, 0.8f);

        display.GetComponent<heartsFader>().reActivate = true;
    }
}