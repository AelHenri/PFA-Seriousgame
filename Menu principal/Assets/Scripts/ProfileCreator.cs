using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileCreator : MonoBehaviour {

    public InputField firstNameInput;
    public InputField lastNameInput;
    public GameObject confirmationPanel;

    ProfileManager profileManager;

	// Use this for initialization
	void Start () {
        profileManager = GameObject.Find("Navigator").GetComponent<ProfileManager>();
	}
	

    public void createNewProfile()
    {
        profileManager.refreshProfiles();
        profileManager.saveNewProfile(firstNameInput.text, lastNameInput.text);
        confirmationPanel.SetActive(true);
    }

    public void dismiss()
    {
        confirmationPanel.SetActive(false);
    }

    public void backButton()
    {
        profileManager.refreshProfiles();
        SceneManager.UnloadScene("ProfileCreator");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
