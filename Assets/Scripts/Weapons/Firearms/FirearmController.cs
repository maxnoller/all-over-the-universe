using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirearmController : NetworkBehaviour, IFirearmController
{
    [SerializeField]private FirearmData firearm_data;
    public FirearmData FirearmData{
        get{return firearm_data;}
    }

    //Events
    public event Action OnBulletShot = delegate { };

    public bool can_shoot{get;set;}
    ReloadBehaviour reload_behaviour;

    bool is_local_player = true;

    void Start(){
        can_shoot = true;
        if(firearm_data.ammo_per_magazine != 0 && is_local_player){
            reload_behaviour = GetComponent<ReloadBehaviour>();
            reload_behaviour.Init(this);
        }
        RaycastController raycast_controller;
        raycast_controller = GetComponent<RaycastController>();
        raycast_controller.Init(this);    
    }

    void OnEnable(){
        if(is_local_player)
            InputController.OnInputUpdate += checkInput;
    }

    void OnDisable(){
        if(is_local_player)
            InputController.OnInputUpdate -= checkInput;
    }

    void Init(bool is_local_player){
        this.is_local_player = is_local_player;
    }

    void checkInput(IInputData input_data){
        if(input_data.has_used){
           shoot();
        }
    }
    float next_fire;
    void shoot(){
        if(can_shoot && Time.time > next_fire){
            next_fire = Time.time + 1f/firearm_data.fire_rate;
            OnBulletShot();
        }
    }

    public ReloadData getReloadData(){
        return new ReloadData(this.firearm_data);
    }

}
