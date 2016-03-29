using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TextReadingSheet : ReadingSheet
{
    public string[] answers;

    public TextReadingSheet(string Path) : base(Path)
    {
        answers = new string[3];
        answers[0] = xmlFile.Root.Element("QuestionPart").Element("answer1").Value;
        answers[1] = xmlFile.Root.Element("QuestionPart").Element("answer2").Value;
        answers[2] = xmlFile.Root.Element("QuestionPart").Element("answer3").Value;

        
        questionScene = SceneManager.GetSceneByName("TextReadingSheetQuestion");
    }



    override
    public IEnumerator loadExemple()
    {
        questionScene = SceneManager.GetSceneByName("TextReadingSheetQuestion"); //if we don't do this the value of isLoaded doesn't seem to be refreshed, thus bypassing the if()
        if (questionScene.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded == true;
            SceneManager.UnloadScene("TextReadingSheetQuestion");
        }
        else
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }


    override
 public IEnumerator loadQuestion()
    {
        exempleScene = SceneManager.GetSceneByName("Exemple"); //if we don't do this the value of isLoaded doesn't seem to be refreshed, thus bypassing the if()
        if (exempleScene.isLoaded)
        {
            SceneManager.LoadScene("TextReadingSheetQuestion", LoadSceneMode.Additive);
            yield return questionScene.isLoaded == true;
            SceneManager.UnloadScene("Exemple");
        }
        else
        {
            SceneManager.LoadScene("TextReadingSheetQuestion", LoadSceneMode.Additive);
        }
    }

    override
        public void endSheet()
    {
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("TextReadingSheetQuestion");
    }
}
