using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/*
 * Scrpit gérant le système de question réponse dans sa globalité
 */

public class Questionnaire : MonoBehaviour{


    public void showQuestion()
    {
        SceneManager.LoadScene("Question", LoadSceneMode.Additive);
    }

    public void showExemple()
    {
        SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }

    public bool startQuestionnaire() { 

        Time.timeScale = 0;
        GlobalQuestionnaire.hasAnswered = false;
        Debug.Log("Hello");
        StartCoroutine(startDisplay());
        Time.timeScale = 1;
        Debug.Log("isAnswerRight:" + GlobalQuestionnaire.isAnswerRight);
        return GlobalQuestionnaire.isAnswerRight;
    }
    public IEnumerator startDisplay()
    {
        showExemple();
        Debug.Log("hasAnswered" + GlobalQuestionnaire.hasAnswered);
        while (!GlobalQuestionnaire.hasAnswered)
            yield return new WaitUntil(() => true);
        Debug.Log("AfterAnswer");
    }
 

 

}
