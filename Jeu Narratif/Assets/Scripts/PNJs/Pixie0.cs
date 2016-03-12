using UnityEngine;
using System.Collections;

public class Pixie0 : PNJ {

    protected override void LauchPNJEvent()
    {
        base.LauchPNJEvent();
        Debug.Log("hello");
        callPlaceArrows();        
    }
}
