using UnityEngine;
using System.Collections;

public class Dice : MonoBehaviour {

    public int nbFrameToChange = 10;
    private SpriteRenderer sr;
    private int nbFrameSinceStart = 0;
    public Sprite[] sprites = new Sprite[6];
    private bool roll;
    public int currentValue;
    public GameObject indicator;
    public bool hasBeenRolled = false;

	// Use this for initialization
	void Start () 
    {
        sr = GetComponent<SpriteRenderer>();
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
            }
            nbFrameSinceStart++;
        }
	}

    void OnMouseDown()
    {
        roll = !roll;
        if(!roll)
            hasBeenRolled = true;
        indicator.SetActive(!roll);
    }
}
