using UnityEngine;
using System.Collections;

public class SheetWithAnswerText : Sheet{

    public string[] answers;


    public SheetWithAnswerText(string path) : base(path)
    {
        answers = new string[3];
        answers[0] = xmlFile.Root.Element("QuestionPart").Element("answer1").Value;
        answers[1] = xmlFile.Root.Element("QuestionPart").Element("answer2").Value;
        answers[2] = xmlFile.Root.Element("QuestionPart").Element("answer3").Value;
    }
}
