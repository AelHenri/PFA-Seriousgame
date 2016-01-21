using UnityEngine;
using System.Collections;

public class CamCentering : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int w = GameObject.Find("Maze").GetComponent<MazeGen>().width;
		int h = GameObject.Find("Maze").GetComponent<MazeGen>().height;
		transform.position = new Vector3 ((w / 2f) - 0.5f, h / 2f, -10f);
		float mazeRatio = w / h;
		float screenRation = Screen.width / Screen.height;
		if (mazeRatio > screenRation)
			GetComponent<Camera>().orthographicSize = w / 3f + 0.5f;
		else
			GetComponent<Camera>().orthographicSize = h / 2f + 1.5f;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
