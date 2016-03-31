using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[Serializable]
public abstract class Sheet : Comparer<Sheet>, IComparable<Sheet>
{

    [NonSerialized]
    protected XDocument xmlFile;
    [NonSerialized]
    protected Scene exempleScene;
    [NonSerialized]
    protected Scene questionScene;

    public string sheetName;
    private int successCount;
    private int failureCount;
    public int sheetNumber;

    [NonSerialized]
    public string textExemple;
    [NonSerialized]
    public string imgExemplePath;
    [NonSerialized]
    protected string dirName;

    //TODO should not ne used
    public string sheetStyle;


    public Sheet(string Path)
    {
        xmlFile = XDocument.Load(Path);
        sheetName = xmlFile.Root.Element("title").Value;
        sheetNumber = Int32.Parse(xmlFile.Root.Element("number").Value);
        textExemple = xmlFile.Root.Element("ExamplePart").Element("text").Value;


        imgExemplePath = Path;
        dirName = System.IO.Path.GetDirectoryName(Path);
        imgExemplePath = System.IO.Path.Combine(dirName, "image_exemple.jpg");
        sheetStyle = xmlFile.Root.Element("type").Value;
    }



    public void incrementSuccesCount()
    {
        this.successCount++;
    }
    public void incrementFailureCount()
    {
        this.failureCount++;
    }


    public int getSheetNumber()
    {
        return sheetNumber;
    }

    public string getName()
    {
        return sheetName;
    }
    public int getSuccesCount()
    {
        return successCount;
    }
    public int getFailureCount()
    {
        return failureCount;
    }



    public abstract IEnumerator loadExemple();
    public abstract IEnumerator loadQuestion();
    public abstract void endSheet();

    /*
     * Needed in order to be able to use List<Sheet>.Sort(), the number of the Sheet represent it's difficulty
     */
    override
    public int Compare(Sheet x, Sheet y)
    {
        if (x.getSheetNumber() < y.getSheetNumber())
            return -1;
        else if (x.getSheetNumber() > y.getSheetNumber())
            return 1;
        else
            return 0;
    }

    public int CompareTo(Sheet x)
    {
        if (this.getSheetNumber() < x.getSheetNumber())
            return -1;
        else if (this.getSheetNumber() > x.getSheetNumber())
            return 1;
        else
            return 0;
    }
}
