using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrsesBarBehavior : MonoBehaviour{

    #region Public Variables
    [Header("Game Objects")]
    [Tooltip("This is the button for the RestaurantPhase")]
    public GameObject RestaurantPhaseButton;

    [Tooltip("This is the button for the DropOff")]
    public GameObject DropOffButton;

    [Tooltip("This is the slider")]
    public Slider ProgressBarSlider;

    #endregion

    #region Private Variables
    #endregion

    // Update is called once per frame
    void Update(){
        UpdateButtons();
    }

    public void UpdateButtons(){
        //If it's past the 50% mark, the button in the middle is active
        if(ProgressBarSlider.value >= 0.46f){
            RestaurantPhaseButton.SetActive(true);
        }

        //If it's 100%, then the button on right is active
        if(ProgressBarSlider.value >= 0.93f){
            DropOffButton.SetActive(true);
        }

        if(ProgressBarSlider.value < 0.3f){
            RestaurantPhaseButton.SetActive(false);
            DropOffButton.SetActive(false);

        }

    }

}
