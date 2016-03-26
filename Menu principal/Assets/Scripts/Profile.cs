using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
public class Profile {

    string fisrtName;
    string lastName;
    bool []isGameFinished;

    List<Sheet> sheetList;
    List<bool> collectedObjects;

    public Profile(string firstName, string lastName)
    {
        this.fisrtName = firstName;
        this.lastName = lastName;
        sheetList = new List<Sheet>();

        isGameFinished = new bool[3]; // There are only 3 Games
    }

    public List<Sheet> getRealSheetList()
    {
        sheetList.Sort();
        return sheetList;
    }

    public void updateSheetList(List<Sheet> l)
    {
        sheetList = l;
    }
    public string getFileName()
    {
        return getFirstName() + "_" + getLastName() + ".profile";
    }
   public string getFirstName()
    {
        return fisrtName;
    }

    public string getLastName()
    {
        return lastName;
    }
}
