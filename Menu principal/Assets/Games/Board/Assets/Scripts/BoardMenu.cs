﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardMenu : MonoBehaviour {



    public GameObject playerNumberPanel;
    public GameObject charSelectPanel;
    public GameObject CurrenChoosingPlayerPanel;
    public GameObject confirmationButton, launchGameButton;

    public PanelAnimation charSelectAnim;
    public PanelAnimation playerNumberAnim;
    public PanelAnimation currChoosingPlayer;



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
    void Update () {
        if (currentChoosingPlayerNumber < Coordinator.nbPlayer)
            playerText.text = "Joueur n° " + (currentChoosingPlayerNumber+1);

        if (currentChoosingPlayerNumber == Coordinator.nbPlayer)
        {
            isSelectionFinished = true;
            confirmationButton.SetActive(false);
            launchGameButton.SetActive(true);
        }

        
	}
}
