using UnityEngine;
using System.Collections;

public static class MazeAccess{
	public static MazeGen maze;
	public static int mazeSet = 0;
}

public class keyscript : MonoBehaviour {


	void Start () {
		transform.position = new Vector3 (2.0f, 5.0f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (MazeAccess.mazeSet == 0) {
			MazeAccess.maze = (MazeGen)FindObjectOfType (typeof(MazeGen));
			MazeAccess.mazeSet = 1;
			int rnd = Random.Range (0, MazeAccess.maze.deadEnd.Count);
			transform.position = new Vector3 (MazeAccess.maze.deadEnd[rnd].x,MazeAccess.maze.deadEnd[rnd].y,0);
		}

		}

	void OnCollisionEnter2D( Collision2D col){
		int rnd = Random.Range (0, MazeAccess.maze.deadEnd.Count);
		transform.position = new Vector3 (MazeAccess.maze.deadEnd[rnd].x,MazeAccess.maze.deadEnd[rnd].y,0);
	}
		
}
