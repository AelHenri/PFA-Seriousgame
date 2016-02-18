using UnityEngine;
using System.Collections;

public static class MazeAccess{
	public static MazeGen maze;
	public static int[] mazeSet;
}


public class Key : MonoBehaviour {


	public void Start () {
		MazeAccess.mazeSet = new int[10];
		for (int i = 0; i<10;i++){
			MazeAccess.mazeSet[i] = 0;
		}
		GameManager.instance.AddKeyToList (this);
		transform.position = new Vector3 (GameManager.instance.level, 5.0f, 0);
	}
	
	// Update is called once per frame
	public void MoveKey (int i) {
		if (MazeAccess.mazeSet[i] == 0) {
			MazeAccess.maze = (MazeGen)FindObjectOfType (typeof(MazeGen));
			MazeAccess.mazeSet[i] = 1;
			int rnd = Random.Range (0, MazeAccess.maze.deadEnd.Count);
			transform.position = new Vector3 (MazeAccess.maze.deadEnd [rnd].x, MazeAccess.maze.deadEnd [rnd].y, 0);
		}
	}

	void OnCollisionEnter2D( Collision2D col){
		int rnd = Random.Range (0, MazeAccess.maze.deadEnd.Count);
		transform.position = new Vector3 (MazeAccess.maze.deadEnd[rnd].x,MazeAccess.maze.deadEnd[rnd].y,0);
	}
		
}
