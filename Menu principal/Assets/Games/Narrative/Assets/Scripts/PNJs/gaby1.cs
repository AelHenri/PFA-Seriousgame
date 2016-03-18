using UnityEngine;
using System.Collections;

public class gaby1 : PNJ {

    private bool firstDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;
    private bool arrowsPlaced = false;
    private bool inClickEvent = false;
    private bool hasAnsweredFalse = false;

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

        if (hasAnsweredFalse)
        {
            hasAnsweredFalse = false;
            Question();
        }

        if (clickEventDone && IsEndDialog() && !arrowsPlaced)
        {
            callPlaceArrows();
            arrowsPlaced = true;
        }
    }

    protected override void PNJClickEvent()
    {
        inClickEvent = true;
        displayDialog(0, 2);
        //inClickEvent = false;
    }

    protected override void PNJLoadEvent()
    {
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        displayDialog(2, dialog.Length);
        clickEventDone = true;
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        hasAnsweredFalse = true;
        base.FalseAnswerEvent();
    }

}
