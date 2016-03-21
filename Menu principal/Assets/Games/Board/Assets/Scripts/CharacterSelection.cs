using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    private List<GameObject> characters;
    private int selectionIndex = 0;

    /*private List<Button> characterButtons;
    public GameObject buttons;*/
    
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Use this for initialization
	void Start () {
        characters = new List<GameObject>();
	    foreach (Transform t in transform)
        {
            characters.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        characters[selectionIndex].SetActive(true);
        /*
        characterButtons = new List<Button>();
        foreach (Transform t in buttons.transform)
        {
            characterButtons.Add(buttons.GetComponentInChildren<Button>());
        }
        */
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
            Coordinator.selectedPlayer[i] = characters[selectionIndex];
            Coordinator.playerSprites[i] = characters[selectionIndex].GetComponent<SpriteRenderer>().sprite;
            Debug.Log(characters[selectionIndex].GetComponent<SpriteRenderer>().sprite);
        }
        SceneManager.LoadScene("BoardMain");
        
    }

	// Update is called once per frame
	void Update () {

	}
}
