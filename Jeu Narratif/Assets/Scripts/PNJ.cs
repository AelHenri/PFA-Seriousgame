using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PNJ : MonoBehaviour {

    public float x = 0f;
    public float y = 0f;
    public bool hasDialog = false;
    public string[] dialog;
    public string PNJName;
    public bool firstTime = true;
    /*public GameObject messageBox;
    public Text messageBoxText;
    public bool messageBoxEnabled = false;*/

    private bool clickable;
    private bool eventDone = false;
    private Vector3 minSize;
    private Vector2 minColliderSize;
    private float shrinkSpeed = 0.07f;
    private StorySceneManager sceneManager;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private StoryGameManager gameManager;

	// Use this for initialization
	protected virtual void Start () {
        transform.position = new Vector3(x, y);
        minSize = transform.localScale;
        sceneManager = (StorySceneManager)FindObjectOfType(typeof(StorySceneManager));
        gameManager = (StoryGameManager)FindObjectOfType(typeof(StoryGameManager));
        animator = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        minColliderSize = boxCollider.size;

        PNJLoadEvent();
    }
	
	protected virtual void Update()
    {
        Collider2D[] hitZone = Physics2D.OverlapCircleAll(new Vector2(x, y), 1.5f);
        bool hasHit = false;
        if (!eventDone && hasDialog)
        {
            foreach (Collider2D collider in hitZone)
            {
                if (collider.gameObject.tag == "Player")
                {
                    clickable = true;
                    //transform.localScale = minSize + new Vector3(0.5f, 0.5f);
                    //boxCollider.size = minColliderSize + new Vector2(0.5f, 0.5f);
                    animator.SetBool("isNearby", true);
                    hasHit = true;
                }
            }
            if (hasHit == false)
            {
                clickable = false;
                animator.SetBool("isNearby", false);
                //transform.localScale = minSize;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //messageBox.SetActive(false);
        }

    }

    protected void OnMouseDown()
    {
        if (clickable)
        {
            PNJClickEvent();
            animator.SetBool("isNearby", false);
            transform.localScale = minSize;
            eventDone = true;
        }
    }

    protected virtual void PNJClickEvent()
    {
        if (!gameManager.IsPNJPresent(PNJName))
        {
            gameManager.AddPNJ(PNJName);
        }
    }

    protected virtual void PNJLoadEvent()
    {

    }

    protected void callPlaceArrows()
    {
        sceneManager.PlaceArrows();
    }

    /*protected void displayDialog()
    {
        messageBox.SetActive(true);
        StopAllCoroutines();
        messageBoxEnabled = true;
        messageBoxText.text = dialog[0];
    }*/

    
}
