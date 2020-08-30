using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmController : MonoBehaviour, IFirearmController
{
    [SerializeField]public FirearmData firearm_data{get;}

    //Events
    public event Action OnBulletShot = delegate { };

    public bool can_shoot{get;set;}
    ReloadBehaviour reload_behaviour;
    SoundBehaviour sound_behaviour;

    void Start(){
        if(firearm_data.ammo_per_magazine != 0)
            reload_behaviour = (ReloadBehaviour)gameObject.AddComponent(typeof(ReloadBehaviour));
        sound_behaviour = gameObject.GetComponent<SoundBehaviour>();
    }

    void OnEnable(){
        InputController.OnInputUpdate += checkInput;
    }

    void OnDisable(){
        InputController.OnInputUpdate -= checkInput;
    }

    void checkInput(IInputData input_data){
        if(input_data.has_used){
           shoot();
        }
    }
    float next_fire;
    void shoot(){
        if(can_shoot && Time.time >= next_fire){
            next_fire = Time.time + 1f/firearm_data.fire_rate;
            OnBulletShot();
        }
    }

}
