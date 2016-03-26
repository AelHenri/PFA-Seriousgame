using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public struct SheetInfos : IComparer<SheetInfos>, IComparable<SheetInfos>
{
    public string sheetName;
    public int sheetNumber;
    public int errorCount;
    public int succesCount;


    public int Compare(SheetInfos x, SheetInfos y)
    {
        if (x.sheetNumber< y.sheetNumber)
            return -1;
        else if (x.sheetNumber > y.sheetNumber)
            return 1;
        else
            return 0;
    }

    public int CompareTo(SheetInfos x)
    {
        if (this.sheetNumber < x.sheetNumber)
            return -1;
        else if (this.sheetNumber > x.sheetNumber)
            return 1;
        else
            return 0;
    }


    public SheetInfos (string name, int number, int error, int sucess)
    {
        this.sheetName = name;
        this.sheetNumber = number;
        this.errorCount = error;
        this.succesCount= sucess;
    }

    public void addSucces()
    {
        this.sheetNumber++;
    }
    public void addFailure()
    {
        this.errorCount++;
    }

    override
    public string ToString()
    {
        return "sheetName: " + sheetName + " sheetNumber: " + sheetNumber + " errorCount: " + errorCount + " succesCount: " + succesCount; 
    }
}
