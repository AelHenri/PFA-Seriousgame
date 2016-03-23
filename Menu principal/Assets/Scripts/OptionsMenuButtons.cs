using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionsMenuButtons : MonoBehaviour
{ 
    void Awake()
    {
        //GameState.freezeTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadOnClick(-1);//Reprendre la partie
        }
    }

    public void LoadOnClick(int optionsNb)
    {
        switch (optionsNb)
        {
            case -1: //Appui sur échap
                Debug.Log("Sélection de reprendre la partie dans le menu options");
                SceneManager.UnloadScene("OptionsMenu");
                break;
            case 0: //Changer le volume de la musique
                Debug.Log("Sélection de changer la musique dans le menu");
                //GameState.pauseMenuLoaded = false;
                //GameState.unfreezeTime();
                //SceneManager.UnloadScene("OptionsMenu");
                break;
            case 1: //Changer le volume des voix
                Debug.Log("Sélection de options dans le menu");
                /*GameState.pauseMenuLoaded = false;
                GlobalQuestionnaire.startQuestionnaire();
                GameState.titleScreenOnlyLoaded = false;
                SceneManager.UnloadScene("PauseMenu");*/
                break;
            case 2: //Crédits
                Debug.Log("Sélection de crédits dans le menu options");
                break;
            case 3: //Quitter les options
                Debug.Log("Sélection de quitter les options dans le menu options");
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
