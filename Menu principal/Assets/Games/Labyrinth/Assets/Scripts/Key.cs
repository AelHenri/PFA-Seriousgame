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
			int rnd;
			int nbKeysThere;
			nbKeysThere = 0;	
			int it = 0;
			while (nbKeysThere != 1 & it<10){
				nbKeysThere = 0;
				it = it+1;
				rnd = Random.Range (0, MazeAccess.maze.deadEnd.Count);
				transform.position = new Vector3 (MazeAccess.maze.deadEnd [rnd].x, MazeAccess.maze.deadEnd [rnd].y, 0);
				foreach(Key k in GameManager.instance.keys){
					if ( Vector3.Distance(k.GetComponent<Key>().transform.position, this.transform.position)< 0.1f){
						nbKeysThere = nbKeysThere + 1;
					}
				Debug.Log(nbKeysThere);
				}
			}

		}
	}

	void OnCollisionEnter2D( Collision2D col){
		int rnd = Random.Range (0, MazeAccess.maze.deadEnd.Count);
		transform.position = new Vector3 (MazeAccess.maze.deadEnd[rnd].x,MazeAccess.maze.deadEnd[rnd].y,0);
	}
		
}
