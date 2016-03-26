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
    Scene questionSceneWithoutAnswerText;
    Scene exempleScene;
    Fading fading;
    

    private float time;
    private bool sheetsExists;
    private float fadingTime;


    string sheetsDirectoryPath;
    string[] sheetsPath;

    private int totalSheets;
    private int currentSheetIndex;

    List<Sheet> availableSheet;
    List<Sheet> uncorrectlyAnsweredSheet;
    List<Sheet> correctlyAnsweredSheet;

    List<SheetInfos> sheetsInfos;

    /* 
     * The purpose of the variable is to put some time before re-showing a sheet that was previouly uncorrectly answered
     * Ex : If this variable = 2 , the there will be 3 sheet of the availableSheet list shown before re showing a sheet that was previously uncorrecly answered 
     */
    public int howManyAvailableBeforeUncorrectlyAnswered = 2;
    private int count = 0;

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
        questionSceneWithoutAnswerText = SceneManager.GetSceneByName("QuestionWithoutAnswerText");
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
            availableSheet.Sort();
        }
        sheetsExists = true;
        changeCurrentSheet();

        sheetsInfos = null;
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
        else if (questionSceneWithoutAnswerText.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("QuestionWithoutAnswerText");
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



    public void startQuestionnaire() {
       GameState.freezeTime();
        this.hasAnswered = false;
        StartCoroutine(startDisplay());
    }

    public IEnumerator startDisplay()
    {

        fadingTime = fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(fadingTime));
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
        fadingTime = fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(fadingTime));
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("Question");
        SceneManager.UnloadScene("QuestionWithoutAnswerText");
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

       
        sortAllSheetList();
        changeCurrentSheet();
    }


    private void sortAllSheetList()
    {
        availableSheet.Sort();
        correctlyAnsweredSheet.Sort();
        uncorrectlyAnsweredSheet.Sort();
    }

    /*
     * Select the first available sheet and makes it the currentSheet
     * if count = howManyAvailableBeforeUncorrectlyAnswered
     *  then the current sheet becomes a uncorrectly answered one
     * if there are no available sheet, it will select the first one not correctly answered
     * if all sheets have been correctly answered once, the current sheet is elected randomly
     */
    private void changeCurrentSheet()
    {
        if (sheetsExists)
        {
            if (availableSheet.Count != 0 && count != howManyAvailableBeforeUncorrectlyAnswered)
            {
                currentSheetIndex = 0;
                currentSheet = availableSheet[0];
            }

            else if (uncorrectlyAnsweredSheet.Count != 0)
            {
                currentSheetIndex = 0;
                currentSheet = uncorrectlyAnsweredSheet[0];
            }
            else if (uncorrectlyAnsweredSheet.Count == 0)
            {
                System.Random rnd = new System.Random();
                currentSheetIndex = rnd.Next(0, correctlyAnsweredSheet.Count);
                currentSheet = correctlyAnsweredSheet[currentSheetIndex];
            }

            if (uncorrectlyAnsweredSheet.Count != 0)
                count++;

            if (count > howManyAvailableBeforeUncorrectlyAnswered)
                count = 0;
        }
        else
            return;
    }


    // maybe usefull to recall start() 
    public void updateAccordindTo(Profile p)
    {
        sheetsInfos = p.getSheetList();
        for (int i = 0; i < sheetsInfos.Count; i++)
        {
            for (int j = 0; j < availableSheet.Count; j++)
            {
                if (sheetsInfos[i].sheetNumber == availableSheet[j].getSheetNumber())
                {
                    if (sheetsInfos[i].succesCount > 0) //means that the student already succeeded at least once
                    {
                        correctlyAnsweredSheet.Add(availableSheet[j]);
                        availableSheet.RemoveAt(j);
                    }
                    else if (sheetsInfos[i].errorCount > 0)
                    {
                        uncorrectlyAnsweredSheet.Add(availableSheet[j]);
                        availableSheet.RemoveAt(j);
                    }
                }
                else if (sheetsInfos[i].sheetNumber > availableSheet[j].sheetNumber)
                    break; // As the list are sorted by the sheet number
            }
        }
        changeCurrentSheet();

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

