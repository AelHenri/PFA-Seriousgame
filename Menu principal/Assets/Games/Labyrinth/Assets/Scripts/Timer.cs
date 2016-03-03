using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Timer : MonoBehaviour {
	
	float startTime;
	bool launched = false;
	public Text timerText;
	
	void Start () {
	}

	public void launch(){
		launched = true;
		startTime = Time.time;
	}
	
	void Update () {
		if (launched) {
			int time = (int) (Time.time - startTime);
			int minutes = time / 60;
			int seconds = time % 60;

			timerText.text = "Temps " + System.String.Format("{0:00}:{1:00}" , minutes, seconds);
			//Debug.Log("Temps " + System.String.Format("{0:00}:{1:00}" , minutes, seconds));
		}
	}

	public float get(){
		return Time.time - startTime;
	}
}
