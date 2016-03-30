using UnityEngine;
using System.Collections;

public class Thierry5 : PNJ{

    private bool disappeared = true;
    private bool inClickEvent = false;
    private bool firstDialogDone = false;
    private bool secondDialogDone = false;
    private bool thirdDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;
    private bool arrowsPlaced = false;
    private bool hasAnsweredTrue = false;

    private bool noGlasses = false;

    public bool Disappeared
    {
        get { return disappeared; }
        set { disappeared = value; }
    }


    protected override void Update()
    {
        if (!disappeared)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            hasDialog = true;
        }
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

        if (clickEventDone && !secondDialogDone && IsEndDialog())
        {
            if (StoryGameManager.instance.IsPNJPresent("Victor"))
            {
                if (hasAnsweredTrue)
                {
                    if (noGlasses)
                    {
                        displayDialog(3, dialog.Length);
                    }
                    else
                    {
                        displayDialog(4, dialog.Length);
                    }
                }
                else
                {
                    displayDialog(4, dialog.Length);
                }
            }
            else
            {
                if (hasAnsweredTrue)
                {
                    if (noGlasses)
                    {
                        displayDialog(3, dialog.Length-2);
                    }
                    else
                    {
                        displayDialog(4, dialog.Length-2);
                    }
                }
                else
                {
                    displayDialog(4, dialog.Length);
                }
            }
            

            secondDialogDone = true;
        }

        if (secondDialogDone && IsEndDialog() && !arrowsPlaced)
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

    protected override void PNJLoadEvent()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;
        if (StoryGameManager.instance.HasObject("Lunettes"))
        {
            displayDialog(1, 3);
            StoryGameManager.instance.AddObject("Secret");
        }
        else
        {
            displayDialog(1, 2);
            noGlasses = true;
        }
        hasAnsweredTrue = true;
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        
        base.FalseAnswerEvent();
    }
}
