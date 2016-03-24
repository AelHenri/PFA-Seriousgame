using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProfileManager : MonoBehaviour {




    public Dropdown profileSelector;
    public Text text;
  

    private string profilesDir;
    private int profilesCount;
    
    private string[] profilesPath;
    private Profile[] profiles;

    private XDocument xmlProfile;


    // Use this for initialization
    void Start() {
        profileSelector.ClearOptions();

        openProfilesDir();
        
        /* Gets all the profiles stored in xml files */
        profilesPath = Directory.GetFiles(profilesDir, "*.profile", SearchOption.AllDirectories);
        profilesCount = profilesPath.Length;
        if (profilesPath == null)
        {
            Debug.Log("Pas de profiles dans le dossier");
            return;
        }
        profiles = new Profile[profilesCount];
        for (int i = 0; i < profilesCount; i++)
        {
            profiles[i] = loadExistingProfile(profilesPath[i]);
        }

        foreach (Profile p in profiles)
        {
            profileSelector.options.Add(new Dropdown.OptionData() { text = p.getFirstName() });     
        }

       //saveNewProfile();
        //loadExistingProfile();
    }
	
    void openProfilesDir()
    {
        try
        {
            profilesDir = Application.dataPath + "/../Profiles";
            profilesDir = Path.GetFullPath(profilesDir);
        }
        catch (DirectoryNotFoundException)
        {
            Debug.Log("Pas de dossier profils");
        }
    }

    //TODO better Naming
    void saveNewProfile()
    {
        BinaryFormatter bf = new BinaryFormatter();
        Profile pro = new Profile("Vladimir", "Swain");
        FileStream file = File.Open(Application.dataPath + "/../Profiles/playerInfo.profile", FileMode.Create);


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
        /*
        Debug.Log("Prénom: " + profile.getFirstName());
        Debug.Log("Nom: " + profile.getLastName());
        */
        file.Close();
        return profile;
    }

	// Update is called once per frame
	void Update () {

        text.text = profiles[profileSelector.value].getFirstName();

    }
}
