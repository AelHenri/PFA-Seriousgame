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
    public float characterDelay = 0.5f;
    public int numberOfQuestions = 1;
    public float fadingTime = 0.5f;

    public CharacterElements[] characters;
    private Dictionary<string, Texture> characterDic;
    private bool displayPortrait = false;
    private Texture currentPortrait;

    private Fading fader;

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

    private Text endText;
    private Text theEnd;
    private GameObject endImage;
    private GameObject theEndHolder;
    private bool waitingForEnd = false;
    private bool waitingForInput = false;

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

        fader = GameObject.Find("Navigator").GetComponent<Fading>();

        PNJTable = new List<string>();
        ObjectsTable = new List<string>();
        PNJTable.Add("Pixie");
        PNJTable.Add("Gaby");
        //PNJTable.Add("Philibert");
        //PNJTable.Add("Camille");
        //PNJTable.Add("Victor");
        //PNJTable.Add("Olivier-happy");

        //ObjectsTable.Add("Lunettes");
        //ObjectsTable.Add("Seve");
        //ObjectsTable.Add("Secret");
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

        endImage = GameObject.Find("EndBox");
        endText = GameObject.Find("EndText").GetComponent<Text>();
        theEndHolder = GameObject.Find("TheEnd");
        theEnd = theEndHolder.GetComponent<Text>();

        endText.text = "coucou";
        endImage.SetActive(false);
        theEndHolder.SetActive(false);
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

        if (waitingForEnd && !messageBoxEnabled)
        {

            if (Input.anyKeyDown)
            {
                GameState.quitNarrative();

            }
        }

        /*if (waitingForInput)
        {
            if (Input.GetMouseButton(0))
            {
                GameState.quitNarrative();
            }
        }*/

        if (messageBoxEnabled)
        {
            
                if (messageFinished)
                {
                    StopAllCoroutines();
                    displayPortrait = false;
                    StoryPlayer.paralyzed = false;
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
        StoryPlayer.paralyzed = true;
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
            AudioClip clip = d.audioLine;
            Texture portrait = characterDic[d.characterName];
            //GUILayout.BeginArea(new Rect(-6f, 6f, 2f, 2f));
            //GUI.DrawTexture(new Rect(-6f, 6f, 2f, 2f), portrait);
            displayPortrait = true;
            currentPortrait = portrait;
            messageBoxText.text = "";
            if (clip != null)
            {
                SoundManager.instance.PlaySingle(clip);
            }
            foreach (char c in s)
            {
                yield return new WaitForSeconds(characterDelay);
                messageBoxText.text += c;
                
            }
            yield return StartCoroutine(WaitForKeyDown());
        }
        
        messageFinished = true;
    }
    

    void OnGUI()
    {
        if (displayPortrait)
        {
            GUI.DrawTexture(new Rect(Screen.width/20, 2.8f*Screen.height/4, 75f, 75f), currentPortrait, ScaleMode.ScaleToFit);
        }
    }

    IEnumerator WaitForKeyDown()
    {
        while (!Input.anyKeyDown)
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

    private IEnumerator TransitionCoroutine()
    {
        fadingTime = fader.beginFade(1);
        yield return StartCoroutine(Questionnaire.CoroutineUtil.WaitForRealSeconds(fadingTime));
        Application.LoadLevel(Application.loadedLevel);
        fader.beginFade(-1);
    }

    public void NextLevel()
    {
        StartCoroutine(TransitionCoroutine());
    }

    public void EndGame()
    {
        StoryPlayer.paralyzed = true;

        theEnd.text = "The End";
        endText.text = "Tu as fini le jeu !";

        endImage.SetActive(true);
        theEndHolder.SetActive(true);

        waitingForEnd = true;

        //GameState.quitNarrative();
    }
}
