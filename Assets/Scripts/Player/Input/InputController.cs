using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InputController : NetworkBehaviour
{
    public static event Action<IInputData> OnInputUpdate = delegate { };
    [SerializeField]IUnityService unityService;

    InputModel input_model;

    void Start(){
        this.input_model = new InputModel();
        if(unityService == null)
            unityService = new UnityService();
    }
    
    void Update(){
        this.input_model.pressed_escape = unityService.GetButtonDown("Cancel");
        this.input_model.delta_time = unityService.GetDeltaTime();
        this.input_model.x_rotation = unityService.GetAxis("Mouse X");
        this.input_model.y_rotation = unityService.GetAxis("Mouse Y");
        this.input_model.axis_horizontal = unityService.GetAxis("Horizontal");
        this.input_model.axis_vertical = unityService.GetAxis("Vertical");
        this.input_model.sprint_button = unityService.GetButton("Sprint");
        this.input_model.sneak_button = unityService.GetButton("Sneak");
        this.input_model.jump_button = unityService.GetButton("Jump");
        this.input_model.has_used = unityService.GetButton("Fire1");
        this.input_model.has_used_secondary = unityService.GetButton("Fire2");
        this.input_model.has_reloaded = unityService.GetButton("Reload");
        this.input_model.has_interacted = unityService.GetButton("Interact");

        if(input_model.input_frozen){
            input_model.axis_horizontal = 0;
            input_model.axis_vertical = 0;
            input_model.sprint_button = false;
            input_model.sneak_button = false;
            input_model.jump_button = false;
            input_model.has_reloaded = false;
            input_model.has_used = false;
            input_model.has_used_secondary = false;
            input_model.has_interacted = false;
        }
        
        OnInputUpdate(this.input_model);
    }

    public void freezeInput(bool frozen){
        this.input_model.input_frozen = frozen;
    }

    public float getAxisVertical(){
        return this.input_model.axis_vertical;
    }

    public float getAxisHorizontal(){
        return this.input_model.axis_vertical;
    }

    public float getDeltaTime(){
        return this.input_model.delta_time;
    }
}
