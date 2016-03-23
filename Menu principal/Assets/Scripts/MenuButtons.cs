using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    void Awake()
    {
        GameState.freezeTime();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadOnClick(0);//Reprendre la partie
        }
    }

	public void LoadOnClick(int optionsNb)
    {
        switch (optionsNb)
        {
            case 0: //Reprendre la partie
                Debug.Log("Sélection de reprendre la partie dans le menu");
                GameState.pauseMenuLoaded = false;
                GameState.unfreezeTime();
                SceneManager.UnloadScene("PauseMenu");
                break;
            case 1: //Options
                Debug.Log("Sélection de options dans le menu");
                //GameState.pauseMenuLoaded = false;
                //GlobalQuestionnaire.startQuestionnaire();
                //GameState.titleScreenOnlyLoaded = false;
                //SceneManager.UnloadScene("PauseMenu");
                SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
                break;
            case 2: //Recommencer
                Debug.Log("Sélection de recommencer dans le menu");
                break;
            case 3: //Quitter la partie
                Debug.Log("Sélection de quitter la partie dans le menu");
                if (GameState.titleScreenOnlyLoaded == false)
                {
                    GameState.pauseMenuLoaded = false;
                    SceneManager.LoadSceneAsync("TitleScreen");
                    GameState.titleScreenOnlyLoaded = true;
                    GameState.unfreezeTime();

                    GameState.quitNarrative();
                    GameState.quitLabyrinth();
                    
                        
                    GameState.gameCurrentlyLoaded = 0;
                }   
                else
                {
                    //quit game
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                    Application.Quit();
                }
                break;
        }
    }
}
