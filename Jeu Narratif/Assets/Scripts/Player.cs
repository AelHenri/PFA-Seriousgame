using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public int speed = 1;

    private Vector3 target;
    private Rigidbody2D rb;
    private bool collided;

    // Use this for initialization
    void Start()
    {
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

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

    /*void OnCollisionEnter2D()
    {
        Debug.Log("Hello");
        collided = true;
    }*/

    private void OnCollisionEnter2D(Collision2D col)
    {
        collided = true;
        Debug.Log("Hello");
        if (col.gameObject.tag == "PNJ")
        {
            //SceneManager sceneManager = GameObject.Find("GameManager").GetComponent(SceneManager);
            SceneManager sceneManager = (SceneManager)FindObjectOfType(typeof(SceneManager));
            sceneManager.PlaceArrows();

            Debug.Log("Coucou");
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.tag == "Arrow")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
