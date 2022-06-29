using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructScript : MonoBehaviour{

    #region Public Variables
    //The amount of time it'll take to blow up
    public float SelfDestructTime;
    #endregion

    #region Private Variables
    #endregion

    // Start is called before the first frame update
    void Start(){
        //Blows self  up
        Object.Destroy(this.gameObject, SelfDestructTime);
    }

}
