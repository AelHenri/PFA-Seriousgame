using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
    ProfileManager profileManager;
    void Awake()
    {
        GameState.freezeTime();
    }

    void Start()
    {
        profileManager = GameObject.Find("Navigator").GetComponent<ProfileManager>();
    }

    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape) && GameState.pauseMenuLoaded == 1)
        {
            LoadOnClick(0);//Reprendre la partie
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.GetKeyDown(KeyCode.Escape) && GameState.pauseMenuLoaded == 1)
        {
            LoadOnClick(0);//Reprendre la partie
        }
#endif

    }

    public void LoadOnClick(int optionsNb)
    {
        switch (optionsNb)
        {
            case 0: //Reprendre la partie
                Debug.Log("Sélection de reprendre la partie dans le menu");
                GameState.pauseMenuLoaded = 0;
                GameState.unfreezeTime();
                SceneManager.UnloadScene("PauseMenu");
                break;
            case 1: //Options
                Debug.Log("Sélection de options dans le menu");
                SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Additive);
                break;
            case 2: //Recommencer
                Debug.Log("Sélection de recommencer dans le menu");
                break;
            case 3: //Quitter la partie
                Debug.Log("Sélection de quitter la partie dans le menu");
                if (GameState.titleScreenOnlyLoaded == false)
                {
                    GameState.pauseMenuLoaded = 0;
                    GameState.titleScreenOnlyLoaded = true;
                    GameState.unfreezeTime();
                    GameState.unfreezeTime();//Unfreeze again in case of a pending question
                    GameState.quitNarrative();
                    GameState.quitLabyrinth();
                    GameState.quitBoard();
                    profileManager.saveExistingProfile(profileManager.getCurrentProfile());
                        
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
