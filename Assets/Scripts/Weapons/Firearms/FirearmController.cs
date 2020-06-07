using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmController : MonoBehaviour, IFirearmController
{
    [SerializeField]public FirearmData firearm_data{get;}

    //Events
    public event Action OnBulletShot = delegate { };
    public event Action<FirearmController> OnAmmoChange = delegate { };

    public bool can_shoot{get;set;}
    ReloadBehaviour reload_behaviour;
    List<IBehaviour> behaviours;

    void Start(){
        if(firearm_data.ammo_per_magazine != 0)
            reload_behaviour = new ReloadBehaviour(this);
            behaviours.Add(reload_behaviour);
    }

    void OnEnable(){
        InputController.OnInputUpdate += checkInput;
        foreach(IBehaviour behaviour in behaviours){
            behaviour.OnEnableBehaviour();
        }
    }

    void OnDisable(){
        InputController.OnInputUpdate -= checkInput;
        foreach(IBehaviour behaviour in behaviours){
            behaviour.OnDisableBehaviour();
        }
    }

    void checkInput(IInputData input_data){
        if(input_data.has_used){
           shoot();
        }
    }
    float next_fire;
    void shoot(){
        if(can_shoot && Time.time >= next_fire){
            next_fire = Time.time + 1f/fire_rate;
            OnBulletShot();
        }
    }

}
