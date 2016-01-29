using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{
    public int level;
    public GameObject loadingImage;



    public void SetActiveRecursively(GameObject rootObject, bool active)
    {
        Debug.Log("Censé passer ici pour activer loading image");
        rootObject.SetActive(active);

        foreach (Transform childTransform in rootObject.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);

        }
    }

    public void LoadScene(int level)
    {
        Debug.Log("Launched");
        loadingImage.SetActive(true);
        Debug.Log("Loading screen loaded, trying to load async");
        if (level >= 10)
        {
            Debug.Log("WIP, not ready yet!!");
        }
        else
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(level);
            Debug.Log("Attempting to load asynchronously");
            //yield return async;
            //yield return 0;
            //SceneManager.LoadScene(level);
        }
        //SetActiveRecursively(loadingImage, false);
    }

    public void BeginLoadScene(int level)
    {
        LoadScene(level);
        Debug.Log("Should be finished loading");
    }


}