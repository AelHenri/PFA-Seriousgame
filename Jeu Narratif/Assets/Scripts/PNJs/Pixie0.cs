using UnityEngine;
using System.Collections;

public class Pixie0 : PNJ {

<<<<<<< HEAD
    protected override void PNJClickEvent()
    {
        //callPlaceArrows();
        base.PNJClickEvent(); 
    }

    protected override void PNJLoadEvent()
    {
        callPlaceArrows();
        base.PNJLoadEvent();
=======
    protected override void LaunchPNJEvent()
    {
        base.LaunchPNJEvent();
        callPlaceArrows();        
>>>>>>> origin/master
    }
}
