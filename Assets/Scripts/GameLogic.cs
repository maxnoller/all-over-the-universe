using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameLogic : NetworkBehaviour
{
    List<Player> online_players = new List<Player>();
    public event Action<Player> OnPlayerRegistered = delegate { };

    public void registerPlayer(Player player){
        if(online_players.Contains(player)){
            Debug.LogError("Already registered player "+player.username+" tried to register to gamelogic again");
            return;
        }
        online_players.Add(player);
        OnPlayerRegistered(player);
    }

    public Player[] get_online_players(){
        Player[] ret = new Player[online_players.Count];
        for(int i = 0; i < online_players.Count; i++){
            ret[i] = online_players[i];
        }
        return ret;
    }
}
