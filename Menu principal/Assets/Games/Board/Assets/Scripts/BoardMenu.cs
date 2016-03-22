using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class BoardMenu : MonoBehaviour {

    public CharacterSelection chars;

    public GameObject playerNumberPanel;
    public GameObject charSelectPanel;
    public GameObject CurrenChoosingPlayerPanel;
    public GameObject confirmationButton, launchGameButton;

    public PanelAnimation charSelectAnim;
    public PanelAnimation playerNumberAnim;
    public PanelAnimation currChoosingPlayer;

    public static int[] playerSpritesNumber;

    public Text playerText;

    public static int currentChoosingPlayerNumber;
    

    private bool isSelectionFinished;

    void Start () {
        playerNumberPanel.SetActive(true);
        charSelectPanel.SetActive(false);
        CurrenChoosingPlayerPanel.SetActive(false);
        isSelectionFinished = false;

        confirmationButton.SetActive(true);
        launchGameButton.SetActive(false);

        currentChoosingPlayerNumber = 666; // We use this value in CharacterSelection.cs
	}

    public void setNbPlayers(int nbPlayers)
    {
        Coordinator.nbPlayer = nbPlayers;
        playerSpritesNumber = new int[nbPlayers];
        StartCoroutine(showCharSelect());
    }

    /*
     * Hide the menu used to chose the numer of players
     * Waits for the end of the animation
     * Show the Character select menu 
     */
    IEnumerator showCharSelect()
    {
        playerNumberAnim.hidePanel();
        yield return new WaitUntil(playerNumberAnim.isPanelNowHidden);
        charSelectPanel.SetActive(true);
        CurrenChoosingPlayerPanel.SetActive(true);

        currentChoosingPlayerNumber = 0;
        
    }

    public void lauchGame()
    {
        SceneManager.LoadScene("BoardMain");
    }

    // Update is called once per frame
    void Update() {
        if (currentChoosingPlayerNumber < Coordinator.nbPlayer)
            playerText.text = "Joueur n° " + (currentChoosingPlayerNumber + 1);

        if (currentChoosingPlayerNumber == Coordinator.nbPlayer)
        {
            isSelectionFinished = true;
            confirmationButton.SetActive(false);
            launchGameButton.SetActive(true);
        }

    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Create);

        CoordinatorSerializable data = new CoordinatorSerializable();

        data.nbPlayer = Coordinator.nbPlayer;
        data.nbBonus =  Coordinator.nbBonus;
        data.playerSpritesNumber = playerSpritesNumber;

        bf.Serialize(file, data);
        file.Close();
    }

    


    public  void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
            Debug.Log(Application.persistentDataPath);
            CoordinatorSerializable data = (CoordinatorSerializable)bf.Deserialize(file);
            Coordinator.nbPlayer = data.nbPlayer;
            Coordinator.nbBonus = data.nbBonus;
            for (int i = 0; i < data.nbPlayer; ++i)
            {
                Coordinator.playerSprites[i] = chars.characters[data.playerSpritesNumber[i]].GetComponent<SpriteRenderer>().sprite;
            }
            

            SceneManager.LoadScene("BoardMain");
        }
    }
	
}
[Serializable]
class CoordinatorSerializable
{
    public  int nbPlayer;
    public  int nbBonus;
    public  int[] playerSpritesNumber;


    
}