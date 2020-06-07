using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CameraModel camera_model;
    public Player local_player;

    void Start()
    {
        camera_model = new CameraModel();
        if(local_player.isLocalPlayer){
            InputController.OnInputUpdate += ChangeRotation;
        } else {
            gameObject.SetActive(false);
        }
    }

    void ChangeRotation(IInputData input_model){
        if(input_model.input_frozen) return;
        camera_model.y_rotation += input_model.x_rotation* camera_model.mouse_sensitivity;
        camera_model.x_rotation -= input_model.y_rotation * camera_model.mouse_sensitivity;
        camera_model.x_rotation = Mathf.Clamp(camera_model.x_rotation, -camera_model.max_rotation, camera_model.max_rotation);
        local_player.transform.rotation = Quaternion.Euler(0, camera_model.y_rotation, 0);
        transform.rotation = Quaternion.Euler(camera_model.x_rotation, camera_model.y_rotation, 0);
    }

    public void setSensitivity(float sensitivity){
        if(sensitivity <= 0){
            Debug.LogWarning("Invalid sensitivity value");
            return;
        }
        this.camera_model.mouse_sensitivity = sensitivity;
    }
}
