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
        Debug.Log("exemplescene : " + exempleScene);
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
        //SceneManager.SetActiveScene(questionScene);

        
        //    SceneManager.UnloadScene("Exemple");
    }

    public void showExemple()
    {
        if (!exempleScene.isLoaded)
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(exempleScene);
        else
        {
            SceneManager.UnloadScene("Exemple");
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
        }
    }

    public bool startQuestionnaire() {
        Time.timeScale = 0;
        GlobalQuestionnaire.hasAnswered = false;
        Debug.Log("Hello");
        //StartCoroutine(WaitAndPrint(2.0F));
        StartCoroutine(startDisplay());
        Time.timeScale = 1;
        Debug.Log("isAnswerRight:" + GlobalQuestionnaire.isAnswerRight);
        return GlobalQuestionnaire.isAnswerRight;
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        print("WaitAndPrint " + Time.time);
    }
    public IEnumerator startDisplay()
    {
        showExemple();
        Debug.Log("hasAnswered" + GlobalQuestionnaire.hasAnswered);
        while (!GlobalQuestionnaire.hasAnswered)
            yield return new WaitUntil(() => GlobalQuestionnaire.hasAnswered);
        Debug.Log("AfterAnswer");
        yield return null;
    }
 

 

}
