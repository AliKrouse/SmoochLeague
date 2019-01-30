using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    public Transform partnerObject;
    private portal partner;

    private bool canBeUsed = true;
    public float rechargeTime;

	void Start ()
    {
        partner = partnerObject.gameObject.GetComponent<portal>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeUsed)
        {
            collision.gameObject.transform.position = partnerObject.position;
            StartCoroutine(rechargePortal());
            partner.StartCoroutine(partner.rechargePortal());
        }
    }

    public IEnumerator rechargePortal()
    {
        canBeUsed = false;
        yield return new WaitForSeconds(rechargeTime);
        canBeUsed = true;
    }
}
