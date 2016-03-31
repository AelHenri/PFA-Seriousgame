using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionsMenuButtons : MonoBehaviour
{
    private int previousPauseMenuLoaded;
    void Awake()
    {
        //GameState.freezeTime();
        previousPauseMenuLoaded = GameState.pauseMenuLoaded;
        GameState.pauseMenuLoaded = 2;
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape) && GameState.pauseMenuLoaded == 2)
        {
            LoadOnClick(3);//Reprendre la partie
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.GetKeyDown(KeyCode.Menu) && GameState.pauseMenuLoaded == 2)
        {
            LoadOnClick(3);//Reprendre la partie
        }
#endif
    }

    public void LoadOnClick(int optionsNb)
    {
        switch (optionsNb)
        {
           
            case 0: //Changer le volume de la musique
                Debug.Log("Sélection de changer la musique dans le menu");
                break;
            case 1: //Changer le volume des voix
                Debug.Log("Sélection de changer la musique voix dans le menu");
                break;
            case 2: //Crédits
                Debug.Log("Sélection de crédits dans le menu options");
                SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
                break;
            case 3: //Quitter les options
                Debug.Log("Sélection de quitter les options dans le menu options");
                GameState.pauseMenuLoaded = previousPauseMenuLoaded;
                if (GameState.isTimeFrozen)
                    {
                    //In-game options menu
                    
                    SceneManager.UnloadScene("OptionsMenu");//Unload the scene
                    }
                    else
                    {
                    //Not in-game options menu
                    SceneManager.LoadScene("TitleScreen");
                    }
                break;
        }
    }
}
