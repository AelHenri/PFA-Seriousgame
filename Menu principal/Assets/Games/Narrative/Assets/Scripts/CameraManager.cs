using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public float widthToBeSeen = 19.2f;
	// Use this for initialization
	void Start () {
        //GetComponent<Camera>().orthographicSize = 6.77f;
        Camera.main.orthographicSize = widthToBeSeen * Screen.height / Screen.width * 0.5f; ;

    }
}
