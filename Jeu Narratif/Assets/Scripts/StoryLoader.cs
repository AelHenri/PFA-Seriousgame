using UnityEngine;
using System.Collections;

public class StoryLoader : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject storyManager;
    // Use this for initialization
    void Awake()
    {
        /*if (ChoicesManager.instance == null)
            Instantiate(storyManager);*/
        if (StoryGameManager.instance == null)
            Instantiate(gameManager);
        
    }

}
