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

    Fading fading;

    private float time;

    public void Start()
    {
        GlobalQuestionnaire.q = this;
        questionScene = SceneManager.GetSceneByName("Question");
        exempleScene = SceneManager.GetSceneByName("Exemple");

        fading = GameObject.Find("Navigator").GetComponent<Fading>();
        
    }

    void Update()
    {
        if (GlobalQuestionnaire.q == null)
            GlobalQuestionnaire.q = this;
    }

    public void showQuestion()
    {
        StartCoroutine(loadQuestion());
    }

    public void showExemple()
    {
        StartCoroutine(loadExemple());
    }

    /*
     * Loads the Exemple scene then wait for it to be fully loaded before destroying the Question scene 
     * in order to avoid having a few frames shown without scene
     */
    IEnumerator loadExemple()
    {
        if (questionScene.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("Question");
        }
        else
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }

    /*
     * Loads the Question scene then wait for it to be fully loaded before destroying the Exemple scene 
     * in order to avoid having a few frames shown without scene
     */
    IEnumerator loadQuestion()
    {
        if (exempleScene.isLoaded)
        {
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
            yield return questionScene.isLoaded;
            SceneManager.UnloadScene("Exemple");
        }
        else
            SceneManager.LoadScene("Question", LoadSceneMode.Additive);
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

        fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
        showExemple();
        fading.beginFade(-1);

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
        fading.beginFade(1);
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
        SceneManager.UnloadScene("Exemple");
        SceneManager.UnloadScene("Question");
        fading.beginFade(-1);
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

