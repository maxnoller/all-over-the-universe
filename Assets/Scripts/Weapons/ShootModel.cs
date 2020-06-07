using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapons/ShoodObject")]
public class ShootModel : ScriptableObject{
    public int damage;
    public float hit_force = 1;
    public float fire_rate = 1;

}