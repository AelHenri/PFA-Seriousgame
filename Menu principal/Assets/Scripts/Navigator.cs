using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour {
    public Games[] games;// Stores playable games
    public uint gamesNb;// Number of games in the navigator
    public uint currentGame;



    void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);
        games = new Games[gamesNb];
        
    }

    // Use this for initialization
    void Start()
    //Game loading?
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Additive);
        Debug.Log("Loading complete");
    }




	
	void Update () {
        //notdestroyed on other scenes
        //Afficher ecran titre
        //Attendre la sélection d'une option ou d'un jeu
        //Coroutiner le jeu lancé
        /*while (!myBoolTrue)
        {
            yield return null;
        }*/
        //Verifier les input du joueur
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Detects keycode escape");
            Debug.Log("menu loaded: " + GameState.pauseMenuLoaded);
            if (!GameState.pauseMenuLoaded)
            {
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
                GameState.pauseMenuLoaded = true;
            }
        }
        //Verifier les achievements du joueur
        //Verifier les statistiques du joueur

    }

    void display_options()
    {

    }

    void display_menu()
    {

    }

    void display_game(uint selected)
    {

        games[selected].Information();
    }

    void load_sheets()
    {
        //foreach (string file in System.IO.Directory.GetFiles(path))
        //{ }
        //Sheet loading and decrypting
    }
}
