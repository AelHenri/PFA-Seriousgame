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
    private DialogManager dialogManager;

    private bool isAnswering = false;

	// Use this for initialization
	protected virtual void Start () {
        transform.position = new Vector3(x, y);
        minSize = transform.localScale;

        /*if (hasDialog)
        {
            dialogManager = GetComponent<DialogManager>();
        }*/
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
                    animator.SetBool("isNearby", true);
                    hasHit = true;
                }
            }
            if (hasHit == false)
            {
                clickable = false;
                animator.SetBool("isNearby", false);
            }
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            //messageBox.SetActive(false);
        }

        QuestionEvent();

    }

    protected void OnMouseDown()
    {
        if (clickable)
        {
            PNJClickEvent();
            animator.SetBool("isNearby", false);
            transform.localScale = minSize;
            eventDone = true;
            clickable = false;
        }
    }

    protected virtual void PNJClickEvent()
    {
        Question();
    }

    protected virtual void Question()
    {
        GlobalQuestionnaire.startQuestionnaire();
        isAnswering = true;
    }

    protected virtual void QuestionEvent()
    {
        if (isAnswering)
        {
            if (GlobalQuestionnaire.hasAnswered)
            {
                if (GlobalQuestionnaire.isAnswerRight)
                {
                    
                    if (!gameManager.IsPNJPresent(PNJName))
                    {
                        gameManager.AddPNJ(PNJName);
                    }
                }
                else
                {
                    //TRUC-A-FAIRE-SI-C-EST-PAS-BON
                }
                isAnswering = false;
            }
        }
    }


    protected virtual void PNJLoadEvent()
    {

    }

    protected void callPlaceArrows()
    {
        sceneManager.PlaceArrows();
    }

    protected void callPlaceTPs()
    {
        sceneManager.PlaceTPs();
    }


    protected void displayDialog(int i)
    {
        gameManager.GetMessage(dialog[i]);
        gameManager.InteractEvent();          
    }

    
}
