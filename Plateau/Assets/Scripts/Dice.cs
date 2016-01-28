using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public int nbFrameToChangeMax = 10;
    private int nbFrameToChange;
    private SpriteRenderer sr;
    private int nbFrameSinceStart = 0;
    public Sprite[] sprites = new Sprite[6];
    private bool roll;
    public int currentValue;
    public GameObject indicator;
    public bool hasBeenRolled = false;
    public bool doubleClickMode = false;
    private int loopCounter = 0;
    public int nbLoopInit = 20;

	// Use this for initialization
	void Start () 
    {
        sr = GetComponent<SpriteRenderer>();
        if (doubleClickMode)
            nbFrameToChange = nbFrameToChangeMax;
        else
            nbFrameToChange = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    if(roll)
        {
            if (nbFrameSinceStart % nbFrameToChange == 0)
            {
                currentValue = Random.Range(1, 7);
                sr.sprite = sprites[currentValue - 1];
                loopCounter++;
                if (loopCounter == nbLoopInit && !doubleClickMode)
                {
                    loopCounter = 0;
                    nbLoopInit--;
                    nbFrameToChange++;
                    if (nbFrameToChange == nbFrameToChangeMax)
                    {
                        roll = false;
                        hasBeenRolled = true;
                        nbLoopInit += loopCounter;
                        indicator.SetActive(!roll);
                        nbFrameToChange = 1;
                    }    
                }
            }
            nbFrameSinceStart++;
        }
	}

    void OnMouseDown()
    {
        if(doubleClickMode)
            roll = !roll;
        if (!doubleClickMode && !roll)
            roll = true;
        if(!roll && doubleClickMode)
            hasBeenRolled = true;
        indicator.SetActive(!roll);
    }
}
