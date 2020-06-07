using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadController : MonoBehaviour{
    public event Action<ReloadModel> OnAmmoChange = delegate { };
    [SerializeField]ReloadModel reload_model;
    ShootController shoot_controller;
    bool is_reloading = false;

    void Start(){
        shoot_controller = GetComponent<ShootController>();
        InputController.OnInputUpdate += checkReload;
        setupShootController();
    }

    void checkReload(IInputData input_data){
        if(input_data.has_reloaded)
            reload();
    }

    void setupShootController(){
        if(shoot_controller != null){
            shoot_controller.OnBulletShot += ProcessUse;
        }
    }

    void ProcessUse(){
        reload_model.ammo_in_magazine -= 1;
        Debug.Log(reload_model.ammo_in_magazine);
        OnAmmoChange(reload_model);
        check_ammo();
    }

    void check_ammo(){
        if(reload_model.has_magazines && reload_model.ammo_in_magazine <= 0){
            allowShooting(false);
        } else if(reload_model.total_ammo <= 0){
            allowShooting(false);
        }
    }
    [HideInInspector]float reload_end;
    void reload(){
        if(reload_model.total_ammo == 0) return;
        int difference = reload_model.magazine_size - reload_model.ammo_in_magazine;
        Debug.Log(difference);
        if(difference > 0){
            is_reloading = true;
            allowShooting(false);
            reload_model.ammo_in_magazine = reload_model.magazine_size;
            reload_model.total_ammo -= difference;
            StartCoroutine(reloadAfterTime());
        }
        
    }

    IEnumerator reloadAfterTime(){
        yield return new WaitForSeconds(this.reload_model.reload_time);
        OnAmmoChange(reload_model);
        is_reloading = false;
        allowShooting(true);
    }

    void allowShooting(bool value){
        shoot_controller.can_shoot = !value;
    }
}