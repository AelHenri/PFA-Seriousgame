using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;




public class Question : MonoBehaviour {

    [HideInInspector]
    Texture2D img_question = null;
    WWW www;

    Sheet currentSheet;
    ReadingSheet rs; 

    public Text answer1;
    public Text answer2;
    public Text answer3;

    Rect rectImgQuestion;

    int imageWidth, imageHeight;

    public RawImage rawImageQuestion;

    public GameObject rightAnswerPanel;
    public GameObject wrongAnswerPanel;


    public AudioClip mistakeSound;
    public AudioClip successSound;
    AudioSource audioSource;

    Questionnaire questionnaire;
    Scene questionScene;
    Scene exempleScene;
    Scene questionSceneWithoutAnswerText;

    // Use this for initialization
    void Start () {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
        currentSheet = questionnaire.currentSheet;
        rs = (ReadingSheet)currentSheet;
        audioSource = GetComponent<AudioSource>();
        

        questionScene = SceneManager.GetSceneByName("Question");
        questionSceneWithoutAnswerText = SceneManager.GetSceneByName("QuestionWithoutAnswerText");
        exempleScene = SceneManager.GetSceneByName("Exemple");
    }

    public void showExemple()
    {
        questionnaire.showExemple();
    }

    /*
     * Loads the Exemple scene then wait for it to be fully loaded before destroying the Question scene 
     * in order to avoid having a few frames shown without scene
     *//*
    IEnumerator loadExemple()
    {
        if (questionScene.isLoaded)
        {
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("Question");
        }
        else if (questionSceneWithoutAnswerText.isLoaded)
        {
            Debug.Log("KOUKO");
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
            yield return exempleScene.isLoaded;
            SceneManager.UnloadScene("QuestionWithoutAnswerText");
        }
        else
            SceneManager.LoadScene("Exemple", LoadSceneMode.Additive);
    }
    */
    public static Texture2D LoadPNG(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }


    // Update is called once per frame
    void Update () {
        if (currentSheet.sheetStyle == "normal")
        {
            answer1.text = rs.answers[0];
            answer2.text = rs.answers[1];
            answer3.text = rs.answers[2];
        }

        if (img_question == null)
        {
            img_question = LoadPNG(rs.imgQuestionPath);
            rawImageQuestion.texture = img_question;

        }
        
    }

    void playAnswerSound(bool isAnswerRight)
    {
        if (isAnswerRight)
        {
            audioSource.clip = successSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = mistakeSound;
            audioSource.Play();
        }

    }

    public void answer1Chosen()
    {
        if (rs.isRightAnswer(1))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        questionnaire.setResult(rs.isRightAnswer(1));
        questionnaire.hasAnswered = true;

        playAnswerSound(rs.isRightAnswer(1));
        StartCoroutine(questionnaire.endQuestionnaire());
    }

    public void answer2Chosen()
    {
        if (rs.isRightAnswer(2))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        playAnswerSound(rs.isRightAnswer(2));
       questionnaire.setResult(rs.isRightAnswer(2));
        questionnaire.hasAnswered = true;
        StartCoroutine(questionnaire.endQuestionnaire());
    }

    public void answer3Chosen()
    {
        if (rs.isRightAnswer(3))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        playAnswerSound(rs.isRightAnswer(3));
        questionnaire.setResult(rs.isRightAnswer(3));
       questionnaire.hasAnswered = true;
        StartCoroutine(questionnaire.endQuestionnaire());
    }
}
