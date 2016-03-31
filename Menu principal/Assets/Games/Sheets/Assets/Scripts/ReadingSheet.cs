using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public class ReadingSheet : Sheet
{
    [NonSerialized]
    public string imgQuestionPath;
    [NonSerialized]
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
        questionScene = SceneManager.GetSceneByName("ReadingSheetQuestion");

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
        questionScene = SceneManager.GetSceneByName("ReadingSheetQuestion"); //if we don't do this the value of isLoaded doesn't seem to be refreshed, thus bypassing the if()
        if (questionScene.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("ReadingSheetQuestion");
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
        exempleScene = SceneManager.GetSceneByName("Exemple"); //if we don't do this the value of isLoaded doesn't seem to be refreshed, thus bypassing the if()
        if (exempleScene.isLoaded)
        {
            SceneManager.LoadScene("ReadingSheetQuestion", LoadSceneMode.Additive);
            yield return questionScene.isLoaded;
            SceneManager.UnloadScene("Exemple");
        }
        else
        {
            SceneManager.LoadScene("ReadingSheetQuestion", LoadSceneMode.Additive);
        }
    }

    override
        public void endSheet()
    {
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("ReadingSheetQuestion");
    }







}
