using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryGameManager : MonoBehaviour {

    public static StoryGameManager instance = null;
    private StorySceneManager scene;
    private List<string> PNJTable;
    private List<string> ObjectsTable;
    private bool firstScene = false;
    private int cpt = 0;

    // Use this for initialization
    void Awake () {

        if (instance == null)        
            instance = this;        
        else if (instance != this)        
            Destroy(gameObject);
        

        DontDestroyOnLoad(gameObject);
        scene = GetComponent<StorySceneManager>();

        PNJTable = new List<string>();
        ObjectsTable = new List<string>();
        PNJTable.Add("Pixie");
        PNJTable.Add("Gaby");
        PNJTable.Add("Philibert");

        InitGame();
	
	}

    private void OnLevelWasLoaded(int index)
    {
        if (!firstScene)
        {
            scene.level++;
            InitGame();
        }
    }

    void InitGame()
    {
        firstScene = true;
        scene.SetupScene();
    }
	
	// Update is called once per frame
	void Update () {
	    if (firstScene)
        {
            cpt++;
            if(cpt>60)
            {
                firstScene = false;
                cpt = 0;
            }
        }
	}

    public void AddPNJ(string PNJName)
    {
        PNJTable.Add(PNJName);
    }

    public void AddObject(string ObjectName)
    {
        ObjectsTable.Add(ObjectName);
    }

    public bool IsPNJPresent(string PNJName)
    {
        if (PNJTable == null)
        {
            return false;
        }
        return PNJTable.Contains(PNJName);
    }

    public bool HasObject(string ObjectName)
    {
        return ObjectsTable.Contains(ObjectName);
    }
}
