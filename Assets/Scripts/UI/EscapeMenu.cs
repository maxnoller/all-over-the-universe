using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    bool showMenu = false;
    GameObject menu;

    void Start(){
        menu = transform.GetChild(0).gameObject;
    }

    void OnEnable()
    {
        InputController.OnInputUpdate += handleInput;
    }

    // Update is called once per frame
    void OnDisable()
    {
        InputController.OnInputUpdate -= handleInput;
    }

    void handleInput(IInputData input_data){
        if(input_data.pressed_escape)
            this.handleMenuChange();
    }

    void handleMenuChange(){
        if(showMenu){
            this.showMenu = false;
            menu.SetActive(false);
        } else {
            this.showMenu = true;
            menu.SetActive(true);
        }
    }

    public void quitApplication(){
        Debug.Log("We here");
     // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         UnityEngine.Application.Quit();
     #endif
    }

    public void openSettings(){
        
    }
}
