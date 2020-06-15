using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadData {
    public int ammo_per_magazine{get;private set;}
    public int total_ammo{get;private set;}
    public float reload_time{get;private set;}

    public ReloadData(FirearmData firearm_data){
        this.ammo_per_magazine = firearm_data.ammo_per_magazine;
        this.total_ammo = firearm_data.total_ammo;
        this.reload_time = firearm_data.reload_time;
    }
}