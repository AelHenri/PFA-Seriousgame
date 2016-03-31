using UnityEngine;
using System.Collections;

public class RPS : MonoBehaviour {

    public static int nbLine = 100;
    public float radius = 4.0f;
    public float radiusext = 4.2f;
    public float speed = 100f;
    public int nbPlayer = 4;
    public GameObject Arrow;
    public GameObject[] Players;
    public bool end
    {
        get;
        private set;
    }
    public int currentArrowPos;

    private Color[] colors = {new Color(84.0f / 255, 172.0f / 255, 210.0f / 255),
                              new Color(225.0f / 255, 73.0f / 255, 56.0f / 255),
                              new Color(251.0f / 255, 160.0f / 255, 38.0f / 255),
                              new Color(97.0f / 255, 189.0f / 255, 109.0f / 255)};
    private LineRenderer[] lines = new LineRenderer[2 * nbLine];
    private GameObject[] spriteHolder;
    private float currentTime = 0;
    private float endTime;
    private float rotation = 0;
    
    // Use this for initialization
    void Start()
    {
        spriteHolder = new GameObject[nbPlayer];
        GameObject line = (GameObject)Resources.Load("Line", typeof(GameObject));
        for (int i = 0; i < 2 * nbLine; ++i)
        {
            GameObject l = Instantiate(line);
            l.transform.parent = transform;
            lines[i] = l.GetComponent<LineRenderer>();
        }

        for (int i = 0; i < nbLine; ++i)
        {
            lines[2 * i].SetPosition(0, transform.position);
            lines[2 * i].SetPosition(1, transform.position + new Vector3(radius * Mathf.Cos(2 * Mathf.PI * i / nbLine), 
                                                                         0,
                                                                         radius * Mathf.Sin(2 * Mathf.PI * i / nbLine)));
            lines[2 * i].material.color = colors[i / (nbLine / nbPlayer)];
            lines[2 * i].SetWidth(0.3f, 0.3f);
            lines[2 * i + 1].SetPosition(0, transform.position);
            lines[2 * i + 1].SetPosition(1, transform.position + new Vector3(radiusext * Mathf.Cos(2 * Mathf.PI * i / nbLine), -0.1f, radiusext * Mathf.Sin(2 * Mathf.PI * i / nbLine)));
            lines[2 * i + 1].material.color = Color.black;
            lines[2 * i + 1].SetWidth(0.3f, 0.3f);
        }

        for (int i = 0; i < nbPlayer; ++i)
        {
            spriteHolder[i] = new GameObject();
            spriteHolder[i].transform.parent = transform;
            SpriteRenderer sr = spriteHolder[i].AddComponent<SpriteRenderer>();
            sr.sprite = Players[i].GetComponent<SpriteRenderer>().sprite;
            Vector3 pos1 = new Vector3(radius * Mathf.Cos(2 * Mathf.PI * i / nbPlayer + Mathf.PI / nbPlayer),
                                      0,
                                      radius * Mathf.Sin(2 * Mathf.PI * i / nbPlayer + Mathf.PI / nbPlayer));
            Vector3 pos2 = new Vector3(radius * Mathf.Cos(2 * Mathf.PI * (i + 1) / nbPlayer),
                                      0,
                                      radius * Mathf.Sin(2 * Mathf.PI * (i + 1) / nbPlayer));
            Vector3 pos3 = Vector3.zero;
            Vector3 pos4 = new Vector3(radius * Mathf.Cos(2 * Mathf.PI * (2 * i + 1) / (2 * nbPlayer)),
                                      0,
                                      radius * Mathf.Sin(2 * Mathf.PI * (2 * i + 1) / (2 * nbPlayer)));
            spriteHolder[i].transform.position = transform.position + ((pos1 + pos2 + pos3 + pos4) / 4);
            spriteHolder[i].transform.eulerAngles = new Vector3(90, 0, 0);
        }
        endTime = Random.Range(7.5f, 10f);
        rotation += Random.Range(0f, 360f);
    }

        // Update is called once per frame
        void Update () {
        if(!end)
        {
            rotation += speed * 1 / Mathf.Pow(currentTime + 1, 2);
            Vector3 ang = Arrow.transform.eulerAngles;
            Arrow.transform.eulerAngles = new Vector3(ang.x, rotation, ang.z);
        }
        currentTime += Time.deltaTime;
        if (currentTime >= endTime)
            end = true;
        if (end && currentTime > endTime + 2)
            Destroy(gameObject);
        currentArrowPos = (nbPlayer - 1) - (int)Mathf.Floor(((rotation + 90f) % 360f / (360f / nbPlayer)));
    }
}
