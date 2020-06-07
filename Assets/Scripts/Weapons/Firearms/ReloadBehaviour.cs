using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBehaviour : IBehaviour{
    IFirearmController firearm_controller;
    public int ammo_in_magazine{get;private set;}
    public int total_ammo{get;private set;}
    public bool has_magazines{get;private set;}
    
    public ReloadBehaviour(IFirearmController firearm_controller){
        this.firearm_controller = firearm_controller;
        this.firearm_controller.OnBulletShot += handleShot;
        if(this.firearm_controller.firearm_data.ammo_per_magazine != 0)
            has_magazines = true;
        total_ammo = firearm_controller.firearm_data.total_ammo;
        InputManager.OnInputUpdate += handleReload;
    }

    OnEnableBehaviour() {

    }
    void handleShot(){
        if(!has_magazines){
            total_ammo--;
        } else {
            ammo_in_magazine--;
        }
        if(total_ammo <= 0 || ammo_in_magazine <= 0)
            firearm_controller.can_shoot = false;
    }
    void handleReload(IInputData input_data){
        if()
    }

}