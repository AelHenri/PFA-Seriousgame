using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	Vector3 minSize;
	void Start(){
		minSize = transform.localScale;
	}
	float shrinkSpeed = 0.07f;
	// Update is called once per frame
	void Update () {
		if (Player.localKeys >= GameManager.instance.level) {
			float t = Mathf.PingPong (Time.time * shrinkSpeed, 0.03f);
			transform.localScale = minSize + new Vector3 (t, t, t); //Vector3.Lerp (transform.localScale, new Vector3 (targetScale, targetScale, targetScale), Time.deltaTime * shrinkSpeed); // * transform.localScale + transform.localScale;
		}
	}
}
