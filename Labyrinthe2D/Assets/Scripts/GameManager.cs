using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public float levelStartDelay = 2f;	
	public MazeGen maze;
	public int nbKeys = 0;
	//public Camera camera;
	public List<Key> keys;

	//public CamCentering cam;

	private Text levelText;		
	public int level =1;
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
		maze = GetComponent<MazeGen> ();
	
		InitGame ();


	}

	private void OnLevelWasLoaded(int index){
		level++; 
		InitGame ();

	}

	void InitGame(){
		doingSetup = true;
		levelImage =GameObject.Find("LevelImage");
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
	
		levelText.text = "Level " + level;
		levelImage.SetActive (true);
		keys.Clear ();
		maze.SetupScene (level);
		Invoke ("HideLevelImage", levelStartDelay);
		//Debug.Log (keys.Count);

	}


	private void HideLevelImage(){

		levelImage.SetActive (false);
		doingSetup = false;
	}

	public void AddKeyToList(Key script){
		keys.Add (script);
	
	}

	IEnumerator MoveKeys(){
		int i;
		for (i= 0; i< level; i++) {

			keys[i].MoveKey(i);
		}
		yield return null;
	}

	// Update is called once per frame
	void Update () {
		if (doingSetup) {
			return;
		}
		StartCoroutine (MoveKeys());
	}
}
