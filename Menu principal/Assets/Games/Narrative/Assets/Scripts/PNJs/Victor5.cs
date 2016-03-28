using UnityEngine;
using System.Collections;

public class Victor5 : PNJ{

    private bool inClickEvent = false;
    private bool firstDialogDone = false;
    private bool questionAsked = false;
    private bool clickEventDone = false;

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
    }

    protected override void PNJClickEvent()
    {
        inClickEvent = true;
        displayDialog(1, 2);
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        displayDialog(0, 1);
        base.PNJLoadEvent();
    }

    protected override void RightAnswerEvent()
    {
        clickEventDone = true;

        displayDialog(2, 3);
        StoryGameManager.instance.AddObject("Seve");
        
        base.RightAnswerEvent();
    }

    protected override void FalseAnswerEvent()
    {
        clickEventDone = true;
        displayDialog(3, 4);
        base.FalseAnswerEvent();
    }
}
