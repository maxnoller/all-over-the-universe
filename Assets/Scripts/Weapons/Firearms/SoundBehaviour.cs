using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoundBehaviour : NetworkBehaviour {
    ReloadBehaviour reload_behaviour;
    IFirearmController firearm_controller;

    Dictionary<string, AudioClip> audio_clips;
    AudioSource audio_source;

    public void init(IFirearmController firearm_controller,
                          ReloadBehaviour reload_behaviour,
                          Dictionary<string, AudioClip> clips){
        this.firearm_controller = firearm_controller;
        this.reload_behaviour = reload_behaviour;
        this.audio_clips = clips;
        audio_source = addAudioSource();
        OnEnable();
    }


    void OnEnable(){
        if(firearm_controller != null)
            firearm_controller.OnBulletShot += handleShot;
        if(reload_behaviour != null)
            reload_behaviour.OnReload += handleReload;
    }

    void OnDisable(){
        if(firearm_controller != null)
            firearm_controller.OnBulletShot -= handleShot;
        if(reload_behaviour != null)
            reload_behaviour.OnReload -= handleReload;
    }

    void handleShot(){
        CmdPlayAudio("shot");
        Debug.Log("shot");
    }
    void handleReload(IReloadModel reload_behaviour){
        CmdPlayAudio("reload");
        Debug.Log("reload");
    }
    [Command]
    public void CmdPlayAudio(string clip_name){
        RpcPlayAudio(clip_name);
    }

    [ClientRpc]
    public void RpcPlayAudio(string clip_name){
        audio_source.PlayOneShot(audio_clips[clip_name]);
    }

    AudioSource addAudioSource(){
        AudioSource ret = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        return ret;
    }
}