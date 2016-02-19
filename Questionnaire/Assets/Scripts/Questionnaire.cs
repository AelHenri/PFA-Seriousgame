using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * Scrpit gérant le système de question réponse dans sa globalité
 */

public class Questionnaire : MonoBehaviour {

    

    string sheetsDirectoryPath;
    string[] sheetsPath;

    bool isAnswerRight;
    [HideInInspector]
    public Sheet currentSheet;



    public bool startQuestionnaire()
    {
        showQuestion();
        return isAnswerRight;
    }

    public void showQuestion()
    {
        SceneManager.LoadScene("Question", LoadSceneMode.Single);
    }

    public void showExemple()
    {
        SceneManager.LoadScene("Exemple", LoadSceneMode.Single);
    }


	// Use this for initialization
	void Start () {
        sheetsDirectoryPath = Application.dataPath + "/../Fiches";
        System.IO.Path.GetFullPath(sheetsDirectoryPath);
        sheetsPath = System.IO.Directory.GetFiles(sheetsDirectoryPath, "*.xml", System.IO.SearchOption.AllDirectories);


        currentSheet= new Sheet(sheetsPath[0]);

    }


    public void setResult(bool result)
    {
        isAnswerRight = result;
    }




    // Update is called once per frame
    void Update () {
        Debug.Log(isAnswerRight);
	
	}
}
