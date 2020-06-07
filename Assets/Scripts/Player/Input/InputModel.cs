using System.Collections;
using System.Collections.Generic;

public class InputModel : IInputData{
    public float axis_horizontal{get;set;}
    public float axis_vertical{get;set;}

    public float y_rotation{get;set;}
    public float x_rotation{get;set;}

    public bool sprint_button{get;set;}
    public bool sneak_button{get;set;}
    public bool jump_button{get;set;}

    public bool input_frozen{get;set;}
    
    public float delta_time{get;set;}

    public bool has_used{get;set;}
    public bool has_used_secondary{get;set;}
    public bool has_reloaded{get;set;}

    public InputModel(){
        this.axis_horizontal = 0f;
        this.axis_vertical = 0f;
        this.y_rotation = 0f;
        this.x_rotation = 0f;
        this.input_frozen = false;
    }
}