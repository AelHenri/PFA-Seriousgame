using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryGameManager : MonoBehaviour {

    public static StoryGameManager instance = null;
    private StorySceneManager scene;
    private List<string> PNJTable;
    private List<string> ObjectsTable;

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
        //PNJTable.Add("Gaby");

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
            Debug.Log("Coucou");
            return false;
        }
        return PNJTable.Contains(PNJName);
    }

    public bool HasObject(string ObjectName)
    {
        return ObjectsTable.Contains(ObjectName);
    }
}
