using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowParticles : MonoBehaviour
{
    public Transform target;
    public Color c;
    private ParticleSystem ps;
    public bool isPlaying;

    void Start()
    {
        ps = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
    
    void Update()
    {
        transform.LookAt(target);
    }

    public void StartParticles()
    {
        ps.startColor = c;
        ps.Play();
        isPlaying = true;
    }

    public void StopParticles()
    {
        ps.Stop();
        isPlaying = false;
    }
}
