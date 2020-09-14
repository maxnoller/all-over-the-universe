using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "shoot_data", menuName="Weapons/ShootData")]
public class ShootData : ScriptableObject {
    [Header("General Weapon Stats")]
    public int damage;
    public float hit_force;
    public int fire_rate;
    public bool use_default_bullethole = true;
    public FireMode fire_mode;
    public enum FireMode {semi, full, single};
}