using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (-1.0f, 5.0f, 0);
		}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal")*0.02f;
		float moveVertical = Input.GetAxis ("Vertical")*0.02f;
	
		transform.Translate (moveHorizontal, moveVertical, 0);
	}
}
