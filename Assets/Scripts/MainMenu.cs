using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class MainMenu : MonoBehaviour
{
    Application application; 
    NetworkManager manager;
    InputField adress_inputField;
    string username;

    public GameObject select_server;
    public GameObject username_object;
    
    void Awake()
    {
        GameObject networkManagerObject = GameObject.Find("NetworkManager");
        application = networkManagerObject.GetComponent<Application>();
        manager = networkManagerObject.GetComponent<NetworkManager>();
    }
    public void host_server(){
        if(!NetworkClient.isConnected && !NetworkServer.active)
        {
            manager.StartHost();
            application.username = username;
            gameObject.SetActive(false);
        }
    }

    public void join_server(){
        if(!NetworkClient.isConnected && !NetworkServer.active)
        {
            application.username = username;
            if(adress_inputField.text != ""){
                manager.networkAddress = adress_inputField.text;
            } else {
                manager.networkAddress = "localhost";
            }
            manager.StartClient();
            print("Connecting to adress "+manager.networkAddress);
        }
    } 

    void Update()
    {
        if(NetworkClient.isConnected && !ClientScene.ready)
        {
            print("NetworkClient is connected");
            ClientScene.Ready(NetworkClient.connection);
            if(ClientScene.localPlayer == null)
                ClientScene.AddPlayer(NetworkClient.connection);
                SceneManager.LoadScene(manager.onlineScene);
        }
    }

    public void set_username(){
        username = GameObject.Find("UsernameField").GetComponent<InputField>().text;
        username_object.SetActive(false);
        select_server.SetActive(true);
        adress_inputField = GameObject.Find("serverAdress_InputField").GetComponent<InputField>();
    }
    
}
