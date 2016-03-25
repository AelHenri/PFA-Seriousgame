using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * Scrpit gérant le système de question réponse dans sa globalité
 */

public class Questionnaire : MonoBehaviour{

    Scene questionScene;
    Scene exempleScene;

    Fading fading;

    private float time;


    string sheetsDirectoryPath;

     string[] sheetsPath;
    /* 0  = Sheet not yet shown
     * 1  = Sheet Shown and aswered correctly
     * -1 = Sheet shown and answered wrongly
     */
     int[] sheetState;

    private int totalSheets, unansweredSheet;
    private int currentSheetIndex;
    [HideInInspector]
    public Sheet currentSheet;
    public  bool isAnswerRight;
    public  bool hasAnswered = false;

    public void Start()
    {
        
        questionScene = SceneManager.GetSceneByName("Question");
        exempleScene = SceneManager.GetSceneByName("Exemple");
        fading = GameObject.Find("Navigator").GetComponent<Fading>();

        sheetsDirectoryPath = Application.dataPath + "/../Fiches";
        System.IO.Path.GetFullPath(sheetsDirectoryPath);
        sheetsPath = System.IO.Directory.GetFiles(sheetsDirectoryPath, "*.xml", System.IO.SearchOption.AllDirectories);
        unansweredSheet = totalSheets = sheetsPath.Length;
        sheetState = new int[sheetsPath.Length];


        changeCurrentSheet();

    }

    void Update()
    {
      
    }

    public void showQuestion()
    {
        StartCoroutine(loadQuestion());
    }

    public void showExemple()
    {
        StartCoroutine(loadExemple());
    }

    /*
     * Loads the Exemple scene then wait for it to be fully loaded before destroying the Question scene 
     * in order to avoid having a few frames shown without scene
     */
    IEnumerator loadExemple()
    {
        if (questionScene.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("Question");
        }
        else
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }

    /*
     * Loads the Question scene then wait for it to be fully loaded before destroying the Exemple scene 
     * in order to avoid having a few frames shown without scene
     */
    IEnumerator loadQuestion()
    {
        if (exempleScene.isLoaded)
        {
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            yield return questionScene.isLoaded;
            SceneManager.UnloadScene("Exemple");
        }
        else
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
    }



    public void startQuestionnaire() {
       GameState.freezeTime();
        this.hasAnswered = false;
        //StartCoroutine(WaitAndPrint(2.0F));
        StartCoroutine(startDisplay());
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
    }
    public IEnumerator startDisplay()
    {

        fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
        showExemple();
        fading.beginFade(-1);

        while (!this.hasAnswered)
            yield return new WaitUntil(() => this.hasAnswered);
        Debug.Log("AfterAnswer");
        yield return null;
    }


    public IEnumerator endQuestionnaire()
    {

        this.updateSheetState();
        time = Time.realtimeSinceStartup;
        yield return new WaitUntil(hasSecondPassed);
        fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("Question");
        fading.beginFade(-1);
        answerGiven();

    }

   public  void setResult(bool result)
    {
        isAnswerRight = result;

    }

    private void answerGiven()
    {
        GameState.unfreezeTime();
        Debug.Log("isAnswerRight:" + this.isAnswerRight);
    }

    public void setAnswer(bool answer)
    {
        isAnswerRight = answer;
    }

    public bool getAnswer()
    {
        return isAnswerRight;
    }

    private bool hasSecondPassed()
    {
        return (Time.realtimeSinceStartup - time) >= 1;
    }




    public void updateSheetState()
    {
        if (isAnswerRight)
            sheetState[currentSheetIndex] = 1;
        else
            sheetState[currentSheetIndex] = -1;
        changeCurrentSheet();
    }

    /*
     * Select the first unanswered sheet and makes it the currentSheet
     * if there are no unanswered sheet, it will select the first one not correctly answered
     */
    private  void changeCurrentSheet()
    {
        for (int i = 0; i < totalSheets; i++)
        {

            if (sheetState[i] == 0 && unansweredSheet > 0)
            {
                currentSheetIndex = i;
                currentSheet = new Sheet(sheetsPath[currentSheetIndex]);
                break;
            }

            else if (sheetState[i] == -1 && unansweredSheet <= 0)
            {
                currentSheetIndex = i;
                currentSheet = new Sheet(sheetsPath[currentSheetIndex]);
                break;
            }
        }
    }

    private void countUnansweredSheet()
    {
        int count = 0;
        for (int i = 0; i < totalSheets; i++)
            if (sheetState[i] == 0)
                count++;

        unansweredSheet = count;
    }





    /*
     * Coroutine used to have a WaitForSeconds alike even if the the time is frozen
     */

    public static class CoroutineUtil
    {
        public static IEnumerator WaitForRealSeconds(float time)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup < start + time)
            {
                yield return null;
            }
        }
    }
}

