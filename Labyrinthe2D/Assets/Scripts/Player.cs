using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (3.0f, 5.0f, 0);
		}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("Horizontal")*0.01f;
		float moveVertical = Input.GetAxis ("Vertical")*0.01f;
		if (moveHorizontal < 0) {
			//float speed = 1.0f;
			transform.Translate (Mathf.Max ( moveHorizontal, -transform.position.x), 0, 0);
		}
		if (moveHorizontal > 0) {
			transform.Translate (Mathf.Min ( moveHorizontal, (19 -transform.position.x)), 0, 0);
		}
		if ( moveVertical > 0) {
			//float speed = 1.0f;
			transform.Translate ( 0, Mathf.Min (moveVertical, 9 - transform.position.y), 0);
		}
		if ( moveVertical < 0) {
			//float speed = 1.0f;
			transform.Translate ( 0, Mathf.Max (moveVertical, - transform.position.y), 0);
		}


		/*GameObject[] objs = GameObject.FindObjectsOfType (typeof(GameObject));
		GameObject cell = null;
		foreach (GameObject go in objs) {
			if ( go.transform.position == transform.position ){
				cell = go;
				break;
			}
*/
		transform.Translate (Mathf.Max (moveHorizontal, -transform.position.x), moveVertical, 0);
	}
}
