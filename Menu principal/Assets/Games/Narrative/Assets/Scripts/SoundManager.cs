using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    
    public AudioSource efxSource;                   
    public AudioSource beginningMusicSource;                
    public AudioSource villageMusicSource;
    public AudioSource rootsMusicSource;
    public AudioSource endMusicSource;
    public static SoundManager instance = null;                 
    public float lowPitchRange = .95f;              
    public float highPitchRange = 1.05f;            

    private StorySceneManager sceneManager;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
        if(GameState.narrativeSound == null)//Destroy taking some time, ensures the pointer to the remaining instance is not overwritten
            GameState.narrativeSound = transform.gameObject;//Pointer to destroy itself when quitting the game

        //beginningMusicSource.Play();
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }

    void Update()
    {
        sceneManager = (StorySceneManager)FindObjectOfType(typeof(StorySceneManager));
        if (sceneManager.level >= 2 && beginningMusicSource.isPlaying)
        {
            beginningMusicSource.Stop();
            villageMusicSource.Play();
        }

        if (sceneManager.level >= 4 && villageMusicSource.isPlaying)
        {
            villageMusicSource.Stop();
            rootsMusicSource.Play();
        }

        /*if (sceneManager.level >= 6 && rootsMusicSource.isPlaying)
        {
            rootsMusicSource.Stop();
            villageMusicSource.Play();
        }*/

        if (sceneManager.level >= 7 && rootsMusicSource.isPlaying)
        {
            rootsMusicSource.Stop();
            endMusicSource.Play();
        }
    }
}
