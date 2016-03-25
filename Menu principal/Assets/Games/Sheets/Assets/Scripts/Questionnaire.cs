using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;


/*
 * Scrpit gérant le système de question réponse dans sa globalité
 */

public class Questionnaire : MonoBehaviour{

    Scene questionScene;
    Scene exempleScene;
    Fading fading;
    

    private float time;
    private bool sheetsExists;


    string sheetsDirectoryPath;
    string[] sheetsPath;

    private int totalSheets;
    private int currentSheetIndex;

    List<Sheet> availableSheet;
    List<Sheet> uncorrectlyAnsweredSheet;
    List<Sheet> correctlyAnsweredSheet;
 

    [HideInInspector]
    public Sheet currentSheet;
    [HideInInspector]
    public  bool isAnswerRight;
    [HideInInspector]
    public  bool hasAnswered = false;

    public void Start()
    {        
        questionScene = SceneManager.GetSceneByName("Question");
        exempleScene = SceneManager.GetSceneByName("Exemple");
        fading = GameObject.Find("Navigator").GetComponent<Fading>();

        availableSheet = new List<Sheet>();
        uncorrectlyAnsweredSheet = new List<Sheet>();
        correctlyAnsweredSheet = new List<Sheet>();

        sheetsDirectoryPath = Application.dataPath + "/../Fiches";
        sheetsDirectoryPath = Path.GetFullPath(sheetsDirectoryPath); // Returns a nice formated path String, no more: "dir/../another_dir"

        if (!Directory.Exists(sheetsDirectoryPath))
        {
            
            Directory.CreateDirectory(sheetsDirectoryPath);
            sheetsExists = false;
            return;
        }
        else
        {
            sheetsPath = Directory.GetFiles(sheetsDirectoryPath, "*.xml", SearchOption.AllDirectories);

            if (sheetsPath.Length == 0)
            {
                sheetsExists = false;
                return;
            }
            totalSheets = sheetsPath.Length;
            for (int i = 0; i < totalSheets; i++)
            {
                availableSheet.Add(new Sheet(sheetsPath[i]));
            }
        }
        sheetsExists = true;
        changeCurrentSheet();

    }

    public bool areThereSheets()
    {
        return sheetsExists;
    }

    public string getSheetDirectoryPath()
    {
        return sheetsDirectoryPath;
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
        StartCoroutine(startDisplay());
    }

    public IEnumerator startDisplay()
    {

        fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
        showExemple();
        fading.beginFade(-1);

        while (!this.hasAnswered)
            yield return new WaitUntil(() => this.hasAnswered);
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
        {
            if (availableSheet.Contains(currentSheet))
            {
                //correctlyAnsweredSheet.Add(availableSheet[currentSheetIndex]);
                correctlyAnsweredSheet.Add(currentSheet);
                availableSheet.RemoveAt(currentSheetIndex);
            }
            else if (uncorrectlyAnsweredSheet.Contains(currentSheet))
            {
                correctlyAnsweredSheet.Add(currentSheet);
                uncorrectlyAnsweredSheet.RemoveAt(currentSheetIndex);
            }
         }
        else
        {
            if (availableSheet.Contains(currentSheet))
            {
                uncorrectlyAnsweredSheet.Add(currentSheet);
                availableSheet.RemoveAt(currentSheetIndex);
            }
            else if (uncorrectlyAnsweredSheet.Contains(currentSheet))
            {
                // No need to add the currentSheet since it's already in the uncorrectlyAnsweredSheet List
                // It may be cool to set a variable in order not to spam the same uncorrectlyAnsweredSheet if there are other uncorrect sheet available
            }
        }

        changeCurrentSheet();
    }

    /*
     * Select the first unanswered sheet and makes it the currentSheet
     * if there are no unanswered sheet, it will select the first one not correctly answered
     */
    private void changeCurrentSheet()
    {
        if (sheetsExists)
        {
            if (availableSheet.Count != 0)
            {
                currentSheetIndex = 0;
                currentSheet = availableSheet[0];
            }

            else if (availableSheet.Count == 0 && uncorrectlyAnsweredSheet.Count != 0)
            {
                currentSheetIndex = 0;
                currentSheet = uncorrectlyAnsweredSheet[0];
            }
            else if (availableSheet.Count == 0 && uncorrectlyAnsweredSheet.Count == 0)
            {
                System.Random rnd = new System.Random();
                currentSheetIndex = rnd.Next(0, correctlyAnsweredSheet.Count);
                currentSheet = correctlyAnsweredSheet[currentSheetIndex];
            }
        }
        else
            return;
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

