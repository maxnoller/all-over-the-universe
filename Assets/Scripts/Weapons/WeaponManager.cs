using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class WeaponManager : NetworkBehaviour{
    GameObject[] weapons = new GameObject[8];
    Player local_player;

    public void init(Player local_player){
        this.local_player = local_player;
    }

    [Command]
    public void CmdRegisterWeapon(GameObject weapon){
        IEquipable weapon_equip = weapon.GetComponent<IEquipable>();
        if(weapon_equip == null){
            Debug.LogError("Trying to register unequipable weapon");
            return;
        }
        CmdGrantAuthority(weapon);
        CmdSetParent(weapon);
        weapon_equip.equip(gameObject);
        RpcEquipWeapon(weapon); 
        addWeapon(weapon);
        Debug.Log("Registering Weapon");
    }

    [ClientRpc]
    void RpcEquipWeapon(GameObject weapon){
        weapon.GetComponent<IEquipable>().equip(gameObject);
        if(isLocalPlayer)
            addWeapon(weapon);
    }

    [Command]
    void CmdUnregisterWeapon(GameObject weapon){
        removeWeapon(weapon);
        RpcUnregisterWeapon(weapon);
    }

    [ClientRpc]
    void RpcUnregisterWeapon(GameObject weapon){
        removeWeapon(weapon);
    }

    [Command]
    public void CmdSetParent(GameObject item){
        item.transform.parent = local_player.camera_controller.transform;
        RpcChangeParent(item);
    }

    [ClientRpc]
    public void RpcChangeParent(GameObject item){
        item.transform.parent = local_player.camera_controller.transform;
    }
    
    [Command]
    void CmdGrantAuthority(GameObject target)
    {
        target.GetComponent<NetworkIdentity>().AssignClientAuthority(GetComponent<NetworkIdentity>().connectionToClient);
    }

    int getWeaponIndex(GameObject element){
        for(int i = 0; i<weapons.Length; i++){
            if(weapons[i] == element){
                return i;
            }
        }
        return -1;
    }

    void removeWeapon(GameObject weapon){
        int idx = getWeaponIndex(weapon);
        if(idx==-1){
            Debug.LogError("Trying to remove non existent weapon from array");
            return;
        }
        weapons[idx] = null;
    }

    void addWeapon(GameObject weapon){
        int slot = firstFreeSlotIndex(weapon);
        if(slot==-1){
            Debug.LogWarning("No free weapon slot");
            return;
        }
        weapons[slot] = weapon;
    }

    int firstFreeSlotIndex(GameObject weapon){
        for(int i = 0; i<weapons.Length;i++){
            if(weapons[i] == null)
                return i;
        }
        return -1;
    }
}