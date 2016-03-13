using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().orthographicSize = 6.77f;

    }
}
