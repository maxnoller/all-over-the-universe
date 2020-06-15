using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ReloadBehaviour : NetworkBehaviour, IReloadModel {
    //Firearm Data
    IFirearmController firearm_controller;
    public ReloadData reload_data{get;private set;}

    //Variables needed to do ammo stuff
    public int ammo_in_magazine{get;private set;}
    public int total_ammo{get;private set;}
    public bool has_magazines{get;private set;}
    bool is_reloading = false;

    //Events
    public static event Action<IReloadModel> OnReload = delegate { };

    //Init method
    public void Init(IFirearmController firearm_controller){
        this.firearm_controller = firearm_controller;
        this.reload_data = firearm_controller.getReloadData();
        if(this.reload_data.ammo_per_magazine != 0){
            has_magazines = true;
            ammo_in_magazine = this.reload_data.ammo_per_magazine;
        }
        total_ammo = reload_data.total_ammo;
        this.OnEnable();
    }

    void OnEnable() {
        if(firearm_controller != null)
            firearm_controller.OnBulletShot += handleShot;
        if(has_magazines)
            InputController.OnInputUpdate += handleReload;
        OnReload(this);
    }

    void OnDisable(){
        firearm_controller.OnBulletShot -= handleShot;
        if(has_magazines)
            InputController.OnInputUpdate -= handleReload;
    }

    //method that gets called everytime a shot is fired to adjust ammo and block shots if there is no ammo left
    void handleShot(){
        if(!has_magazines){
            total_ammo--;
        } else {
            ammo_in_magazine--;
        }
        OnReload(this);
        if(total_ammo <= 0 || ammo_in_magazine <= 0)
            firearm_controller.can_shoot = false;
    }

    //Reload method, changes ammo etc
    void reload(){
        is_reloading = true;
        firearm_controller.can_shoot = false;
        int difference = reload_data.ammo_per_magazine-ammo_in_magazine;
        total_ammo-=difference;
        ammo_in_magazine=reload_data.ammo_per_magazine;
        StartCoroutine(reload_timed_finish(reload_data.reload_time));
    }

    //Delayed part of reload method (to implement reload time)
    IEnumerator reload_timed_finish(float seconds){
        yield return new WaitForSeconds(seconds);
        this.firearm_controller.can_shoot = true;
        is_reloading = false;
        OnReload(this);
    }

    //Checks input data if reload was pressed
    void handleReload(IInputData input_data){
        if(!is_reloading && input_data.has_reloaded){
            reload();
        }
    }

}