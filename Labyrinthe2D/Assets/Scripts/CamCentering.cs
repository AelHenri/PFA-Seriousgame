using UnityEngine;
using System.Collections;

public class CamCentering : MonoBehaviour {

	public GameObject man;
	Vector3 targetPos;

	
	// Update is called once per frame
	void FixedUpdate () {
			MazeGen maze = man.GetComponent<MazeGen>();
			Debug.Log(maze.width);
			int w = maze.width;
			int h = maze.height;
			transform.position = new Vector3 ((w / 2f) - 0.5f, h / 2f, -10f);
			float mazeRatio = w / h;
			float screenRation = Screen.width / Screen.height;
			if (mazeRatio > screenRation)
				GetComponent<Camera>().orthographicSize = w / 3f + 0.5f;
			else
				GetComponent<Camera>().orthographicSize = h / 2f + 1.5f;

	}
}
