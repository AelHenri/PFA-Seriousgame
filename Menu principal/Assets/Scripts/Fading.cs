using UnityEngine;
using System.Collections;
using System;
public class Fading : MonoBehaviour {
    public Texture2D fadeOutTexture;
    public float fadingSpeed = 0.8f;

    private int drawDepth = -1000;
    private float alpha = 0.0f;
    private int fadeDir = -1; // -1 = in, out = 1 


    void OnGUI()
    {
        GUI.depth = drawDepth;

        alpha += fadeDir * fadingSpeed * Time.unscaledDeltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }


    public  float beginFade( int direction)
    {
        fadeDir = direction;
        return fadingSpeed;
    }



}
