using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class Validation : MonoBehaviour {

    public FicheXml fiche;

    public menu menu;
    public AjoutImage ajt;

    public InputField nomFiche;
    public InputField intputNumeroFiche;
    public InputField inputExemple;
    public InputField inputReponse1;
    public InputField inputReponse2;
    public InputField inputReponse3;


    public GameObject saveButton;

    public Toggle toggleRep1;
    public Toggle toggleRep2;
    public Toggle toggleRep3;

    string targetPath;
    string destFile;

    public void validation()
    {

        if (isEverythingFilled())
        {
            fiche.creerDossierFiche();
            fiche.copierImages();
            fiche.genererFiche();
            menu.showSucceedDialogue();
        }
        else
            menu.showIncompleteSheetError();
    }

 
    public bool isTextReadingSheet()
    {
        if (inputReponse1 == null && inputReponse2 == null && inputReponse3 == null)
            return true;
        else
            return false;
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
            saveButton.SetActive(true);
            
        }
        else
        {
            saveButton.SetActive(false);
        }
        
    }


    bool isEverythingFilled()
    {

        if (nomFiche.text == "" || inputExemple.text == "")
            return false;
        if (!isTextReadingSheet())
        {
            if (inputReponse1.text == "" || inputReponse2.text == "" || inputReponse3.text == "" )
            return false;
        }
        return true;
        
    }
    

}
