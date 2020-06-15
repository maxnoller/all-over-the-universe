using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RaycastController : NetworkBehaviour {
    FirearmController firearm_controller;
    Transform player_camera_transform;

    public void Init(FirearmController firearm_controller){
        this.firearm_controller = firearm_controller;
        this.player_camera_transform = transform.parent.GetChild(0).transform;
        this.OnEnable();
    }

    void OnEnable(){
        if(firearm_controller != null)
            firearm_controller.OnBulletShot += processShot;
    }

    void OnDisable(){
        if(firearm_controller != null)
            firearm_controller.OnBulletShot -= processShot;
    }

    public void processShot(){
        Cmd_performRaycast();
    }

    [Command]
    public void Cmd_performRaycast(){
        RaycastHit hit;
        if(Physics.Raycast(player_camera_transform.position,
                           player_camera_transform.forward,
                           out hit)){
            if(hit.transform == transform.parent) return;
            IDamageable hit_damage = hit.transform.GetComponent<IDamageable>();
            if(hit_damage != null){
                hit_damage.takeDamage(firearm_controller.FirearmData.damage);
            }
        }
    }
}