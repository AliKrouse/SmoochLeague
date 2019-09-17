using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kissChecker : MonoBehaviour
{
    private playerController player;
    private playerController otherPlayer;

    private void Start()
    {
        player = transform.parent.GetComponent<playerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (otherPlayer == null)
            {
                if (player.cupidTarget == null)
                {
                    otherPlayer = collision.gameObject.GetComponent<playerController>();
                    player.targetPlayer = otherPlayer;
                }
                else
                {
                    if (collision.gameObject == player.cupidTarget)
                    {
                        otherPlayer = collision.gameObject.GetComponent<playerController>();
                        player.targetPlayer = otherPlayer;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (otherPlayer != null)
        {
            if (collision.gameObject == otherPlayer.gameObject)
            {
                otherPlayer = null;
                player.targetPlayer = null;
            }
        }
    }
}
