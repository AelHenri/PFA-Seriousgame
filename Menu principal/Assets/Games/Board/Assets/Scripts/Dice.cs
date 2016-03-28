using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public int nbFrameToChangeMax = 10;
    public int nbLoopInit = 20;
    public Sprite[] sprites = new Sprite[6];
    public bool roll
    {
        get;
        private set;
    }
    public int currentValue
    {
        get;
        private set;
    }
    public GameObject indicator;
    public bool hasBeenRolled
    {
        get;
        set;
    }
    public bool doubleClickMode = false;

    private int nbFrameToChange;
    private SpriteRenderer sr;
    private int nbFrameSinceStart = 0;
    private int loopCounter = 0;
    private int nbLoop;
    private bool launchable;

	// Use this for initialization
	void Start () 
    {
        hasBeenRolled = false;
        sr = GetComponent<SpriteRenderer>();
        if (doubleClickMode)
            nbFrameToChange = nbFrameToChangeMax;
        else
            nbFrameToChange = 1;
        nbLoop = nbLoopInit;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (roll)
        {
            if(nbFrameSinceStart % 2 == 0)
            {
                if (nbFrameSinceStart % nbFrameToChange == 0)
                {
                    currentValue = Random.Range(1, 7);
                    sr.sprite = sprites[currentValue - 1];
                    loopCounter++;
                    if ((loopCounter == nbLoop || nbLoop < 0) && !doubleClickMode)
                    {
                        loopCounter = 0;
                        nbLoop -= 2;
                        nbFrameToChange++;
                        if (nbFrameToChange == nbFrameToChangeMax)
                        {
                            roll = false;
                            hasBeenRolled = true;
                            nbLoop = nbLoopInit;
                            nbFrameToChange = 1;
                            launchable = false;
                        }
                    }
                }
            }
            nbFrameSinceStart++;
        }
	}

    void OnMouseDown()
    {
        if(launchable)
        {
            if (doubleClickMode)
                roll = !roll;
            if (!doubleClickMode && !roll)
            {
                roll = true;
                indicator.SetActive(false);
            }    
            if (!roll && doubleClickMode)
                hasBeenRolled = true;
        }  
    }

    void OnEnable()
    {
        indicator.SetActive(true);
        launchable = true;
    }
}
