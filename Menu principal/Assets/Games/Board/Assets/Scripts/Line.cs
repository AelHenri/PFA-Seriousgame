using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

    public GameObject begin
    {
        private get;
        set;
    }
    public GameObject end
    {
        private get;
        set;
    }

    private LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if(begin != null)
            lr.SetPosition(0, begin.transform.position - new Vector3(0, 1, 0));
        if(end != null)
            lr.SetPosition(1, end.transform.position - new Vector3(0, 1, 0));
    }
}
