using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class Exemple : MonoBehaviour {

    public Text exempleText;
    [HideInInspector]
    Texture2D img_exemple = null;
    WWW www;

    Questionnaire questionnaire;
    Sheet currentSheet;
    Scene questionScene;
    Scene exempleScene;
    public RawImage rawImageExemple;


    public void initExemple(Sheet sheet)
    {
        exempleText.text = sheet.textExemple;
    }



    // Use this for initialization
    void Start () {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
        currentSheet = questionnaire.currentSheet;

        questionScene = SceneManager.GetSceneByName("Question");
        exempleScene = SceneManager.GetSceneByName("Exemple");
    }




    public void showQuestion()
    {
        StartCoroutine(loadQuestion());
    }


    /*
    * Loads the Question scene then wait for it to be fully loaded before destroying the Exemple scene 
    * in order to avoid having a few frames shown without scene
    */
    IEnumerator loadQuestion()
    {
        if (exempleScene.isLoaded)
        {
            if (currentSheet.sheetStyle == "normal")
                SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            else if (currentSheet.sheetStyle == "noAnswerText")
                SceneManager.LoadScene("QuestionWithoutAnswerText", LoadSceneMode.Additive);
            yield return questionScene.isLoaded;
            SceneManager.UnloadScene("Exemple");
        }
        else
        {
            if (currentSheet.sheetStyle == "normal")
                SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            else if (currentSheet.sheetStyle == "noAnswerText")
                SceneManager.LoadScene("QuestionWithoutAnswerText", LoadSceneMode.Additive);
        }
    }


    // Update is called once per frame
    void Update()
    {
        exempleText.text = questionnaire.currentSheet.textExemple;

        if (img_exemple == null) {
            img_exemple = LoadPNG(questionnaire.currentSheet.imgExemplePath);
            rawImageExemple.texture = img_exemple;
        }
     
    }


    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }





}
