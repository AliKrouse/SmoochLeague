using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class slipperyFloor : MonoBehaviour
{
    public float slideVelocity;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            playerController pc = collision.gameObject.GetComponent<playerController>();

            if (Mathf.Abs(ReInput.players.GetPlayer(pc.PLAYERNUMBER).GetAxis("Move")) > float.Epsilon)
                SetVelocity(rb, sr);
        }
    }

    public void SetVelocity(Rigidbody2D rb, SpriteRenderer sr)
    {
        if (sr.flipX)
        {
            rb.velocity = Vector2.left * slideVelocity;
        }
        else
        {
            rb.velocity = Vector2.right * slideVelocity;
        }
    }
}
