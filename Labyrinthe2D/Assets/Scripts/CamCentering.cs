using UnityEngine;
using System.Collections;

public class CamCentering : MonoBehaviour {

	public GameObject man;
	Vector3 targetPos;

	
	// Update is called once per frame
	public void centerCamera () {
			MazeGen maze = GameObject.Find("GameManager(Clone)").GetComponent<MazeGen>();

			int w = maze.width;
			int h = maze.height;
			transform.position = new Vector3 ((w / 2f) - 0.5f, h / 2f, -10f);
			float mazeRatio = w / h;
			float screenRatio = Screen.width / Screen.height;
			if (mazeRatio > screenRatio)
				GetComponent<Camera>().orthographicSize = w * 1.2f ;
			else
				GetComponent<Camera>().orthographicSize = h / 1.3f ;

	}
}
