using UnityEngine;
using System.Collections;

public class gaby2 : PNJ {

    protected override void PNJClickEvent()
    {
        callPlaceArrows();
        base.PNJClickEvent();
    }

    protected override void PNJLoadEvent()
    {
        displayDialog(0, dialog.Length);
        base.PNJLoadEvent();
    }
}
