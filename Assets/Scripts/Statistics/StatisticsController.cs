using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class StatisticsController : NetworkBehaviour
{
    GameLogic game_logic;
    Dictionary<Player, PlayerStatisticsModel> player_statistics = new Dictionary<Player, PlayerStatisticsModel>();

    public override void OnStartServer(){
        Debug.Log("Statistics Starting");
        game_logic = GameObject.Find("GameLogic").GetComponent<GameLogic>();
        foreach(Player player in game_logic.get_online_players()){
            register(player);
        }
        game_logic.OnPlayerRegistered += register;
    }

    void register(Player player){
        if(player_statistics.ContainsKey(player)){
            Debug.LogWarning("Player already registered");
            return;
        }
        player_statistics[player] = new PlayerStatisticsModel();
        player.GetComponent<HealthController>().OnPlayerDied += handleDeath;
    }

    void unregister(Player player){
        if(!player_statistics.ContainsKey(player)){
            Debug.LogWarning("Trying to unregister not registered player: "+player.username);
        }
        player_statistics.Remove(player);
        player.GetComponent<HealthController>().OnPlayerDied -= handleDeath;
    }

    void handleDeath(Player victim, Player killer){
        player_statistics[victim].deaths++;
        player_statistics[killer].kills++;
        Debug.Log("Victims Deaths: "+player_statistics[victim].deaths+", Killers kills:"+player_statistics[killer].kills);
    }

    void OnClientStart(){
        Debug.LogError("Starting Statistics on client!");
    }
}
