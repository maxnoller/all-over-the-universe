using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthController : NetworkBehaviour {
    Player local_player;
    private const int max_health = 100;
    [SyncVar]public int health = max_health;
    public event Action<Player> OnPlayerDied = delegate { };
    public static event Action<int> OnPlayerHealthChanged = delegate { };
    
    void Start(){
        local_player = GetComponent<Player>();
    }

    public void takeDamage(int damage){
        if(!isServer) return;
        this.health -= damage;
        TargetCallHealthEvent();
        if(health <= 0){
            OnPlayerDied(local_player);
        }
    }

    public void resetHelath(){
        if(!isServer) return;
        this.health = max_health;
        TargetCallHealthEvent();
    }

    public void heal(int amount){
        if(!isServer) return;
        this.health += amount;
        TargetCallHealthEvent();
    }

    [TargetRpc]
    public void TargetCallHealthEvent(){
        OnPlayerHealthChanged(health);
    }
}