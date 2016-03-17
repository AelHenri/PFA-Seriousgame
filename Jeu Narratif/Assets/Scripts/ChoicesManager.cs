using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChoicesManager : MonoBehaviour {

    public static ChoicesManager instance = null;
    private List<string> PNJTable;
    private List<string> ObjectsTable;
    //private Transform storyHolder;

    void Awake()
    {
        /*if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);*/

        DontDestroyOnLoad(gameObject);

        PNJTable = new List<string>();
        ObjectsTable = new List<string>();
        //storyHolder = new GameObject("Story").transform;
        BeginStory();

    }
    
    public void Coucou()
    {
        Debug.Log("coucou");
    }

    private void BeginStory()
    {
        PNJTable.Add("Pixie");
        PNJTable.Add("Gaby");
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
        return PNJTable.Contains(PNJName);
    }

    public bool HasObject(string ObjectName)
    {
        return ObjectsTable.Contains(ObjectName);
    }

}
