using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FirearmController : NetworkBehaviour, IFirearmController, IEquipable
{
    [SerializeField]private FirearmData firearm_data;
    public FirearmData FirearmData{
        get{return firearm_data;}
    }

    //Events
    public event Action OnBulletShot = delegate { };
    public event Action OnEquip = delegate { };

    public bool can_shoot{get;set;}
    ReloadBehaviour reload_behaviour;
    SoundBehaviour sound_behaviour;

    override
    public void OnStartAuthority(){
        this.OnEnable();
        this.OnEquip();
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

    public void equip(GameObject parent){
        this.init();
        transform.localPosition = firearm_data.local_position;
        transform.localRotation = firearm_data.local_rotation;

        if(isLocalPlayer)
            gameObject.layer = 8;
    }

    public void init(){
        can_shoot = true;
        initReloadBehaviour();
        initSoundBehaviour();
        initRaycastController();
        OnEnable();
    }

    void initSoundBehaviour(){
        sound_behaviour = gameObject.GetComponent<SoundBehaviour>();
        Dictionary<string, AudioClip> audio_clips = new Dictionary<string, AudioClip>();
        audio_clips["shot"] = firearm_data.shot_audio_clip;
        audio_clips["reload"] = firearm_data.reload_audio_clip;
        audio_clips["equip"] = firearm_data.equip_audio_clip;

        sound_behaviour.init(this, reload_behaviour, audio_clips);
    }

    void initRaycastController(){
        RaycastController raycast_controller;
        raycast_controller = GetComponent<RaycastController>();
        raycast_controller.Init(this);    
    }

    void initReloadBehaviour(){
        if(firearm_data.ammo_per_magazine != 0){
            reload_behaviour = gameObject.GetComponent<ReloadBehaviour>();
            reload_behaviour.Init(this);
        }
    }
}
