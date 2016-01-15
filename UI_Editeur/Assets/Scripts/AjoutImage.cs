using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class AjoutImage : MonoBehaviour
{
    /* Sert a afficher ou non l'explorateur de fichiers */
	private bool guiEnable = false;

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




    public void ajouterImageExemple()
    {
        guiEnable = true;
        loadImageIndic = true;
    }

    public void enleverImageExemple()
    {
        
        showAjoutImageIndic = true;
        imagePathIndic = null;
    }

    public void ajouterImageQuestion()
    {
        guiEnable = true;
        loadImageQues = true;
    }

    public void enleverImageQuestion()
    {
        showAjoutImageQues = true;
        imagePathQues = null;
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



    /* 
     * Cette fonction est appelée avant chaque rendu d'image 
     * C'est ici qu'on ajoute tout les éléments que l'on souhaite afficher a l'écran
     * l'explorateur de fichier, les images des QCM
     */   
    void OnGUI(){


        /* Partie qui affiche l'image sélectionnée */
        if (img_indication != null && imagePathIndic != null)
        {
            GUI.DrawTexture(new Rect((panelWidthExemple / 2) - imageWitdh/2, (panelHeightExemple / 2) - imageHeight/2, imageWitdh, imageHeight), img_indication);
            showAjoutImageIndic = false;
        }

        if (img_question != null && imagePathQues != null)
        {
            GUI.DrawTexture(new Rect(canvasWidth - panelWidthExemple + imageWitdh/6, canvasHeight - panelHeightExemple + imageHeight/6, imageWitdh, imageHeight), img_question);
            showAjoutImageQues = false;
        }




        if (guiEnable) {
            Debug.Log(guiEnable);

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
                    guiEnable = false;
                }
                else if (loadImageIndic)
                {
                    imagePathIndic = fb.outputFile.FullName﻿.ToString();


                    StartCoroutine(LoadATexture( ("file:///" + imagePathIndic), img_indication));
                    guiEnable = false;
                    loadImageIndic = false;
                }
                else if(loadImageQues)
                {
                    imagePathQues = fb.outputFile.FullName﻿.ToString();


                    StartCoroutine(LoadATexture( ("file:///" + imagePathQues), img_question));
                    guiEnable = false;
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
        
        if (showAjoutImageIndic == false)
        {
            canvasGImgButton1.interactable = false;
            canvasGImgButton1.alpha = 0;

            canvasGimgAnnule.interactable = true;
            canvasGimgAnnule.alpha = 1;
        }

        else if (showAjoutImageIndic == true)
        {
            canvasGImgButton1.interactable = true;
            canvasGImgButton1.alpha = 1 ;

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
            canvasGImgAjouetImg2.interactable = true;
            canvasGImgAjouetImg2.alpha = 1;

            canvasGImgAnnule2.interactable = false;
            canvasGImgAnnule2.alpha = 0;
        }      


    }

 
}
