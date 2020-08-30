using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour, IUseable {
    public event Action OnBulletShot = delegate { };
    [SerializeField]ShootModel shoot_model;
    public bool can_shoot;
    private float next_fire;

    void OnEnable(){
        if(InputController.OnInputUpdate != null)
            InputController.OnInputUpdate += checkUse;
    }

    void OnDisable(){
        if(InputController.OnInputUpdate != null)
            InputController.OnInputUpdate -= checkUse;
    }
    public void checkUse(IInputData input_data){
        if(input_data.has_used)
            use();
    }

    public void use(){
        if(can_shoot && Time.time > next_fire)
            OnBulletShot();
            next_fire = Time.time + 1f/shoot_model.fire_rate;
    }
}