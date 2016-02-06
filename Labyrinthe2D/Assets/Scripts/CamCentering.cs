using UnityEngine;
using System.Collections;

public class CamCentering : MonoBehaviour {

	public float interpVelocity;
	public float minDistance;
	public float followDistance;
	public GameObject man;
	//public GameManager man;
	public GameObject target;
	public Vector3 offset;
	Vector3 targetPos;
	// Use this for initialization
	void Start () {
		targetPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target)
		{
			//maze = GetComponent<MazeGen>();
			Debug.Log(man);
			//if (man){
			//	Debug.Log("coucou");
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
			//}
			//if (target.GetComponent<MazeGen>() != null)
			//Debug.Log(target.GetComponent<Maze>());//.width);
			/*GetComponent<Camera>().orthographicSize = Mathf.Max(maze.width,maze.height);
			Vector3 posNoZ = transform.position;
			posNoZ.z = target.transform.position.z;
			
			Vector3 targetDirection = (target.transform.position - posNoZ);
			

			
			targetPos = transform.position ;
			transform.position = Vector3.Lerp( transform.position, targetPos + offset, 0.25f);*/
			
		}

}


			                                  }
