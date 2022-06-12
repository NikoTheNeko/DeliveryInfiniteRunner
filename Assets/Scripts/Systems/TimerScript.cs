using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour{

    #region Public Variables

    [Header("Timer Variables")]
    [Tooltip("Amount of seconds to complete a delivery")]
    public float MaxDeliveryTime = 90f;

    [Header("UI/Game Objects")]
    [Tooltip("The text that shows how much time you have left")]
    public Text TimerText;
    [Tooltip("The scene changer object to swap scenes to game over")]
    public SceneButtonChanger SceneSwapper;

    #endregion

    #region Private Variables
    [Header("Serialized Private Variables for Debugging")]

    [Tooltip("Timer variable that handles the time")]
    [SerializeField] private float TimerTime = 0f;

    #endregion

    // Start is called before the first frame update
    void Start(){
        TimerTime = MaxDeliveryTime;
    }

    // Update is called once per frame
    void Update(){
        TimerChecks();
    }

    #region Timer Functions

    /**
        Timer checks checks if the timer is more than 0 to count down
        and if it's lower then it'll change scenes
    **/
    private void TimerChecks(){
        if(TimerTime > 0){
            CountDownTimer();
            TimerText.text = "Time Left: " + FormatTime();
        }

        if(TimerTime <= 0){
            SceneSwapper.SceneSwitch();
        }
    }

    /**
        Counts down TimerTime with Timer.DeltaTime
    **/
    private void CountDownTimer(){
        TimerTime -= Time.deltaTime;
    }

    /**
        This formats the time into seconds to mintues:seconds
    **/
    private string FormatTime(){
        //Gets Minutes and Seconds
        float minutes = Mathf.FloorToInt(TimerTime / 60);
        float seconds = Mathf.Round(TimerTime % 60);

        //Creates a string to hold the information correctly
        string FormattedTime = minutes + ":" + seconds;

        //Returns corrected format
        return FormattedTime;
    }

    #endregion

}
