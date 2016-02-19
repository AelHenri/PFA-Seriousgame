using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class Exemple : MonoBehaviour {

    public Text exempleText;
    [HideInInspector]
    Texture2D img_exemple = null;
    WWW www;

    Rect rectImgExemple;

    public Questionnaire questionnaire;


    public void initExemple(Sheet sheet)
    {
        exempleText.text = sheet.textExemple;
    }



    // Use this for initialization
    void Start () {
        rectImgExemple = new Rect(Screen.width/4, Screen.height/4, 450, 350);
    }

    // Update is called once per frame
    void Update()
    {
        exempleText.text = questionnaire.currentSheet.textExemple;

        if (img_exemple == null) {
            img_exemple = LoadPNG(questionnaire.currentSheet.imgExemplePath);
        }
     
    }


    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    void OnGUI()
    {
        if (img_exemple != null)
         GUI.DrawTexture(rectImgExemple, img_exemple);
    }




}
