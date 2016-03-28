using UnityEngine;
using System.Collections;

public class gaby4 : PNJ{

    protected override void PNJLoadEvent()
    {
        if (StoryGameManager.instance.IsPNJPresent("Camille"))
        {
            displayDialog(0, dialog.Length);
        }
        else
        {
            displayDialog(0, 1);
        }
        base.PNJLoadEvent();
    }
}
