using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public GameObject background;
    public GameObject[] PNJs;
    public GameObject[] Arrows;

    private Transform sceneHolder;

    void BackgroundSetup()
    {
        sceneHolder = new GameObject("Scene").transform;

        GameObject toInstantiate = background;
        GameObject instance = Instantiate(toInstantiate, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;

        instance.transform.SetParent(sceneHolder);
    }

    void PNJPlacement()
    {
        int PNJcount = PNJs.Length;

        for (int i = 0; i < PNJcount; i++)
        {
            GameObject instance = Instantiate(PNJs[i], PNJs[i].transform.position, Quaternion.identity) as GameObject;
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
        for (int i = 0; i < Arrows.Length; i++)
        {
            GameObject instance = Instantiate(Arrows[i], Arrows[i].transform.position, Quaternion.identity) as GameObject;
            instance.transform.SetParent(sceneHolder);
        }
    }

}
