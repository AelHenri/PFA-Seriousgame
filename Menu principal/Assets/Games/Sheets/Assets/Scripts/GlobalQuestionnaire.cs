﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class GlobalQuestionnaire {


    static string sheetsDirectoryPath;
    static string[] sheetsPath;
    [HideInInspector]
    static public Sheet currentSheet;
    static public Questionnaire q;
    public static bool isAnswerRight;
    public static bool hasAnswered = false;

    // Use this for initialization
    public static void Start()
    {
        sheetsDirectoryPath = Application.dataPath + "/../Fiches";
        System.IO.Path.GetFullPath(sheetsDirectoryPath);
        sheetsPath = System.IO.Directory.GetFiles(sheetsDirectoryPath, "*.xml", System.IO.SearchOption.AllDirectories);
        currentSheet = new Sheet(sheetsPath[1]);
    }

    public static void setResult(bool result)
    {
        isAnswerRight = result;

    }

    public static void startQuestionnaire()
    {
        q.startQuestionnaire();
    } 

    public static void setAnswer(bool answer)
    {
        isAnswerRight = answer;
    }

    public static bool getAnswer()
    {
        return isAnswerRight;
    }

}