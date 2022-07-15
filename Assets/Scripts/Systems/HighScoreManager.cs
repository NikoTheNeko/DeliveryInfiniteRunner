using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour{

    #region Public Variables
    [Header("Game Object Variables")]
    [Tooltip("This is the text that shows a player's score")]
    [SerializeField]public Text ScoreText;

    [Tooltip("This holds the Score Manager, that way it can just access it when it can")]
    [SerializeField]public ScoreManager ScoreManagerObject;

    [Tooltip("Holds the instance of a Score Manager to prevent duplicates")]
    public static HighScoreManager instance;
    #endregion

    #region Private Variables
    //The high score of the player
    private int HighScore;
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
        //Sets High Score to the playerpref high score
        HighScore = PlayerPrefs.GetInt("HighScore");

        //Finds the Score Manager, then assigns it to the Score Manager Object
        ScoreManagerObject = GameObject.Find("Score Manager").GetComponent<ScoreManager>();  
        ScoreText = GameObject.Find("High Score Text").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update(){
        //If the current scene is the GameOver Scene, then fucking grab it and eat it delicious
        if(ScoreText == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver Scene"))
            ScoreText = GameObject.Find("High Score Text").GetComponent<Text>();
        if(ScoreManagerObject == null && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOver Scene"))
            ScoreManagerObject = GameObject.Find("Score Manager").GetComponent<ScoreManager>();  

        //Updates the high score
        if(ScoreManagerObject.GetDeliveriesMade() > HighScore){
            //Sets the new high score to set it as the high score
            HighScore = ScoreManagerObject.GetDeliveriesMade();
            //Sets the playerprefs highscore to the new high score
            PlayerPrefs.SetInt("HighScore", HighScore);

        }

        ScoreText.text = "Score: " + ScoreManagerObject.GetDeliveriesMade() + "\nHighScore: " + HighScore;

    }
}
