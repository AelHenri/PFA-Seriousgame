using UnityEngine;
using System.Collections;

public class StoryPlayer : MonoBehaviour
{

    public int speed = 1;

    private Vector3 target;
    private Rigidbody2D rb;
    private bool collided;
    private SceneManager sceneManager;
    private int currentScene;
    private bool paralyzed = false;

    // Use this for initialization
    void Start()
    {
        sceneManager = (SceneManager)FindObjectOfType(typeof(SceneManager));
        currentScene = sceneManager.level;
        float x = sceneManager.scene[currentScene].playerX;
        float y = sceneManager.scene[currentScene].playerY;
        transform.position = new Vector3(x, y);
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!paralyzed)
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
        else
        {
            Paralyze();
        }
    }

    public void Paralyze()
    {
        if (!paralyzed)
        {
            paralyzed = true;
        }
        else
        {
            paralyzed = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        collided = true;
        if (col.gameObject.tag == "PNJ")
        {
            //SceneManager sceneManager = GameObject.Find("GameManager").GetComponent(SceneManager);
            
            //sceneManager.PlaceArrows();
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
