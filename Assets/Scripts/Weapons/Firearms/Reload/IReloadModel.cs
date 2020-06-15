using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface in order to only pass neccessary values trough reload event
public interface IReloadModel {
    int total_ammo{get;}
    bool has_magazines{get;}
    int ammo_in_magazine{get;}
    ReloadData reload_data{get;}
}