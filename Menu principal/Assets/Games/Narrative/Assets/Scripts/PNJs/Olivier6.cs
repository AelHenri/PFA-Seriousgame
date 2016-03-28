using UnityEngine;
using System.Collections;

public class Olivier6 : PNJ{

    private bool inClickEvent = false;
    private bool firstDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;
    private bool hasAnsweredFalse = false;
    private bool arrowsPlaced = false;

    protected override void Update()
    {

        base.Update();

        if (!firstDialogDone && inClickEvent && IsEndDialog())
        {
            firstDialogDone = true;
        }

        if (firstDialogDone && !questionAsked)
        {
            questionAsked = true;
            Question();
        }

        if (clickEventDone && !arrowsPlaced && IsEndDialog())
        {
            arrowsPlaced = true;
            if (StoryGameManager.instance.HasObject("Seve") && !hasAnsweredFalse)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                callPlaceArrows();
            }
        }
    }
    
    protected override void PNJLoadEvent()
    {
        inClickEvent = true;
        displayDialog(0, 4);
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;

        if (StoryGameManager.instance.HasObject("Seve"))
        {
            displayDialog(4, 8);
        }
        else
        {
            displayDialog(8, 13);
        }
        
        StoryGameManager.instance.AddPNJ("Olivier-sad");
        StoryGameManager.instance.AddPNJ("Olivier-happy");

        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        hasAnsweredFalse = true;
        displayDialog(13, dialog.Length);
        base.FalseAnswerEvent();
    }
}
