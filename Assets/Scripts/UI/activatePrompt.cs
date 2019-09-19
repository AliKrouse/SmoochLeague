using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activatePrompt : MonoBehaviour
{
    public GameObject prompt;
    public float waitTime;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (waitTime > 0)
            waitTime -= Time.deltaTime;

        if (waitTime <= 0)
            prompt.SetActive(true);
	}
}
