using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthController : NetworkBehaviour, IDamageable {
    Player local_player;
    private const int max_health = 100;
    [SyncVar]public int health = max_health;

    public delegate void OnPlayerDiedEventHandler(Player victim, Player killer);
    public event OnPlayerDiedEventHandler OnPlayerDied = delegate { };

    public static event Action<int> OnPlayerHealthChanged = delegate { };

    public Player last_to_hit;
    
    void Start(){
        local_player = GetComponent<Player>();
    }
    
    public void registerHit(int damage, Player hit_player){
        takeDamage(damage);
        last_to_hit = hit_player;
    }
    void takeDamage(int damage){
        if(!isServer) return;
        this.health -= damage;
        TargetCallHealthEvent(connectionToClient);
        if(health <= 0){
            OnPlayerDied(local_player, last_to_hit);
        }
    }

    public void resetHealth(){
        if(!isServer) return;
        this.health = max_health;
        TargetCallHealthEvent(connectionToClient);
    }

    public void heal(int amount){
        if(!isServer) return;
        this.health += amount;
        TargetCallHealthEvent(connectionToClient);
    }

    [TargetRpc]
    public void TargetCallHealthEvent(NetworkConnection target){
        OnPlayerHealthChanged(health);
    }
}