using UnityEngine;
using System.Collections;

public class Camille2 : PNJ
{

    protected override void PNJClickEvent()
    {
        Question();
        callPlaceTPs();
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        //displayDialog();
        //callPlaceArrows();
        base.PNJLoadEvent();
    }
}
