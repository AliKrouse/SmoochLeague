using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorBelt : MonoBehaviour
{
    public Vector2 direction;
    private Transform goTowards;
    public float speed;
    
	void Start ()
    {
        goTowards = transform.GetChild(1);
	}
	
	void Update ()
    {
		
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        //collision.gameObject.transform.Translate(direction * speed);
        Vector2 targetPosition = new Vector2(goTowards.position.x, collision.gameObject.transform.position.y);
        collision.gameObject.transform.position = Vector2.MoveTowards(collision.gameObject.transform.position, targetPosition, Time.deltaTime * speed);
    }
}
