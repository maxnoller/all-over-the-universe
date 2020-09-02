using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    GameObject menu;

    void Start(){
        menu = transform.GetChild(0).gameObject;
    }
    public void changeActive(){
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void changeActive(bool active){
        menu.SetActive(active);
    }
}
