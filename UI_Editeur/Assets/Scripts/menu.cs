using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

    public GameObject panelIncompleteSheetError;
    public GameObject panelSucceedDialogue;
    public GameObject panelOpenSheetError;

    private bool isDialogueBoxShown = false;
	// Use this for initialization
	void Start () {
	
	}
	
    public void onClickQuitter() {
        Application.Quit();
    }


    public void showIncompleteSheetError()
    {
        panelIncompleteSheetError.SetActive(true);
    }

    public void showSucceedDialogue()
    {
        panelSucceedDialogue.SetActive(true);
    }

    public void showOpenSheetError()
    {
        panelOpenSheetError.SetActive(true);
    }

    public void ackSucceedDialogue()
    {
        panelSucceedDialogue.SetActive(false);
    }


    public void ackOpenSheetError()
    {
        panelOpenSheetError.SetActive(false);
    }

    public void ackIncomplteSheet()
    {
        panelIncompleteSheetError.SetActive(false);
    }

    public bool isThereADialogueBox()
    {
        return isDialogueBoxShown;
    }



	// Update is called once per frame
	void Update () {
        isDialogueBoxShown = panelSucceedDialogue.activeSelf || panelIncompleteSheetError.activeSelf;	
	}


}
