using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileMenu : MonoBehaviour {
    public bool hasFinishedGame1;
    public bool hasFinishedGame2;
    public bool hasFinishedGame3;

    public GameObject firstStar;
    public GameObject firstGoldenStar;
    public GameObject secondStar;
    public GameObject secondGoldenStar;
    public GameObject thirdStar;
    public GameObject thirdGoldenStar;
    public Dropdown profileSelector;
    public Text text;

    Profile[] profiles;

    ProfileManager profileManager;

    // Use this for initialization
    void Start () {
        profileSelector.ClearOptions();

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

        profileManager = GameObject.Find("Navigator").GetComponent<ProfileManager>();

        profiles = profileManager.getProfiles();
        if (profiles == null)
            return;
        else
        {
            foreach (Profile p in profiles)
            {
                profileSelector.options.Add(new Dropdown.OptionData() { text = p.getFirstName() });
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        text.text = profiles[profileSelector.value].getFirstName();
    }
}
