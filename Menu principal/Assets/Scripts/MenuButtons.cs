﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
    public GameObject scene;
    public uint navigatorScreen;

    void Awake()
    {
        scene = transform.parent.gameObject;//Canvas parent du "Menu Canvas"
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadOnClick(0);//Reprendre la partie
        }
    }

	public void LoadOnClick(int optionsNb)
    {
        switch (optionsNb)
        {
            case 0: //Reprendre la partie
                Debug.Log("Selection de reprendre la partie dans le menu");
                GameState.pauseMenuLoaded = false;
                Destroy(scene);
                break;
            case 1: //Options
                Debug.Log("Selection de options dans le menu");
                break;
            case 2: //Recommencer
                Debug.Log("Selection de recommencer dans le menu");
                break;
            case 3: //Quitter la partie
                Debug.Log("Selection de quitter la partie dans le menu");
                if (GameState.titleScreenOnlyLoaded == false)
                {
                    GameState.pauseMenuLoaded = false;
                    SceneManager.LoadSceneAsync((int)navigatorScreen);
                    GameState.titleScreenOnlyLoaded = true;
                }
                else
                {
                    //quit game
                    #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                    #endif
                    Application.Quit();
                }
                break;
        }
    }
}