using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

    public GameObject delExempleImgButton;
    public GameObject panelIncompleteSheetError;

	// Use this for initialization
	void Start () {
	
	}
	
    public void onClickQuitter() {
        Application.Quit();
    }

    public void ackIncomplteSheet()
    {
        panelIncompleteSheetError.SetActive(false);
    }

    public void showIncompleteSheetError()
    {
        panelIncompleteSheetError.SetActive(true);
    }


	// Update is called once per frame
	void Update () {
	
	}


}
