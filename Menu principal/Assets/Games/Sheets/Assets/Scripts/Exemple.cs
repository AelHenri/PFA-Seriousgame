using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
public class Exemple : MonoBehaviour {

    public Text exempleText;
    [HideInInspector]
    Texture2D img_exemple = null;
    WWW www;



    public RawImage rawImageExemple;


    public void initExemple(Sheet sheet)
    {
        exempleText.text = sheet.textExemple;
    }



    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("GlobalQ:"+ GlobalQuestionnaire.currentSheet);
        exempleText.text = GlobalQuestionnaire.currentSheet.textExemple;

        if (img_exemple == null) {
            img_exemple = LoadPNG(GlobalQuestionnaire.currentSheet.imgExemplePath);
            rawImageExemple.texture = img_exemple;
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





}
