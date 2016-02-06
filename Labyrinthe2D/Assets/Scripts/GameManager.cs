using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour {
	public static GameManager instance = null; 
	public MazeGen maze;
	public int keys = 0;
	public Camera camera;
	//public CamCentering cam;
	private int level =1;
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

	void InitGame(){
	//	cam.SetupScene (level);
		maze.SetupScene (level);

	}

	// Update is called once per frame
	void Update () {
	
	}
}
