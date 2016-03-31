using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Coordinator : MonoBehaviour {

    public static bool isFromSavedGame = false;

    public static int nbPlayer = 4;
    public static int nbBonus = 3;
    public GameObject Dice;
    public GameObject Map;
    public static GameObject[] Players = new GameObject[nbPlayer];
    public int[] playerPos = new int[nbPlayer];
    public GameObject[] bonusPrefabs = new GameObject[nbBonus];
    public static Sprite[] playerSprites = new Sprite[nbPlayer]; // MODIFIED THIS
    public static Vector3[] savecPos = new Vector3[nbPlayer]; //added this
    public GameObject Canvas;
    public GameObject TextComp;

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
    private bool bonusEnd = true;
    private GameObject RPS;
    private GameObject RPSTemp;
    private bool bm2Init = true;
    private int bm2playerToMove;
    private float time = 0f;
    private float turnBegin;
    private bool questionnaireLaunched = false;
    private bool timeSet = false;
    private int[] goodAnswersinARow = new int[nbPlayer];
    private bool end = false;
    private float timeEnd = 0f;
    private float timeEOT = 0f;
    private float timeDice = 0f;
    private float timeEvent = 0f;
    private bool relaunch = false;
    private bool endOfTurn;
    private bool eventTime;
    private bool eventQuestion;
    private int eventPlayer;
    private bool eventInit = true;
    private bool eventMove = false;
    private bool eventEnd = false;
    private Move m1, m2;
    private int winner = -1;
    private bool draw = false;


    public Animator animator;

    Questionnaire questionnaire;

    // Use this for initialization
    void Start () {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();

        //GameObject terrain = (GameObject)Resources.Load((UnityEngine.Random.Range(0,2) == 0)?"TIle":"TGlace", typeof(GameObject));
        //Instantiate(terrain);
        m = Map.GetComponent<Map>();
        m.PrepareMap();
        Vector3 pos = m.tiles[0].transform.position + new Vector3(0, 0, 0);
        GameObject player = (GameObject)Resources.Load("Player", typeof(GameObject));
        RPS = (GameObject)Resources.Load("RandomPlayerSelector", typeof(GameObject));

        //animator = Canvas.GetComponent("LevelImage").GetComponent<Animator>();

        for (int i = 0; i < nbPlayer; ++i)
        {
            Players[i] = (GameObject)Instantiate(player);
            Players[i].GetComponent<SpriteRenderer>().sprite = playerSprites[i];


            Players[i].transform.position = pos;
                
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
        {
            if (isFromSavedGame)
            {
                Players[i].transform.position = savecPos[i];
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

        for (int i = 0; i < nbPlayer; ++i)
        {
            goodAnswersinARow[i] = 0;
        }

        //Setting Bonus Behaviors
        bonusesBehavior[0] = BonusMoins1;
        bonusesBehavior[1] = BonusMoins2;
        bonusesBehavior[2] = BonusPlus3;
        
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if(!end)
            for(int i = 0; i < nbPlayer; ++i)
                if (playerPos[i] == m.nbTiles - 1)
                    {
                        end = true;
                        timeEnd = time;
                        if (winner != -1)
                            draw = true;
                        winner = i;
                    }

        if (end && (time - timeEnd) > 10f)
        {
            GameState.quitBoard();
        }

        if (end)
        {
            d.gameObject.SetActive(false);
            for (int i = 0; i < nbBonus; ++i)
                bonus[currentPlayer][i].SetActive(false);
            Canvas.SetActive(true);
            Text t = TextComp.GetComponent<Text>();
            if (!draw)
                t.text = "Bravo Joueur " + (winner + 1) + ", tu as gagné !";
            else
                t.text = "Egalité";
            if(timeEnd == 0f)
                timeEnd = time;
            return;
        }

        if (endOfTurn && (time - timeEOT) > 5f)
        {
            endOfTurn = false;
            TurnEnd();
        }
            
        if(endOfTurn)
            return;

        if (relaunch && (time - timeDice) > 2f)
            relaunch = false;

        if (relaunch)
            return;

        if(eventTime)
        {
            if (eventInit)
            {
                RPSTemp = Instantiate(RPS);
                RPS rps = RPSTemp.GetComponent<RPS>();
                rps.Players = new GameObject[nbPlayer - 1];
                int count = 0;
                for (int i = 0; i < nbPlayer; ++i)
                    if (i != currentPlayer)
                        rps.Players[count++] = Players[i];
                rps.nbPlayer = nbPlayer - 1;
                RPSTemp.gameObject.SetActive(true);
                eventPlayer = -1;
                eventInit = false;
            }

            if (RPSTemp == null)
            {
                Debug.Log(eventPlayer);
                eventInit = true;
                eventQuestion = true;
                eventTime = false;
                timeEvent = time;
                return;
            }

            if (RPSTemp.GetComponent<RPS>().end && eventPlayer == -1)
            {
                eventPlayer = RPSTemp.GetComponent<RPS>().currentArrowPos;
                if (eventPlayer >= currentPlayer)
                    eventPlayer++;
                Debug.Log(eventPlayer);
            }
            return;
        }

        if(eventQuestion && (time - timeEvent) > 3f)
        {
            Canvas.SetActive(false);
            if (!questionnaireLaunched)
            {
                questionnaire.startQuestionnaire();
                questionnaireLaunched = true;
            }
            else
            {
                if (questionnaire.hasAnswered)
                {
                    questionnaireLaunched = false;
                    if (questionnaire.isAnswerRight)
                    {
                        d.gameObject.SetActive(true);
                        eventMove = true;
                        timeEvent = time;
                    }
                    else
                    {
                        endOfTurn = true;
                        timeEOT = time;
                    }
                    eventQuestion = false;
                }
            }
            return;
        }

        if(eventQuestion)
        {
            Canvas.SetActive(true);
            Text t = TextComp.GetComponent<Text>();
            t.text = "Joueur " + (eventPlayer + 1) + " aide Joueur " + (currentPlayer + 1);
            timeEnd = time;
            return;
        }

        if(eventMove && (time - timeEvent) > 3f)
        {
            m1 = Players[currentPlayer].GetComponent<Move>();
            m2 = Players[eventPlayer].GetComponent<Move>();
            Move(m1, 4, currentPlayer);
            Move(m2, 4, eventPlayer);
            eventMove = false;
            eventEnd = true;
            return;
        }

        if(eventEnd)
        {
            if(!m1.moving && !m2.moving)
            {
                eventEnd = false;
                endOfTurn = true;
                timeEOT = time;
            }
            return;
        }

        

        if(beginOfTurn)
        {
            d.gameObject.SetActive(false);
            for (int i = 0; i < nbBonus; ++i)
                bonus[currentPlayer][i].SetActive(false); 
            if (!timeSet)
            {
                turnBegin = time;
                timeSet = true;
            }

            if ((time - turnBegin > 1) && beginOfTurn)
            {
                Canvas.SetActive(true);
                Text t = TextComp.GetComponent<Text>();
                t.text = "Joueur " + (currentPlayer + 1) + " à toi de jouer !";
            }

            if ((time - turnBegin > 2) && beginOfTurn)
                animator.SetTrigger("End Transition");

            //AddBonus();
        }

        if((time - turnBegin > 3) && beginOfTurn)
        {
            Canvas.SetActive(false);
            if (!questionnaireLaunched)
            { 
                questionnaire.startQuestionnaire();
                questionnaireLaunched = true;
            }
            else
            {
                if (questionnaire.hasAnswered)
                {
                    questionnaireLaunched = false;
                    if (questionnaire.isAnswerRight)
                    {
                        beginOfTurn = false;
                        beforeDiceThrow = true;
                        goodAnswersinARow[currentPlayer] += 1;
                        if (goodAnswersinARow[currentPlayer] % 3 == 0)
                            AddBonus();
                        d.gameObject.SetActive(true);
                        for (int i = 0; i < nbBonus; ++i)
                            bonus[currentPlayer][i].SetActive(true); 
                    }
                    else
                    {
                        goodAnswersinARow[currentPlayer] = 0;
                        endOfTurn = true;
                        timeEOT = time;
                    }
                }     
            }
        }

        if(beforeDiceThrow && !d.roll)
        {
            for (int i = 0; i < nbBonus; ++i)
                if (bonus[currentPlayer][i].GetComponent<Bonus>().wasUsed)
                {
                    d.gameObject.SetActive(false);
                    bonusEnd = false;
                    bonusesBehavior[i](currentPlayer);
                    if (bonusEnd)
                    {
                        bonus[currentPlayer][i].GetComponent<Bonus>().wasUsed = false;
                        d.gameObject.SetActive(true);
                    }
                }    
        }
        else if(beforeDiceThrow && d.roll)
        {
            for (int i = 0; i < nbBonus; ++i)
                bonus[currentPlayer][i].SetActive(false); 
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
        if (!isFromSavedGame)
            player.transform.position = m.tiles[playerPos[place]].transform.position;
        player.transform.GetChild(0).gameObject.SetActive(true);
        player.transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < nbBonus; ++i)
            bonus[place][i].SetActive(true);
    }

    void SetSecondaryPlayer(GameObject player, int place)
    {
        //Debug.Log(player.transform.childCount);
        player.transform.GetChild(0).gameObject.SetActive(false);
        player.GetComponent<Animator>().enabled = false;
        Vector3 pos = player.transform.position;
        int nbPlayerOnSameTile = -1;
        foreach(int i in playerPos)
            if (i == playerPos[place])
                nbPlayerOnSameTile++;
        if (!isFromSavedGame)
            player.transform.position = new Vector3(pos.x - 0.3f + 0.3f * nbPlayerOnSameTile, pos.y, pos.z - 0.3f);
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
            move.endPosition[move.endPosition.Count- 1] = new Vector3(pos.x - 0.3f + 0.3f * nbPlayerOnSameTile, pos.y, pos.z - 0.3f);
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
            Debug.Log("Dice tile");
            rolled = false;
            beginOfTurn = true;
            timeDice = time;
            relaunch = true;
        }
        else if (tileType == Tile.TileType.Event)
        {
            Debug.Log("Event tile");
            eventTime = true;
            eventInit = true;
            rolled = false;
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
            Debug.Log("Normal Tile");
            endOfTurn = true;
            timeEOT = time;
        }
    }

    void TurnEnd()
    {
        SetSecondaryPlayer(Players[currentPlayer], currentPlayer);
        currentPlayer = (currentPlayer + 1) % nbPlayer;
        SetMainPlayer(Players[currentPlayer], currentPlayer);
        rolled = false;
        beginOfTurn = true;
        warping = false;
        timeSet = false;
    }

    // BONUS BEHAVIORS


    void BonusPlus3(int player)
    {
        Move move = Players[player].GetComponent<Move>();
        Move(move, 3, player);
        bonusEnd = true;
    }

    void BonusMoins2(int player)
    {
        if (bm2Init)
        {
            RPSTemp = Instantiate(RPS);
            RPS rps = RPSTemp.GetComponent<RPS>();
            rps.Players = new GameObject[nbPlayer - 1];
            int count = 0;
            for (int i = 0; i < nbPlayer; ++i)
                if (i != currentPlayer)
                    rps.Players[count++] = Players[i];
            rps.nbPlayer = nbPlayer - 1;
            RPSTemp.gameObject.SetActive(true);
            bm2playerToMove = -1;
            bm2Init = false;
        }

        if (RPSTemp == null)
        {
            Debug.Log(bm2playerToMove);
            Move move = Players[bm2playerToMove].GetComponent<Move>();
            Move(move, -2, bm2playerToMove);
            bonusEnd = true;
            bm2Init = true;
            return;
        }

        if (RPSTemp.GetComponent<RPS>().end && bm2playerToMove == -1)
        {
            bm2playerToMove = RPSTemp.GetComponent<RPS>().currentArrowPos;
            if (bm2playerToMove >= currentPlayer)
                bm2playerToMove++;
            Debug.Log(bm2playerToMove);
        }
    }

    void BonusMoins1(int player)
    {
        for(int i = 0; i < nbPlayer; ++i)
            if(i != player)
            { 
                Move move = Players[i].GetComponent<Move>();
                Move(move, -1, i);
            }
        bonusEnd = true;
    }


    
}
