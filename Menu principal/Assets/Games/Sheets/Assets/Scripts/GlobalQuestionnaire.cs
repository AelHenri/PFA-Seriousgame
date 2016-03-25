using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class GlobalQuestionnaire {


    static string sheetsDirectoryPath;

    static string[] sheetsPath;
    /* 0  = Sheet not yet shown
     * 1  = Sheet Shown and aswered correctly
     * -1 = Sheet shown and answered wrongly
     */
    static int[] sheetState;

    static private int totalSheets, unansweredSheet;
    static private int currentSheetIndex;
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
        unansweredSheet = totalSheets = sheetsPath.Length;
        sheetState = new int[sheetsPath.Length];


      changeCurrentSheet();
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

    public static void updateSheetState()
    {
        if (isAnswerRight)
            sheetState[currentSheetIndex] = 1;
        else
            sheetState[currentSheetIndex] = -1;
        changeCurrentSheet();
    }

    /*
     * Select the first unanswered sheet and makes it the currentSheet
     * if there are no unanswered sheet, it will select the first one not correctly answered
     */
    private static void changeCurrentSheet()
    {
        for (int i = 0; i < totalSheets; i++)
        {
            
            if (sheetState[i] == 0 && unansweredSheet > 0 )
            {
                currentSheetIndex = i;
                currentSheet = new Sheet(sheetsPath[currentSheetIndex]);
                break;
            }
              
            else if( sheetState[i] == -1 && unansweredSheet <= 0)
            {
                currentSheetIndex = i;
                currentSheet = new Sheet(sheetsPath[currentSheetIndex]);
                break;
            }
        }
    }

    private static void countUnansweredSheet()
    {
        int count = 0;
        for (int i = 0; i < totalSheets; i++)
            if (sheetState[i] == 0)
                count++;

        unansweredSheet = count;
    }
}
