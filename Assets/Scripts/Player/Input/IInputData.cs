using System.Collections;
using System.Collections.Generic;

public interface IInputData {
    float axis_horizontal{get;set;}
    float axis_vertical{get;set;}

    float y_rotation{get;set;}
    float x_rotation{get;set;}

    bool sprint_button{get;set;}
    bool sneak_button{get;set;}
    bool jump_button{get;set;}

    bool input_frozen{get;set;}
    
    float delta_time{get;set;}
    
    bool has_used{get;set;}
    bool has_used_down{get;set;}
    bool has_reloaded{get;set;}
    bool has_used_secondary{get;set;}

    bool pressed_escape{get;set;}
    bool has_interacted{get;set;}
}