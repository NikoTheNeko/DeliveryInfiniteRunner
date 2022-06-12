using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehavior : MonoBehaviour{

    #region Public Variables

    [Header("Score Manager")]
    [Tooltip("This is the Score Manager, it wil be fetched when the object is created")]
    public ScoreManager ScoreManagerObject;

    #endregion

    #region Private Variables
    #endregion

    void Start() {
        //Finds the Score Manager, then assigns it to the Score Manager Object
        ScoreManagerObject = GameObject.Find("Score Manager").GetComponent<ScoreManager>();   
    }

    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Player")){
            Destroy(this.gameObject);
            ScoreManagerObject.AddCheckpoint();
        }

    }
}
