using UnityEngine;
using System.Collections;

public class SkipArrow : MonoBehaviour {

    private StorySceneManager sceneManager;
    
    void OnTriggerEnter2D(Collider2D player)
    {
        sceneManager = (StorySceneManager)FindObjectOfType(typeof(StorySceneManager));
        sceneManager.level++;
        ;
    }
}
