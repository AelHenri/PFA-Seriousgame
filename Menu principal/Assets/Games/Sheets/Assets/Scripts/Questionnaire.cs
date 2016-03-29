using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Xml.Linq;


/*
 * Scrpit gérant le système de question réponse dans sa globalité
 */

public class Questionnaire : MonoBehaviour{

    Fading fading;
    Profile currentProfile = null;
    

    private float time;
    private bool sheetsExists;
    private float fadingTime;


    string sheetsDirectoryPath;
    string[] sheetsPath;

    private int totalSheets;
    private int currentSheetIndex;
    private int currentSheetNumber; //the number is a totally different thing compared to the index

    List<Sheet> availableSheet;
    List<Sheet> uncorrectlyAnsweredSheet;
    List<Sheet> correctlyAnsweredSheet;

 
    List<Sheet> profileSheets;

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
    [HideInInspector]
    public bool hasAnsweredAll = false;

    private XDocument xmlSheet;

    public int howManyRightAnswers;

    private bool isAnswering;
    private string sheetType;

    public void Start()
    {        
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
                xmlSheet = XDocument.Load(sheetsPath[i]);
                sheetType =  xmlSheet.Root.Element("type").Value.ToString();

                if (sheetType == "TextReadingSheet")
                    availableSheet.Add(new TextReadingSheet(sheetsPath[i]));
                else if (sheetType == "ReadingSheet")
                    availableSheet.Add(new ReadingSheet(sheetsPath[i]));
            }
            availableSheet.Sort();
        }
        sheetsExists = true;
        changeCurrentSheet();

        profileSheets = null;
    }

    public bool areThereSheets()
    {
        return sheetsExists;
    }

    public string getSheetDirectoryPath()
    {
        return sheetsDirectoryPath;
    }
    public void setCurrentProfile(Profile p)
    {
        currentProfile = p;
    }

    public void showQuestion()
    {
        StartCoroutine(currentSheet.loadQuestion());
    }

    public void showExemple()
    {
        StartCoroutine(currentSheet.loadExemple());
    }


    public void startQuestionnaire(int numberOfQuestion)
    {
        StartCoroutine(multipleQuestionnaire(numberOfQuestion));
    }

    public IEnumerator multipleQuestionnaire(int numberOfQuestion)
    {
        int count = 0;
        hasAnsweredAll= false;
        howManyRightAnswers = 0;
        while (count < numberOfQuestion)
        {

            startQuestionnaire();
            while (!this.hasAnswered)
                yield return new WaitUntil(() => this.hasAnswered);
            yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1)); //this is mandatory, without this, it doesn't work
            count++;
            if (isAnswerRight)
                howManyRightAnswers++;
        }
        hasAnsweredAll = true;
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

  
        time = Time.realtimeSinceStartup;
        yield return new WaitUntil(hasSecondPassed);
        fadingTime = fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(fadingTime));
        currentSheet.endSheet();
        fading.beginFade(-1);
        answerGiven();
        this.updateSheetState();

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

        if (currentProfile != null)
            updateSheetsInfos();
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
                currentSheetNumber = availableSheet[0].getSheetNumber();
            }

            else if (uncorrectlyAnsweredSheet.Count != 0)
            {
                currentSheetIndex = 0;
                currentSheet = uncorrectlyAnsweredSheet[0];
                currentSheetNumber = uncorrectlyAnsweredSheet[0].getSheetNumber();
            }
            else if (uncorrectlyAnsweredSheet.Count == 0)
            {
                System.Random rnd = new System.Random();
                currentSheetIndex = rnd.Next(0, correctlyAnsweredSheet.Count);
                currentSheet = correctlyAnsweredSheet[currentSheetIndex];
                currentSheetNumber = correctlyAnsweredSheet[currentSheetIndex].getSheetNumber();
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
        profileSheets = p.getRealSheetList();
        Sheet tmp;
        for (int i = 0; i < profileSheets.Count; i++)
        {
            if (availableSheet.Exists(x => x.sheetNumber == profileSheets[i].sheetNumber)) ////means that the student already encountered the sheet once
            {
                tmp = availableSheet.Find(x => x.sheetNumber == profileSheets[i].sheetNumber);
                if (profileSheets[i].getSuccesCount() > 0) //means that the student already succeeded at least once
                {
                    correctlyAnsweredSheet.Add(tmp);
                    availableSheet.RemoveAt(availableSheet.IndexOf(tmp));
                }
                else if (profileSheets[i].getFailureCount() > 0)
                {
                    uncorrectlyAnsweredSheet.Add(tmp);
                    availableSheet.RemoveAt(availableSheet.IndexOf(tmp));
                }
            }
        }
        changeCurrentSheet();
    }


    void updateSheetsInfos()
    {
        int index, insertIndex;
        Sheet tmp;
 
 
        if (profileSheets.Exists(x => x.sheetNumber == currentSheetNumber))
        {
            index = profileSheets.FindIndex(x => x.sheetNumber == currentSheetNumber);
            if (isAnswerRight)
                profileSheets[index].incrementSuccesCount();
            else
                profileSheets[index].incrementFailureCount();
        }
        else
        {
            tmp = currentSheet;
            if (isAnswerRight)
                tmp.incrementSuccesCount();
            else
                tmp.incrementFailureCount();

            insertIndex = profileSheets.FindIndex(x => x.sheetNumber >= tmp.getSheetNumber());
            if (insertIndex == -1 || insertIndex == 0) //means there's no sheetNumber above this new one
                profileSheets.Add(tmp);
            else
                profileSheets.Insert(insertIndex - 1, tmp);
        }

    }


    public void updateCurrentProfile()
    {
        if (currentProfile != null)
        {
            currentProfile.updateSheetList(profileSheets);
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

