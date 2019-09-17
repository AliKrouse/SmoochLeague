using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class abilityArrow : MonoBehaviour
{
    private Player p;

    public GameObject arrow;

    public playerController target1, target2;
    public cupidsArrow a1, a2;

    public float rechargeTime1, rechargeTime2;
    public bool canFire;
    private int arrowsFired;

    public float arrowForce;

    private GameObject target;

    public float loveStruckTime;
    
	void Start ()
    {
        p = ReInput.players.GetPlayer(GetComponent<playerController>().PLAYERNUMBER);

        target = transform.GetChild(0).gameObject;
        arrow = Resources.Load<GameObject>("arrow");

        canFire = true;

        rechargeTime1 = 1;
        rechargeTime2 = 10;
        arrowForce = 750;

        loveStruckTime = 8;
    }
	
	void Update ()
    {
        if (p.GetButtonDown("Ability") && canFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject a = Instantiate(arrow, target.transform.position, Quaternion.identity);
        a.GetComponent<cupidsArrow>().mom = this;
        Vector2 direction = target.transform.position - transform.position;
        a.GetComponent<Rigidbody2D>().AddForce(direction * arrowForce);
        arrowsFired++;

        if (a1 == null)
            a1 = a.GetComponent<cupidsArrow>();
        else if (a2 == null)
            a2 = a.GetComponent<cupidsArrow>();

        if (arrowsFired == 1)
            StartCoroutine(ShortRecharge());
        if (arrowsFired == 2)
            StartCoroutine(LongRecharge());
    }

    IEnumerator ShortRecharge()
    {
        canFire = false;
        yield return new WaitForSeconds(rechargeTime1);
        canFire = true;
    }

    IEnumerator LongRecharge()
    {
        canFire = false;
        yield return new WaitForSeconds(rechargeTime2);
        arrowsFired = 0;
        canFire = true;
    }

    public IEnumerator TiePlayers()
    {
        target1.cupidTarget = target2.gameObject;
        target1.loveParticles.target = target2.transform;
        target1.loveParticles.c = target2.playerColor;
        target1.loveParticles.StartParticles();

        target2.cupidTarget = target1.gameObject;
        target2.loveParticles.target = target1.transform;
        target2.loveParticles.c = target1.playerColor;
        target2.loveParticles.StartParticles();

        yield return new WaitForSeconds(loveStruckTime);

        target1.cupidTarget = null;
        target1.loveParticles.StopParticles();

        target2.cupidTarget = null;
        target2.loveParticles.StopParticles();

        target1 = null;
        target2 = null;
        Destroy(a1.gameObject);
        Destroy(a2.gameObject);
    }
}
