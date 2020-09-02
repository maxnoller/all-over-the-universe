using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    bool cursor_visible = false;
    InputController input_controller;

    void Start(){
        input_controller = GetComponent<InputController>();
    }

    void OnEnable(){
        InputController.OnInputUpdate += handleInput;
    }

    void OnDisable(){
        InputController.OnInputUpdate -= handleInput;
    }

    void handleInput(IInputData input_data){
        if(input_data.pressed_escape)
            handleEscapePress();
    }

    void handleEscapePress(){
        if(!this.cursor_visible){
            this.cursor_visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            input_controller.freezeInput(true);
        } else {
            this.cursor_visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            input_controller.freezeInput(false);
        }
    }

}