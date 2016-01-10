using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Validation : MonoBehaviour {

    public FicheXml fiche;

    public CanvasGroup canvasGValidation;
    public CanvasGroup canvasGConfirmation;
    public AjoutImage ajt;

    public InputField inputReponse1;
    public InputField inputReponse2;
    public InputField inputReponse3;
    public InputField inputExemple;
    public InputField nomFiche;



    public Toggle toggleRep1;
    public Toggle toggleRep2;
    public Toggle toggleRep3;

    bool showConfirmation = false;

    string targetPath;
    string destFile;

    public void validation()
    {

        fiche.creerDossierFiche();
        fiche.copierImages();
        fiche.genererFiche();
        showConfirmation = true;
    }

    public void confirmation()
    {
        showConfirmation = false;
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
