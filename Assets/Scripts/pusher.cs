﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pusher : MonoBehaviour
{
    public GameObject mother;
    public float expandRate, fadeRate;
    public float maxSize;

    public SpriteRenderer sr;
    public Color c;

    private float a;

    public float pushForce;
    
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = c;

        a = 1;
	}
	
	void Update ()
    {
        float size = transform.localScale.x;
        size += Time.deltaTime * expandRate;
        transform.localScale = new Vector2(size, size);
        
        a -= Time.deltaTime * fadeRate;
        sr.color = new Color(c.r, c.g, c.b, a);

        if (size > maxSize)
            GetComponent<CircleCollider2D>().enabled = false;

        if (a < 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && collision.gameObject != mother)
        {
            Debug.Log("pushing");
            Vector2 direction = collision.gameObject.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * pushForce);
        }
    }
}
