using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationUIHandler : MonoBehaviour{

    #region Public Variables
    [Header("Notification Prefabs")]
    [Tooltip("This is the restaurant notification, includes animation")]
    public GameObject RestaurantNotif;
    [Tooltip("This is the delivery notification, includes animation")]
    public GameObject DeliveredNotif;
    [Tooltip("This is the notification spawn location")]
    public Transform NotifSpawnPosition;

    [Header("Phone Interface Variables")]
    [Tooltip("This has all the phone UI and it's animations")]
    public GameObject[] PhoneUI;

    [Tooltip("This is where the phone will spawn")]
    public Transform PhoneSpawnPosition;


    #endregion

    #region Private Variables
    private Quaternion Rotato = new Quaternion(0,0,0,0);
    #endregion

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
    }

    public void ShowRestaurantNotif(){
        Object.Instantiate(RestaurantNotif, NotifSpawnPosition.position, Rotato, NotifSpawnPosition);
    }

    public void ShowDeliveredNotif(){
        Object.Instantiate(DeliveredNotif, NotifSpawnPosition.position, Rotato, NotifSpawnPosition);
    }

    public void ShowCustomerNotif(int CustomerIndex){
        Object.Instantiate(PhoneUI[CustomerIndex], PhoneSpawnPosition.position, Rotato, PhoneSpawnPosition);
    }
}
