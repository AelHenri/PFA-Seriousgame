using UnityEngine;
using System.Collections;

public class Coordinator : MonoBehaviour {

    public static int nbPlayer = 4;
    public GameObject Dice;
    public GameObject Map;
    public GameObject[] Players = new GameObject[nbPlayer];
    public int[] playerPos = new int[nbPlayer];

    private Dice d;
    private Map m;
    private int currentPlayer = 0;
    private bool rolled = false;

	// Use this for initialization
	void Start () {
        m = Map.GetComponent<Map>();
        m.PrepareMap();
        Vector3 pos = m.tiles[0].transform.position;
        GameObject player = (GameObject)Resources.Load("Player", typeof(GameObject));
        for (int i = 0; i < nbPlayer; ++i)
        {
            Players[i] = (GameObject)Instantiate(player, pos, Quaternion.identity);
            Players[i].SetActive(true);
        }  
        d = Dice.GetComponent<Dice>();
        for (int i = 0; i < nbPlayer; ++i)
            playerPos[i] = -1;
        for (int i = 0; i < nbPlayer; ++i)
        {
            playerPos[i] = 0;
            SetSecondaryPlayer(Players[i], i);
        } 
        SetMainPlayer(Players[currentPlayer], currentPlayer);
    }
	
	// Update is called once per frame
	void Update () {
        Move move = Players[currentPlayer].GetComponent<Move>();
        if (d.hasBeenRolled)
        {
            Move(move);
            d.hasBeenRolled = false;
            rolled = true;
        }
        if(!move.moving && rolled)
        {
            SetSecondaryPlayer(Players[currentPlayer], currentPlayer);
            currentPlayer = (currentPlayer + 1) % nbPlayer;
            SetMainPlayer(Players[currentPlayer], currentPlayer);
            rolled = false;
        }    
    }

    void SetMainPlayer(GameObject player, int place)
    {
        player.GetComponent<Animator>().enabled = true;
        player.transform.position = m.tiles[playerPos[place]].transform.position;
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.localScale = new Vector3(1, 1, 1);
    }

    void SetSecondaryPlayer(GameObject player, int place)
    {
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.GetComponent<Animator>().enabled = false;
        Vector3 pos = player.transform.position;
        int nbPlayerOnSameTile = -1;
        foreach(int i in playerPos)
            if (i == playerPos[place])
                nbPlayerOnSameTile++;
        player.transform.position = new Vector3(pos.x - 0.3f + 0.3f * nbPlayerOnSameTile, pos.y - 0.3f, pos.z);
        player.transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }

    void Move(Move move)
    {
        int temp = playerPos[currentPlayer];
        playerPos[currentPlayer] = (playerPos[currentPlayer] + d.currentValue) % m.nbTiles;
        
        move.startPosition.Add(Players[currentPlayer].transform.position);
        for (int i = temp; i != (playerPos[currentPlayer] + 1) % m.nbTiles; i = (i + 1) % m.nbTiles)
        {
            move.startPosition.Add(m.tiles[i].transform.position);
            move.endPosition.Add(m.tiles[i].transform.position);
        }
        move.moving = true;
    }
}
