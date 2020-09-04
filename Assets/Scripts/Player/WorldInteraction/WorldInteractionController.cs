using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WorldInteractionController : NetworkBehaviour
{
    Transform player_camera_transform;
    Player player;

    void Start(){
        player = GetComponent<Player>();
        player_camera_transform = player.camera_controller.transform;
    }
    
    void OnEnable(){
        InputController.OnInputUpdate += handleInput;
    }

    void OnDisable(){
        InputController.OnInputUpdate -= handleInput;
    }

    void handleInput(IInputData input_data){
        if(input_data.has_interacted)
            check_interaction();
    }

    void check_interaction(){
        CmdPerformRaycast();
    }

    [Command]
    public void CmdPerformRaycast(){
        RaycastHit hit;
        if(Physics.Raycast(player_camera_transform.position,
                           player_camera_transform.forward,
                           out hit)){
            if(hit.transform == transform.parent) return;
            
            GameObject item = hit.transform.gameObject;
            IEquipable equipable = hit.transform.GetComponent<IEquipable>();
            if(equipable != null){
                RpcInteract(item);
            }
        }
    }

    [ClientRpc]
    public void RpcInteract(GameObject equipable){
        player.equip(equipable);
    }
}
