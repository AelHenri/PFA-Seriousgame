using UnityEngine;
using System.Collections;

public class PNJ : MonoBehaviour {

    public float x = 0f;
    public float y = 0f;
    public bool hasDialog = false;

    private bool clickable;
    private bool eventDone = false;
    private Vector3 minSize;
    private Vector2 minColliderSize;
    private float shrinkSpeed = 0.07f;
    private SceneManager sceneManager;
    private BoxCollider2D boxCollider;

	// Use this for initialization
	protected virtual void Start () {
        transform.position = new Vector3(x, y);
        minSize = transform.localScale;
        sceneManager = (SceneManager)FindObjectOfType(typeof(SceneManager));
        
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        minColliderSize = boxCollider.size;
    }
	
	protected virtual void Update()
    {
        Collider2D[] hitZone = Physics2D.OverlapCircleAll(new Vector2(x, y), 1.5f);
        bool hasHit = false;
        if (!eventDone)
        {
            foreach (Collider2D collider in hitZone)
            {
                if (collider.gameObject.tag == "Player")
                {
                    clickable = true;
                    transform.localScale = minSize + new Vector3(0.5f, 0.5f);
                    boxCollider.size = minColliderSize + new Vector2(0.5f, 0.5f);
                    hasHit = true;
                }
            }
            if (hasHit == false)
            {
                clickable = false;
                transform.localScale = minSize;
            }
        }

    }

    protected void OnMouseDown()
    {
        if (clickable)
        {
            LauchPNJEvent();
            transform.localScale = minSize;
            eventDone = true;
        }
    }

    protected void OnMouseEnter()
    {
        if (clickable && !eventDone)
        {
            Debug.Log("coucou");
            transform.GetComponent<SpriteRenderer>().color = new Color(0.9f,0.9f,0.9f);
        }
    }
    protected void OnMouseExit()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    protected virtual void LauchPNJEvent()
    {

    }

    protected void callPlaceArrows()
    {
        sceneManager.PlaceArrows();
    }

    
}
