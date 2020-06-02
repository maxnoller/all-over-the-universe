using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    PlayerModel player_model;
    public InputController input_controller;
    public MovementController movement_controller;
    public CameraController camera_controller;
    
    void Start()
    {
        player_model = new PlayerModel();

        camera_controller = (CameraController)transform.GetChild(0).gameObject.AddComponent(typeof(CameraController));
        camera_controller.local_player = this;
        
        if(!isLocalPlayer) return;
        input_controller =  (InputController)gameObject.AddComponent(typeof(InputController));
        movement_controller = (MovementController)gameObject.AddComponent(typeof(MovementController));
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void getUsername(){
        this.player_model.username = GameObject.Find("NetworkManager").GetComponent<Application>().username;
    }
    
    void Update()
    {
        
    }
}
