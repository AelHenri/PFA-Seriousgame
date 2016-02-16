using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	public int speed = 10;
	public Rigidbody2D rb;
	public int globalKeys;
	public int localKeys;
	public Text KeyText;
	public Text EndingText;


	float moveHorizontal;
	float moveVertical;
	float restartLevelDelay = 1f;


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
			globalKeys = globalKeys + 1;
			localKeys = localKeys + 1;
			other.gameObject.SetActive(false);
			KeyText.text = "Clés : " + localKeys;
		}
	}
	// Update is called once per frame
	void Update () {
		moveHorizontal = (Input.GetAxis ("Horizontal"))*speed;
		moveVertical = Input.GetAxis ("Vertical")*speed;
		//transform.Translate (moveHorizontal, moveVertical, 0);
		//Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0);


		}
	void FixedUpdate(){
		if (moveHorizontal >= 0 && moveVertical>=0)
			rb.velocity = new Vector3 ( Mathf.Min( moveHorizontal,4),Mathf.Min( moveVertical,4), 0);  
		if (moveHorizontal >= 0 && moveVertical<=0)
			rb.velocity = new Vector3 ( Mathf.Min( moveHorizontal,4),Mathf.Max( moveVertical,-4), 0);  
		if (moveHorizontal <= 0 && moveVertical>=0)
			rb.velocity = new Vector3 ( Mathf.Max( moveHorizontal,-4),Mathf.Min( moveVertical,4), 0);  
		if (moveHorizontal <= 0 && moveVertical<=0)
			rb.velocity = new Vector3 ( Mathf.Max( moveHorizontal,-4),Mathf.Max( moveVertical,-4), 0);  

	}

	
}
