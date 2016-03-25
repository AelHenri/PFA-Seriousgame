using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    public GameObject noSheetPanel;
    public Text sheetDirectoryPath;


    private Questionnaire questionnaire;

    public void showNoSheetError()
    {
        noSheetPanel.SetActive(true);
    }

    public void hideNoSheetError()
    {
        noSheetPanel.SetActive(false);
    }


	// Use this for initialization
	void Start () {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
        if (!questionnaire.areThereSheets())
        {
            sheetDirectoryPath.text = questionnaire.getSheetDirectoryPath();
            showNoSheetError();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
