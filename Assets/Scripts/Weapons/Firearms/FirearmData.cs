using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Weapons/FirearmData")]
public class FirearmData : ScriptableObject {
    [Header("General Weapon Stats")]
    public int damage;
    public float hit_force;
    public int fire_rate;
    public bool use_default_bullethole = true;
    [Header("Ammo stats")]
    public int total_ammo;
    public int ammo_per_magazine;
    public float reload_time;
    [Header("Visual and Audio")]
    public AudioClip shot_audio_clip;
    public float shot_audio_volume = 1f;
    public string shot_animation;
    public AudioClip reload_audio_clip;
    public float reload_audio_volume = 1f;
    public string reload_animation;
}