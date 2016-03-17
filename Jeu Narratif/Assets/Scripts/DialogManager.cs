﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public GameObject messageBox;
    public Text messageBoxText;
    public float characterDelay = 0.1f;

    public bool messageBoxEnabled = false;

    public string message;

    public void InteractEvent()
    {
        messageBox.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(TypeMessage());
        //Interact.DisableControl();
        messageBoxEnabled = true;
    }

    IEnumerator TypeMessage()
    {
        messageBoxText.text = "";

        foreach (char c in message)
        {
            yield return new WaitForSeconds(characterDelay);
            messageBoxText.text += c;
            if (messageBox.GetComponent<AudioSource>() != null)
            {
                messageBox.GetComponent<AudioSource>().Play();
            }
        }
    }

    void Update()
    {
        if (messageBoxEnabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StopAllCoroutines();
                messageBox.SetActive(false);
                //Interact.EnableControl();
                messageBoxEnabled = false;
            }
        }
    }
}
