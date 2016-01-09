using UnityEngine;
using System.Collections;

public class validation : MonoBehaviour {

    
    public CanvasGroup canvasGValidation;
    public CanvasGroup canvasGConfirmation;
    public ajout_image ajt;
    

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
    }

    public void onClickConfirmation()
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
