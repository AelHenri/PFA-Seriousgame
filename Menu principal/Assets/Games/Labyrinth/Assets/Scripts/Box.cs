// controls the shrinking and growing of the exit

using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	private Vector3 minSize;
	private float shrinkSpeed = 0.07f;


	void Start(){
		minSize = transform.localScale;
	}

	void Update () {
		if (Player.localKeys  == GameManager.instance.level) {
			float t = Mathf.PingPong (Time.time * shrinkSpeed, 0.03f);
			transform.localScale = minSize + new Vector3 (t, t, t); 
		}
	}
}
