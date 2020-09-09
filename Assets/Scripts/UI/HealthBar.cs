using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider health_slider;

    void Start(){
        this.health_slider = GetComponent<Slider>();
        OnEnable();
    }   

    void OnEnable(){
        HealthController.OnPlayerHealthChanged += updateHealthBar;
    }

    void OnDisable(){
        HealthController.OnPlayerHealthChanged -= updateHealthBar;
    }
   
   void updateHealthBar(int value){
       this.health_slider.value = value;
   }
}
