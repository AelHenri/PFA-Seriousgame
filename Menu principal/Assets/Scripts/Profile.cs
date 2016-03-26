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

    List<SheetInfos> sheets;
    List<bool> collectedObjects;

    public Profile(string firstName, string lastName)
    {
        this.fisrtName = firstName;
        this.lastName = lastName;
        sheets = new List<SheetInfos>();

        isGameFinished = new bool[3]; // There are only 3 Games
    }

    public void addSheetInfo(SheetInfos s)
    {
        sheets.Add(s);
    }

    public List<SheetInfos> getSheetList()
    {
        sheets.Sort();
        return sheets;
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
