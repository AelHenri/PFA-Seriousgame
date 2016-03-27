using UnityEngine;
using System.Collections;

public class gaby3 : PNJ{

    private bool firstDialogDone = false;

    protected override void Update()
    {
        base.Update();
        if (!firstDialogDone && IsEndDialog() && StoryGameManager.instance.IsPNJPresent("Camille"))
        {
            displayDialog(1, 2);
            firstDialogDone = true;
        }
    }

    protected override void PNJLoadEvent()
    {
        displayDialog(0, 1);
        base.PNJLoadEvent();
    }
}
