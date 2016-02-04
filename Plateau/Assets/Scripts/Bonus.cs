using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public Sprite actif;
    public Sprite inactif;

    private SpriteRenderer sr;
    private bool active = false;

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
        active = !active;
        sr.sprite = (active) ? actif : inactif;
    }
}
