using UnityEngine;
using System.Collections;

public abstract class Games : MonoBehaviour {
    private GameObject Navigator;
    struct GameStateLog
    {
        Time time;
        string gameParameters;
        string chosenMCQ;
        string gameLevel;
        string userInput;
        float answeringTime;
    }
    struct Statistics{//Used to record game logs
        string gameName;
        string userName;
        GameStateLog gameState;
        bool[] questionsDone;
        float averageAnsweringTime;
        float goodAnswersRate;
        float averageTimeOnGoodAnswers;
        float averageTimeOnBadAnswers;
    }
    private bool[] achievementsList;
    private Statistics[] statisticsList;
    private GameObject presentationImage;//shown on title screen
    public bool achievementsChanged;//Used to warn navigator
    public bool statisticsChanged;//Used to warn navigator

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //not destroyed on other scenes
	}

    void Userinput()
    {

    }
    
    Statistics[] GetStatistics()
    {
        return new Statistics[2];
    }


    bool[] Getachievements()
    {
        return new bool[2];
    }


    public void Information ()
    {
        //used when printing image on title screen?
    }
}
