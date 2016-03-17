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
	void Update () {
		
		#if (UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER)	

			moveHorizontal = (Input.GetAxis ("Horizontal"))*speed;
			moveVertical = Input.GetAxis ("Vertical")*speed;

		#else 

		float xDir = 0.0f;
		float yDir= 0.0f;
		float speedM = 50.0f;
		float speed1 = 0.75F;


		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

			// Move object across XY plane

			if ( touchDeltaPosition.x>0 ){
				xDir = 	Mathf.Min(touchDeltaPosition.x ,speedM);
			}
			if ( touchDeltaPosition.x<=0 ){
				xDir = Mathf.Max(touchDeltaPosition.x ,-speedM);
			}
			if ( touchDeltaPosition.y>0 ){
				yDir = Mathf.Min(touchDeltaPosition.y  ,speedM);
			}
			if ( touchDeltaPosition.y<=0 ){
				yDir =Mathf.Max(touchDeltaPosition.y  ,-speedM);
			}
			
		rb.velocity = new Vector3 ( xDir * speed1,  yDir * speed1 , 0);  
		}
		else
			rb.velocity = new Vector3 ( 0,  0, 0);  
	
		#endif

	}

	/*
	void FixedUpdate(){
		if (moveHorizontal >= 0 && moveVertical>=0)
			rb.velocity = new Vector3 ( Mathf.Min( moveHorizontal,maxSpeed),Mathf.Min( moveVertical,maxSpeed), 0);  
		if (moveHorizontal >= 0 && moveVertical<=0)
			rb.velocity = new Vector3 ( Mathf.Min( moveHorizontal,maxSpeed),Mathf.Max( moveVertical,-maxSpeed), 0);  
		if (moveHorizontal <= 0 && moveVertical>=0)
			rb.velocity = new Vector3 ( Mathf.Max( moveHorizontal,-maxSpeed),Mathf.Min( moveVertical,maxSpeed), 0);  
		if (moveHorizontal <= 0 && moveVertical<=0)
			rb.velocity = new Vector3 ( Mathf.Max( moveHorizontal,-maxSpeed),Mathf.Max( moveVertical,-maxSpeed), 0);  

	}
*/
}
