using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class randomizeImage : MonoBehaviour
{
    private Image i;
    public Sprite[] options;
    
	void Start ()
    {
        i = GetComponent<Image>();
        int o = Random.Range(0, options.Length);
        i.sprite = options[o];
	}
}
