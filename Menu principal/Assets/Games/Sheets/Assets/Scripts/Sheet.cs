using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public class Sheet : Comparer<Sheet>, IComparable<Sheet>
{
    [NonSerialized]
    protected XDocument xmlFile;
    [NonSerialized]
    protected Scene exempleScene;
    [NonSerialized]
    protected Scene questionScene;
    [NonSerialized]
    protected Scene questionSceneWithoutAnswerText;

    public string textExemple;
    public string sheetName;
    public string[] answers;
    public string imgExemplePath, imgQuestionPath;
    public int sheetNumber;

    private int successCount;
    private int failureCount;

    protected string dirName;
    public string sheetStyle;

    int rightAnswer;
    

    public Sheet(string Path)
    {

        xmlFile = XDocument.Load(Path);
        textExemple = xmlFile.Root.Element("ExamplePart").Element("text").Value;
        sheetName = xmlFile.Root.Element("title").Value;
        sheetNumber = Int32.Parse(xmlFile.Root.Element("number").Value);
        imgExemplePath = Path;
        imgQuestionPath = Path;

        failureCount = 0;
        successCount = 0;
        sheetStyle = xmlFile.Root.Element("style").Value;
        if (sheetStyle == "normal")
        {
            answers = new string[3];
            answers[0] = xmlFile.Root.Element("QuestionPart").Element("answer1").Value;
            answers[1] = xmlFile.Root.Element("QuestionPart").Element("answer2").Value;
            answers[2] = xmlFile.Root.Element("QuestionPart").Element("answer3").Value;
        }
        dirName = System.IO.Path.GetDirectoryName(Path);
        imgExemplePath = System.IO.Path.Combine(dirName, "image_exemple.jpg");
        imgQuestionPath = System.IO.Path.Combine(dirName, "image_question.jpg");

        if (xmlFile.Root.Element("QuestionPart").Element("answer1").Attribute("value").ToString().Equals("value=\"true\""))
            rightAnswer = 1;
        else if (xmlFile.Root.Element("QuestionPart").Element("answer2").Attribute("value").ToString() == "value=\"true\"")
            rightAnswer = 2;
        else
            rightAnswer = 3;

        exempleScene = SceneManager.GetSceneByName("Exemple");
        questionScene = SceneManager.GetSceneByName("Question");
        questionSceneWithoutAnswerText = SceneManager.GetSceneByName("QuestionWithoutAnswerText");

    }

    public void addSucces()
    {
        this.successCount++;
    }
    public void addFailure()
    {
        this.failureCount++;
    }

    public int getSuccesCount()
    {
        return successCount;
    }
    public int getFailureCount()
    {
        return failureCount;
    }

    public string[] getAnswers()
    {
        return answers;
    }
    
    public string[] getImagesPath()
    {
        string[] imgsPath = new string[2];
        imgsPath[0] = imgExemplePath;
        imgsPath[1] = imgQuestionPath;
        return imgsPath;
    }

    public int getSheetNumber()
    {
        return sheetNumber;
    }

    public string getName()
    {
        return sheetName;
    }

    public bool isRightAnswer(int myAnsmer)
    {
        return myAnsmer == rightAnswer;
    }




    /*
     * Loads the Exemple scene then wait for it to be fully loaded before destroying the Question scene 
     * in order to avoid having a few frames shown without scene
     */
   public IEnumerator loadExemple()
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
  public  IEnumerator loadQuestion()
    {
        if (exempleScene.isLoaded)
        {
            if (this.sheetStyle == "normal")
                SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            else if (this.sheetStyle == "noAnswerText")
                SceneManager.LoadScene("QuestionWithoutAnswerText", LoadSceneMode.Additive);
            yield return questionScene.isLoaded;
            SceneManager.UnloadScene("Exemple");
        }
        else
        {
            if (this.sheetStyle == "normal")
                SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            else if (this.sheetStyle == "noAnswerText")
                SceneManager.LoadScene("QuestionWithoutAnswerText", LoadSceneMode.Additive);
        }
    }







    /*
     * Needed in order to be able to use List<Sheet>.Sort(), the number of the Sheet represent it's difficulty
     */
    override
    public int Compare(Sheet x, Sheet y)
    {
        if (x.getSheetNumber() < y.getSheetNumber())
            return -1;
        else if (x.getSheetNumber() > y.getSheetNumber())
            return 1;
        else
            return 0;
    }

    public int CompareTo(Sheet x)
    {
        if (this.getSheetNumber() < x.getSheetNumber())
            return -1;
        else if (this.getSheetNumber() > x.getSheetNumber())
            return 1;
        else
            return 0;
    }
}
