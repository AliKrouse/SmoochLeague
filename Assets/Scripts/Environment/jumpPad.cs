using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    public float jumpForce;
    private Vector2 direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction = transform.up;
        Debug.DrawRay(transform.position, direction * 5, Color.red, 1);

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1;
        rb.AddRelativeForce(direction * jumpForce);
    }
}
