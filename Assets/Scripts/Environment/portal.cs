using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public Transform partnerObject;
    private portal partner;

    private bool canBeUsed = true;
    public float rechargeTime;

    private SpriteRenderer sr;
    private Color c;

	void Start ()
    {
        partner = partnerObject.gameObject.GetComponent<portal>();

        if (GetComponent<SpriteRenderer>() != null)
            sr = GetComponent<SpriteRenderer>();

        if (sr != null)
            c = sr.color;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canBeUsed)
        {
            Debug.Log(collision.gameObject.name + " entered portal at " + transform.position);
            collision.gameObject.transform.position = partnerObject.position;

            StartCoroutine(rechargePortal());
            partner.StartCoroutine(partner.rechargePortal());
        }
    }

    public IEnumerator rechargePortal()
    {
        canBeUsed = false;

        if (sr != null)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }

        yield return new WaitForSeconds(rechargeTime);

        if (sr != null)
        {
            GetComponent<SpriteRenderer>().color = c;
        }

        canBeUsed = true;
    }
}
