using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour {





  

    private string profilesDir;
    private int profilesCount;
    
    private string[] profilesPath;
    private Profile[] profiles;
    private int currentProfileIndex;
    Profile currentProfile = null;
    Questionnaire questionnaire;

    private XDocument xmlProfile;


    // Use this for initialization
    void Start() {
        questionnaire = GameObject.Find("Navigator").GetComponent<Questionnaire>();

            #if UNITY_ANDROID
                    profilesDir = Application.persistentDataPath + "/../../../../SeriousGame/Profiles"; //Doesn't Work, we need to enable Write Access
            #endif
            #if UNITY_EDITOR
            profilesDir = Application.dataPath + "/../Profiles";
            #endif

        profilesDir = Path.GetFullPath(profilesDir);

        
        if (!Directory.Exists(profilesDir))
        {
            Directory.CreateDirectory(profilesDir);
        }
     
 
        refreshProfiles();
        

        if (profilesPath == null)
        {
            return;
        }


        currentProfileIndex = 0;
        currentProfile = null;
    }
	
    /*
     * Scans / Re Scans the profileDir
     */
    public void refreshProfiles()
    {
        profilesPath = Directory.GetFiles(profilesDir, "*.profile", SearchOption.AllDirectories);
        profilesCount = profilesPath.Length;
        profiles = new Profile[profilesCount];
        for (int i = 0; i < profilesCount; i++)
        {
            Debug.Log(profilesDir);
            profiles[i] = loadExistingProfile(profilesPath[i]);
        }
    }
    public Profile[] getProfiles()
    {
        return profiles;
    }

    public bool thereIsAProfile()
    {
        return currentProfile != null;
    }

   public void saveNewProfile(string firstName, string lastName)
    {
        BinaryFormatter bf = new BinaryFormatter();
        Profile pro = new Profile(firstName, lastName);
        FileStream file = File.Open(Application.dataPath + "/../Profiles/" + pro.getFileName(), FileMode.Create);
        

        bf.Serialize(file, pro);
        file.Close();
    }

    public void saveExistingProfile(Profile profileToBeSaved)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/../Profiles/" + profileToBeSaved.getFileName(), FileMode.Create);
        bf.Serialize(file, profileToBeSaved);
        file.Close();
    }

    Profile loadExistingProfile(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);
        Profile profile = (Profile)bf.Deserialize(file);
        
        
        file.Close();
        return profile;
    }
    public void setCurrentProfile(int index)
    {
        if (index < 0)
        {
            currentProfile = null;
            currentProfileIndex = 0;
        }
        else
        {
            if (currentProfile != null) //means that a profile is already selected, thus we need to save it before changing to another profile
            {
                questionnaire.updateCurrentProfile();
                saveExistingProfile(currentProfile);
            }
            currentProfileIndex = index;
            currentProfile = profiles[index];
            questionnaire.setCurrentProfile(currentProfile);
            questionnaire.updateAccordindTo(currentProfile);
        }
    }

    public int getCurrentProfileIndex()
    {
        return currentProfileIndex;
    }
    public Profile getCurrentProfile()
    {
        return currentProfile;
    }

    public void saveCurrentProfile()
    {
        if (currentProfile != null)
        {
            questionnaire.updateCurrentProfile();
            saveExistingProfile(currentProfile);
        }
    }
    void OnApplicationQuit()
    {
        saveCurrentProfile();
    }
     void OnApplicationPause()
    {
        saveCurrentProfile();
    }


}
