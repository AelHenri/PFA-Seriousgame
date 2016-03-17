using UnityEngine;
using System.Collections;

public class StoryLoader : MonoBehaviour
{

    public GameObject gameManager;
    public GameObject storyManager;
    // Use this for initialization
    void Awake()
    {
        if (StoryGameManager.instance == null)
            GameState.narrative = Instantiate(gameManager);
        
    }

}
