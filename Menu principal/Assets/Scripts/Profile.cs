using UnityEngine;
using System.Collections;

public class Profile : MonoBehaviour {
    public bool hasFinishedGame1;
    public bool hasFinishedGame2;
    public bool hasFinishedGame3;

    public GameObject firstStar;
    public GameObject firstGoldenStar;
    public GameObject secondStar;
    public GameObject secondGoldenStar;
    public GameObject thirdStar;
    public GameObject thirdGoldenStar;

	// Use this for initialization
	void Start () {
        if (hasFinishedGame1)
        {
            firstStar.SetActive(false);
            firstGoldenStar.SetActive(true);
        }
        if (hasFinishedGame2)
        {
            secondStar.SetActive(false);
            secondGoldenStar.SetActive(true);
        }
        if (hasFinishedGame3)
        {
            thirdStar.SetActive(false);
            thirdGoldenStar.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
