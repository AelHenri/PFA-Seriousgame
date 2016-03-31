using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Navigator : MonoBehaviour {
    public List<GameObject> collectibles;
    public List<GameObject> collectiblesLeftToFind;

    void Awake ()
    {
        DontDestroyOnLoad(transform.gameObject);//Has to remain between scenes
 
        
    }

    // Use this for initialization
    void Start()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Additive);

    }


	
	void Update () {
        //Player input
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
#endif

    }
   void pauseGame()
    {
        Debug.Log("menu loaded: " + GameState.pauseMenuLoaded);
        if (GameState.pauseMenuLoaded == 0)
        {
            SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
            GameState.pauseMenuLoaded = 1;
        }
    }
   void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
            pauseGame();    
    }

}
