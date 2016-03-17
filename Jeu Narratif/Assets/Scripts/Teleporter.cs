using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

    //public bool isActive = false;
    public GameObject outPoint;
    public Teleporter otherTeleportPoint;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D player)
    {
        player.transform.position = otherTeleportPoint.outPoint.transform.position;
    }
}
