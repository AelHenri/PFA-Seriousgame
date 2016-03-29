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
        if (questionScene.isLoaded)
        {
            Debug.Log("KOUKUO");
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("TextReadingSheetQuestion");
        }
        else
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }


    override
 public IEnumerator loadQuestion()
    {
        if (exempleScene.isLoaded)
        {
            SceneManager.LoadScene("TextReadingSheetQuestion", LoadSceneMode.Additive);
            yield return questionScene.isLoaded;
            SceneManager.UnloadScene("Exemple");
        }
        else
        {
            SceneManager.LoadScene("TextReadingSheetQuestion", LoadSceneMode.Additive);
        }
    }
}
