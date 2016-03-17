using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	public int rightAnswer = 0;
	public int speed = 15;
	public int maxSpeed = 5;
	public Rigidbody2D rb;
	public int globalKeys;
	public static int localKeys;
	public Text KeyText;
	public Text EndingText;

	float moveHorizontal;
	float moveVertical;
	float restartLevelDelay = 1f;

	private Vector2 touchOrigin = -Vector2.one;


	// Use this for initialization
	void Start () {
		Debug.Log("new key");
		EndingText = GameObject.Find("EndingText").GetComponent<Text>();
		EndingText.gameObject.SetActive (false);
		//EndingText.gameObject.SetActive (true);
		transform.position = new Vector3 (-1.0f, GameManager.instance.maze.height / 2, 0);
		rb = GetComponent<Rigidbody2D> ();
		globalKeys = GameManager.instance.nbKeys;
		localKeys = 0;
		//KeyText.text = "Clés : " + globalKeys;
		KeyText.text = "Clés : " + localKeys;

	}

	private void OnDisable(){
		GameManager.instance.nbKeys = globalKeys;
	
	}

	private void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}

	private void Hide(){
		EndingText.gameObject.SetActive (false);
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "exit") {
			if (localKeys < GameManager.instance.level) {
				if (GameManager.instance.level - localKeys == 1){
				EndingText.text = "Il manque " + (GameManager.instance.level - localKeys) + " clé";
				}
				else {
					EndingText.text = "Il manque " + (GameManager.instance.level - localKeys) + " clés";
				}
				EndingText.gameObject.SetActive (true);
				Invoke ("Hide", 3);
			} else {
				enabled = false;
				Invoke ("Restart", restartLevelDelay);
			}
		}
	
		else if (other.tag == "key") {
			if (rightAnswer == 1){
			globalKeys = globalKeys + 1;
			localKeys = localKeys + 1;
			other.gameObject.SetActive(false);
			KeyText.text = "Clés : " + localKeys;
			}
			else{
				globalKeys = globalKeys + 1;
				localKeys = localKeys + 1;
				other.gameObject.GetComponent<Key>().MoveKey();
				KeyText.text = "Clés : " + localKeys;
			}
		}
	}


	// Update is called once per frame
	void FixedUpdate () {		
		float xDir = 0.0f;
		float yDir = 0.0f;
		float speedM = 1000.0f;
		float speed1 = 0.8F;

		if (Input.touchCount > 0) {
			// The screen has been touched so store the touch
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				
				Vector3 touchPosition = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x, touch.position.y, 10));                
			
				// if the finger is far from player move toward him with velocity so that he doesn't go through walls
				if (Vector3.Distance (touchPosition, transform.position) > 0.2f) {
					xDir = ((touchPosition.x - transform.position.x) / (Mathf.Abs (touchPosition.x - transform.position.x))) * 8.0f;
					if ((Mathf.Abs (touchPosition.x - transform.position.x)) < 0.1f) {
						xDir = 0.0f;

					}
					yDir = ((touchPosition.y - transform.position.y) / (Mathf.Abs (touchPosition.y - transform.position.y))) * 8.0f;
					if ((Mathf.Abs (touchPosition.y - transform.position.y)) < 0.1f) {
						yDir = 0.0f;
					}
					rb.velocity = new Vector3 (xDir, yDir, 0);  			
				}
				//if it's very close change position 
				else{
					rb.velocity = new Vector3 ( 0,  0, 0);  
					transform.position = Vector3.MoveTowards (transform.position, touchPosition, Time.deltaTime * 20.0f);
				}
			}
		}
		// if the player doesn't touch the screen, don't move
		else
			rb.velocity = new Vector3 ( 0,  0, 0);  
	}
}
