using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int speed = 1;

    private Vector3 target;
    private Rigidbody2D rb;
    private bool collided;

	// Use this for initialization
	void Start () {
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
	    if (Input.GetMouseButton(0))
        {
            collided = false;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
        }

        if (!collided)
        {   
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        
	}

    void OnCollisionEnter2D()
    {
        collided = true;
        /*rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;*/
    }
}
