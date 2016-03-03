using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public Sprite actif;
    public Sprite inactif;
    public bool active = false;
    public bool wasUsed = false;

    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = inactif;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (active)
        {
            Switch();
            wasUsed = true;
        }       
    }

    public void Switch()
    {
        active = !active;
        sr.sprite = (active) ? actif : inactif;
    }
}
