using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LoadOnClick : MonoBehaviour
{
    public GameObject loadingImage;




    public void LoadScene(string level)
    {

        loadingImage.SetActive(true);
        if (String.Equals(level, "WIP"))
        {
            Debug.Log("WIP, not ready yet!!");
        }
        else
        {
            SceneManager.LoadSceneAsync(level);
            GameState.pauseMenuLoaded = 0;    
        }
        //SetActiveRecursively(loadingImage, false);
        GameState.titleScreenOnlyLoaded = false;
    }

    public void BeginLoadScene(string level)
    {
        LoadScene(level);
    }

    public void BeginLoadSceneAdditive(string level)
    {
        LoadSceneAdditive(level);
    }

    public void LoadSceneAdditive(string level)
    {
        //loadingImage.SetActive(true);
        if (String.Equals(level, "WIP"))
        {
            Debug.Log("WIP, not ready yet!!");
        }
        else
        {
            SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
            GameState.pauseMenuLoaded = 0;
            Debug.Log("Attempting to load scene asynchronously");
        }
        GameState.titleScreenOnlyLoaded = false;
    }
}