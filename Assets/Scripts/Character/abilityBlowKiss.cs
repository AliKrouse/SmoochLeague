using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class abilityBlowKiss : MonoBehaviour
{
    private Player p;
    public GameObject kiss;
    public int kisses;
    public int maxKisses;
    public float rechargeTime;

    private GameObject target;

    private Coroutine recharge;

    void Start ()
    {
        p = ReInput.players.GetPlayer(GetComponent<playerController>().PLAYERNUMBER);

        target = transform.GetChild(0).gameObject;

        kiss = Resources.Load<GameObject>("blown kiss");
        maxKisses = 3;
        kisses = maxKisses;
        rechargeTime = 0.5f;
    }
	
	void Update ()
    {
        if (p.GetButtonDown("Ability") && kisses > 0)
            BlowKiss();
	}

    void BlowKiss()
    {
        GameObject k = Instantiate(kiss, target.transform.position, Quaternion.identity);
        k.GetComponent<blownKiss>().direction = target.transform.position - transform.position;
        kisses--;
        if (recharge == null)
            recharge = StartCoroutine(rechargeKisses());
    }

    public IEnumerator rechargeKisses()
    {
        while (kisses < maxKisses)
        {
            yield return new WaitForSeconds(rechargeTime);
            kisses++;
        }
        if (kisses == maxKisses)
            recharge = null;
    }
}
