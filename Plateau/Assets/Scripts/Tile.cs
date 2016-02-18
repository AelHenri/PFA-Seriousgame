using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public enum TileType : byte
    {
        Neutral = 0,
        Warp = 1,
        Event = 2,
        Dice = 3,
        Start = 4,
        End = 5
    };

    public TileType type;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
