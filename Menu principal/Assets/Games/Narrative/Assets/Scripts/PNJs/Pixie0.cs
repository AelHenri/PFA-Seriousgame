using UnityEngine;
using System.Collections;

public class Pixie0 : PNJ
{

    protected override void PNJClickEvent()
    {
        //callPlaceArrows();
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        for (int i=0; i < dialog.Length; i++)
        {
            displayDialog(i);
        }        
        callPlaceArrows();
        base.PNJLoadEvent();
    }
}
