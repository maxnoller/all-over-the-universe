using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RaycastController : NetworkBehaviour {
    FirearmController firearm_controller;
    Transform player_camera_transform;
    NetworkPoolController object_pool_controller;
    Player local_player; //TODO: somehow rework this this is disgusting

    public void Init(FirearmController firearm_controller, GameObject player){
        this.firearm_controller = firearm_controller;
        this.player_camera_transform = player.GetComponent<Player>().camera_controller.transform;
        this.object_pool_controller = GameObject.Find("ObjectPool").GetComponent<NetworkPoolController>();
        local_player = player_camera_transform.parent.GetComponent<Player>();
        this.OnEnable();
    }

    override
    public void OnStartAuthority(){
        this.OnEnable();
    }

    void OnEnable(){
        if(firearm_controller != null && hasAuthority)
            firearm_controller.OnBulletShot += processShot;
    }

    void OnDisable(){
        if(firearm_controller != null && hasAuthority)
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
            object_pool_controller.RpcOrderObject("bullethole", hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal), hit.transform);
            IDamageable hit_damage = hit.transform.GetComponent<IDamageable>();
            if(hit_damage != null){
                hit_damage.registerHit(firearm_controller.FirearmData.damage, local_player);
            }
            Debug.DrawLine (transform.position, hit.point, Color.cyan, 10);
        }
    }
}