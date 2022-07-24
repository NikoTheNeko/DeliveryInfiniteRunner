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
    public float JumpForce = 3000;

    [Tooltip("This is the force that will shove the player back if you hold down, Keep positive!!!! Please!!!")]
    public float HardDropForce = 300;

    [Tooltip("When you get hit by something, you decrease by this amount")]
    public float ObstacleSlowdown = 5;

    [Tooltip("Halts the player so we can end the game")]
    public bool StopPlayer = false;


    [Header("Rigidbody & Game Object Variables")]
    [Tooltip("Rigidbody of the character")]
    public Rigidbody2D PlayerRigidbody;

    [Tooltip("This is the standing player with it's collision")]
    public GameObject StandingObject;

    [Tooltip("This is the sliding player with it's collision")]
    public GameObject SlidingObject;

    [Header("Audio Objects")]

    [Tooltip("This is the noise that will play if the player runs into a box")]
    public AudioSource BoxStumble;

    [Tooltip("Boingyoingyoing")]
    public AudioSource JumpSFX;
    

    #endregion

    #region Private Variables
    
    [Header("Private Variables, just visible for testing porpoises")]

    //[Tooltip("The Speed the player is going at")]
    [SerializeField]private float PlayerSpeed;
    
    [Tooltip("This is allows the player to jump")]
    [SerializeField]private bool CanJump = true;
    [SerializeField]private bool CanSlide = true;
    [SerializeField]private bool Grounded = true;

    [SerializeField]private bool MobileHardDropBool = false;

    #endregion

    // Start is called before the first frame update
    void Start(){
        //Sets the player speed to the starting speed.
        PlayerSpeed = PlayerStartingSpeed;

    }

    // Update is called once per frame
    void Update(){
        if(!StopPlayer){
            HandleMovement();
        }
    }

    #region Movement Functions

    /**
        Just for organization purposes, I think it looks nicer if I have this function rather than putting it all in update.
        That's all
        Sorry if you're on the github and seeing this, this is just how I code
        It looks nicer.
    **/
    private void HandleMovement(){
        GainMomentum();
        MoveRight();
        JumpCheck();
        HardDrop();
        SlidingManager();
        MobileHardDropHelper();
    }

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
            //No longer grounded you are in the sky nyoom
            Grounded = false;
            //Creates a jump vector to shoot the player up and adds it as a force
            Vector2 JumpVector = new Vector2(0, JumpForce);
            PlayerRigidbody.AddForce(JumpVector);
            //Disables jump (you are in the air nyoom)
            CanJump = false;
            //You are no longer on the ground, be free my child: Marks can slide to false
            //The joke here is that the previous variable was "IsGrounded" but past Niko
            //Decided to make a thing in advanced but here we are
            CanSlide = false;
            //Plays the jump noice
            JumpSFX.Play();
        }
    }

    //Resets the Jump
    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump, marks as Can Slide to true
        if(CollidedObject.gameObject.CompareTag("Ground")){
            CanJump = true;
            CanSlide = true;
            Grounded = true;
        }

        //If you run into an object, play the object's particle system. Then kill it.
        if(CollidedObject.gameObject.CompareTag("Obstacle")){
            DecreaseSpeed(ObstacleSlowdown);
        }
    }

    /**
        This does a couple of things, it'll make sure that the player is sliding and also swap sprites if they are.
        I guess it'd be better to put this into 2 different functions but whatever
    **/
    private void SlidingManager(){
        //If the player is grounded and holding the slide button, then swap sprites
        if(CanSlide && Input.GetButton("Slide")){
            SlidingObject.SetActive(true);
            StandingObject.SetActive(false);
        } else if(Input.GetButtonUp("Slide") || !CanSlide) {
            //If the player lets go of slide or is now in the air, then swap sprites.
            SlidingObject.SetActive(false);
            StandingObject.SetActive(true);
        }
    }

    /**
        If the player is in the air
    **/
    private void HardDrop(){
        //Checks if the player is holding the slide button and is in the air by checking if it can slide
        if(Input.GetButton("Slide") && !CanSlide){
            Vector2 HardDropVector = new Vector2(0, -HardDropForce);
            PlayerRigidbody.AddForce(HardDropVector);
        }
    }

    public void Unstuck(){
        if(CanJump == false && CanSlide == false && Grounded == true){
            CanSlide = true;
            CanJump = true;
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
        //Plays the you ran into something noise
        BoxStumble.Play();
    }

    /**
        Call this to stop the player
    **/
    public void StopTheGame(){
        StopPlayer = true;
        PlayerSpeed = 0;
    }


    #endregion

    #region Mobile Functions
    
    /**
        Jump Function but for mobile
    **/ 
    public void MobileJump(){
        //Checks if you can jump and if you pressed the jump button
        if(CanJump){
            //No longer grounded you are in the sky nyoom
            Grounded = false;
            //Creates a jump vector to shoot the player up and adds it as a force
            Vector2 JumpVector = new Vector2(0, JumpForce);
            PlayerRigidbody.AddForce(JumpVector);
            //Disables jump (you are in the air nyoom)
            CanJump = false;
            //You are no longer on the ground, be free my child: Marks can slide to false
            //The joke here is that the previous variable was "IsGrounded" but past Niko
            //Decided to make a thing in advanced but here we are
            CanSlide = false;
            //Plays the jump noice
            JumpSFX.Play();
        }
    }

    /**
        Mobile slide
        This does a couple of things, it'll make sure that the player is sliding and also swap sprites if they are.
        I guess it'd be better to put this into 2 different functions but whatever
    **/
    public void MobileSlideDown(){
        //If the player is grounded and holding the slide button, then swap sprites
        if(CanSlide){
            SlidingObject.SetActive(true);
            StandingObject.SetActive(false);
        }
    }

    /**
        Mobile version when you let go
    **/

    public void MobileSlideUp(){
        //If the player lets go of slide or is now in the air, then swap sprites.
        SlidingObject.SetActive(false);
        StandingObject.SetActive(true);
        MobileHardDropBool = false;
    }

    /**
        If the player is in the air they slamma jamma on the floor
    **/
    public void MobileHardDrop(){
        //Checks if the player is holding the slide button and is in the air by checking if it can slide
        if(!CanSlide){
            MobileHardDropBool = true;
        } else if(CanSlide){
            MobileHardDropBool = false;
        }

    }

    private void MobileHardDropHelper(){
        //If the hard drop is activated then go
        if(MobileHardDropBool){
            Vector2 HardDropVector = new Vector2(0, -HardDropForce);
            PlayerRigidbody.AddForce(HardDropVector);
        }
    }


    #endregion


}
