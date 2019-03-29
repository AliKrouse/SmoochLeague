using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuCharacters : MonoBehaviour
{
    public int characterNumber;
    public string characterName;

    public bool isSelected;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (isSelected)
        {
            transform.localScale = Vector2.one;
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            transform.localScale = new Vector2(0.9f, 0.9f);
            GetComponent<Image>().color = new Color(0.82f, 0.82f, 0.82f);
        }
	}
}
