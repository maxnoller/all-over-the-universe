using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapons/ReloadObject")]
public class ReloadModel : ScriptableObject{
    public bool has_magazines;
    public int magazine_size;
    public int total_ammo;
    [HideInInspector]public int ammo_in_magazine;
    public float reload_time;

    public ReloadModel(){
        ammo_in_magazine = magazine_size;
    }
}