using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    MovementModel movement_model;
    CharacterController character_controller;
    float movement_modifier = 1f;

    public void Start(){
        movement_model = new MovementModel();
        InputController.OnInputUpdate += Move;
        character_controller = GetComponent<CharacterController>();
    }

    void Move(IInputData input_model){
        checkForRunAndSneak(input_model);
        Vector3 direction = 
            transform.TransformDirection(this.calculate(input_model));
        character_controller.Move(direction);   
    }
    
    void checkForRunAndSneak(IInputData input_model){
        if(input_model.sprint_button)
            movement_modifier = movement_model.sprintSpeedModifier;
        else if(input_model.sneak_button)
            movement_modifier = movement_model.sneakSpeedModifier;
    }

    Vector3 calculate(IInputData input_model){
        float delta_time = input_model.delta_time;
        float movement_multiplier = movement_model.speed * delta_time * movement_modifier;
        float x = input_model.axis_horizontal * movement_multiplier;
        float z = input_model.axis_vertical * movement_multiplier;
        float y = calculateHeight(input_model.jump_button, input_model.delta_time);
        return new Vector3(x, y, z);
    }

    float calculateHeight(bool jumpButtonPressed, float deltaTime){
        movement_model.downwardsSpeed -= movement_model.Gravity * deltaTime;
        if(character_controller.isGrounded){
            movement_model.downwardsSpeed = 0;
            if(jumpButtonPressed){
                movement_model.downwardsSpeed = -movement_model.Gravity * deltaTime;
                movement_model.downwardsSpeed = movement_model.jumpSpeed;
            }
        }
        return movement_model.downwardsSpeed * deltaTime;
    }
}