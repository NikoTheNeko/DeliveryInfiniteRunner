using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonChanger : MonoBehaviour{

    #region Public Variables
    [Header("Scene Name")]
    [Tooltip("The scene you'll be switching to")]
    public string SceneToSwitchTo;

    [Tooltip("How long it will take to move to the next scene")]    
    public float TransitionTime = 1f;
    
    [Tooltip("The object that will be enabled to show the transition")]

    public GameObject TransitionObject;

    [Tooltip("The thing to start the transition")]
    private bool TransitionStart = false;


    #endregion

    #region Private Variables
    #endregion

    private void Update() {
        if(TransitionStart)
            TransitionTime -= Time.deltaTime;
        

        if(TransitionTime <= 0)
            SceneManager.LoadScene(SceneToSwitchTo, LoadSceneMode.Single);
    }

    //Switches the scene to what the object has specified
    public void SceneSwitch(){
        //Sets the object to active so the animation will play
        TransitionObject.SetActive(true);
        
        //Starts the timer
        TransitionStart = true;
    }

}
