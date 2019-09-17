using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stun : MonoBehaviour
{
    public float stunTime;
    private playerController player;
    
	void Start ()
    {
        player = GetComponent<playerController>();
        stunTime = 1.5f;

        StartCoroutine(stunPlayer());
	}

    private IEnumerator stunPlayer()
    {
        player.enabled = false;
        GetComponent<SpriteRenderer>().flipY = true;
        yield return new WaitForSeconds(stunTime);
        GetComponent<SpriteRenderer>().flipY = false;
        player.enabled = true;
        Destroy(this);
    }
}
