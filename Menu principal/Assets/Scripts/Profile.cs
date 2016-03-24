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

    List<string> correctlyAnswered;
    List<bool> collectedObjects;

    public Profile(string firstName, string lastName)
    {
        this.fisrtName = firstName;
        this.lastName = lastName;

        isGameFinished = new bool[3]; // There are only 3 Games
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
