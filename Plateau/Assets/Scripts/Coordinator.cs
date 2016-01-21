using UnityEngine;
using System.Collections;

public class Coordinator : MonoBehaviour {

    public GameObject Dice;
    public GameObject Map;
    public GameObject Player;
    public int[] playerPos = new int[1];

    private Dice d;
    private Map m;

	// Use this for initialization
	void Start () {
        m = Map.GetComponent<Map>();
        m.PrepareMap();
        Vector3 pos = m.tiles[0].transform.position;
        Player.transform.position = pos;
        Player.SetActive(true);
        d = Dice.GetComponent<Dice>();
	}
	
	// Update is called once per frame
	void Update () {
        if(d.hasBeenRolled)
        {
            int temp = playerPos[0];
            playerPos[0] = (playerPos[0] + d.currentValue) % m.nbTiles;
            Move move = Player.GetComponent<Move>();
            move.startPosition.Add(Player.transform.position);
            for (int i = temp; i != (playerPos[0] + 1) % m.nbTiles; i = (i + 1) % m.nbTiles)
            {
                move.startPosition.Add(m.tiles[i].transform.position);
                move.endPosition.Add(m.tiles[i].transform.position);
            }    
            d.hasBeenRolled = false;
        } 
	}
}
