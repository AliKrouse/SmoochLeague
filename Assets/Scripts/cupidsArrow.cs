using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupidsArrow : MonoBehaviour
{
    public float destroyTime;
    public abilityArrow mom;

    private playerController hitPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<CircleCollider2D>().enabled = false;

        if (collision.gameObject.CompareTag("Player"))
        {
            transform.parent = collision.gameObject.transform;
            hitPlayer = collision.gameObject.GetComponent<playerController>();

            if (mom.target1 == null)
            {
                mom.target1 = hitPlayer;
            }
            else if (mom.target2 == null)
            {
                mom.target2 = hitPlayer;
                mom.StartCoroutine(mom.TiePlayers());
            }
        }
        else
        {
            StartCoroutine(DestroyArrow());
        }
    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
