using UnityEngine;
using System.Collections;

public class Camille2 : PNJ
{
    private bool inClickEvent = false;
    private bool firstDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;
    private bool tpPlaced = false;

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

        if (clickEventDone && IsEndDialog() && !tpPlaced)
        {
            displayDialog(dialog.Length - 1, dialog.Length);
            callPlaceTPs();
            tpPlaced = true;
        }
    }

    protected override void PNJClickEvent()
    {
        inClickEvent = true;
        displayDialog(0, 1);
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(1, 3);
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(3, 5);
        base.RightAnswerEvent();
    }
}
