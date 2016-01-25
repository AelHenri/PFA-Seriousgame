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
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Detects keycode escape");
            SceneManager.LoadScene(1);
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
