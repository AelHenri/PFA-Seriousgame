using UnityEngine;
using System.Collections;

public class Pixie7 : PNJ{

    private bool discoveryDialogDone = false;
    private bool pixieBeginningDialogDone = false;
    private bool revelationDialogDone = false;
    private bool pixieQuestion = false;
    private bool goAdventure = false;
    private bool hasAnsweredFalse = false;
    private bool penultimateDialogDone = false;
    private bool endDialogDone = false;
    private bool endEvent = false;
    private bool hasAnswered = false;

    protected override void Update()
    {
        base.Update();

        if (IsEndDialog() && discoveryDialogDone && !pixieBeginningDialogDone)
        {
            displayDialog(2, 4);
            pixieBeginningDialogDone = true;
        }

        if (IsEndDialog() && pixieBeginningDialogDone  && !revelationDialogDone)
        {
            if (!StoryGameManager.instance.HasObject("Secret"))
            {
                displayDialog(5, 6);
            }
            else
            {
                displayDialog(4, 5);
                goAdventure = true;
            }
            revelationDialogDone = true;
        }

        if (revelationDialogDone && IsEndDialog() && !StoryGameManager.instance.HasObject("Secret") && !pixieQuestion)
        {
            Question();
            pixieQuestion = true;
        }

        if (IsEndDialog() && (StoryGameManager.instance.HasObject("Secret") || hasAnswered ) && !penultimateDialogDone && !hasAnsweredFalse)
        {
            if (StoryGameManager.instance.IsPNJPresent("Olivier-happy"))
            {
                displayDialog(8, 10);
            }
            else
            {
                displayDialog(8, 9);
            }

            penultimateDialogDone = true;
        }

        if (IsEndDialog() && penultimateDialogDone && !endDialogDone)
        {
            displayDialog(10, dialog.Length - 1);
            endDialogDone = true;
        }

        if (IsEndDialog() && (endDialogDone || hasAnsweredFalse) && !endEvent)
        {
            StoryGameManager.instance.EndGame();
            endEvent = true;
        }

    }

    protected override void PNJLoadEvent()
    {
        if (StoryGameManager.instance.IsPNJPresent("Camille"))
        {
            displayDialog(0, 2);
        }
        else
        {
            displayDialog(dialog.Length - 1, dialog.Length);
        }
        discoveryDialogDone = true;
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        hasAnswered = true;
        goAdventure = true;
        displayDialog(6, 7);
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        hasAnswered = true;
        hasAnsweredFalse = true;
        displayDialog(7, 8);
        base.FalseAnswerEvent();
    }
}
