using UnityEngine;
using System.Collections;

public class StoryGameManager : MonoBehaviour {

    public static StoryGameManager instance = null;
    public StorySceneManager scene;
    private bool doingSetup = false;
    private int cpt = 0;

	// Use this for initialization
	void Awake () {

        if (instance == null)        
            instance = this;        
        else if (instance != this)        
            Destroy(gameObject);
        

        DontDestroyOnLoad(gameObject);
        scene = GetComponent<StorySceneManager>();

        InitGame();
	
	}

    private void OnLevelWasLoaded(int index)
    {
        if (!doingSetup)
        {
            scene.level++;
            InitGame();
        }
    }

    void InitGame()
    {
        doingSetup = true;
        scene.SetupScene();
    }
	
	// Update is called once per frame
	void Update () {
        if (doingSetup)
        {
            cpt++;
            if (cpt > 60)
            {
                doingSetup = false;
                cpt = 0;
            }
        }

	
	}
}
