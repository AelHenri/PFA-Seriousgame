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

    public void Start()
    {
        GlobalQuestionnaire.q = this;
        questionScene = SceneManager.GetSceneByName("Question");
        exempleScene = SceneManager.GetSceneByName("Exemple");
    }

    void Update()
    {
        if (GlobalQuestionnaire.q == null)
            GlobalQuestionnaire.q = this;
    }

    public void showQuestion()
    {
        if (!questionScene.isLoaded)
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
        else
        {
            SceneManager.UnloadScene("Question");
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
        } 
    }

    public void showExemple()
    {
        if (!exempleScene.isLoaded)
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);

        else
        {
            SceneManager.UnloadScene("Exemple");
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
        }
    }

    public void startQuestionnaire() {
        Time.timeScale = 0;
        GlobalQuestionnaire.hasAnswered = false;
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
        showExemple();
        while (!GlobalQuestionnaire.hasAnswered)
            yield return new WaitUntil(() => GlobalQuestionnaire.hasAnswered);
        Debug.Log("AfterAnswer");
        answerGiven();
        yield return null;
    }
 

   public IEnumerator endQuestionnaire() 
    {
        yield return new WaitForSeconds(1);
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("Question");
    } 
 
    private void answerGiven()
    {
        Time.timeScale = 1;
        Debug.Log("isAnswerRight:" + GlobalQuestionnaire.isAnswerRight);
    }
}
