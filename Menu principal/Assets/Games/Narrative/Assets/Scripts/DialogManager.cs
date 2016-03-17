using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    
    public float characterDelay = 0.1f;

    private bool messageBoxEnabled = false;
    private GameObject messageBox;
    private Text messageBoxText;
    private string message;

    public void InteractEvent()
    {
        

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

    public void GetMessage(string newMessage)
    {
        message = newMessage;
    }
}
