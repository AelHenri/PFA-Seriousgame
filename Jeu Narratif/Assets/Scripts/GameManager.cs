using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
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

    void InitGame()
    {
        scene.SetupScene();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
