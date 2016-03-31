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

	void Start () {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
        if (!questionnaire.areThereSheets())
        {
            sheetDirectoryPath.text = questionnaire.getSheetDirectoryPath();
            showNoSheetError();
        }
	}
	
}
