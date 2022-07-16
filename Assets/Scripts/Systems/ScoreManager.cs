using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour{

    #region Public Variables
    [Header("Game Objects")]
    [Tooltip("The Text that will show the score")]
    [SerializeField] public Text ScoreDisplay;

    [Tooltip("The timer used to keep track of time")]
    [SerializeField] public TimerScript TimerObject;

    [Tooltip("Holds the instance of a Score Manager to prevent duplicates")]
    [SerializeField] public static ScoreManager instance;
    #endregion

    #region Private Variables
    [Tooltip("Checks how many checkpoints a player has encountered, 2 for pick up 4 for delivery")]
    [SerializeField] private int CheckpointCounter = 0;

    [Tooltip("A player's score")]
    [SerializeField] private int DeliveriesMade = 0;

    #endregion

    void Awake(){
        if(instance == null){
            //If not preserve itself
            //Sets this so this doesn't get destroyed on load
            instance = this;
            DontDestroyOnLoad(this);
        } else if (instance != this){
            //If the object already exists, then just delete itself
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start(){
        ScoreDisplay = GameObject.Find("Score Text").GetComponent<Text>();
        TimerObject = GameObject.Find("Timer").GetComponent<TimerScript>();
        UpdateScoreText(); 
    }

    // Update is called once per frame
    void Update(){
        //If the current scene is the runner Scene and also the objects are empty, then GET THEM.
        if(ScoreDisplay == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RunnerScene"))
            ScoreDisplay = GameObject.Find("Score Text").GetComponent<Text>();
        if(TimerObject == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RunnerScene"))
            TimerObject = GameObject.Find("Timer").GetComponent<TimerScript>();
        
    }

    #region UI / Text Updating Functions

    /**
        This is a very simple function, it just updates the score text
    **/
    private void UpdateScoreText(){
        ScoreDisplay.text = DeliveriesMade.ToString();
    }

    #endregion

    #region Score Functions 

    /**
        This adds a Checkpoint to the amount of Checkpoints in CheckpointCounter
        2 Checkpoints are needed to get 1 delivery made
        Checkpoint 1 = Pick Up Food (1)
        Checkpoint 2 = Deliver Food (2)
    **/
    public void AddCheckpoint(){
        //Increments the Checkpoint counter
        //CheckpointCounter++;

        //If the Checkpoint Counter is equal to 2,
        //A succesful delivery has been made, increment deliveries made
        //Also resets the timer
        //if(CheckpointCounter == 2){
            DeliveriesMade++;
            UpdateScoreText();
        //    CheckpointCounter = 0;
            TimerObject.ResetTimer();
        //}
    }


    #endregion

    #region Getters
    //Returns the score (deliveries made)
    public int GetDeliveriesMade(){
        return DeliveriesMade;
    }

    #endregion
}
