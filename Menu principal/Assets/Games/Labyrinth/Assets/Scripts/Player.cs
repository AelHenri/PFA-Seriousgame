using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Player : MonoBehaviour {
	public int speed = 10;
	public int maxSpeed = 3;
	public Rigidbody2D rb;
	public static int localKeys;
	//public int globalKeys; if we want to count the total number of keys

	private	bool bonus = false;
	private bool isAnswering = false;
	private GameObject collidedKey;
	private GameObject collidedBonus;
	private float moveHorizontal;
	private float moveVertical;
	private float restartLevelDelay = 1f;
	private Questionnaire questionnaire;

	public Text KeyText;// show after a question
	public Text EndingText; // shown at exit point

	void Start () {
		Debug.Log("new key");
		EndingText = GameObject.Find("EndingText").GetComponent<Text>();
		EndingText.gameObject.SetActive (false);
		transform.position = new Vector3 (-1.0f, GameManager.instance.maze.height / 2, 0);
		rb = GetComponent<Rigidbody2D> ();
		//globalKeys = GameManager.instance.nbKeys;
		localKeys = 0;
		KeyText.text = "Clés : " + localKeys;

        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
	}

	// if we want to count the total number of keys 
	/*private void OnDisable(){
		GameManager.instance.nbKeys = globalKeys;
	}*/

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
				Invoke ("Hide", 4.5f);
			} else {
				
				GameManager.instance.timers [GameManager.instance.level - 1] = GameObject.Find ("Timer").GetComponent<Timer> ().get ();
				enabled = false;
				Invoke ("Restart", restartLevelDelay);
			}

		} else if (other.tag == "key") {
			questionnaire.startQuestionnaire ();	
			isAnswering = true;
			collidedKey = other.gameObject;

		} else if (other.tag == "gamebonus") {
			Debug.Log ("collided a bonus");
			collidedBonus = other.gameObject;
			questionnaire.startQuestionnaire ();	
			bonus = true;
		}
	}

	void Update () {
		moveHorizontal = (Input.GetAxis ("Horizontal"))*speed;
		moveVertical = Input.GetAxis ("Vertical")*speed;
		if (isAnswering) {
				if (questionnaire.hasAnswered) {
					if (questionnaire.isAnswerRight) {
						// globalKeys = globalKeys + 1;
						localKeys = localKeys + 1;
						collidedKey.SetActive(false);
						KeyText.text = "Clés : " + localKeys;
					GameManager.instance.gameText.GetComponent<Text>().text = "Bravo! \n Tu as récupéré une clé!";	
					GameManager.instance.gameText.SetActive(true);
					Invoke ("HideGameText", 3.0f);
				}
					else{
					GameManager.instance.gameText.GetComponent<Text>().text ="Dommage... La clé s'est déplacée.";	
					GameManager.instance.gameText.SetActive(true);
						Invoke ("HideGameText", 3.0f);
						collidedKey.GetComponent<Key> ().MoveKey ();
					}	
					isAnswering = false;
				}
			}

		// if the player is answering to a bonus question
		if (bonus) {
			if (questionnaire.hasAnswered) {
				if (questionnaire.isAnswerRight) {
					GameManager.instance.bonusPresent = false;
					GameManager.instance.gameText.GetComponent<Text>().text = "Bravo! \n Tu as gagné un cornichon!";	
					GameManager.instance.gameText.SetActive(true);
					Invoke ("HideGameText", 3.0f);
				}
				else{
					GameManager.instance.gameText.GetComponent<Text>().text = "Dommage... Pas de cornichon.";	
					GameManager.instance.gameText.SetActive(true);
					Invoke ("HideGameText", 3.0f);
					}	
				collidedBonus.SetActive (false);
				bonus = false;
			}
		}
	}

	public void HideGameText(){
		GameManager.instance.gameText.SetActive (false);
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
