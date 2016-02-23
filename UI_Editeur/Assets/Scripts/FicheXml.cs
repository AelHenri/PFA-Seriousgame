using UnityEngine;
using System.Collections;
using System.Xml.Linq;


/* 
 * C'est ce scrpit qui s'occupe de tout ce qui est génération :
 * Créer le dossier
 * Generer la Fiche xml
 * Copier les images
 */


public class FicheXml : MonoBehaviour {
    public AjoutImage ajt;
    public Validation vali;
    XDocument fiche;

    string valueRep1;
    string valueRep2;
    string valueRep3;

    string nomFiche;
    string textExemple;
    string textReponse1;
    string textReponse2;
    string textReponse3;

    string cheminFiche;
    string destImageExemple;
    string destImageQuestion;
    string destFiche;

    /*
     * Récupère les valeurs des différents textes entrés
     * Récupère quelle est la bonne et quelles sont les mauvaise réponses
     * Initialise les chemins de destintation( Dossier, images, fiche)
     */
    void Start () {

        valueRep1 = vali.toggleRep1.isOn.ToString().ToLower();
        valueRep2 = vali.toggleRep2.isOn.ToString().ToLower();
        valueRep3 = vali.toggleRep3.isOn.ToString().ToLower();

        nomFiche = vali.nomFiche.text;
        textExemple = vali.inputExemple.text;
        textReponse1 = vali.inputReponse1.text;
        textReponse2 = vali.inputReponse2.text;
        textReponse3 = vali.inputReponse3.text;

        cheminFiche = Application.dataPath + "/../Fiches/";
        cheminFiche = System.IO.Path.Combine(cheminFiche, nomFiche);

        destImageExemple = System.IO.Path.Combine(cheminFiche, "image_exemple.jpg");
        destImageExemple = System.IO.Path.GetFullPath(destImageExemple);

        destImageQuestion = System.IO.Path.Combine(cheminFiche, "image_question.jpg");
        destImageQuestion = System.IO.Path.GetFullPath(destImageQuestion);
        
    }
	

    public void genererFiche()
    {
        fiche = new XDocument(new XElement("FicheQCM",
                                   new XElement("title", nomFiche ),

                                   new XElement("partieExemple",
                                        new XElement("text", textExemple)
                                        //new XElement("image", destImageExemple) // Path a modifier vu que les images seront copiées dans le même dossier que la fiche
                                                ),//</partieExemple>
                                   new XElement("partieQuestion",
                                        new XElement("question"),             //utile ?
                                        //new XElement("image", destImageQuestion),
                                        new XElement("answer1", textReponse1, new XAttribute("value", valueRep1)),
                                        new XElement("answer2", textReponse2, new XAttribute("value", valueRep2)),
                                        new XElement("answer3", textReponse3, new XAttribute("value", valueRep3))
                                        )//</partieQuestion>
                                            )//</FicheQCM>
                                    );


        destFiche = System.IO.Path.Combine(cheminFiche, nomFiche + ".xml");
        fiche.Save(destFiche); //A sauver dans un dossier aver les images de la fiches.
    }

    public void copierImages()
    {

        if (ajt.imagePathIndic != destImageExemple)
            System.IO.File.Copy(ajt.imagePathIndic, destImageExemple, true);

        if (ajt.imagePathQues != destImageQuestion)
            System.IO.File.Copy(ajt.imagePathQues, destImageQuestion, true);
    }

    public void creerDossierFiche()
    {
        this.Start();
        if (!System.IO.Directory.Exists(cheminFiche))
            System.IO.Directory.CreateDirectory(cheminFiche);
    }
}
