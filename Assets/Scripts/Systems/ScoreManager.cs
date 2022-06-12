using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour{

    #region Public Variables
    [Header("Game Objects")]
    [Tooltip("The Text that will show the score")]
    public Text ScoreDisplay;
    #endregion

    #region Private Variables
    [SerializeField] private int CheckpointCounter = 0;

    [SerializeField] private int DeliveriesMade = 0;

    #endregion

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(this);
        UpdateScoreText(); 
    }

    // Update is called once per frame
    void Update(){
        
    }

    #region UI / Text Updating Functions

    /**
        This is a very simple function, it just updates the score text
    **/
    private void UpdateScoreText(){
        ScoreDisplay.text = "Deliveries: " + DeliveriesMade;
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
        CheckpointCounter++;

        //If the Checkpoint Counter is equal to 2,
        //A succesful delivery has been made, increment deliveries made
        if(CheckpointCounter == 4){
            DeliveriesMade++;
            UpdateScoreText();
            CheckpointCounter = 0;
        }
    }

    #endregion

    #region Getters
    //Returns the score (deliveries made)
    public int GetDeliveriesMade(){
        return DeliveriesMade;
    }

    #endregion
}
