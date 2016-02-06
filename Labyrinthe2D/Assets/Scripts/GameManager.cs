using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public static GameManager instance = null;
	public float levelStartDelay = 2f;	
	public MazeGen maze;
	public int keys = 0;
	public Camera camera;
	//public CamCentering cam;

	private Text levelText;		
	private int level =1;
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
		maze.SetupScene (level);
		Invoke ("HideLevelImage", levelStartDelay);
	}
	private void HideLevelImage(){

		levelImage.SetActive (false);
		doingSetup = false;
	}

	// Update is called once per frame
	void Update () {
		if (doingSetup) {
			return;
		}
	}
}
