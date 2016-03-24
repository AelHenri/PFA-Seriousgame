using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class GameState  {
    //Pause menu
    public static bool pauseMenuLoaded = false;
    public static bool titleScreenOnlyLoaded = true;

    //Games
    public static int gameCurrentlyLoaded;
    
    //Narrative game
    public static GameObject narrative;
    public static GameObject narrativeSound;
    
    //Labyrinth game
    public static GameObject labyrinth;

    
    //Time freezing
    public static bool isTimeFrozen = false;
    private static bool timeAlreadyFrozen = false;

    public static void freezeTime()
    {
        if (!isTimeFrozen)
        {
            Debug.Log("Freezing time");
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Time already frozen");
            timeAlreadyFrozen = true;
        }
        isTimeFrozen = true;
    }

    public static void unfreezeTime()
    {
        if (!timeAlreadyFrozen)
        {
            Debug.Log("Unfreezing time");
            Time.timeScale = 1;
            isTimeFrozen = false;
        }
        else
        {
            Debug.Log("Time still frozen");
            timeAlreadyFrozen = false;
        }
    }

    public static void quitNarrative()
    {
        if (narrative != null)
        {
            Object.Destroy(narrative);
            Object.Destroy(narrativeSound);
            SceneManager.LoadSceneAsync("TitleScreen");
        }
    }

    public static void quitLabyrinth()
    {

        if (labyrinth != null)
        {
            Object.Destroy(labyrinth);
            SceneManager.LoadSceneAsync("TitleScreen");
        }
    }

    public static void quitBoard()
    {
        //SceneManager.LoadSceneAsync("TitleScreen");
    }

}
