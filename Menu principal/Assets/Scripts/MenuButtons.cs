using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
    public uint navigatorScreen;

    void Awake()
    {
        Time.timeScale = 0;
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
                Debug.Log("Selection de reprendre la partie dans le menu");
                GameState.pauseMenuLoaded = false;
                Time.timeScale = 1;
                SceneManager.UnloadScene("PauseMenu");
                break;
            case 1: //Options
                Debug.Log("Selection de options dans le menu");
                GameState.pauseMenuLoaded = false;
                GlobalQuestionnaire.startQuestionnaire();
                GameState.titleScreenOnlyLoaded = false;
                SceneManager.UnloadScene("PauseMenu");
                break;
            case 2: //Recommencer
                Debug.Log("Selection de recommencer dans le menu");
                break;
            case 3: //Quitter la partie
                Debug.Log("Selection de quitter la partie dans le menu");
                if (GameState.titleScreenOnlyLoaded == false)
                {
                    GameState.pauseMenuLoaded = false;
                    SceneManager.LoadSceneAsync((int)navigatorScreen);
                    GameState.titleScreenOnlyLoaded = true;
                    Time.timeScale = 1;
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
