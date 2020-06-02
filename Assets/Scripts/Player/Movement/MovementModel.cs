using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModel{
    public float Gravity = 9.81f;
    public float speed = 6f;
    public float jumpSpeed = 4;
    [Range(1f, 2f)]public float sprintSpeedModifier = 1;
    [Range(0f, 1f)]public float sneakSpeedModifier = 0.8f;

    public float downwardsSpeed = 0;

}