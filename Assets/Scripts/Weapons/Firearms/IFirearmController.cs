using System;
using System.Collections;
using System.Collections.Generic;

public interface IFirearmController {
    ShootBehaviour shoot_behaviour{get;}
    event Action OnEquip;
    FirearmData FirearmData{get;}
    bool can_shoot{get;set;}
    ReloadData getReloadData();
}