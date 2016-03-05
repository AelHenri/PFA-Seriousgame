using UnityEngine;
using System.Collections;
using System;

public class Coordinator : MonoBehaviour {

    public static int nbPlayer = 4;
    public static int nbBonus = 3;
    public GameObject Dice;
    public GameObject Map;
    public GameObject[] Players = new GameObject[nbPlayer];
    public int[] playerPos = new int[nbPlayer];
    public GameObject[] bonusPrefabs = new GameObject[nbBonus];

    delegate void BonusBehavior(int playerWhoActivate);

    
    private Dice d;
    private Map m;
    private int currentPlayer = 0;
    private bool rolled = false;
    private GameObject[][] bonus = new GameObject[nbPlayer][];
    private BonusBehavior[] bonusesBehavior = new BonusBehavior[nbBonus];
    private bool beginOfTurn = true;
    private bool beforeDiceThrow = false;
    private bool warping = false;

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
        {
            bonus[i] = new GameObject[nbBonus];
            for (int j = 0; j < nbBonus; ++j)
            {
                bonus[i][j] = (GameObject)Instantiate(bonusPrefabs[j]);
                bonus[i][j].SetActive(false);
                bonus[i][j].transform.parent = transform;
            }
        }
        for (int i = 0; i < nbPlayer; ++i)
            playerPos[i] = -1;
        for (int i = 0; i < nbPlayer; ++i)
        {
            playerPos[i] = 0;
            SetSecondaryPlayer(Players[i], i);
        } 
        SetMainPlayer(Players[currentPlayer], currentPlayer);


        //Setting Bonus Behaviors
        bonusesBehavior[0] = BonusMoins1;
        bonusesBehavior[1] = BonusMoins2;
        bonusesBehavior[2] = BonusPlus3;
         
    }
	
	// Update is called once per frame
	void Update () {

        
        if(beginOfTurn)
        {
            AddBonus();
            beginOfTurn = false;
            beforeDiceThrow = true;
        }
        
        if(beforeDiceThrow && GlobalQuestionnaire.startQuestionnaire())
        {
            for (int i = 0; i < nbBonus; ++i)
                if (bonus[currentPlayer][i].GetComponent<Bonus>().wasUsed)
                {
                    bonusesBehavior[i](currentPlayer);
                    bonus[currentPlayer][i].GetComponent<Bonus>().wasUsed = false;
                }    
        }

        Move move = Players[currentPlayer].GetComponent<Move>();
        //Happen after the dice was rolled
        if (d.hasBeenRolled)
        {
            beforeDiceThrow = false;
            Move(move, d.currentValue, currentPlayer);
            d.hasBeenRolled = false;
            rolled = true;
        }

        //Happen when player move is over
        if(!move.moving && rolled)
        {
            TileBehavior();
        } 
    }

    void SetMainPlayer(GameObject player, int place)
    {
        player.GetComponent<Animator>().enabled = true;
        player.transform.position = m.tiles[playerPos[place]].transform.position;
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < nbBonus; ++i)
            bonus[place][i].SetActive(true);
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
        for (int i = 0; i < nbBonus; ++i)
            bonus[place][i].SetActive(false);
    }

    void Move(Move move, int nbTile, int player)
    {
        int temp = playerPos[player];
        if(nbTile >= 0)
            playerPos[player] = Math.Min((playerPos[player] + nbTile),m.nbTiles - 1);
        if (nbTile < 0)
            playerPos[player] = Math.Max((playerPos[player] + nbTile), 0);
        move.startPosition.Add(Players[player].transform.position);
        for (int i = temp; i != (playerPos[player] + nbTile / Math.Abs(nbTile)); i = (i + nbTile / Math.Abs(nbTile)))
        {
            move.startPosition.Add(m.tiles[i].transform.position);
            move.endPosition.Add(m.tiles[i].transform.position);
        }
        if(player != currentPlayer)
        {
            Vector3 pos = move.endPosition[move.endPosition.Count - 1];
            int nbPlayerOnSameTile = -1;
            foreach (int i in playerPos)
                if (i == playerPos[player])
                    nbPlayerOnSameTile++;
            move.endPosition[move.endPosition.Count- 1] = new Vector3(pos.x - 0.3f + 0.3f * nbPlayerOnSameTile, pos.y - 0.3f, pos.z);
        }
        move.moving = true;
    }

    void AddBonus()
    {
        bool full = true;
        for (int i = 0; i < nbBonus; ++i)
            full = bonus[currentPlayer][i].GetComponent<Bonus>().active && full;
        if (full)
            return;
        bool done = false;
        while(!done)
        {
            int draw = UnityEngine.Random.Range(0, nbBonus);
            if (!bonus[currentPlayer][draw].GetComponent<Bonus>().active)
            {
                bonus[currentPlayer][draw].GetComponent<Bonus>().Switch();
                done = true;
            }  
        }
    }

    void TileBehavior()
    {
        Tile.TileType tileType = m.tiles[playerPos[currentPlayer]].GetComponent<Tile>().type;
        if (tileType == Tile.TileType.Dice)
        {
            rolled = false;
            beginOfTurn = true;
        }
        else if (tileType == Tile.TileType.Event)
        {
            SetSecondaryPlayer(Players[currentPlayer], currentPlayer);
            currentPlayer = (currentPlayer + 1) % nbPlayer;
            SetMainPlayer(Players[currentPlayer], currentPlayer);
            rolled = false;
            beginOfTurn = true;
        }
        else if (tileType == Tile.TileType.Warp && !warping)
        {
            int warp = -1;
            for (int i = 0; i < m.nbTiles; ++i)
            {
                Tile.TileType type = m.tiles[i].GetComponent<Tile>().type;
                if (type == Tile.TileType.Warp && i != playerPos[currentPlayer])
                    warp = i;
            }
            // TO CHANGE 
            Move(Players[currentPlayer].GetComponent<Move>(), warp - playerPos[currentPlayer], currentPlayer);
            //
            warping = true;
        }
        else
        {
            SetSecondaryPlayer(Players[currentPlayer], currentPlayer);
            currentPlayer = (currentPlayer + 1) % nbPlayer;
            SetMainPlayer(Players[currentPlayer], currentPlayer);
            rolled = false;
            beginOfTurn = true;
            warping = false;
        }
    }

    // BONUS BEHAVIORS


    void BonusPlus3(int player)
    {
        Move move = Players[player].GetComponent<Move>();
        Move(move, 3, player);
    }

    void BonusMoins2(int player)
    {
        int k;
        while ((k = UnityEngine.Random.Range(0, nbPlayer)) == player);
        Move move = Players[k].GetComponent<Move>();
        Move(move, -2, k);
    }

    void BonusMoins1(int player)
    {
        for(int i = 0; i < nbPlayer; ++i)
            if(i != player)
            { 
                Move move = Players[i].GetComponent<Move>();
                Move(move, -1, i);
            }  
    }

    
}
