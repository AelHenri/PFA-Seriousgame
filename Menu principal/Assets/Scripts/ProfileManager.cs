using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProfileManager : MonoBehaviour {





  

    private string profilesDir;
    private int profilesCount;
    
    private string[] profilesPath;
    private Profile[] profiles;
    Profile currentProfile = null;
    Questionnaire questionnaire;

    private XDocument xmlProfile;


    // Use this for initialization
    void Start() {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();


        profilesDir = Application.dataPath + "/../Profiles";
        profilesDir = Path.GetFullPath(profilesDir);

        //saveNewProfile();
        if (!Directory.Exists(profilesDir))
        {
            profilesPath = null;
            profiles = null;
        }
     else
        {
            /* Gets all the profiles */
            profilesPath = Directory.GetFiles(profilesDir, "*.profile", SearchOption.AllDirectories);
            profilesCount = profilesPath.Length;

            profiles = new Profile[profilesCount];
            for (int i = 0; i < profilesCount; i++)
            {
                profiles[i] = loadExistingProfile(profilesPath[i]);
            }
        }

        if (profilesPath == null)
        {
            Debug.Log("Pas de profiles dans le dossier");
            return;
        }


       
       //loadExistingProfile();
    }
	

    public Profile[] getProfiles()
    {
        return profiles;
    }



    //TODO better Naming
    void saveNewProfile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Profile pro = new Profile("Thomas", "Montegrave");
        FileStream file = File.Open(Application.dataPath + "/../Profiles/playerInfo.profile", FileMode.Create);
        //SheetInfos s = new SheetInfos("koukou", 1, 1, 1);
        Debug.Log(new SheetInfos("Elepant", 2, 0, 5).sheetName);
        pro.addSheetInfo(new SheetInfos("koukou", 1, 1, 1));

        bf.Serialize(file, pro);
        file.Close();


    }

    void saveExistingProfile(Profile profileToBeSaved)
    {

    }
    //TODO BETTER NAMING
    Profile loadExistingProfile(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Open(Application.dataPath + "/../Profiles/playerInfo.profile", FileMode.Open);
        FileStream file = File.Open(path, FileMode.Open);
        

        Profile profile = (Profile)bf.Deserialize(file);
        
       /* Debug.Log("Prénom: " + profile.getFirstName() + "Nom: " + profile.getLastName());
        Debug.Log("Sheet count: " + profile.getSheetList().Count);
        Debug.Log(profile.getSheetList()[0]);
        */
        file.Close();
        return profile;
    }
    public void setCurrentProfile(int index)
    {
        Debug.Log("Set Current Profile");
        currentProfile = profiles[index];
        questionnaire.updateAccordindTo(currentProfile);
        
    }
	// Update is called once per frame
	void Update () {


    }
}
