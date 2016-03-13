using UnityEngine;
using System.Collections;

public class gaby1 : PNJ {

    protected override void PNJClickEvent()
    {
        callPlaceArrows();
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        base.PNJLoadEvent();
    }
}
