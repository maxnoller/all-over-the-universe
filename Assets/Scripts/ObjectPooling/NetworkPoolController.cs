using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkPoolController : NetworkBehaviour {

    ObjectPool local_object_pool;

    void Start(){
        local_object_pool = gameObject.GetComponent<ObjectPool>();
    }


    [ClientRpc]
    public void RpcOrderObject(string tag, Vector3 position, Quaternion rotation){
        local_object_pool.SpawnFromPool(tag, position, rotation);
    }

    [Command]
    public void CmdOrderObject(string tag, Vector3 position, Quaternion rotation){
        RpcOrderObject(tag, position, rotation);
    }
}