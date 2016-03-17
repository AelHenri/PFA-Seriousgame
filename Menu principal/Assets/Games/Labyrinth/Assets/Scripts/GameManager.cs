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
	public GameObject bonusImage;
	public Text bonusText;

	private Text levelText;		
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
		doingSetup = true;
		bonusImage = GameObject.Find ("BonusImage");
		bonusText = GameObject.Find ("BonusText").GetComponent<Text>();
		bonusText.text = "Bravo! Tu as gagné un cornichon!";
		bonusImage.SetActive(false);

		levelImage =GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		levelText.text = "Niveau " + level;
		levelImage.SetActive (true);
		keys.Clear ();
		maze.SetupScene (level);
		GameObject.Find("Timer").GetComponent<Timer>().launch();
		Invoke ("HideLevelImage", levelStartDelay);
		GameObject.Find("Main Camera").GetComponent<CamCentering>().centerCamera();
        
        audioSource.clip = music[(level-1)%3];
        audioSource.Play();

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
		StartCoroutine (MoveBonus ());
	}
}
