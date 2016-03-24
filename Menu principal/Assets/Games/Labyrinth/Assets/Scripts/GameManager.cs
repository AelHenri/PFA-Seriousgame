using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float levelStartDelay = 2f;	
	public MazeGen maze;
	public int nbKeys = 0;
	public List<Key> keys;
	public GameBonus bonus;
	private GameObject endGameImage;
	private GameObject scores;
	private Text scoresText;
	public GameObject gameText;
	private GameObject endGameText;
	public bool bonusPresent = false;
	private Text levelText;
	public float[] timers = new float[4];
	public int level =1;
    public AudioSource audioSource;
    public AudioClip[] music = new AudioClip[4];
	private GameObject levelImage;
	private bool doingSetup;
	// Use this for initialization

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
		keys = new List<Key> ();
		bonus = new GameBonus ();
		maze = GetComponent<MazeGen> ();
        audioSource = GetComponent<AudioSource>();
		InitGame ();


	}

	private void OnLevelWasLoaded(int index){
		if (!doingSetup) {
			level++;
			InitGame ();
		}

	}


	void InitGame(){
		Debug.Log ("level");
		Debug.Log (level);
		doingSetup = true;
		bonusPresent = false;

		gameText = GameObject.Find ("gameText");
		gameText.GetComponent<Text> ().text = "Bravo! Tu as gagné un cornichon!";
		gameText.SetActive (false);
		levelImage = GameObject.Find ("LevelImage");
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();

		GameObject timerText = GameObject.Find ("Timer");
		endGameImage = GameObject.Find ("endGameImage");
		scores = GameObject.Find ("scores");
		scoresText = GameObject.Find ("scoresText").GetComponent<Text> ();
		scores.SetActive (false);

		endGameText = GameObject.Find ("endGameText");
		endGameText.GetComponent<Text> ().text = "Bravo! Tu as gagné!";
		endGameText.SetActive (false);

		endGameImage.SetActive (false);
		if (level == 2) {
			timerText.SetActive (false);
			endGameText.SetActive (true);
			endGameImage.SetActive (true);

			int j;
			int taille = 1;
			for (j = 0; j < 4; j++) {
				if (Mathf.Round (timers [j]) > 10)
					taille = Mathf.Max (taille, 2);
				if (Mathf.Round (timers [j]) > 100)
					taille = Mathf.Max (taille, 3);
			}
			scoresText.text = "Résultats\n\n";
			for (j = 0; j < 4; j++) {
				if (taille == 1) {
					scoresText.text += "Niveau " + (j + 1) + " :   " + Mathf.Round (timers [j]) + " secondes\n";
				} else if (taille == 2) {
					if ( Mathf.Round (timers [j])<10)
						scoresText.text += "Niveau " + (j + 1) + " :   0" + Mathf.Round (timers [j]) + " secondes\n";	
					else
						scoresText.text += "Niveau " + (j + 1) + " :   " + Mathf.Round (timers [j]) + " secondes\n";
				} else {
					if ( Mathf.Round (timers [j])<10)
						scoresText.text += "Niveau " + (j + 1) + " :  00" + Mathf.Round (timers [j]) + " secondes\n";	
					else if ( Mathf.Round (timers [j])<100)
						scoresText.text += "Niveau " + (j + 1) + " :  0" + Mathf.Round (timers [j]) + " secondes\n";
					else
						scoresText.text += "Niveau " + (j + 1) + " :  " + Mathf.Round (timers [j]) + " secondes\n";	
				}
			}
				scores.SetActive (true);
		//	Invoke ("HideLevelImage", 5f);
			enabled = false;
			//GameOver ();
			enabled = false;
			Application.Quit ();
		} else {
			levelText.text = "Niveau " + level;
			levelImage.SetActive (true);
			keys.Clear ();
			maze.SetupScene (level);
			GameObject.Find ("Timer").GetComponent<Timer> ().launch ();
			Invoke ("HideLevelImage", levelStartDelay);
			GameObject.Find ("Main Camera").GetComponent<CamCentering> ().centerCamera ();
			audioSource.clip = music [(level - 1) % 3];
			audioSource.Play ();
		}
	}
		

	private void HideLevelImage(){
		levelImage.SetActive (false);
		doingSetup = false;
	}



	public void AddKeyToList(Key script){
		keys.Add (script);
	}

	public void SetBonus(GameBonus script){
		bonus = script;
	}


	IEnumerator MoveKeys(){
		int i;
		for (i= 0; i< level; i++) {

			keys[i].MoveKey(i);
		}
		yield return null;
	}


	IEnumerator MoveBonus(){
		bonus.MoveBonus(1);
		yield return null;
	}
	// Update is called once per frame
	void Update () {
		if (doingSetup) {
			return;
		}
		StartCoroutine (MoveKeys());
		if (bonusPresent == true) {
			StartCoroutine (MoveBonus ());
		}
	}
}
