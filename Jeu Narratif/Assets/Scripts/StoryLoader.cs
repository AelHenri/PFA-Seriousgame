using UnityEngine;
using System.Collections;

public class StoryLoader : MonoBehaviour
{

    public GameObject gameManager;
    // Use this for initialization
    void Awake()
    {
        if (StoryGameManager.instance == null)
            Instantiate(gameManager);
    }

}
