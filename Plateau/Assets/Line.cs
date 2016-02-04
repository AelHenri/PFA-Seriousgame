using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

    public GameObject begin;
    public GameObject end;

    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        lr.SetPosition(0, begin.transform.position);
        lr.SetPosition(1, end.transform.position);
    }
}
