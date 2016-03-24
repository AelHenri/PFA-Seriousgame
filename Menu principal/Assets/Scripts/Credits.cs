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
        Debug.Log("Offset: " + offset);
        if (offset < -200 && !soundPlayed) // Screen.height)
        {
            erwan.Play();
            soundPlayed = true;
            
        }
        if(offset < -300 || Input.anyKeyDown)
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


Réalisation, Musique, Histoire, Graphisme, Voix:

Borel Hugo,
Fichant Le Fur Florian,
Grelet Léo, 
Le Moigne Yves,
Montaigu Thomas,
Rakotosaona Lalatiana,
Toussaint Henri




(et un peu Internet)



";

        GUI.Label(position, text, this.style);

        GUI.EndGroup();
        
    }
}