using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoryGameManager : MonoBehaviour {

    public static StoryGameManager instance = null;
    public float characterDelay = 0.02f;

    private StorySceneManager scene;
    private List<string> PNJTable;
    private List<string> ObjectsTable;
    private bool firstScene = false;
    private int cpt = 0;

    private bool messageBoxEnabled = false;
    private GameObject messageBox;
    private Text messageBoxText;
    private string message;

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
        messageBox = GameObject.Find("MessageBox");
        messageBoxText = GameObject.Find("MessageText").GetComponent<Text>();
        messageBox.SetActive(false);
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

        if (messageBoxEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StopAllCoroutines();
                messageBox.SetActive(false);
                //Interact.EnableControl();
                messageBoxEnabled = false;
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



    public void InteractEvent()
    {
        messageBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeMessage());
        //Interact.DisableControl();
        messageBoxEnabled = true;
    }

    IEnumerator TypeMessage()
    {
        messageBoxText.text = "";

        foreach (char c in message)
        {
            yield return new WaitForSeconds(characterDelay);
            messageBoxText.text += c;
            if (messageBox.GetComponent<AudioSource>() != null)
            {
                messageBox.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void GetMessage(string newMessage)
    {
        message = newMessage;
    }
}
