using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    public int nbTiles = 10;
    public float width = 9.0f;
    public float height = 3.0f;
    public GameObject[] tiles;
    public bool isReady = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PrepareMap()
    {
        tiles = new GameObject[nbTiles];
        GameObject r = (GameObject)Resources.Load("Neutral", typeof(GameObject));
        for (int k = 0; k < nbTiles; k++)
        {
            float x = -1 * width + k * ((float)2 * width / (nbTiles - 1));
            //tiles[k] = (GameObject)Instantiate(r, new Vector3(Random.Range(-1 * width, width), Random.Range(0, -1 * height), 0), Quaternion.identity);
            tiles[k] = (GameObject)Instantiate(r, new Vector3(x, k % 2, 0), Quaternion.identity);
        }
        isReady = true;
    }
}
