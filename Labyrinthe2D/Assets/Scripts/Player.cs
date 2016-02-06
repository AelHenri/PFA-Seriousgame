﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public int speed = 10;
	public Rigidbody2D rb;
	float moveHorizontal;
	float moveVertical;
	float restartLevelDelay = 1f;
	public int keys;
	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (-1.0f, 5.0f, 0);
		rb = GetComponent<Rigidbody2D> ();
		keys = GameManager.instance.keys;
	}

	private void OnDisable(){
		GameManager.instance.keys = keys;
	
	}

	private void Restart(){

		Application.LoadLevel (Application.loadedLevel);
	}

	private void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
		if (other.tag == "exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} 
		else if (other.tag == "key") {
			keys = keys + 1;
		}
	}
	// Update is called once per frame
	void Update () {
		moveHorizontal = (Input.GetAxis ("Horizontal"))*speed;
		//Debug.Log (moveHorizontal);
		moveVertical = Input.GetAxis ("Vertical")*speed;
		//transform.Translate (moveHorizontal, moveVertical, 0);
		//Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0);


		}
	void FixedUpdate(){
		if (moveHorizontal >= 0 && moveVertical>=0)
			rb.velocity = new Vector3 ( Mathf.Min( moveHorizontal,2),Mathf.Min( moveVertical,2), 0);  
		if (moveHorizontal >= 0 && moveVertical<=0)
			rb.velocity = new Vector3 ( Mathf.Min( moveHorizontal,2),Mathf.Max( moveVertical,-2), 0);  
		if (moveHorizontal <= 0 && moveVertical>=0)
			rb.velocity = new Vector3 ( Mathf.Max( moveHorizontal,-2),Mathf.Min( moveVertical,2), 0);  
		if (moveHorizontal <= 0 && moveVertical<=0)
			rb.velocity = new Vector3 ( Mathf.Max( moveHorizontal,-2),Mathf.Max( moveVertical,-2), 0);  

	}

	
}
