using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private float offset;
    public float speed = 29.0f;
    public GUIStyle style;
    public Rect viewArea;
    public AudioSource erwan;
    private bool soundPlayed = false;
    public Texture2D background;

    private void Start()
    {
        if (this.viewArea.width == 0.0f)
        {
            this.viewArea = new Rect(transform.GetComponent<RectTransform>().anchorMin.x * Screen.width,
                                     transform.GetComponent<RectTransform>().anchorMin.y * Screen.height,
                                     (transform.GetComponent<RectTransform>().anchorMax.x - transform.GetComponent<RectTransform>().anchorMin.x) * Screen.width,
                                     (transform.GetComponent<RectTransform>().anchorMax.y - transform.GetComponent<RectTransform>().anchorMin.y) * Screen.height);
        }
        this.offset = this.viewArea.height;
        
    }

    private void Update()
    {
        this.offset -= Time.unscaledDeltaTime * this.speed;
        Debug.Log("Credits: Offset =  " + offset);
        /*if (offset < -200 && !soundPlayed)
        {
            erwan.Play();
            soundPlayed = true;
            
        }*/
        if(offset < (-1)* (transform.GetComponent<RectTransform>().anchorMax.y - transform.GetComponent<RectTransform>().anchorMin.y) * Screen.height || Input.anyKeyDown)
        {
            SceneManager.UnloadScene("Credits");
        }
     }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
        GUI.BeginGroup(this.viewArea);
        GUI.depth = -100;
        var position = new Rect(0, this.offset, this.viewArea.width, this.viewArea.height);
        var text = @"PFA - Serious Game

Une idée originale de
    Borel Hugo,
    Fichant Le Fur Florian,
    Grelet Léo, 
    Le Moigne Yves,
    Montaigu Thomas,
    Rakotosaona Lalatiana Marie-Julie,
    Toussaint Henri

Jeu narratif:
    Voix:
        Fichant Le Fur Florian,
        Grelet Léo,
        Toussaint Henri

    Réalisation & Design:
        Toussaint Henri

Jeu de labyrinthe:
    Réalisation:
        Le Moigne Yves,
        Rakotosaona Lalatiana Marie-Julie

    Design:
        Le Moigne Yves

Jeu de plateau:
    Réalisation & Design:
        Borel Hugo

Menus & Profils & QCM:
    Réalisation:
        Fichant Le Fur Florian,
        Montaigu Thomas

Editeur de fiches:
    Réalisation:
        Montaigu Thomas

Design général:
        Le Moigne Yves

Musique:
        Grelet Léo
        Celestial Aeon Project
        F.T.G.
        Music For Your Media

Avec les conseils avisés de M. Raphaël Marczak,
et Mme Myriam Desainte-Catherine,


Merci et bon jeu!

";

        GUI.Label(position, text, this.style);

        GUI.EndGroup();
        
    }
}