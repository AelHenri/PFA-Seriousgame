using UnityEngine;
using System.Collections;

[System.Serializable]
public class StoryScene
{
    public GameObject background;
    public PNJ[] PNJs;
    public Arrow[] Arrows;
    public float playerX;
    public float playerY;
}

[System.Serializable]
public class Arrow
{
    public GameObject arrow;
    public bool isTp;
}

public class StorySceneManager : MonoBehaviour {

    public StoryScene[] scene;
    public int level = 0;

    private Transform sceneHolder;
    private StoryGameManager gameManager;

    void BackgroundSetup()
    {
        sceneHolder = new GameObject("Scene").transform;

        GameObject toInstantiate = scene[level].background;
        GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
        
        instance.transform.SetParent(sceneHolder);
        gameManager = (StoryGameManager)FindObjectOfType(typeof(StoryGameManager));
    }

    public void PNJPlacement()
    {
        int PNJcount = scene[level].PNJs.Length;

        for (int i = 0; i < PNJcount; i++)
        {
            PNJ currentPNJ = scene[level].PNJs[i];
            if (gameManager.IsPNJPresent(currentPNJ.PNJName) || currentPNJ.firstTime)
            {
                PNJ instance = Instantiate(scene[level].PNJs[i], scene[level].PNJs[i].transform.position, Quaternion.identity) as PNJ;
                instance.transform.SetParent(sceneHolder);
            }           
            
        }
    }

	 public void SetupScene()
    {
        BackgroundSetup();
        PNJPlacement();
        PlaceTPs();
    }

    public void PlaceArrows()
    {
        for (int i = 0; i < scene[level].Arrows.Length; i++)
        {
            GameObject instance = Instantiate(scene[level].Arrows[i].arrow, scene[level].Arrows[i].arrow.transform.position, Quaternion.identity) as GameObject;
            instance.transform.SetParent(sceneHolder);
        }
    }

    public void PlaceTPs()
    {
        for (int i = 0; i < scene[level].Arrows.Length; i++)
        {
            if (scene[level].Arrows[i].isTp)
            {
                GameObject instance = Instantiate(scene[level].Arrows[i].arrow, scene[level].Arrows[i].arrow.transform.position, Quaternion.identity) as GameObject;
                instance.transform.SetParent(sceneHolder);
            }
           
        }
    }

}
