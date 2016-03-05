using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class GlobalQuestionnaire {


    static string sheetsDirectoryPath;
    static string[] sheetsPath;
    [HideInInspector]
    static public Sheet currentSheet;
    static public Questionnaire q;
    static public bool isAnswerRight;
    public static bool hasAnswered = false;

    // Use this for initialization
    public static void Start()
    {
        sheetsDirectoryPath = Application.dataPath + "/../Fiches";
        System.IO.Path.GetFullPath(sheetsDirectoryPath);
        sheetsPath = System.IO.Directory.GetFiles(sheetsDirectoryPath, "*.xml", System.IO.SearchOption.AllDirectories);

        Debug.Log("Coucou, je suis dans globalq");
        currentSheet = new Sheet(sheetsPath[0]);
    }

    public static void setResult(bool result)
    {
        isAnswerRight = result;


    }

    public static bool startQuestionnaire()
    {
        return q.startQuestionnaire();
    } 

  


}
