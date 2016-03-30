using UnityEngine;
using System.Collections;

public class Pixie7 : PNJ{

    private bool discoveryDialogDone = false;
    private bool pixieBeginningDialogDone = false;
    private bool pixieQuestion = false;

    protected override void Update()
    {
        base.Update();

        if (IsEndDialog() && discoveryDialogDone && !pixieBeginningDialogDone)
        {
            displayDialog(2, 4);
            pixieBeginningDialogDone = true;
        }

        if (IsEndDialog() && pixieBeginningDialogDone  && !pixieQuestion)
        {
            if (StoryGameManager.instance.HasObject("Secret"))
            {
                Question();
            }
            else
            {
                displayDialog(4, 5);
            }
            pixieQuestion = true;
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
}
