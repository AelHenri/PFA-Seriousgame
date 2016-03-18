using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryPlayer : MonoBehaviour
{

    public int speed = 1;

    private Vector3 target;
    private Rigidbody2D rb;
    private bool collided;
    private StorySceneManager sceneManager;
    private int currentScene;
    private bool paralyzed = false;

    void Start()
    {

        sceneManager = (StorySceneManager)FindObjectOfType(typeof(StorySceneManager));
        currentScene = sceneManager.level;
        float x = sceneManager.scene[currentScene].playerX;
        float y = sceneManager.scene[currentScene].playerY;
        transform.position = new Vector3(x, y);
        target = transform.position;
        rb = GetComponent<Rigidbody2D>();

        
    }
    
    void FixedUpdate()
    {
        //Debug.Log(paralyzed);
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
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        collided = true;
        if (col.gameObject.tag == "PNJ")
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.tag == "Arrow")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        else if (trig.tag == "TP")
        {
            collided = true;
        }
    }

    public void FreezePlayer()
    {
        Debug.Log("Freeze Player !");
        paralyzed = true;
        Debug.Log(paralyzed);
    }

    public void DefreezePlayer()
    {
        Debug.Log("Defreeze Player !");
        Debug.Log(paralyzed);
        paralyzed = false;
        Debug.Log(paralyzed);
    }
   
}
