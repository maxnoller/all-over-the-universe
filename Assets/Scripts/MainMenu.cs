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
    InputField portInputField;
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
            ushort port = 7777;
            if(portInputField.text == "" || !ushort.TryParse(portInputField.text, out port))
                Debug.Log("Could not parse port resorting to default port");
            manager.GetComponent<TelepathyTransport>().port = port;
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
                string[] split_string = adress_inputField.text.Split(':');
                if(split_string.Length > 1){
                    manager.GetComponent<TelepathyTransport>().port = ushort.Parse(split_string[1]);
                }

                manager.networkAddress = split_string[0];
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
        initSelectServer();
    }

    public void initSelectServer(){
        select_server.SetActive(true);
        adress_inputField = GameObject.Find("serverAdress_InputField").GetComponent<InputField>();
        portInputField = GameObject.Find("PortInputField").GetComponent<InputField>();
    }
    
}
