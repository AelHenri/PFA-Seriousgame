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

    private float time;

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
        if (exempleScene.isLoaded)
        {
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            SceneManager.UnloadScene("Exemple");
        }
        else
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
    }

    public void showExemple()
    {
        if (questionScene.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            SceneManager.UnloadScene("Question");
        }
        else
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }

    public void startQuestionnaire() {
        GameState.freezeTime();
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
        yield return null;
    }
 

   public IEnumerator endQuestionnaire() 
    {
        GlobalQuestionnaire.updateSheetState();
        time = Time.realtimeSinceStartup;
        yield return new WaitUntil(hasSecondPassed);
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("Question");
        answerGiven();
    } 
 
    private void answerGiven()
    {
        GameState.unfreezeTime();
        Debug.Log("isAnswerRight:" + GlobalQuestionnaire.isAnswerRight);
    }

    private bool hasSecondPassed()
    {
        return (Time.realtimeSinceStartup - time) >= 1;
    }
}

