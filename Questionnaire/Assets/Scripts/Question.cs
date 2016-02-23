using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;




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

    public Questionnaire questionnaire;
    public RawImage rawImageQuestion;

    public GameObject rightAnswerPanel;
    public GameObject wrongAnswerPanel;

    // Use this for initialization
    void Start () {
	    
        currentSheet = questionnaire.currentSheet;
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



    public void answer1Chosen()
    {
        if (currentSheet.isRightAnswer(1))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        questionnaire.setResult(currentSheet.isRightAnswer(1));
        
    }

    public void answer2Chosen()
    {
        if (currentSheet.isRightAnswer(2))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        questionnaire.setResult(currentSheet.isRightAnswer(2));
    }

    public void answer3Chosen()
    {
        if (currentSheet.isRightAnswer(3))
            rightAnswerPanel.SetActive(true);
        else
            wrongAnswerPanel.SetActive(true);
        questionnaire.setResult(currentSheet.isRightAnswer(3));
    }
}
