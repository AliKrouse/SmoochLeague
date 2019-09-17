using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blownKiss : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
	
	void Update ()
    {
        transform.position += (Vector3)direction * speed;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.AddComponent<stun>();

            //playerController player = collision.gameObject.GetComponent<playerController>();
            //if (player.stunPlayer == null)
            //    player.stunPlayer = player.StartCoroutine(player.stun());
        }
        Destroy(this.gameObject);
    }
}
