using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Navigator : MonoBehaviour {
    public Games[] games;// Stores playable games
    public uint gamesNb;// Number of games in the navigator
    public uint currentGame;



    void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);//Has to remain between scenes
        games = new Games[gamesNb];
 
        
    }

    // Use this for initialization
    void Start()
    //Game loading?
    {
        GameState.gameCurrentlyLoaded = 0;
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Additive);

    }




	
	void Update () {
        //Verifier les input du joueur
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("menu loaded: " + GameState.pauseMenuLoaded);
            if (GameState.pauseMenuLoaded == 0)
            {
                SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
                GameState.pauseMenuLoaded = 1;
            }
        }

    }


}
