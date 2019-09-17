using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartsFader : MonoBehaviour
{
    public bool reActivate;

    private SpriteRenderer[] hearts;
    private Color c;

    public float waitTime;
    private float timer;

    private float a;
    public float fadeTime;

	void Start ()
    {
        hearts = new SpriteRenderer[4];
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i).GetComponent<SpriteRenderer>();
        }

        c = hearts[0].color;
	}

	void Update ()
    {
        if (reActivate)
        {
            timer = waitTime;

            a = 1;
            foreach (SpriteRenderer h in hearts)
                h.color = new Color(c.r, c.g, c.b, a);

            reActivate = false;
        }

        if (timer > 0)
            timer -= Time.deltaTime;

        if (timer <= 0 && a > 0)
        {
            a -= Time.deltaTime * fadeTime;

            foreach (SpriteRenderer h in hearts)
                h.color = new Color(c.r, c.g, c.b, a);
        }
	}
}
