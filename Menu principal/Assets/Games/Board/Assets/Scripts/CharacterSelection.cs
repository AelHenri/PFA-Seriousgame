using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    public List<GameObject> characters;
    private int selectionIndex = 0;

   


	// Use this for initialization
	void Start () {
        characters = new List<GameObject>();
	    foreach (Transform t in transform)
        {
            characters.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

 
	}
	
    public void selectCharacter(int index)
    {
        if (index < 0  || index >= characters.Count)
            return;
        if (index == selectionIndex)
            return;

        characters[selectionIndex].SetActive(false);
        selectionIndex = index;
        characters[selectionIndex].SetActive(true);
    }

    public void ok()
    {
        for (int i = 0; i < Coordinator.nbPlayer; i++)
        {
            Coordinator.playerSprites[i] = characters[selectionIndex].GetComponent<SpriteRenderer>().sprite;
        }
        SceneManager.LoadScene("BoardMain");
        
    }

    public void confirmSelectedChar()
    {
        Coordinator.playerSprites[BoardMenu.currentChoosingPlayerNumber] = characters[selectionIndex].GetComponent<SpriteRenderer>().sprite;
        BoardMenu.playerSpritesNumber[BoardMenu.currentChoosingPlayerNumber] = selectionIndex;
        BoardMenu.currentChoosingPlayerNumber++;
    }


    // Update is called once per frame
    void Update () {
        if (BoardMenu.currentChoosingPlayerNumber != 666)
            characters[selectionIndex].SetActive(true);


    }
}
