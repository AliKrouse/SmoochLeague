using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeInAndOut : MonoBehaviour
{
    public float fadeTime;
    private Image i;
    private Text t;
    private bool fadingIn;
    private float a;

    void Start()
    {
        if (GetComponent<Image>() != null)
            i = GetComponent<Image>();
        if (GetComponent<Text>() != null)
            t = GetComponent<Text>();

        a = 0;
        fadingIn = true;
    }
    
    void Update()
    {
        if (fadingIn)
        {
            a += Time.deltaTime * fadeTime;

            if (i != null)
                i.color = new Color(i.color.r, i.color.g, i.color.b, a);
            if (t != null)
                t.color = new Color(t.color.r, t.color.g, t.color.b, a);

            if (a >= 1)
                fadingIn = false;
        }
        else
        {
            a -= Time.deltaTime * fadeTime;

            if (i != null)
                i.color = new Color(i.color.r, i.color.g, i.color.b, a);
            if (t != null)
                t.color = new Color(t.color.r, t.color.g, t.color.b, a);

            if (a <= 0)
                fadingIn = true;
        }
    }
}
