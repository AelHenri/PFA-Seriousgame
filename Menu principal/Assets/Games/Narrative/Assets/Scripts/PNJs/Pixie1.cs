using UnityEngine;
using System.Collections;

public class Pixie1 : PNJ
{

    protected override void PNJClickEvent()
    {
        //callPlaceArrows();
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        //displayDialog();
        //callPlaceArrows();
        base.PNJLoadEvent();
    }
}
