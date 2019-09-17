using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class abilityPush : MonoBehaviour
{
    private Player p;

    public GameObject ring;
    public float pauseTime, rechargeTime;
    private bool canPush;

    private Rigidbody2D rb;

    void Start()
    {
        p = ReInput.players.GetPlayer(GetComponent<playerController>().PLAYERNUMBER);
        rb = GetComponent<Rigidbody2D>();

        canPush = true;

        ring = Resources.Load<GameObject>("push ring");

        pauseTime = 0.35f;
        rechargeTime = 2f;
    }

    void Update ()
    {
        if (p.GetButtonDown("Ability") && canPush)
        {
            StartCoroutine(Push());
        }
	}

    private IEnumerator Push()
    {
        canPush = false;
        GetComponent<playerController>().enabled = false;
        Vector2 v = rb.velocity;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        GameObject spawnedRing = Instantiate(ring, transform.position, Quaternion.identity);
        spawnedRing.GetComponent<pusher>().mother = this.gameObject;
        yield return new WaitForSeconds(pauseTime);
        rb.gravityScale = 1;
        rb.velocity = v;
        GetComponent<playerController>().enabled = true;
        StartCoroutine(RechargePush());
    }

    private IEnumerator RechargePush()
    {
        yield return new WaitForSeconds(rechargeTime);
        canPush = true;
    }
}
