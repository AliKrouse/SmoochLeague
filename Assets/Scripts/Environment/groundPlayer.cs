using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundPlayer : MonoBehaviour
{
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController player = collision.gameObject.GetComponent<playerController>();
            player.isGrounded = true;
            player.jumpNumber = player.maxJump;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<playerController>().isGrounded = false;
    }
}
