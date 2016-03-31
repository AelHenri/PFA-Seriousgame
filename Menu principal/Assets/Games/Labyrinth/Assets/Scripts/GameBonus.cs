using UnityEngine;
using System.Collections;

public static class MazeAccessB{
	public static MazeGen maze;
	public static int mazeSet;
}
	
public class GameBonus : MonoBehaviour {
	public void Start () {
		MazeAccessB.mazeSet = 0;			
		GameManager.instance.SetBonus (this);
		transform.position = new Vector3 (1, 5.0f, 0);
	}
		
	public void MoveBonus (int i) {
		gameObject.GetComponent<Renderer> ().enabled = true;
		MazeAccessB.maze = (MazeGen)FindObjectOfType (typeof(MazeGen));
		// this is just a verification , we choose whether there is a bonus or not
		if (MazeAccessB.maze.deadEnd.Count - GameManager.instance.level <= 1) {
			MazeAccessB.mazeSet = 1;
			gameObject.GetComponent<Renderer> ().enabled = false;
		}
		if (MazeAccessB.mazeSet == 0) {
			MazeAccessB.mazeSet = 1;
			int rnd;
			int nbKeysThere;
			nbKeysThere = -1;	
			int it = 0;
			while (nbKeysThere != 0 & it<10){
				nbKeysThere = 0;
				it = it+1;
				rnd = Random.Range (0, MazeAccessB.maze.deadEnd.Count);
				transform.position = new Vector3 (MazeAccessB.maze.deadEnd [rnd].x, MazeAccessB.maze.deadEnd [rnd].y, 0);
				foreach(Key k in GameManager.instance.keys){
					if ( Vector3.Distance(k.GetComponent<Key>().transform.position, this.transform.position)< 0.1f){
						nbKeysThere = nbKeysThere + 1;
					}
				}
			}

			if (it == 10){
				it = 0;
				while (nbKeysThere != 1 & it<10){
					nbKeysThere = 0;
					it = it+1;
					int rnd1, rnd2;
						rnd1 = Random.Range (0, MazeAccessB.maze.width);
					rnd2 = Random.Range (0, MazeAccessB.maze.height);
					transform.position = new Vector3 (rnd1,rnd2, 0);
					foreach(Key k in GameManager.instance.keys){
						if ( Vector3.Distance(k.GetComponent<Key>().transform.position, this.transform.position)< 0.1f){
							nbKeysThere = nbKeysThere + 1;
						}
						Debug.Log(nbKeysThere);
					}
				}
			}
		}
	}
}
