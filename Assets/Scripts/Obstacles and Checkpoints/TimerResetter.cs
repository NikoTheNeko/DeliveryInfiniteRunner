using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerResetter : MonoBehaviour{

    #region Public Variables

    [Tooltip("The timer used to keep track of time")]
    [SerializeField] public TimerScript TimerObject;

    #endregion

    #region Private Variables
    #endregion

    void Start() {
        //Finds the Score Manager, then assigns it to the Score Manager Object
        TimerObject = GameObject.Find("Timer").GetComponent<TimerScript>();   
    }

    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Player")){
            TimerObject.ResetTimer();
        }

    }
}