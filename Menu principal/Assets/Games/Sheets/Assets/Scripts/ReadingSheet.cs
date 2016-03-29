using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public class ReadingSheet : Sheet
{
 
    public string imgQuestionPath;
    int rightAnswer;
    

    public ReadingSheet(string Path):base(Path)
    {
        xmlFile = XDocument.Load(Path); 

        imgQuestionPath = Path;    
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




    
    public string[] getImagesPath()
    {
        string[] imgsPath = new string[2];
        imgsPath[0] = imgExemplePath;
        imgsPath[1] = imgQuestionPath;
        return imgsPath;
    }



    public bool isRightAnswer(int myAnsmer)
    {
        return myAnsmer == rightAnswer;
    }




    /*
     * Loads the Exemple scene then wait for it to be fully loaded before destroying the Question scene 
     * in order to avoid having a few frames shown without scene
     */
     override
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
     override
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








}
