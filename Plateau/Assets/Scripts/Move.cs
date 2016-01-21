using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {

    public int nbStep = 120;

    public List<Vector3> startPosition = new List<Vector3>();
    public List<Vector3> endPosition = new List<Vector3>();
    private int i = 0;

    private int currentStep = 0;
    private Animator an;

	// Use this for initialization
	void Start () {
        an = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (startPosition.Count != 0 && endPosition.Count != 0)
        {
            transform.position = Vector3.Lerp(startPosition[i], endPosition[i], (float)currentStep / (float)nbStep);
            currentStep++;
            an.SetInteger("State", 1);
        }
        if (currentStep == nbStep)
        {
            startPosition[i] = endPosition[i];
            if (i == endPosition.Count - 1)
            {
                startPosition = new List<Vector3>();
                endPosition = new List<Vector3>();
                an.SetInteger("State", 0);
                i = 0;
            }
            else
                i += 1;
            currentStep = 0;
        }
            
	}
}
