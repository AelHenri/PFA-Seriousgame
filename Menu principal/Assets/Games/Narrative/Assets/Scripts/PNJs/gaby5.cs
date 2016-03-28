using UnityEngine;
using System.Collections;

public class gaby5 : PNJ{

    private Thierry5 thierry;

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

        if (clickEventDone && !hasAnsweredTrue && IsEndDialog() && !arrowsPlaced)
        {
            callPlaceArrows();
            arrowsPlaced = true;
        }

    }

    protected override void PNJClickEvent()
    {
        inClickEvent = true;
        displayDialog(0, 2);
        base.PNJClickEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;
        thierry = (Thierry5)FindObjectOfType(typeof(Thierry5));
        thierry.Disappeared = false;
        hasAnsweredTrue = true;
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        if (StoryGameManager.instance.IsPNJPresent("Victor"))
        {
            displayDialog(2, dialog.Length);
        }
        else
        {
            displayDialog(2, 5);
        }
        base.RightAnswerEvent();
    }
}
