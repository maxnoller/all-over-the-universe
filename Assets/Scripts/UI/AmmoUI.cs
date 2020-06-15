using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour {
    Text ammoText;

    void Start(){
        ammoText = GetComponent<Text>();
        OnEnable();
    }

    void OnEnable(){
        if(ammoText != null)
            ReloadBehaviour.OnReload += handleAmmoChange;
    }

    void OnDisable(){
        if(ammoText != null)
            ReloadBehaviour.OnReload -= handleAmmoChange;
    }

    void handleAmmoChange(IReloadModel reload_model){
        if(reload_model.has_magazines){
            ammoText.text = reload_model.ammo_in_magazine+" / "+reload_model.reload_data.ammo_per_magazine;
        } else {
            ammoText.text = reload_model.total_ammo.ToString();
        }
    }
}