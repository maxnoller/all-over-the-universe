using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AssignAuthority : NetworkBehaviour {
    public GameObject weapon;
    void OnEnable(){
        CmdGrantAuthority(weapon);
    }

    [Command]
    void CmdGrantAuthority(GameObject target)
    {
        target.GetComponent<NetworkIdentity>().AssignClientAuthority(GetComponent<NetworkIdentity>().connectionToClient);
    }
}