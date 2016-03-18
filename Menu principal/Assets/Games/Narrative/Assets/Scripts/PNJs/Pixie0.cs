using UnityEngine;
using System.Collections;

public class Pixie0 : PNJ
{
    private bool loadEventDone = false;
    private bool arrowsPlaced = false;

    protected override void Update()
    {
        base.Update();
        if (loadEventDone && IsEndDialog() && !arrowsPlaced)
        {
            callPlaceArrows();
            arrowsPlaced = true;
        }
    }

    protected override void PNJClickEvent()
    {
        //callPlaceArrows();
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        displayDialog(0, dialog.Length);
        base.PNJLoadEvent();
        loadEventDone = true;
    }
}
