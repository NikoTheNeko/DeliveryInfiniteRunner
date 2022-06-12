using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour{

    #region Public Variables

    [Header("Offsets and Spawning Chunker")]
    [Tooltip("The X Offset from the player, where the spawner will be based on player")]
    public float xOffsetOfSpawner = 165;

    [Tooltip("The Y Offset from the player, where the spawner will be based on player")]
    public float yOffsetOfSpawner = 45;

    [Tooltip("How wide a chunk will be")]
    public float ChunkThiccness = 125;

    [Header("Phase Management")]
    [Tooltip("Starting Phase, No Obstacles to get player ready")]
    public int EmptyPhase = 1;

    //[Tooltip("PickUp Phase, Obstacles player getting to the restaurant")]
    //public int PickUpPhase = 14;

    [Tooltip("Restaurant Phase, Goal Phase, pick up the food")]
    public int RestaurantPhase = 15;

    //[Tooltip("Delivery Phase, Obstacles player getting to the person")]
    //public int DeliveryPhase = 25;

    [Tooltip("DropOff Phase, Goal Phase, drop off the food, gain a point")]

    public int DropOffPhase = 26;

    [Header("Game Objects")]
    [Tooltip("This is the player's position. You only want the X.")]
    public Transform PlayerObject;

    [Header("Chunk Arrays")]
    [Tooltip("Obstacle Chunks have chunks that contain obstacles")]
    public GameObject[] ObstacleChunks;

    [Tooltip("Empty Chunks, no obstacles in them")]
    public GameObject[] EmptyChunks;

    [Tooltip("Restaurant chunks, has restaurants you pick up food from")]

    public GameObject[] RestaurantChunks;

    [Tooltip("Delivery Chunks, has people you deliver food to")]

    public GameObject[] DeliveryChunks;

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

    /**
        This is gonna be a little bit tricky but bear with me
        So, this is the amount of chunks that have been spawned
        Each phase has their own marker (public values), and will spawn things
        according to which phase it's on.
    **/
    private int ChunkCounter = 2;

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
        Object.Instantiate(EmptyChunks[0], FirstChunkPos, Rotato);
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

    #region Placement Functions

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

    #endregion

    #region Chunk Spawning Functions

    /**
        This spawns chunks.
        I'll make it specific later on with the phases and stuff but for now this spawns
        a random chunk in the array after a certain distance has been passed.
    **/
    private void SpawnChunks(){
        Debug.Log("Chunk Counter is at: " + ChunkCounter);
        //Gets the distance traveled fromt he last chunk spawned
        XDistanceCounter = transform.position.x - XPosOfLastChunkPlaced;
        //If ChunkThiccness units have passed, then spawn a new chunk
        if(XDistanceCounter >= ChunkThiccness){
            //This checks what phase the chunk is on, and depending on which one it's on,
            //It'll change what chunks are spawning in.
            
            //Reset Phase back to 0
            if(ChunkCounter > DropOffPhase){
                ChunkCounter = 0;
            //Fifth Phase, Drop Off (Completes a whole cycle, player gets a point)
            } else if (ChunkCounter == DropOffPhase){
                SpawnSpecificChunk(DeliveryChunks);
            //Fourth Phase, Delivery Phase (Repeat of Pick Up Phase, Obstacles before drop off)
            } else if (ChunkCounter > RestaurantPhase && ChunkCounter < DropOffPhase){
                SpawnSpecificChunk(ObstacleChunks);
            //Third Phase, Restaurant Phase (Spawns Restaurant to pick up food)
            } if(ChunkCounter == RestaurantPhase){
                SpawnSpecificChunk(RestaurantChunks);
            //Second Phase, Pick Up phase (obstacles before pick up)
            } if (ChunkCounter > EmptyPhase && ChunkCounter < RestaurantPhase){
                SpawnSpecificChunk(ObstacleChunks);
            //First Phase, empty phase (breather at start)
            } else if(ChunkCounter <= EmptyPhase){
                SpawnSpecificChunk(EmptyChunks);
            }        
            
        }
    }

    /**
        Spawns a random chunk of a certain array
    **/
    private void SpawnSpecificChunk(GameObject[] ChunksArray){
        //Generates a random chunk to grab
        int RandomChunkNumber = Random.Range(0, ChunksArray.Length);
        //Spawn new chunk
        Object.Instantiate(ChunksArray[RandomChunkNumber], transform.position, Rotato);
        
        //Increments a chunk
        ChunkCounter++;

        //Reset counter and update position
        XPosOfLastChunkPlaced = transform.position.x;
        XDistanceCounter = 0;
    }

    #endregion

    #region Rigidbody Functions
    /**
        Chunk Deleter fuck those chunks
    **/
    void OnTriggerEnter2D(Collider2D CollidedObject){

        //If you're touching the ground, reset the jump
        if(CollidedObject.gameObject.CompareTag("Chunk")){
            Destroy(CollidedObject.gameObject);
        }

        Destroy(CollidedObject.gameObject);

    }
    #endregion
}
