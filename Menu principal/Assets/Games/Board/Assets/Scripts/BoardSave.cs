using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardSave : MonoBehaviour {

    public Sprite[] playerSprites;
    public Vector3[] playerPositions;
    public List<Vector3>[] savedStartPosition;
    public List<Vector3>[] savedEndPosition;
    public int nbPlayers;

    CharacterSelection chars;

    public void translate(CoordinatorSerializable cs)
    {
        nbPlayers = cs.nbPlayer;
        playerSprites = new Sprite[nbPlayers];
        playerPositions = new Vector3[nbPlayers];
        savedStartPosition = new List<Vector3>[nbPlayers];
        savedEndPosition = new List<Vector3>[nbPlayers];

        for (int i = 0; i < nbPlayers; i++)
        {
            savedStartPosition[i] = new List<Vector3>();
            savedEndPosition[i] = new List<Vector3>();
        }

        for (int i = 0; i < nbPlayers; i++)
        {
            playerSprites[i] = chars.characters[cs.playerSpritesNumber[i]].GetComponent<SpriteRenderer>().sprite;
            playerPositions[i] = cs.playerPos[i].toVector3();

            if (cs.savedStartPosition[i].Count == 0)
                savedStartPosition[i].Add(playerPositions[i]);
            else
            {
                for (int j = 0; j < cs.savedStartPosition[i].Count; j++)
                    savedStartPosition[i].Add(cs.savedStartPosition[i][j].toVector3());
            }
             for (int j = 0; j < cs.savedEndPosition[i].Count; j++)
                 savedEndPosition[i].Add(cs.savedEndPosition[i][j].toVector3());
            Debug.Log(savedStartPosition[i].Count);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
	// Use this for initialization
	void Start () {
        chars = GameObject.Find("CharacterSelection").GetComponent<CharacterSelection>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
