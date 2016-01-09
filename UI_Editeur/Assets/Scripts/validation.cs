using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class validation : MonoBehaviour {

    
    public CanvasGroup canvasGValidation;
    public CanvasGroup canvasGConfirmation;
    public ajout_image ajt;

    public InputField inputReponse1;
    public InputField inputReponse2;
    public InputField inputReponse3;
    public InputField inputExemple;

    string text_reponse1;
    string text_reponse2;
    string text_reponse3;
    string text_exemple;

    public Toggle toggleRep1;
    public Toggle toggleRep2;
    public Toggle toggleRep3;

    bool showConfirmation = false;

    string targetPath;
    string destFile;

    public void onClickValidation()
    {
        destFile = System.IO.Path.Combine(targetPath, "image_exemple.jpg");
        System.IO.File.Copy(ajt.imagePathIndic, destFile, true);

        destFile = System.IO.Path.Combine(targetPath, "image_question.jpg");
        System.IO.File.Copy(ajt.imagePathIndic, destFile, true);

        showConfirmation = true;

        text_reponse1 = inputReponse1.text;
        text_reponse2 = inputReponse2.text;
        text_reponse3 = inputReponse3.text;
        text_exemple = inputExemple.text;

        Debug.Log(text_reponse1);
        Debug.Log(text_reponse2);
        Debug.Log(text_reponse3);
        Debug.Log(text_exemple);
 


        Debug.Log(toggleRep1.ToString() + " : " + toggleRep1.isOn);
        Debug.Log(toggleRep2.ToString() + " : " + toggleRep2.isOn);
        Debug.Log(toggleRep3.ToString() + " : " + toggleRep3.isOn);


    }

    public void onClickConfirmation()
    {
        showConfirmation = false;
    }


    void genererFicheXML()
    {
        
    }


	// Use this for initialization
	void Start ()
    {
        targetPath = Application.dataPath + "/../Fiches";

        if (!System.IO.Directory.Exists(targetPath))
            System.IO.Directory.CreateDirectory(targetPath);
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ajt.imagePathIndic != null && ajt.imagePathQues!= null)
        {
            canvasGValidation.alpha = 1;
            canvasGValidation.interactable = true;
        }
        else
        {
            canvasGValidation.alpha = 0;
            canvasGValidation.interactable = false;
        }

        if (showConfirmation)
            canvasGConfirmation.alpha = 1;
        else
            canvasGConfirmation.alpha = 0;
        canvasGConfirmation.interactable = showConfirmation;
    }


}
