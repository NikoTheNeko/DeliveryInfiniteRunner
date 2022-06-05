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

    [SerializeField]private bool WasHit = false;
    [SerializeField]private float DeathTimer = 5f;
    #endregion

    // Start is called before the first frame update
    void Start(){
        
    }

    void Update(){
        if(WasHit)
            DeathTimer -= Time.deltaTime;

        if(DeathTimer < 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Player")){
            BoxSpawner.Play();
            WasHit = true;
            ObjectSprite.SetActive(false);
            Debug.Log("There's some twink and he fucking ran into me");
        }

    }
}
