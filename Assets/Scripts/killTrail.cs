using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killTrail : MonoBehaviour
{
	void Update ()
    {
        GetComponent<TrailRenderer>().time -= Time.deltaTime;
	}
}
