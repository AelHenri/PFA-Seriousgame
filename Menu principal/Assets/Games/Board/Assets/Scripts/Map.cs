using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    public int nbTiles = 20;
    public float width = 8.0f;
    public float height = 4.0f;
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
        GameObject neutral = (GameObject)Resources.Load("Neutral", typeof(GameObject));
        GameObject cross = (GameObject)Resources.Load("Cross", typeof(GameObject));
        GameObject dice = (GameObject)Resources.Load("Dice", typeof(GameObject));
        GameObject eventt = (GameObject)Resources.Load("Event", typeof(GameObject));
        GameObject start = (GameObject)Resources.Load("Start", typeof(GameObject));
        GameObject warp = (GameObject)Resources.Load("Warp", typeof(GameObject));
        for (int k = 0; k < nbTiles; k++)
        {
            float x = -1 * width + k * ((float)2 * width / (nbTiles - 1));
            Vector3 pos = new Vector3(x, 2 * (k % 3) - height, 0);
            //tiles[k] = (GameObject)Instantiate(r, new Vector3(Random.Range(-1 * width, width), Random.Range(0, -1 * height), 0), Quaternion.identity);
            if (k == 0)
                tiles[k] = (GameObject)Instantiate(start, pos, Quaternion.identity);
            else if(k == nbTiles - 1)
                tiles[k] = (GameObject)Instantiate(cross, pos, Quaternion.identity);
            else if ((k % 3 == 2 && k / 3 == 3) || (k % 3 == 2 && k / 3 == 5))
                tiles[k] = (GameObject)Instantiate(warp, pos, Quaternion.identity);
            else if (k % 3 == 2)
            {
                int r = Random.Range(1, 3);
                if (r == 1)
                    tiles[k] = (GameObject)Instantiate(eventt, pos, Quaternion.identity);
                else
                    tiles[k] = (GameObject)Instantiate(dice, pos, Quaternion.identity);
            }
            else
                tiles[k] = (GameObject)Instantiate(neutral, pos, Quaternion.identity);
            tiles[k].transform.parent = transform;
        }
        for(int k = 0; k < nbTiles - 1; ++k)
        {
            GameObject line = (GameObject)Resources.Load("Line", typeof(GameObject));
            GameObject l = (GameObject)Instantiate(line, Vector3.zero, Quaternion.identity);
            Line li = l.GetComponent<Line>();
            li.begin = tiles[k];
            li.end = tiles[k + 1];
        }
        isReady = true;
    }
}
