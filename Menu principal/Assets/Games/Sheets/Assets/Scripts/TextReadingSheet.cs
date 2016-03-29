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
    }



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


    override
 public IEnumerator loadQuestion()
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
