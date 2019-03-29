using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSpotlight : MonoBehaviour
{
    private RectTransform rt;
    public float speed;
    public bool hasBeenMoved;
    
	void Start ()
    {
        rt = GetComponent<RectTransform>();
	}
	
	void Update ()
    {
        if (rt.sizeDelta.x < 300)
        {
            if (hasBeenMoved)
            {
                float newX = rt.sizeDelta.x;
                newX += Time.deltaTime * speed;
                rt.sizeDelta = new Vector2(newX, 500);
            }
        }
        else
            hasBeenMoved = false;
	}
}
