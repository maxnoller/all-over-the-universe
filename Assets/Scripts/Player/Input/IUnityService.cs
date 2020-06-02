using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityService : IUnityService
{
    public float GetDeltaTime(){
        return Time.deltaTime;
    }
    public float GetAxis(string axisName){
        return Input.GetAxis(axisName);
    }
    public bool GetButton(string buttonName){
        return Input.GetButton(buttonName);
    }
}

interface IUnityService{
    float GetDeltaTime();
    float GetAxis(string axisName);
    bool GetButton(string buttonName);
}
