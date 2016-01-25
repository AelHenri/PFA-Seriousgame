using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{
    public int level;
    public GameObject loadingImage;



    public void SetActiveRecursively(GameObject rootObject, bool active)
    {
        rootObject.SetActive(active);

        foreach (Transform childTransform in rootObject.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }

    public void LoadScene(int level)
    {
        Debug.Log("Launched");
        SetActiveRecursively(loadingImage, true);
        Debug.Log("Loading screen loaded, trying to load async");
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
        Debug.Log("Attempting to load asynchronously");
        //yield return async;
        //yield return 0;
        //SceneManager.LoadScene(level);
    }

    public void BeginLoadScene(int level)
    {
        LoadScene(level);
        Debug.Log("Should be finished loading");
    }


}