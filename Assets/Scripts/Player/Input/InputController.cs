using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class InputController : NetworkBehaviour
{
    public event Action<IInputData> OnInputUpdate = delegate { };
    [SerializeField]IUnityService unityService;

    InputModel input_model;

    void Start(){
        this.input_model = new InputModel();
        if(unityService == null)
            unityService = new UnityService();
    }
    
    void Update(){
        if(this.input_model.input_frozen) return;
        this.input_model.x_rotation = unityService.GetAxis("Mouse X");
        this.input_model.y_rotation = unityService.GetAxis("Mouse Y");
        this.input_model.axis_horizontal = unityService.GetAxis("Horizontal");
        this.input_model.axis_vertical = unityService.GetAxis("Vertical");
        this.input_model.sprint_button = unityService.GetButton("Sprint");
        this.input_model.sneak_button = unityService.GetButton("Sneak");
        this.input_model.jump_button = unityService.GetButton("Jump");
        this.input_model.delta_time = unityService.GetDeltaTime();
        OnInputUpdate(this.input_model);
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
