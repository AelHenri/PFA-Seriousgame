using UnityEngine;
using System.Collections;

public class CamCentering : MonoBehaviour {
	public void centerCamera () {
			MazeGen maze = GameObject.Find("GameManager(Clone)").GetComponent<MazeGen>();
			int w = maze.width;
			int h = maze.height;
			transform.position = new Vector3 ((w / 2f) - 0.5f, h / 2f - 0.215f, -10f);
			if (h > w)
				GetComponent<Camera>().orthographicSize = w * 1.1f ; // 1.2
			else
				GetComponent<Camera>().orthographicSize = (h + 0.5f) / 1.6f ;// 1.7

	}
}
