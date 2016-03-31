using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public class BoardMenu : MonoBehaviour {

    public CharacterSelection chars;
    public BoardSave boardSave;

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
        playerNumberPanel.SetActive(false);
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

        CoordinatorSerializable data = new CoordinatorSerializable(Coordinator.nbPlayer, Coordinator.nbBonus);
        //a faire en plus propre, genre peut etre avec un construteur

 
        data.playerSpritesNumber = playerSpritesNumber;

          
        for (int i = 0; i < data.nbPlayer; i++)
        {
            data.playerPos[i] = new Vector3Serializer(Coordinator.Players[i].transform.position);
           //data.playerCurrentStep[i] = Coordinator.Players[i].GetComponent<Move>().
            Debug.Log(Coordinator.Players[i].GetComponent<Move>().startPosition.Count);
            if (Coordinator.Players[i].GetComponent<Move>().startPosition.Count != 0)
            {
                for (int j = 0; j < Coordinator.Players[i].GetComponent<Move>().startPosition.Count; j++)
                    data.savedStartPosition[i].Add(new Vector3Serializer(Coordinator.Players[i].GetComponent<Move>().startPosition[j]));
            }

                for (int j = 0; j < Coordinator.Players[i].GetComponent<Move>().endPosition.Count; j++)
                    data.savedEndPosition[i].Add(new Vector3Serializer(Coordinator.Players[i].GetComponent<Move>().endPosition[j]));
            

        }
        

        bf.Serialize(file, data);
        file.Close();
    }

    


    public  void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
            //Debug.Log(Application.persistentDataPath);
            CoordinatorSerializable data = (CoordinatorSerializable)bf.Deserialize(file);
            Coordinator.nbPlayer = data.nbPlayer;
            Coordinator.nbBonus = data.nbBonus;

            boardSave.translate(data);
            Coordinator.isFromSavedGame = true;
            SceneManager.LoadScene("BoardMain");
        }
    }
	
}
[Serializable]
public class CoordinatorSerializable
{
    public int nbPlayer;
    public int nbBonus;
    public int[] playerSpritesNumber;
    public Vector3Serializer[] playerPos;
    public List<Vector3Serializer>[] savedStartPosition;
    public List<Vector3Serializer>[] savedEndPosition;
    public int[] playerCurrentStep;

    public CoordinatorSerializable(int nbPlayers, int nbBonus)
    {
        this.nbPlayer = nbPlayers;
        this.nbBonus = nbBonus;
        this.playerPos = new Vector3Serializer[nbPlayer];

        this.savedStartPosition = new List<Vector3Serializer>[nbPlayer];
        this.savedEndPosition = new List<Vector3Serializer>[nbPlayer];
        for (int i = 0; i < nbPlayer; i++)
        {
            savedStartPosition[i] = new List<Vector3Serializer>();
            savedEndPosition[i] = new List<Vector3Serializer>();
        }
    }


}


[Serializable]
public class Vector3Serializer
{
    public float x;
    public float y;
    public float z;

    public Vector3Serializer(Vector3 v3)
    {
        x = v3.x;
        y = v3.y;
        z = v3.z;
    }
    public Vector3 toVector3()
    {
        return new Vector3(x, y, z);
    } 
}