using UnityEngine;
using System.Collections;

[System.Serializable]
public class Scene
{
    public GameObject background;
    public GameObject[] PNJs;
    public GameObject[] Arrows;
    public float playerX;
    public float playerY;
}

public class StorySceneManager : MonoBehaviour {

    public Scene[] scene;
    public int level = 0;

    private Transform sceneHolder;

    void BackgroundSetup()
    {
        sceneHolder = new GameObject("Scene").transform;

        GameObject toInstantiate = scene[level].background;
        GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;

        instance.transform.SetParent(sceneHolder);
    }

    void PNJPlacement()
    {
        int PNJcount = scene[level].PNJs.Length;

        for (int i = 0; i < PNJcount; i++)
        {
            GameObject instance = Instantiate(scene[level].PNJs[i], scene[level].PNJs[i].transform.position, Quaternion.identity) as GameObject;
            instance.transform.SetParent(sceneHolder);
        }
    }

	 public void SetupScene()
    {
        BackgroundSetup();
        PNJPlacement();
    }

    public void PlaceArrows()
    {
        for (int i = 0; i < scene[level].Arrows.Length; i++)
        {
            GameObject instance = Instantiate(scene[level].Arrows[i], scene[level].Arrows[i].transform.position, Quaternion.identity) as GameObject;
            instance.transform.SetParent(sceneHolder);
        }
    }

}
