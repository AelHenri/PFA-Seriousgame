using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.UI;
using System.IO;

public class ProfileManager : MonoBehaviour {




    public Dropdown profileSelector;
    //public Text text;
  

    private string profilesDir;
    private string[] profilesName;
    private string[] profilesPath;

    private XDocument xmlProfile;


    // Use this for initialization
    void Start () {
        profileSelector.ClearOptions();

        profilesDir = Application.dataPath + "/../Profiles";
        profilesDir = System.IO.Path.GetFullPath(profilesDir);

        /* Gets all the profiles stored in xml files */
        profilesPath = System.IO.Directory.GetFiles(profilesDir, "*.xml", System.IO.SearchOption.AllDirectories);
        profilesName = new string[profilesPath.Length];

        /* Puts the student name, written in his xml profile */
        for (int i = 0; i < profilesPath.Length; i++)
        {
            xmlProfile = XDocument.Load(profilesPath[i]);
            profilesName[i] = xmlProfile.Root.Element("Name").Value;
        }

        foreach (string s in profilesName)
        {
            profileSelector.options.Add(new Dropdown.OptionData() { text = s });     
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        //text.text = profilesName[profileSelector.value];

    }
}
