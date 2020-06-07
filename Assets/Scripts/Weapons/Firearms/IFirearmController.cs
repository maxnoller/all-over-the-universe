using System;
using System.Collections;
using System.Collections.Generic;

public interface IFirearmController {
    event Action OnBulletShot;
    FirearmData firearm_data{get;}
    bool can_shoot{get;set;}
}