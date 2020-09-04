using System;
using System.Collections;
using System.Collections.Generic;

public interface IFirearmController {
    event Action OnBulletShot;
    event Action OnEquip;
    FirearmData FirearmData{get;}
    bool can_shoot{get;set;}
    ReloadData getReloadData();
}