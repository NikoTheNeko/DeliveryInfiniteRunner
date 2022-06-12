using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour{

    #region Public Variables
    public ParticleSystem BoxSpawner;
    
    public Collider2D ObstacleCollider;

    public GameObject ObjectSprite;

    #endregion

    #region Private Variables

    [SerializeField]private float DeathTimer = 5f;
    #endregion

    // Start is called before the first frame update
    void Start(){
        //Decouples the parent so that way it's no longer getting scaled incorrectly
        transform.parent = null;

    }

    void Update(){
    }

    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Player")){
            BoxSpawner.Play();
            ObjectSprite.SetActive(false);
            Destroy(this.gameObject, DeathTimer);
            Debug.Log("There's some twink and he fucking ran into me");
        }

    }
}
