using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnWeapon : NetworkBehaviour {
    bool weapon_spawned = false;
    [SerializeField]GameObject weapon;

    void Update(){
        if(!isLocalPlayer) return;
        if(!weapon_spawned && Input.GetButton("Fire2")){
            CmdSpawnWeapon();
            weapon_spawned = true;
        }
    }

    [Command]
    public void CmdSpawnWeapon(){
        Debug.Log("Spawning Weapon for player: "+GetComponent<Player>().username);
        GameObject weapon_object = Instantiate(weapon);
        weapon_object.transform.parent = transform;
        NetworkServer.Spawn(weapon_object, GetComponent<NetworkIdentity>().connectionToClient);
        RpcSetParent(weapon_object);
    }

    [ClientRpc]
    public void RpcSetParent(GameObject weapon_object){
        weapon_object.transform.parent = transform;
    }
}