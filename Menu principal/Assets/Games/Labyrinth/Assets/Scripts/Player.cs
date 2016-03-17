using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	public int rightAnswer = 0;
	public int speed = 10;
	public int maxSpeed = 3;
	public Rigidbody2D rb;
	public int globalKeys;
	public static int localKeys;
	public Text KeyText;
	public Text EndingText;

	bool bonus = false;
	bool isAnswering = false;
	GameObject collidedKey;
	GameObject collidedBonus;
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
				if (GameManager.instance.level - localKeys == 1) {
					EndingText.text = "Il manque " + (GameManager.instance.level - localKeys) + " clé";
				} else {
					EndingText.text = "Il manque " + (GameManager.instance.level - localKeys) + " clés";
				}
				EndingText.gameObject.SetActive (true);
				Invoke ("Hide", 3);
			} else {
				enabled = false;
				Invoke ("Restart", restartLevelDelay);
			}
		} else if (other.tag == "key") {
			GlobalQuestionnaire.startQuestionnaire ();	
			isAnswering = true;
			collidedKey = other.gameObject;
		} else if (other.tag == "gamebonus") {
			Debug.Log ("collided a bonus");
			collidedBonus = other.gameObject;
			GlobalQuestionnaire.startQuestionnaire ();	
			bonus = true;
		}
	}
	// Update is called once per frame
	void Update () {
		moveHorizontal = (Input.GetAxis ("Horizontal"))*speed;
		moveVertical = Input.GetAxis ("Vertical")*speed;
		if (isAnswering) {
				if (GlobalQuestionnaire.hasAnswered) {
					if (GlobalQuestionnaire.isAnswerRight) {
						globalKeys = globalKeys + 1;
						localKeys = localKeys + 1;
						collidedKey.SetActive(false);
						KeyText.text = "Clés : " + localKeys;
					}
				else{
						collidedKey.GetComponent<Key>().MoveKey();
						KeyText.text = "Clés : " + localKeys;
					}	
					isAnswering = false;
				}
			}

		if (bonus) {
			if (GlobalQuestionnaire.hasAnswered) {
				if (GlobalQuestionnaire.isAnswerRight) {
					Debug.Log ("bonus gagne");
					GameManager.instance.bonusPresent = false;
					GameManager.instance.bonusImage.SetActive(true);
					Invoke ("HideBonusImage", 3.0f);
				}
				else{
					Debug.Log ("desolé");
				}	
				collidedBonus.SetActive (false);
				bonus = false;
			}
		}
	}

	public void HideBonusImage(){
		GameManager.instance.bonusImage.SetActive (false);
	}

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
}
