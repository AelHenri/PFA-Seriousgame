using UnityEngine;
using System.Collections;

public class OuvrirFicheXml : MonoBehaviour {

    bool showFileBrowser = false;
    string xmlFilePath;

    FileBrowser fileBrowser = new FileBrowser();


    /* Paramètres pour le skin de l'explorateur de fichier, les valeurs a modifier sont dans Unity */
    public GUISkin[] skins;
    public Texture2D file, folder, back, drive;
    public GUIStyle backStyle, cancelStyle, selectStyle;
    string[] layoutTypes = { "Type 0", "Type 1" };


    public void openXmlFile()
    {
        showFileBrowser = true;
    }


    void Start() {

        /* Initialisation du File Browser */
        fileBrowser.fileTexture = file;
        fileBrowser.directoryTexture = folder;
        fileBrowser.backTexture = back;
        fileBrowser.driveTexture = drive;
        fileBrowser.showSearch = true;
        fileBrowser.searchRecursively = true;
    }

    void OnGUI()
    {




        if (showFileBrowser)
        {

            /*
             * Pour gérer les skins/ modes de vues
             */
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Label("Layout Type");
            fileBrowser.setLayout(GUILayout.SelectionGrid(fileBrowser.layoutType, layoutTypes, 1));
            GUILayout.Space(10);
            //select from available gui skins
            GUILayout.Label("GUISkin");
            foreach (GUISkin s in skins)
            {
                if (GUILayout.Button(s.name))
                {
                    fileBrowser.guiSkin = s;
                }
            }
            GUILayout.Space(10);

            /* Récupération du chemin vers la fiche Xml */
            if (fileBrowser.draw())
            {
                if (fileBrowser.outputFile == null)
                {
                    showFileBrowser = false;
                }

                else
                {
                    xmlFilePath = fileBrowser.outputFile.FullName﻿.ToString();
                    Debug.Log(xmlFilePath);
                }

            }
        }
    }


}
