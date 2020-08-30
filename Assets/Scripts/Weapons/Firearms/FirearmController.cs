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

    void Start(){
        can_shoot = true;
        if(firearm_data.ammo_per_magazine != 0)
            reload_behaviour = (ReloadBehaviour)gameObject.AddComponent(typeof(ReloadBehaviour));
        sound_behaviour = gameObject.GetComponent<SoundBehaviour>();
        RaycastController raycast_controller;
        raycast_controller = GetComponent<RaycastController>();
        raycast_controller.Init(this);    
        OnEnable();
    }

    void OnEnable(){
        if(hasAuthority)
            InputController.OnInputUpdate += checkInput;
    }

    void OnDisable(){
        if(hasAuthority)
            InputController.OnInputUpdate -= checkInput;
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
