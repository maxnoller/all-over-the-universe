using System;
using UnityEngine;

public interface IEquipable
{
    event Action OnEquip;
    void equip(GameObject player);
}
