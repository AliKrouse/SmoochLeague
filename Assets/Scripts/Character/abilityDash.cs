﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class abilityDash : MonoBehaviour
{
    private Player p;
    private bool dashing, canDash;
    public float speed, dashTime, rechargeTime;

    private GameObject target;

    private Rigidbody2D rb;

    public GameObject trail;

    void Start ()
    {
        p = ReInput.players.GetPlayer(GetComponent<playerController>().PLAYERNUMBER);

        target = transform.GetChild(0).gameObject;

        canDash = true;
        speed = 25;
        dashTime = 0.075f;
        rechargeTime = 1;

        rb = GetComponent<Rigidbody2D>();

        trail = Resources.Load<GameObject>("dash trail");
    }
	
	void Update ()
    {
        if (p.GetButtonDown("Ability") && canDash)
            StartCoroutine(Dash());

        if (dashing)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        }
    }

    private IEnumerator Dash()
    {
        GetComponent<playerController>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);

        float mag = rb.velocity.magnitude;
        Vector2 dir = target.transform.position - transform.position;

        GameObject dashTrail = Instantiate(trail, transform);

        dashing = true;
        yield return new WaitForSeconds(dashTime);
        dashing = false;

        rb.velocity = dir * mag;

        dashTrail.GetComponent<killTrail>().enabled = true;
        
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<playerController>().enabled = true;
        StartCoroutine(rechargeDash());
    }

    private IEnumerator rechargeDash()
    {
        canDash = false;
        yield return new WaitForSeconds(rechargeTime);
        canDash = true;
    }
}
