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
    public ShootBehaviour shoot_behaviour{get;set;}
    public event Action OnEquip = delegate { };

    public bool can_shoot{get;set;}
    ReloadBehaviour reload_behaviour;
    SoundBehaviour sound_behaviour;

    override
    public void OnStartAuthority(){
        this.OnEquip();
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
        initShootBehaviour();
        initReloadBehaviour();
        initSoundBehaviour();
    }

    void initSoundBehaviour(){
        sound_behaviour = gameObject.GetComponent<SoundBehaviour>();
        Dictionary<string, AudioClip> audio_clips = new Dictionary<string, AudioClip>();
        audio_clips["shot"] = firearm_data.shot_audio_clip;
        audio_clips["reload"] = firearm_data.reload_audio_clip;
        audio_clips["equip"] = firearm_data.equip_audio_clip;

        sound_behaviour.init(this, reload_behaviour, audio_clips);
    }

    void initReloadBehaviour(){
        if(firearm_data.ammo_per_magazine != 0){
            reload_behaviour = gameObject.GetComponent<ReloadBehaviour>();
            reload_behaviour.Init(this);
        }
    }

    void initShootBehaviour(){
        this.shoot_behaviour = GetComponent<ShootBehaviour>();
        shoot_behaviour.init(this, firearm_data.shoot_data);
    }
}
