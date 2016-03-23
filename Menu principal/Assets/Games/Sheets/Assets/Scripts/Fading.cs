using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {
    public Texture2D fadeOutTexture;
    public float fadingSpeed = 0.8f;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    public int fadeDir = -1; // -1 = in, out = 1 
    public static bool loadedFromGame = false;

    void OnGUI()
    {

        GUI.depth = drawDepth;

        if (loadedFromGame)
        {
            alpha += fadeDir * fadingSpeed * Time.unscaledDeltaTime;
            alpha = Mathf.Clamp01(alpha);

            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
        }
        
    }


    public  float beginFade( int direction)
    {
        fadeDir = direction;
        return fadingSpeed;
    }
    /*
    void OnLevelWasLoaded()
    {
        beginFade(-1);
    }*/
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // To avoid having the fade in animation when coming from the "Question" scene
        if (alpha == 0.0f)
            loadedFromGame = false;
	}
}
