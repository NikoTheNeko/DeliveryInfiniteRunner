using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{

    #region Public Variables
    [Header("Camera Settings")]
    [Tooltip("Give the X and Y offets, (Z Will be handled separately)")]
    public Vector2 Offset;

    [Tooltip("The Game Object that will be tracked")]
    public Transform TrackingTarget;

    [Tooltip("Max Zoom for the camera")]
    public float MaxCameraZoom = 28;

    [Tooltip("Min Zoom for the camera")]
    public float MinCameraZoom = 14;

    [Tooltip("How the smooth the camera will be moving")]
    [Range(0.0f, 1.0f)]
    public float TrackingSmoothness = 0.5f;
    
    [Tooltip("How the smooth the camera will be zoom")]
    [Range(0.0f, 1.0f)]
    public float ZoomSmoothness = 0.5f;

    [Header("Extra Objects")]
    [Tooltip("The Camera")]
    public Camera CameraObject;

    [Tooltip("The player object")]
    public PlayerMovement PlayerObject;

    #endregion

    #region Private Variables

    private float ZoomDifference;

    #endregion

    // Start is called before the first frame update
    void Start(){
        //Gets the difference of the zooms
        ZoomDifference = Mathf.Abs(MaxCameraZoom - MinCameraZoom);
    }

    // Update is called once per frame
    void FixedUpdate(){
        TrackTarget();
        AdjustZoom();
        
    }

    private void TrackTarget(){
        //Gets the current the position the camera
        Vector3 CurrentPos = transform.position;

        //Gets how fast the character is moving
        float PlayerSpeedPercentage = PlayerObject.GetSpeedPercentage();

        //Creates a new Offset Vector
        Vector3 OffsetVector = new Vector3(Offset.x + (2 * PlayerSpeedPercentage), Offset.y + (2 * PlayerSpeedPercentage), -10);

        //This gets ONLY the X of the target, so when the player jumps, the camera doesn't move either
        Vector3 TargetPosCorrected = new Vector3(TrackingTarget.position.x, 0, 0);

        //Adds the offset to the position;
        Vector3 TargetOffsetPos = TargetPosCorrected + OffsetVector;

        //Creates a LERP for of the Vectors
        Vector3 TrackingPos = Vector3.Lerp(CurrentPos, TargetOffsetPos, TrackingSmoothness);

        //Sets camera position to the lerp'd position
        transform.position = TrackingPos;
    }

    /**
        Adjusts the zoom based on how fast the character is moving.
    **/
    private void AdjustZoom(){
        //Gets the percentage of how fast the player is moving
        float PlayerSpeedPercentage = PlayerObject.GetSpeedPercentage();    

        //Gets the new size by starting at the minimum camera size to the max camera size
        float TargetSize = MinCameraZoom + (PlayerSpeedPercentage * ZoomDifference);

        //Lerps the new size to the current size. Makes things less jarring
        //If no one got me I know LERP got me.
        float NewSize = Mathf.Lerp(CameraObject.orthographicSize, TargetSize, ZoomSmoothness);

        //Makes the LERP zoom the size
        CameraObject.orthographicSize = NewSize;
    }


}
