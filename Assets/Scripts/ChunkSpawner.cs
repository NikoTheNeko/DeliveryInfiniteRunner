using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour{

    #region Public Variables

    [Header("Offsets and Spawning Chunker")]
    [Tooltip("This is the player's position. You only want the X.")]
    public float xOffsetOfSpawner = 165;

    public float yOffsetOfSpawner = 45;

    public float ChunkThiccness = 125;

    [Header("Game Objects")]
    [Tooltip("This is the player's position. You only want the X.")]
    public Transform PlayerObject;

    [Header("Chunk Arrays")]
    [Tooltip("This is the chunk array, put all unique chunks here")]
    public GameObject[] ObstacleChunks;

    #endregion

    #region Private Variables

    //The last X position the chunk was placed.
    private float XPosOfLastChunkPlaced = 0;
    //This is how far the distance is between the last chunk spawned
    private float XDistanceCounter = 0;

    //I need to create a quaternion for spawning, I don't know what quarternions are
    //still and I'm too afraid to ask
    private Quaternion Rotato = new Quaternion(0,0,0,0);
    
    //The offset vector for the spawner
    Vector2 OffsetVector;

    #endregion

    // Start is called before the first frame update
    void Start(){
        //Creates the offset of the spawnner
        OffsetVector = new Vector2(xOffsetOfSpawner, yOffsetOfSpawner);

        //Starts following the player immediately
        FollowPlayer();

        //Gets the FIRST chunk, by getting the current position and minusing by the chunk size offset
        Vector3 FirstChunkPos = new Vector3(transform.position.x - ChunkThiccness, transform.position.y, 0);
        
        //Spawns the first chunk and the next
        Object.Instantiate(ObstacleChunks[0], FirstChunkPos, Rotato);
        //Debug.Log("First Chunk spawned at: " + FirstChunkPos.x + ", " + FirstChunkPos.y);

        Object.Instantiate(ObstacleChunks[0], transform.position, Rotato);
        //Debug.Log("Second Chunk spawned at: " + transform.position.x + ", " + transform.position.y);

        //Sets the first X pos
        XPosOfLastChunkPlaced = transform.position.x;

        
    }

    // Update is called once per frame
    void Update(){
        FollowPlayer();
        SpawnChunks();
    }

    /**
        This is going to be tracking the player at all time nad moving accordingly
        This is so we can spawn in chunks in the correct position
    **/
    private void FollowPlayer(){
        //Gets the X position of the player ONLY
        Vector2 PlayerPosX = new Vector2(PlayerObject.position.x, 0);

        //Creates a new Position Vector and adds the offset
        Vector2 NewPos = PlayerPosX + OffsetVector;

        //Sets the current position to the new position
        transform.position = NewPos;
    }

    /**
        This spawns chunks.
        I'll make it specific later on with the phases and stuff but for now this spawns
        a random chunk in the array after a certain distance has been passed.
    **/
    private void SpawnChunks(){
        //Gets the distance traveled fromt he last chunk spawned
        XDistanceCounter = transform.position.x - XPosOfLastChunkPlaced;
        //If ChunkThiccness units have passed, then spawn a new chunk
        if(XDistanceCounter >= ChunkThiccness){
            int RandomChunkNumber = Random.Range(0, ObstacleChunks.Length);
            //Spawn new chunk
            Object.Instantiate(ObstacleChunks[RandomChunkNumber], transform.position, Rotato);
            //Reset counter and update position
            XPosOfLastChunkPlaced = transform.position.x;
            XDistanceCounter = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D CollidedObject){
        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Chunk")){
            Destroy(CollidedObject.gameObject);
        }

        Destroy(CollidedObject.gameObject);

    }
}
