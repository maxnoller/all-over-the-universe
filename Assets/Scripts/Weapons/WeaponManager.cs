using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour{
    List<GameObject> weapons;
    Player local_player;

    void Init(Player local_player){
        this.local_player = local_player;
    }

    [Command]
    void Cmd_RegisterWeapon(GameObject weapon){
        GameObject weapon_object = Instantiate(weapon);
        weapon_object.transform.parent = local_player.transform;
        NetworkServer.Spawn(weapon_object);
        this.weapons.Add(weapon_object);
        Target_RegisterWeapon(weapon_object);
    }

    [TargetRpc]
    void Target_RegisterWeapon(GameObject weapon){
        this.weapons.Add(weapon);
    }

    [Command]
    void Cmd_UnregisetrWeapon(GameObject weapon){
        
    }
}