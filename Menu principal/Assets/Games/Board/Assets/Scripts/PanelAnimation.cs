using UnityEngine;
using System.Collections;

public class PanelAnimation : MonoBehaviour {

    Animator anim;
    bool showPanel;

	void Start () {
        anim = GetComponent<Animator>();
	
	}
	
    public void hidePanel()
    {
        anim.SetTrigger("Hide Panel");
    }

     public bool isPanelNowHidden()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Panel is now hidden"))
            return true;
        else
            return false; 
    }

	void Update () {
	
	}
}
