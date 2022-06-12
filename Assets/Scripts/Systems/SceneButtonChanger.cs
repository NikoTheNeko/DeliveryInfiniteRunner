using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonChanger : MonoBehaviour{

    #region Public Variables
    [Header("Scene Name")]
    [Tooltip("The scene you'll be switching to")]
    public string SceneToSwitchTo;
    #endregion

    #region Private Variables
    #endregion

    //Switches the scene to what the object has specified
    public void SceneSwitch(){
        SceneManager.LoadScene(SceneToSwitchTo, LoadSceneMode.Single);
    }
}
