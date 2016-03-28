using UnityEngine;
using System.Collections;

public class Victor4 : PNJ{
    private bool inClickEvent = false;
    private bool firstDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;
    private bool arrowsPlaced = false;
    private bool hasAnsweredTrue = false;

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

        if (clickEventDone && IsEndDialog() && !arrowsPlaced)
        {
            callPlaceArrows();
            arrowsPlaced = true;
        }

    }

    protected override void PNJClickEvent()
    {
        inClickEvent = true;
        displayDialog(0, 1);
        base.PNJClickEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(1, 3);

        hasAnsweredTrue = true;
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(3, dialog.Length);
        base.RightAnswerEvent();
    }
}
