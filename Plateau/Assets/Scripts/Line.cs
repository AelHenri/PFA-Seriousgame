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
        if(begin != null)
            lr.SetPosition(0, begin.transform.position);
        if(end != null)
            lr.SetPosition(1, end.transform.position);
    }
}
