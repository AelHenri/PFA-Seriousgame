﻿using UnityEngine;
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

    Questionnaire questionanire;
    Scene questionScene;
    Scene exempleScene;
    // Use this for initialization
    void Start () {
        questionanire = GameObject.Find("Navigator").GetComponent<Questionnaire>();
        currentSheet = questionanire.currentSheet;    
        audioSource = GetComponent<AudioSource>();

        questionScene = SceneManager.GetSceneByName("Question");
        exempleScene = SceneManager.GetSceneByName("Exemple");
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
        answer1.text = currentSheet.answers[0];
        answer2.text = currentSheet.answers[1];
        answer3.text = currentSheet.answers[2];

        if (img_question == null)
        {
            img_question = LoadPNG(currentSheet.imgQuestionPath);
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
        if (currentSheet.isRightAnswer(1))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        questionanire.setResult(currentSheet.isRightAnswer(1));
        questionanire.hasAnswered = true;

        playAnswerSound(currentSheet.isRightAnswer(1));
        StartCoroutine(questionanire.endQuestionnaire());
    }

    public void answer2Chosen()
    {
        if (currentSheet.isRightAnswer(2))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        playAnswerSound(currentSheet.isRightAnswer(2));
       questionanire.setResult(currentSheet.isRightAnswer(2));
        questionanire.hasAnswered = true;
        StartCoroutine(questionanire.endQuestionnaire());
    }

    public void answer3Chosen()
    {
        if (currentSheet.isRightAnswer(3))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        playAnswerSound(currentSheet.isRightAnswer(3));
        questionanire.setResult(currentSheet.isRightAnswer(3));
       questionanire.hasAnswered = true;
        StartCoroutine(questionanire.endQuestionnaire());
    }
}
