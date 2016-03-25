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

        loadingImage.SetActive(true);
        if (String.Equals(level, "WIP"))
        {
            Debug.Log("WIP, not ready yet!!");
        }
        else
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(level);
            GameState.pauseMenuLoaded = false;
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
            AsyncOperation async = SceneManager.LoadSceneAsync(level, LoadSceneMode.Additive);
            GameState.pauseMenuLoaded = false;
            Debug.Log("Attempting to load asynchronously");
            //yield return async;
            //yield return 0;
            //SceneManager.LoadScene(level);
        }
        //SetActiveRecursively(loadingImage, false);
        GameState.titleScreenOnlyLoaded = false;
    }
}