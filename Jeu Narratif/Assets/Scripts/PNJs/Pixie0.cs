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
        callPlaceArrows();
        base.PNJLoadEvent();
    }
}
