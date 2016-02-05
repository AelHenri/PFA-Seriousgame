using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LoadOnClick : MonoBehaviour
{
    public GameObject loadingImage;



   /* public void SetActiveRecursively(GameObject rootObject, bool active)
    {
        rootObject.SetActive(active);

        foreach (Transform childTransform in rootObject.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);

        }
    }*/

    public void LoadScene(string level)
    {
        Debug.Log("Loading in progress");
        loadingImage.SetActive(true);
        Debug.Log("Loading screen loaded, trying to load async");
        if (String.Equals(level, "WIP"))
        {
            Debug.Log("WIP, not ready yet!!");
        }
        else
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(level);
            GameState.pauseMenuLoaded = false;
            Debug.Log("Attempting to load asynchronously");
            //yield return async;
            //yield return 0;
            //SceneManager.LoadScene(level);
        }
        //SetActiveRecursively(loadingImage, false);
        GameState.titleScreenOnlyLoaded = false;
    }

    public void BeginLoadScene(string level)
    {
        LoadScene(level);
        Debug.Log("level "+level+" should be finished loading");
    }


}