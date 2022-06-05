using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    #region Public Variables

    [Header("Player Speed Variables")]
    [Tooltip("This is the player's max speed")]
    public float PlayerMaxSpeed = 20;

    [Tooltip("This is the player's min speed")]
    public float PlayerMinSpeed = 5;

    [Tooltip("This is the player's starting speed")]
    public float PlayerStartingSpeed = 5;

    [Tooltip("This is how fast the player will gain momentum and speed up, this goes at a LINEAR rate (Per Second).")]
    public float MomentumRate = 0.5f;

    [Tooltip("This is how high the player will jump")]
    public float JumpForce = 300;

    [Header("Rigidbody Variables")]
    public Rigidbody2D PlayerRigidbody;

    public Collider2D GroundChecker;

    #endregion

    #region Private Variables
    
    //[Tooltip("The Speed the player is going at")]
    [SerializeField]private float PlayerSpeed;
    
    [Tooltip("This is allows the player to jump")]
    [SerializeField]private bool CanJump = true;
    [SerializeField]private bool CanSlide = true;
    
    #endregion

    // Start is called before the first frame update
    void Start(){
        //Sets the player speed to the starting speed.
        PlayerSpeed = PlayerStartingSpeed;

    }

    // Update is called once per frame
    void Update(){
        GainMomentum();
        MoveRight();
        JumpCheck();
    }

    #region Movement Functions

    /**
        This moves the player right, infinitely, forever.
        It's an infinite runner after all.
    **/
    private void MoveRight(){
        //Makes a velocity vector
        // X = New Velocity, Y = Current falling Velocity
        float YVelocity = PlayerRigidbody.velocity.y;
        Vector2 VelocityVector = new Vector2(PlayerSpeed, YVelocity);
        PlayerRigidbody.velocity = VelocityVector;
    }

    private void JumpCheck(){
        //Checks if you can jump and if you pressed the jump button
        if(CanJump && Input.GetButtonDown("Jump")){
            //Creates a jump vector to shoot the player up and adds it as a force
            Vector2 JumpVector = new Vector2(0, JumpForce);
            PlayerRigidbody.AddForce(JumpVector);
            //Disables jump (you are in the air nyoom)
            CanJump = false;
        }
    }

    //Resets the Jump
    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Ground")){
            CanJump = true;
        }

        //If you run into an object, play the object's particle system. Then kill it.
        if(CollidedObject.gameObject.CompareTag("Obstacle")){
            DecreaseSpeed(10);
        }
    }



    #endregion

    #region Momentum functions

    /**
        This function is used to gain momentum for the player.
    **/
    private void GainMomentum(){
        //We don't need to waste computations, cap at Max or higher
        if(PlayerSpeed < PlayerMaxSpeed){
            //This might look weird but you're adding at a rate PER second, so
            //it's the speed * time.DeltaTime, so your momentum rate per second
            float MomentumGain = (MomentumRate * Time.fixedDeltaTime);
            PlayerSpeed += MomentumGain;
        }
    }

    #endregion

    #region Public Functions - Speed

    /**
        Gets the Player Speed and Returns it
    **/
    public float GetPlayerSpeed(){
        return PlayerSpeed;
    }

    /**
        Gets the percentage of the speed
    **/
    public float GetSpeedPercentage(){
        return PlayerSpeed / PlayerMaxSpeed;
    }

    /**
        Decreases the player speed
    **/
    public void DecreaseSpeed(float SpeedLoss){
        //If the player speed is less than min, then just set it to the minimum
        if(PlayerSpeed - SpeedLoss < PlayerMinSpeed){
            PlayerSpeed = PlayerMinSpeed;
        } else {
            //Subtracts the speed
            PlayerSpeed -= SpeedLoss;
        }
    }


    #endregion


}
