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

    Questionnaire questionnaire;

	// Use this for initialization
	void Start () {
		Debug.Log("new key");
		EndingText = GameObject.Find("EndingText").GetComponent<Text>();
		EndingText.gameObject.SetActive (false);
		transform.position = new Vector3 (-1.0f, GameManager.instance.maze.height / 2, 0);
		rb = GetComponent<Rigidbody2D> ();
		globalKeys = GameManager.instance.nbKeys;
		localKeys = 0;
		//KeyText.text = "Clés : " + globalKeys;
		KeyText.text = "Clés : " + localKeys;

        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
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
	// Update is called once per frame
	void Update () {
		moveHorizontal = (Input.GetAxis ("Horizontal"))*speed;
		moveVertical = Input.GetAxis ("Vertical")*speed;
		if (isAnswering) {
				if (questionnaire.hasAnswered) {
					if (questionnaire.isAnswerRight) {
						globalKeys = globalKeys + 1;
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
						//KeyText.text = "Clés : " + localKeys;
					}	
					isAnswering = false;
				}
			}

		if (bonus) {
			if (questionnaire.hasAnswered) {
				if (questionnaire.isAnswerRight) {
					Debug.Log ("bonus gagne");
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
