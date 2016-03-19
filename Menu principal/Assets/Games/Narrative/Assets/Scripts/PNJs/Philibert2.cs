using UnityEngine;
using System.Collections;

public class Philibert2 : PNJ
{

    private bool inClickEvent = false;
    private bool firstDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;
    private bool arrowsPlaced = false;
    private bool hasAnsweredTrue = false;
    private bool secondDialogDone = false;
    private bool thirdDialogDone = false;
    private bool fourthDialogDone = false;

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

        if (clickEventDone && IsEndDialog() && !secondDialogDone)
        {
            displayDialog(4, 5);
            secondDialogDone = true;
        }

        if (secondDialogDone && IsEndDialog() && !thirdDialogDone && StoryGameManager.instance.IsPNJPresent("Camille"))
        {
            displayDialog(5, 6);
            thirdDialogDone = true;
        }
        else if (secondDialogDone && IsEndDialog() && !thirdDialogDone && !StoryGameManager.instance.IsPNJPresent("Camille"))
        {
            thirdDialogDone = true;
        }

        if (thirdDialogDone &&  IsEndDialog() && !fourthDialogDone)
        {
            displayDialog(6, 7);
            fourthDialogDone = true;
        }

        if (fourthDialogDone && IsEndDialog() && !arrowsPlaced)
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

    protected override void PNJLoadEvent()
    {
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(2, 3);
        StoryGameManager.instance.AddObject("Lunettes");
        hasAnsweredTrue = true;
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(3, 4);
        base.RightAnswerEvent();
    }
}
