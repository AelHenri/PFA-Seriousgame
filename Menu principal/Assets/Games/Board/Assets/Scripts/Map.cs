using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    public int nbTiles = 20;
    //public float width = 8.0f;
    //public float height = 4.0f;
    public GameObject[] tiles
    {
        get;
        private set;
    }
    public bool isReady
    {
        get;
        private set;
    }

    private Vector3[] positions = {new Vector3(-7.73f,10f,-3.89f),
                                   new Vector3(-8.43f,10f,-1.21f),
                                   new Vector3(-8.8f,10,0.84f),
                                   new Vector3(-8.24f,10,3f),
                                   new Vector3(-6.39f,10,4.11f),
                                   new Vector3(-4.15f,10f,4.08f),
                                   new Vector3(-2.78f,10f,2.68f),
                                   new Vector3(-5.38f,10f,2.37f),
                                   new Vector3(-7.03f,10f,1.42f),
                                   new Vector3(-4.8f,10f,0.84f),
                                   new Vector3(-3.02f,10f,1.02f),
                                   new Vector3(-2.38f,10f,-0.7f),
                                   new Vector3(-2.03f,10f,-2.81f),
                                   new Vector3(-0.48f,10f,-4.43f),
                                   new Vector3(1.9f,10f,-4.48f),
                                   new Vector3(0.28f,10f,-3f),
                                   new Vector3(-0.85f,10f,-0.93f),
                                   new Vector3(0.82f,10f,-0.91f),
                                   new Vector3(2.18f,10f,-2.550f),
                                   new Vector3(2.45f,10f,-0.4f),
                                   new Vector3(3.04f,10f,2.27f),
                                   new Vector3(4.66f,10f,4.09f),
                                   new Vector3(6.79f,10f,4.1f),
                                   new Vector3(8.86f,10f,2.97f),
                                   new Vector3(8.89f,10f,0.47f),
                                   new Vector3(7.02f,10f,-0.49f),
                                   new Vector3(4.73f,10f,-0.28f),
                                   new Vector3(4.56f,10f,2.06f),
                                   new Vector3(6.55f,10f,2.77f),
                                   new Vector3(7.11f,10f,1.61f)
                                  };
	// Use this for initialization
	void Start () {
        isReady = false; 
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
            //float x = -1 * width + k * ((float)2 * width / (nbTiles - 1));
            Vector3 pos = positions[k];
            //tiles[k] = (GameObject)Instantiate(r, new Vector3(Random.Range(-1 * width, width), Random.Range(0, -1 * height), 0), Quaternion.identity);
            if (k == 0)
                tiles[k] = (GameObject)Instantiate(start);
            else if(k == nbTiles - 1)
                tiles[k] = (GameObject)Instantiate(cross);
            else if ((k % 3 == 2 && k / 3 == 3) || (k % 3 == 2 && k / 3 == 5))
                tiles[k] = (GameObject)Instantiate(warp);
            else if (k % 3 == 2)
            {
                int r = Random.Range(1, 3);
                if (r == 1)
                    tiles[k] = (GameObject)Instantiate(eventt);
                else
                    tiles[k] = (GameObject)Instantiate(dice);
            }
            else
                tiles[k] = (GameObject)Instantiate(neutral);
            tiles[k].transform.position = pos;
            tiles[k].transform.parent = transform;
        }
        for(int k = 0; k < nbTiles - 1; ++k)
        {
            GameObject line = (GameObject)Resources.Load("Line", typeof(GameObject));
            GameObject l = (GameObject)Instantiate(line, Vector3.zero, Quaternion.identity);
            Line li = l.GetComponent<Line>();
            li.begin = tiles[k];
            li.end = tiles[k + 1];
            li.transform.parent = transform;
        }
        isReady = true;
    }
}
