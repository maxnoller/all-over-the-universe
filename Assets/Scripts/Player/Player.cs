using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    PlayerModel player_model;
    [SyncVar]public string username;
    public InputController input_controller;
    public MovementController movement_controller;
    public CameraController camera_controller;
    public HealthController health;
    
    void Start()
    {
        player_model = new PlayerModel();

        camera_controller = (CameraController)transform.GetChild(0).gameObject.AddComponent(typeof(CameraController));
        camera_controller.local_player = this;

        health = GetComponent<HealthController>();

        setupLocalPlayer();

        if(!isServer) return;
        GameObject.Find("GameLogic").GetComponent<GameLogic>().registerPlayer(this);
    }

    void setupLocalPlayer(){
        if(!isLocalPlayer) return;
        input_controller =  (InputController)gameObject.AddComponent(typeof(InputController));
        movement_controller = (MovementController)gameObject.AddComponent(typeof(MovementController));

        getUsername();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void getUsername(){
        this.player_model.username = GameObject.Find("NetworkManager").GetComponent<Application>().username;
        this.username = player_model.username;
    }

    public void equip(GameObject item){
        CmdGrantAuthority(item);
        CmdSetParent(item);
        Destroy(item.GetComponent<Rigidbody>());
        item.GetComponent<IEquipable>().equip(gameObject);
    }

    [Command]
    public void CmdSetParent(GameObject item){
        item.transform.parent = camera_controller.transform;
        RpcChangeParent(item);
    }

    [ClientRpc]
    public void RpcChangeParent(GameObject item){
        item.transform.parent = camera_controller.transform;
    }
    
    [Command]
    void CmdGrantAuthority(GameObject target)
    {
        target.GetComponent<NetworkIdentity>().AssignClientAuthority(GetComponent<NetworkIdentity>().connectionToClient);
    }
}
