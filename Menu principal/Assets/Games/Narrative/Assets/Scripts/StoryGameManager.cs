using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CharacterElements
{
    public string characterName;
    public Texture characterPortrait;
}

public class StoryGameManager : MonoBehaviour {

    public static StoryGameManager instance = null;
    public float characterDelay = 0.02f;

    public CharacterElements[] characters;
    private Dictionary<string, Texture> characterDic;
    private bool displayPortrait = false;
    private Texture currentPortrait;

    private StorySceneManager scene;
    private List<string> PNJTable;
    private List<string> ObjectsTable;
    private bool firstScene = false;
    private int cpt = 0;

    private bool messageBoxEnabled = false;
    private bool messageFinished = false;
    private GameObject messageBox;
    private Text messageBoxText;
    private string[] message;
    private DialogElements[] dialog;

    private StoryPlayer player;

    // Use this for initialization
    void Awake () {

        if (instance == null)        
            instance = this;        
        else if (instance != this)        
            Destroy(gameObject);
        

        DontDestroyOnLoad(gameObject);
        scene = GetComponent<StorySceneManager>();
        player = (StoryPlayer)FindObjectOfType(typeof(StoryPlayer));
        InitCharacterDic();

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
            
                if (messageFinished)
                {
                    StopAllCoroutines();
                    displayPortrait = false;
                    player.DefreezePlayer();
                    messageBox.SetActive(false);
                    //Interact.EnableControl();
                    messageBoxEnabled = false;
                    //messageFinished = true;
                }
               
        }
    }

    void InitCharacterDic()
    {
        characterDic = new Dictionary<string, Texture>();
        foreach (CharacterElements c in characters)
        {
            characterDic[c.characterName] = c.characterPortrait;
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
        player.FreezePlayer();
        messageBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeMessage());
        //Interact.DisableControl();
        messageBoxEnabled = true;
    }

    IEnumerator TypeMessage()
    {
        messageFinished = false;
        foreach (DialogElements d in dialog)
        {
            string s = d.dialogLine;

            Texture portrait = characterDic[d.characterName];
            //GUILayout.BeginArea(new Rect(-6f, 6f, 2f, 2f));
            //GUI.DrawTexture(new Rect(-6f, 6f, 2f, 2f), portrait);
            displayPortrait = true;
            currentPortrait = portrait;
            messageBoxText.text = "";
            foreach (char c in s)
            {
                yield return new WaitForSeconds(characterDelay);
                messageBoxText.text += c;
                if (messageBox.GetComponent<AudioSource>() != null)
                {
                    messageBox.GetComponent<AudioSource>().Play();
                }
            }
            yield return StartCoroutine(WaitForKeyDown());
        }
        
        messageFinished = true;
    }
    

    void OnGUI()
    {
        if (displayPortrait)
        {
            GUI.DrawTexture(new Rect(300f, 520f, 75f, 75f), currentPortrait, ScaleMode.ScaleToFit);
        }
    }

    IEnumerator WaitForKeyDown()
    {
        while (!Input.GetMouseButton(0))
            yield return null;
    }

    public void GetMessage(DialogElements[] newMessage)
    {
        dialog = newMessage;
    }

    public bool isMessagesFinished()
    {
        return messageFinished;
    }
}
