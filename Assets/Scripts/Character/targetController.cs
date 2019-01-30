using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class targetController : MonoBehaviour
{
    private Player p;

    private GameObject player;
    private SpriteRenderer sr, playerSR;
    public float distance;

    public bool active;

    public Vector3 dir;

    void Start ()
    {
        player = transform.parent.gameObject;
        sr = GetComponent<SpriteRenderer>();
        playerSR = player.GetComponent<SpriteRenderer>();

        p = ReInput.players.GetPlayer(0);
    }
	
	void Update ()
    {
        dir = new Vector3(p.GetAxis("Target H"), p.GetAxis("Target V"));

        if (dir != Vector3.zero)
        {
            active = true;
            sr.enabled = true;

            transform.position = player.transform.position + dir.normalized * distance;

            if (transform.position.x > player.transform.position.x)
                playerSR.flipX = false;
            if (transform.position.x < player.transform.position.x)
                playerSR.flipX = true;
        }
        else
        {
            active = false;
            sr.enabled = false;

            if (playerSR.flipX == false)
            {
                transform.position = player.transform.position + Vector3.right * distance;
            }
            if (playerSR.flipX == true)
            {
                transform.position = player.transform.position + Vector3.left * distance;
            }
        }
    }
}
