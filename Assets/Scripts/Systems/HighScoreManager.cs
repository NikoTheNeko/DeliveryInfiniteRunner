using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour{

    #region Public Variables
    public Text ScoreText;

    public ScoreManager ScoreManagerObject;
    #endregion

    #region Private Variables
    private int HighScore;
    #endregion

    // Start is called before the first frame update
    void Start(){
        DontDestroyOnLoad(this);
        //Finds the Score Manager, then assigns it to the Score Manager Object
        ScoreManagerObject = GameObject.Find("Score Manager").GetComponent<ScoreManager>();  
    }

    // Update is called once per frame
    void Update(){
        if(ScoreManagerObject.GetDeliveriesMade() > HighScore)
            HighScore = ScoreManagerObject.GetDeliveriesMade();

        ScoreText.text = "Score: " + ScoreManagerObject.GetDeliveriesMade() + "\nHighScore: " + HighScore;
    }
}
