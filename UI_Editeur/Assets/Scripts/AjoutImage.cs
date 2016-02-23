using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class AjoutImage : MonoBehaviour
{
    private int bottomLayerNumber = 1;
    public OuvrirFicheXml ovFich;
    /* Sert a afficher ou non l'explorateur de fichiers */
	private bool showFileBrowser = false;

    /* Variables pour afficher & charger le images */
    [HideInInspector]
    public string imagePathIndic = null;
    private bool showAjoutImageIndic = true; 
    private bool loadImageIndic = false;

    [HideInInspector]
    public string imagePathQues = null;
    private bool showAjoutImageQues = true;
    private bool loadImageQues = false;
    

	FileBrowser fb = new FileBrowser();
    [HideInInspector]
    public Texture2D img_indication = null;
    [HideInInspector]
    public Texture2D img_question = null;
    WWW www;

    /* Variables permettant d'afficher ou non les bouttons correspondants, CF fonction Update() */
    public CanvasGroup canvasGImgButton1; 
    public CanvasGroup canvasGimgAnnule; 
    public CanvasGroup canvasGImgAjouetImg2;
    public CanvasGroup canvasGImgAnnule2;

    /* Pour obtenir la taille du Panel et bien positionner l'image */
    public GameObject PanelExemple; 
    float panelHeightExemple;
    float panelWidthExemple;
    public GameObject Canvas;
    float canvasHeight;
    float canvasWidth;
    float imageWitdh;
    float imageHeight;


    /* Paramètres pour le skin de l'explorateur de fichier */
    public GUISkin[] skins;
    public Texture2D file, folder, back, drive;
    public GUIStyle backStyle, cancelStyle, selectStyle; 
    string[] layoutTypes = { "Type 0", "Type 1" };

    Rect rectImgExemple;
    float xImgExemple, yImgExemple;
    public RawImage rawImageExemple;


    Rect rectImgQuestion;
    float xImgQuestion, yImgQuestion;
    public RawImage rawImageQuestion;



    public void ajouterImageExemple()
    {
        Debug.Log("click ajout img ex");
        showFileBrowser = true;
        loadImageIndic = true;
    }

    public void enleverImageExemple()
    {
        
        showAjoutImageIndic = true;
        imagePathIndic = null;
        rawImageExemple.gameObject.SetActive(false);
    }

    public void ajouterImageQuestion()
    {

        showFileBrowser = true;
        loadImageQues = true;
        Debug.Log("click ajout img ques");
    }

    public void enleverImageQuestion()
    {
        showAjoutImageQues = true;
        imagePathQues = null;
        rawImageQuestion.gameObject.SetActive(false);
    }


    void Start()
    {
        //setup file browser style
        //  fb.guiSkin = skins[0]; //set the starting skin

        //set the various textures
        fb.fileTexture = file;
        fb.directoryTexture = folder;
        fb.backTexture = back;
        fb.driveTexture = drive;
        //show the search bar
        fb.showSearch = true;
        //search recursively (setting recursive search may cause a long delay)
        fb.searchRecursively = true;

        panelHeightExemple = PanelExemple.GetComponent<RectTransform>().rect.height;
        panelWidthExemple = PanelExemple.GetComponent<RectTransform>().rect.width;

        canvasHeight = Canvas.GetComponent<RectTransform>().rect.height;
        canvasWidth = Canvas.GetComponent<RectTransform>().rect.width;

        img_indication = new Texture2D(960, 720, TextureFormat.DXT1, false);
        imagePathIndic = null;

        img_question = new Texture2D(960, 720, TextureFormat.DXT1, false);
        imagePathQues = null;

        imageWitdh = Screen.width / 3;
        imageHeight = imageWitdh;



    }
   
    
    /*
     * Fonction qui charge l'image dans la texture "img_indication" pour pouvoir être affichée
     */
   public IEnumerator LoadATexture(string st, Texture2D img)
    {
        www = new WWW(st);
        yield return www;

        www.LoadImageIntoTexture(img);
    }

    public void desactiverBouttonAjout()
    {
        canvasGImgButton1.interactable = false;
        canvasGImgButton1.alpha = 0;
        canvasGImgAjouetImg2.interactable = false;
        canvasGImgAjouetImg2.alpha = 0;
    }

    public void activerBouttonAjout(){
        canvasGImgButton1.interactable = true;
        canvasGImgAjouetImg2.interactable = true;

    }

    /* 
     * Cette fonction est appelée avant chaque rendu d'image 
     * C'est ici qu'on ajoute tout les éléments que l'on souhaite afficher a l'écran
     * l'explorateur de fichier, les images des QCM
     */   
    void OnGUI(){

        GUI.depth = bottomLayerNumber;
        
        xImgQuestion = rawImageQuestion.gameObject.transform.position.x - (rawImageQuestion.rectTransform.rect.width/2);
        yImgQuestion = rawImageQuestion.gameObject.transform.position.y - rawImageQuestion.rectTransform.rect.height + 15;
        Rect rectImgQuestion = new Rect(xImgQuestion, yImgQuestion, rawImageQuestion.rectTransform.rect.width, rawImageQuestion.rectTransform.rect.height);
        



        xImgExemple= rawImageExemple.gameObject.transform.position.x - (rawImageExemple.rectTransform.rect.width/2);
        yImgExemple = rawImageExemple.gameObject.transform.position.y - (rawImageExemple.rectTransform.rect.height/2);
        Rect rectImgExemple = new Rect(xImgExemple, yImgExemple, rawImageExemple.rectTransform.rect.width, rawImageExemple.rectTransform.rect.height);
        

        /* Partie qui affiche l'image sélectionnée */
        if (img_indication != null && imagePathIndic != null)
        {
            rawImageExemple.texture = img_indication;
            rawImageExemple.gameObject.SetActive(true);


            /* affiche un bouton pour changer l'image quand les souris survole l'image */
            Input.GetMouseButton(1);
                {
                if (rectImgExemple.Contains(Event.current.mousePosition) && showFileBrowser == false && !ovFich.isSearchingFile())
                    if (GUI.Button(rectImgExemple, "Changer l'image"))
                    {
                        showFileBrowser = true;
                        loadImageIndic = true;
                    }      
            }
            showAjoutImageIndic = false;
        }

        if (img_question != null && imagePathQues != null)
        {

            rawImageQuestion.texture = img_question;
            rawImageQuestion.gameObject.SetActive(true);
            /* affiche un bouton pour changer l'image quand les souris survole l'image */
            Input.GetMouseButton(1);
            {
                if (rectImgQuestion.Contains(Event.current.mousePosition) && showFileBrowser == false && !ovFich.isSearchingFile())
                    if (GUI.Button(rectImgQuestion, "Changer l'image"))
                    {
                        showFileBrowser = true;
                        loadImageQues = true;
                    }
            }
            showAjoutImageQues = false;
        }




        if (showFileBrowser) {
            /*
             * Pour gérer les skins/ modes de vues
             */
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Layout Type");
            fb.setLayout(GUILayout.SelectionGrid(fb.layoutType, layoutTypes, 1));
            GUILayout.Space(10);
            //select from available gui skins
            GUILayout.Label("GUISkin");
            foreach (GUISkin s in skins)
            {
                if (GUILayout.Button(s.name))
                {
                    fb.guiSkin = s;
                }
            }
            GUILayout.Space(10);
            
            /* 
             * Partie qui gère l'affichage de l'explorateur de fichier
             * et la récupération du chemin de l'image à afficher
             */
            if (fb.draw ()) {
                if (fb.outputFile == null)
                {
                    Debug.Log("Cancel hit");
                    showFileBrowser = false;
                    loadImageIndic = false;
                    loadImageQues = false;
                }
                else if (loadImageIndic)
                {
                    imagePathIndic = fb.outputFile.FullName﻿.ToString();


                    StartCoroutine(LoadATexture( ("file:///" + imagePathIndic), img_indication));
                    showFileBrowser = false;
                    loadImageIndic = false;
                    loadImageQues = false;
                }
                else if(loadImageQues)
                {
                    imagePathQues = fb.outputFile.FullName﻿.ToString();


                    StartCoroutine(LoadATexture( ("file:///" + imagePathQues), img_question));
                    showFileBrowser = false;
                    loadImageIndic = false;
                    loadImageQues = false;
                    

                }

					
			}
		}
		


    }


    /* 
     * Ici on désactive ou active les boutons d'ajout d'image et d'annulation
     */
    void Update()
    {

        if (showFileBrowser)
        {
            this.desactiverBouttonAjout();
            ovFich.desactiverBouttonOuvrir();
        }
        else
        {
            activerBouttonAjout();
            ovFich.activerBouttonOuvrir();

            if (showAjoutImageIndic == false)
            {
                canvasGImgButton1.interactable = false;
                canvasGImgButton1.alpha = 0;

                canvasGimgAnnule.interactable = true;
                canvasGimgAnnule.alpha = 1;
            }

            else if (showAjoutImageIndic == true)
            {
                //canvasGImgButton1.interactable = true;
                canvasGImgButton1.alpha = 1;

                canvasGimgAnnule.interactable = false;
                canvasGimgAnnule.alpha = 0;
            }

            if (showAjoutImageQues == false)
            {
                canvasGImgAjouetImg2.interactable = false;
                canvasGImgAjouetImg2.alpha = 0;

                canvasGImgAnnule2.interactable = true;
                canvasGImgAnnule2.alpha = 1;
            }

            else if (showAjoutImageQues == true)
            {
                //canvasGImgAjouetImg2.interactable = true;
                canvasGImgAjouetImg2.alpha = 1;

                canvasGImgAnnule2.interactable = false;
                canvasGImgAnnule2.alpha = 0;
            }
        }

    }

 
}
