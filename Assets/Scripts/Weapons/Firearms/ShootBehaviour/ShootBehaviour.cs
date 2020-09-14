using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ShootBehaviour : NetworkBehaviour {
    
    public event Action OnBulletShot = delegate { };

    ShootData shoot_data;

    Transform player_camera_transform;

    NetworkPoolController object_pool_controller;
    FirearmController firearm_controller;

    //timestamp when the next bullet can be fired from this weapon
    float next_fire;

    public void init(FirearmController firearm_controller, ShootData shoot_data){
        this.firearm_controller = firearm_controller;
        this.player_camera_transform = player.GetComponent<Player>().camera_controller.transform;
        this.object_pool_controller = GameObject.Find("ObjectPool").GetComponent<NetworkPoolController>();
        this.shoot_data = shoot_data;
        this.OnEnable();
    }

    override
    public void OnStartAuthority(){
        this.OnEnable();
    }

    void OnEnable(){
        if(hasAuthority)
            InputController.OnInputUpdate += handleInput;
    }

    void OnDisable(){
        if(hasAuthority)
            InputController.OnInputUpdate -= handleInput;
    }

    public void handleInput(IInputData input_data){
        if(shoot_data == null) return;
        if((shoot_data.fire_mode == ShootData.FireMode.semi && input_data.has_used)
            || (shoot_data.fire_mode == ShootData.FireMode.full && input_data.has_used_down)){
            tryShoot();
        }
    }

    public void tryShoot(){
        if(firearm_controller.can_shoot && Time.time >= next_fire){
            next_fire = Time.time + 1f/shoot_data.fire_rate;
            OnBulletShot();
            CmdPerformRaycast();
        }
    }

    [Command]
    public void CmdPerformRaycast(){
        RaycastHit hit;
        if(Physics.Raycast(player_camera_transform.position,
                           player_camera_transform.forward,
                           out hit)){
            if(hit.transform == transform.parent) return;
            object_pool_controller.RpcOrderObject("bullethole", hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.transform);
            IDamageable hit_damage = hit.transform.GetComponent<IDamageable>();
            if(hit_damage != null){
                hit_damage.registerHit(firearm_controller.FirearmData.damage, local_player);
            }
            Debug.DrawLine (transform.position, hit.point, Color.cyan, 10);
        }
    }


}