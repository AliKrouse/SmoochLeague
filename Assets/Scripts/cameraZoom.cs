using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    private Camera c;

    public List<Transform> players = new List<Transform>();
    public float minY, maxY;
    public float minOrtho, maxOrtho;
    public float zoomSpeed;
    
	void Start ()
    {
        c = GetComponent<Camera>();

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (g.activeSelf)
                players.Add(g.transform);
        }
	}
	
	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, Midpoint(), -10), Time.deltaTime * 10);

        if (FurthestPlayerDistance() > c.orthographicSize && c.orthographicSize < maxOrtho)
        {
            c.orthographicSize += Time.deltaTime * zoomSpeed;
        }

        if (FurthestPlayerDistance() < (c.orthographicSize * 0.75) && c.orthographicSize > minOrtho)
        {
            c.orthographicSize -= Time.deltaTime * zoomSpeed;
        }

        if (c.orthographicSize > maxOrtho)
            c.orthographicSize = maxOrtho;
        if (c.orthographicSize < minOrtho)
            c.orthographicSize = minOrtho;
	}

    private float Midpoint()
    {
        float midY = 0;
        float highestPlayer = 0;
        float lowestPlayer = 0;

        foreach (Transform t in players)
        {
            if (t.position.y > highestPlayer)
                highestPlayer = t.position.y;
            if (t.position.y < lowestPlayer)
                lowestPlayer = t.position.y;
        }

        midY = (highestPlayer + lowestPlayer) / 2;
        if (midY < minY)
            midY = minY;
        if (midY > maxY)
            midY = maxY;

        return midY;
    }

    private float FurthestPlayerDistance()
    {
        float furthest = 0;

        foreach (Transform t in players)
        {
            Vector2 tSimple = new Vector2(0, t.position.y);
            Vector2 pSimple = new Vector2(0, transform.position.y);
            float d = Vector2.Distance(tSimple, pSimple);
            if (d > furthest)
                furthest = d;
        }

        return furthest;
    }
}
