using UnityEngine;
using System.Collections;
using System.Xml.Linq;

public class Sheet {
    XDocument xmlFile;

    //string sheetName;
    public string textExemple;

    public string[] answers;
    public string imgExemplePath, imgQuestionPath;

    private string dirName;

    int rightAnswer;
    

    public Sheet(string Path)
    {
        xmlFile = XDocument.Load(Path);
       // sheetName = xmlFile.Root.Element("title").Value;
        textExemple = xmlFile.Root.Element("partieExemple").Element("text").Value;

        imgExemplePath = Path;
        imgQuestionPath = Path; 
        answers = new string[3];
        answers[0] = xmlFile.Root.Element("partieQuestion").Element("answer1").Value;
        answers[1] = xmlFile.Root.Element("partieQuestion").Element("answer2").Value;
        answers[2] = xmlFile.Root.Element("partieQuestion").Element("answer3").Value;

        dirName = System.IO.Path.GetDirectoryName(Path);
        imgExemplePath = System.IO.Path.Combine(dirName, "image_exemple.jpg");
        imgQuestionPath = System.IO.Path.Combine(dirName, "image_question.jpg");

        if (xmlFile.Root.Element("partieQuestion").Element("answer1").Attribute("value").ToString().Equals("value=\"true\""))
            rightAnswer = 1;
        else if (xmlFile.Root.Element("partieQuestion").Element("answer2").Attribute("value").ToString() == "value=\"true\"")
            rightAnswer = 2;
        else
            rightAnswer = 3;

    }


    
    public string[] getAnswers()
    {
        return answers;
    }

    public string[] getImagesPath()
    {
        string[] imgsPath = new string[2];
        imgsPath[0] = imgExemplePath;
        imgsPath[1] = imgQuestionPath;
        return imgsPath;
    }



    public bool isRightAnswer(int myAnsmer)
    {
        return myAnsmer == rightAnswer;
    }

    // Use this for initialization
    void Start () {

	
	}
	

   
	// Update is called once per frame
	void Update () {
	
	}
}
