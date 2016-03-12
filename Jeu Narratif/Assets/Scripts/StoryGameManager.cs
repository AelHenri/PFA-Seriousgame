using UnityEngine;
using System.Collections;

public class StoryGameManager : MonoBehaviour {

    public static StoryGameManager instance = null;
    public SceneManager scene;

	// Use this for initialization
	void Awake () {

        if (instance == null)        
            instance = this;        
        else if (instance != this)        
            Destroy(gameObject);
        

        DontDestroyOnLoad(gameObject);
        scene = GetComponent<SceneManager>();

        InitGame();
	
	}

    private void OnLevelWasLoaded(int index)
    {
        scene.level++;
        InitGame();

    }

    void InitGame()
    {
        scene.SetupScene();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
