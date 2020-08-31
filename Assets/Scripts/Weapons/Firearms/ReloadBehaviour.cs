using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBehaviourOld : MonoBehaviour{
    IFirearmController firearm_controller;
    public int ammo_in_magazine{get;private set;}
    public int total_ammo{get;private set;}
    public bool has_magazines{get;private set;}
    public event Action<ReloadBehaviour> OnReload = delegate { };
    
    public ReloadBehaviourOld(IFirearmController firearm_controller){
        this.firearm_controller = firearm_controller;
        if(this.firearm_controller.FirearmData.ammo_per_magazine != 0)
            has_magazines = true;
        total_ammo = firearm_controller.FirearmData.total_ammo;
    }

    public void OnEnableBehaviour() {
        firearm_controller.OnBulletShot += handleShot;
        if(has_magazines)
            InputController.OnInputUpdate += handleReload;
    }

    public void OnDisableBehaviour(){
        firearm_controller.OnBulletShot -= handleShot;
        if(has_magazines)
            InputController.OnInputUpdate -= handleReload;
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

    void reload(){
        firearm_controller.can_shoot = false;
        int difference = firearm_controller.FirearmData.ammo_per_magazine-ammo_in_magazine;
        total_ammo-=difference;
        ammo_in_magazine=firearm_controller.FirearmData.ammo_per_magazine;
    }

    IEnumerator reload_timed_finish(float seconds){
        yield return new WaitForSeconds(seconds);
        this.firearm_controller.can_shoot = true;
    }

    void handleReload(IInputData input_data){
        if(input_data.has_reloaded){
            reload();
        }
    }

}